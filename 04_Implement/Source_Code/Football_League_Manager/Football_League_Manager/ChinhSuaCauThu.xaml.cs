using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for ChinhSuaCauThu.xaml
    /// </summary>
    public partial class ChinhSuaCauThu : Window
    {
        public List<CauThu> cauThus { get; set; }
        public string MaDoiBong { get; set; }
        List<ViTri> viTris = new List<ViTri>();

        class ViTri
        {
            public string MaVT { get; set; }
            public string TenVT { get; set; }

            public override string ToString()
            {
                return TenVT;
            }
        }

        public ChinhSuaCauThu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select MaCT, TenCT, NgaySinh, SoAo, TenVT, QuocTich from CauThu CT join ViTri VT on CT.MaVT = VT.MaVT where MaDB = @madb", new object[] { MaDoiBong });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            cauThus = new List<CauThu>();
            
            while (sqlDataReader.Read())
            {
                CauThu cauThu = new CauThu();
                cauThu.MaCauThu = sqlDataReader.GetString(0);
                cauThu.TenCauThu = sqlDataReader.GetString(1);

                cauThu.NgaySinhCauThu = sqlDataReader.GetDateTime(2).ToString("dd/MM/yyyy");
                cauThu.SoAo = sqlDataReader.GetInt32(3);
                cauThu.ViTri = sqlDataReader.GetString(4);
                cauThu.QuocTichCT = sqlDataReader.GetString(5);
                cauThus.Add(cauThu);
            }   

            sqlCommand = dataProvider.ExcuteQuery("select * from ViTri");
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                ViTri viTri = new ViTri();
                viTri.MaVT = sqlDataReader.GetString(0);
                viTri.TenVT = sqlDataReader.GetString(1);

                viTris.Add(viTri);
            }

            itemsCombobox.ItemsSource = viTris;
            CauThuDataGrid.ItemsSource = cauThus;
        }

        CauThu row_list;
        string MaCT;
        private void CauThuDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CauThuDataGrid.SelectedIndex != -1)
            {
                row_list = (CauThu)CauThuDataGrid.SelectedItem;
                MaCT = row_list.MaCauThu;
                TenCauThuText.Text = row_list.TenCauThu;
                SoAoText.Text = row_list.SoAo.ToString();
                NgaySinhDatePicker.SelectedDate = DateTime.Parse(row_list.NgaySinhCauThu);
                QuocTichText.Text = row_list.QuocTichCT;

                for (int i = 0; i < viTris.Count; i++)
                    if (viTris[i].TenVT == cauThus[CauThuDataGrid.SelectedIndex].ViTri)
                        itemsCombobox.SelectedIndex = i;

                XoaButton.Visibility = Visibility.Visible;
                SuaButton.Visibility = Visibility.Visible;
                HuyBoButton.Visibility = Visibility.Visible;
               
                TenCauThuText.IsEnabled = false;
            }
        }

        private void ThemButton_Click(object sender, RoutedEventArgs e)
        {
            string temp = cauThus[cauThus.Count - 1].MaCauThu;

            string temp1 = temp.Substring(2);

            int temp2 = int.Parse(temp1) + 1;

            string MaCT = "CT";
            if (temp2 < 10) MaCT += "0";

            MaCT += temp2.ToString();

            string MaViTri = "";
            for (int i = 0; i < viTris.Count; i++)
                if (viTris[i].TenVT.Trim() == itemsCombobox.SelectedItem.ToString())
                    MaViTri = viTris[i].MaVT;

            DataProvider dataProvider = new DataProvider();
            int check = dataProvider.ExcuteNonQuery("insert into CauThu values ( @ma , @madb , @ten , @ns , @qt , @soao , @mavt , @a , @b , @c )", new object[] { MaCT, MaDoiBong, TenCauThuText.Text, DateTime.Parse(NgaySinhDatePicker.SelectedDate.ToString()), QuocTichText.Text, int.Parse(SoAoText.Text), MaViTri, 0, 0, 0 });

            if (0 == check) MessageBox.Show("Lỗi");
            else
            {
                MessageBox.Show("Thành công");
                CauThu cauThu = new CauThu();
                cauThu.MaCauThu = MaCT;
                cauThu.TenCauThu = TenCauThuText.Text;
                DateTime a = (DateTime)NgaySinhDatePicker.SelectedDate;
                cauThu.NgaySinhCauThu = a.ToString("dd/MM/yyyy");
                cauThu.SoAo = int.Parse(SoAoText.Text);
                cauThu.ViTri = itemsCombobox.SelectedItem.ToString();
                cauThu.QuocTichCT = QuocTichText.Text;

                cauThus.Add(cauThu);
                CauThuDataGrid.ItemsSource = null;
                CauThuDataGrid.ItemsSource = cauThus;

                TenCauThuText.Text = SoAoText.Text = QuocTichText.Text = "";
                NgaySinhDatePicker.SelectedDate = null;
                itemsCombobox.SelectedIndex = -1;
            }
        }

        private void XoaButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Xoá cầu thủ này khỏi đội bóng", "Cảnh báo", MessageBoxButton.OKCancel);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                DataProvider dataProvider = new DataProvider();
                int check = dataProvider.ExcuteNonQuery("delete CauThu where MaCT = @ma", new object[] { row_list.MaCauThu });
                if (1 == check) MessageBox.Show("Ngon");

                cauThus.RemoveAt(CauThuDataGrid.SelectedIndex);

                CauThuDataGrid.ItemsSource = cauThus;

                XoaButton.Visibility = Visibility.Hidden;
                SuaButton.Visibility = Visibility.Hidden;
                HuyBoButton.Visibility = Visibility.Hidden;
            }
            else
            {
                HuyBoButton_Click(null, null);
            }
        }
 
        private void SuaButton_Click(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            string MaViTri = "";
            for (int i = 0; i < viTris.Count; i++)
                if (viTris[i].TenVT.Trim() == itemsCombobox.SelectedItem.ToString())
                    MaViTri = viTris[i].MaVT;
            int check = dataProvider.ExcuteNonQuery("update CauThu set SoAo = @soao , QuocTich = @qt , MaVT = @vt , NgaySinh = @ns where MaCT = @mact and MaDB = @madb", new object[] { int.Parse(SoAoText.Text), QuocTichText.Text, MaViTri, DateTime.Parse(NgaySinhDatePicker.SelectedDate.ToString()), row_list.MaCauThu, MaDoiBong });
            MessageBox.Show(check.ToString());
            foreach (var item in cauThus)
            {
                if (item.MaCauThu == row_list.MaCauThu)
                {
                    item.SoAo = int.Parse(SoAoText.Text);
                    item.ViTri = itemsCombobox.SelectedItem.ToString();
                    item.QuocTichCT = QuocTichText.Text;
                    item.NgaySinhCauThu = ((DateTime)NgaySinhDatePicker.SelectedDate).ToString("dd/MM/yyyy");
                    break;
                }
            }

            CauThuDataGrid.ItemsSource = null;
            CauThuDataGrid.ItemsSource = cauThus;
        }

        private void HuyBoButton_Click(object sender, RoutedEventArgs e)
        {
            CauThuDataGrid.SelectedIndex = -1;
            XoaButton.Visibility = Visibility.Hidden;
            SuaButton.Visibility = Visibility.Hidden;
            HuyBoButton.Visibility = Visibility.Hidden;

            NgaySinhDatePicker.SelectedDate = null;

            TenCauThuText.Text = SoAoText.Text = QuocTichText.Text = "";
            itemsCombobox.SelectedIndex = -1;

            TenCauThuText.IsEnabled = true;
        }

        private void TenCauThuText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string check = "0123456789";
            if (TenCauThuText.Text != "" && check.Contains(TenCauThuText.Text[TenCauThuText.Text.Length-1]))
            {
                MessageBox.Show("Tên cầu thủ không được phép chứa số!");
                TenCauThuText.Text = TenCauThuText.Text.Substring(0, TenCauThuText.Text.Length - 1);
            }
        }

        private void NgaySinhDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime ns = (DateTime)NgaySinhDatePicker.SelectedDate;

            if (KiemTraNSHopLe(now, ns) == false)
            {
                MessageBox.Show("Ngày sinh cầu thủ không hợp lệ!");
                //NgaySinhDatePicker.SelectedDate = null;
            }
        }

        bool KiemTraNSHopLe(DateTime now, DateTime ns)
        {
            if (ns.Year > now.Year) return false;

            if (ns.Year < now.Year) return true;

            if (ns.Month > now.Month) return false;

            if (ns.Month < now.Month) return true;

            if (ns.Day > now.Day) return false;

            if (ns.Day < now.Day) return true;

            return true;
        }

        private void SoAoText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SoAoText.Text != "")
            {
                try
                {
                    int.Parse(SoAoText.Text);
                }
                catch(Exception)
                {
                    MessageBox.Show("Số áo cầu thủ phải là số nguyên");
                    SoAoText.Text = "";
                }
            }
        }
    }
}
