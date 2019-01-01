using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Collections;
using System.Data;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for AddListCT.xaml
    /// </summary>
    public partial class AddListCT : Window
    {
        
        ObservableCollection<DataObject> list;
        DataProvider data = new DataProvider();
        int index = -1;
        public List<CauThu> cauThus;
        List<string> viTris = new List<string>();
        public AddListCT()
        {
            InitializeComponent();
            list = new ObservableCollection<DataObject>();
            cauThus = new List<CauThu>();

        }
        public class DataObject
        {
            public int STT { get; set; }
            public string TenCT { get; set; }
            public string NgaySinhCT { get; set; }
            public string ViTriCT { get; set; }
            public int SoAoCT { get; set; }
            public string QuocTichCT { get; set; }
            
        }

        DanhSachTrongTai dsTT = new DanhSachTrongTai();
        CauThu CT = new CauThu();
        LuatThiDau luatThiDau = new LuatThiDau();
        List<string> arr = new List<string>();
        DateTime dateTime = new DateTime();
        
        public void Done_click(object sender, RoutedEventArgs e)
        {
            
            if (list.Count < 1)
            {
                MessageBox.Show("Số lượng cầu thủ ít nhất là 16 ");
            }
            else if (list.Count > 25)
            {
                MessageBox.Show("Số lượng cầu thủ nhiều nhất là 25 ");
            }
            else
            {
                foreach (var ct in list)
                {
                    string maCT = "CT";
                    if (ct.STT < 10) { maCT = "CT0"; }
                    CT = new CauThu();
                    CT.MaCauThu = maCT + ct.STT;
                    CT.TenCauThu = ct.TenCT;
                    CT.NgaySinhCauThu = ct.NgaySinhCT;
                    CT.ViTri = ct.ViTriCT;
                    CT.SoAo = ct.SoAoCT;
                    CT.QuocTichCT = ct.QuocTichCT;
                    if (ct.ViTriCT == "Hậu vệ") { CT.MaVT = "HV"; }
                    if (ct.ViTriCT == "Tiền đạo") { CT.MaVT = "TĐ"; }
                    if (ct.ViTriCT == "Tiền vệ") { CT.MaVT = "TV"; }
                    if (ct.ViTriCT == "Thủ môn") { CT.MaVT = "TM"; }
                    cauThus.Add(CT);
                    this.Close();
                }
                
            }
            
        }
        
        public void Update_click(object sender, RoutedEventArgs e)
        {
            bool checkName = true;
            bool checkQT = true;
            for (int j = 0; j < name.Text.Length; j++)
            {
                if (name.Text[j] == '0' || name.Text[j] == '1' || name.Text[j] == '2' || name.Text[j] == '3' || name.Text[j] == '4' || name.Text[j] == '5' ||
                       name.Text[j] == '6' || name.Text[j] == '7' || name.Text[j] == '8' || name.Text[j] == '9')
                {
                    checkName = false;
                    break;
                }
            }
            for (int j = 0; j < QuocTich.Text.Length; j++)
            {
                if (QuocTich.Text[j] == '0' || QuocTich.Text[j] == '1' || QuocTich.Text[j] == '2' || QuocTich.Text[j] == '3' || QuocTich.Text[j] == '4' || QuocTich.Text[j] == '5' ||
                       QuocTich.Text[j] == '6' || QuocTich.Text[j] == '7' || QuocTich.Text[j] == '8' || QuocTich.Text[j] == '9')
                {
                    checkQT = false;
                    break;
                }
            }

            if (name.Text == "")
            {
                MessageBox.Show("Thêm tên cầu thủ");
            }
            else if (NgaySinh.Text == "")
            {
                MessageBox.Show("Thêm ngày sinh cầu thủ");
            }
            else if (QuocTich.Text == "")
            {
                MessageBox.Show("Thêm quốc tịch cầu thủ");
            }
            else if (Number.Text == "")
            {
                MessageBox.Show("Thêm số áo cầu thủ");
            }
            else if (ViTri.Text == null)
            {
                MessageBox.Show("Thêm vị trí cầu thủ");
            }
            else if(checkName == false)
            {
                MessageBox.Show("Tên cầu thủ không được để số");
            }
            else if (checkQT == false)
            {
                MessageBox.Show("Quốc tịch không được để số ");
            }
            else
            {

                string temp = list[index].SoAoCT.ToString();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i] == temp)
                    {
                        arr.RemoveAt(i);
                    }
                }
                DataObject dataObject = new DataObject();
                dataObject.STT = index + 1;
                dataObject.TenCT = name.Text;
                dataObject.QuocTichCT = QuocTich.Text;
                dataObject.ViTriCT = ViTri.Text;
                dateTime = (DateTime)NgaySinh.SelectedDate;
                dataObject.NgaySinhCT = dateTime.ToString("dd/MM/yyyy");


                if (arr.Contains(Number.Text))
                {
                    MessageBox.Show("Số áo đã tồn tại");
                }
                else
                {
                    arr.Add(Number.Text);
                    dataObject.SoAoCT = int.Parse(Number.Text);
                    list[index] = dataObject;
                    CauThuDataGrid.ItemsSource = list;
                    name.Text = null;
                    ViTri.Text = null;
                    Number.Text = null;
                    NgaySinh.Text = null;
                    QuocTich.Text = null;
                }
            }
        }

        public void Delete_click(object sender, RoutedEventArgs e)
        {
            string temp = list[index].SoAoCT.ToString();
            for(int i = 0; i < arr.Count;i++)
            {
                if(arr[i] == temp)
                {
                    arr.RemoveAt(i);
                }
            }
            list.RemoveAt(index);
            
            for (int i = index; i < list.Count; i++)
            {
                DataObject dataObject = new DataObject();
                dataObject.STT = i + 1;
                dataObject.TenCT = list[i].TenCT;
                dataObject.ViTriCT = list[i].ViTriCT;
                dataObject.NgaySinhCT = list[i].NgaySinhCT;
                dataObject.SoAoCT = list[i].SoAoCT;
                dataObject.QuocTichCT = list[i].QuocTichCT;
                list[i] = dataObject;
            }
            CauThuDataGrid.ItemsSource = list;
            MessageBox.Show("Xóa cầu thủ thành công");
            name.Text = null;
            ViTri.Text = null;
            Number.Text = null;
            NgaySinh.Text = null;
            QuocTich.Text = null;

        }

        public void Add_click(object sender, RoutedEventArgs e)
        {


            SqlCommand sqlcommand = data.ExcuteQuery("select * from BangThamSo");
            SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
            int i = list.Count + 1;
            bool check = true;
            bool checkQT = true;
           
            while (sqldatareader.Read())
            {
                luatThiDau.SoCauThuToiThieu = sqldatareader.GetInt32(0);
                luatThiDau.SoCauThuToiDa = sqldatareader.GetInt32(1);
            }
            
            for(int j = 0; j < name.Text.Length; j++ )
            {
                if(name.Text[j] == '0' || name.Text[j] == '1' || name.Text[j] == '2' || name.Text[j] == '3' || name.Text[j] == '4' || name.Text[j] == '5' ||
                       name.Text[j] == '6' || name.Text[j] == '7' || name.Text[j] == '8' || name.Text[j] == '9')
                {
                    check = false;
                    break;
                }
            }

            for (int j = 0; j < QuocTich.Text.Length; j++)
            {
                if (QuocTich.Text[j] == '0' || QuocTich.Text[j] == '1' || QuocTich.Text[j] == '2' || QuocTich.Text[j] == '3' || QuocTich.Text[j] == '4' || QuocTich.Text[j] == '5' ||
                       QuocTich.Text[j] == '6' || QuocTich.Text[j] == '7' || QuocTich.Text[j] == '8' || QuocTich.Text[j] == '9')
                {
                    checkQT = false;
                    break;
                }
            }

            if (i <= luatThiDau.SoCauThuToiDa)
            {
                
                if (name.Text == "" )
                {
                    MessageBox.Show("Thêm tên cầu thủ");
                }
                else if (NgaySinh.Text == "")
                {
                    MessageBox.Show("Thêm ngày sinh cầu thủ");
                }
                else if (QuocTich.Text == "")
                {
                    MessageBox.Show("Thêm quốc tịch cầu thủ");
                }
                else if (Number.Text == "")
                {
                    MessageBox.Show("Thêm số áo cầu thủ");
                }
                else if (ViTri.Text == null)
                {
                    MessageBox.Show("Thêm vị trí cầu thủ");
                }
                else if (check == false)
                {
                    MessageBox.Show("Tên cầu thủ không được để số");
                }
                else if (checkQT == false)
                {
                    MessageBox.Show("Quốc tịch cầu thủ không được để số");
                }
                else
                {
                    
                    DataObject ct = new DataObject();
                    ct.STT = i;
                    ct.TenCT = name.Text;
                    ct.QuocTichCT = QuocTich.Text;
                    dateTime = (DateTime)NgaySinh.SelectedDate;
                    ct.NgaySinhCT = dateTime.ToString("dd/MM/yyyy");
                    ct.ViTriCT = ViTri.Text;
                    
                    if (arr.Contains(Number.Text))
                    {
                        MessageBox.Show("Số áo đã tồn tại");
                    }
                    else
                    {
                            arr.Add(Number.Text);
                            ct.SoAoCT = int.Parse(Number.Text);
                            list.Add(ct);
                            CauThuDataGrid.ItemsSource = list;

                            name.Text = null;
                            ViTri.Text = null;
                            Number.Text = null;
                            NgaySinh.Text = null;
                            QuocTich.Text = null;
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Số cầu thủ không được quá 25 cầu thủ");
            }
        }

        public void Combobox_loaded(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = data.ExcuteQuery("select * from ViTri");
            SqlDataReader sqldatareader = sqlCommand.ExecuteReader();

            ViTri vt = new ViTri();

            while (sqldatareader.Read())
            {
                vt.MaVT = sqldatareader.GetString(0);
                vt.TenVT = sqldatareader.GetString(1);
                viTris.Add(vt.TenVT);
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = viTris;
        }

        private void CauThuDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CauThuDataGrid.SelectedIndex != -1)
            {
                var row_list = (DataObject)CauThuDataGrid.SelectedItem;
                name.Text = row_list.TenCT;
                QuocTich.Text = row_list.QuocTichCT;
                ViTri.Text = row_list.ViTriCT;
                Number.Text = row_list.SoAoCT.ToString();
                NgaySinh.SelectedDate = DateTime.Parse(row_list.NgaySinhCT);

                index = CauThuDataGrid.SelectedIndex;

                UpdateButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
        }

        
        private void SoAo_TextChanged(object sender, TextChangedEventArgs e)
       {
            try
            {
                if (Number.Text != "")
                {
                    int.Parse(Number.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập đúng số áo");
                Number.Text = null;
            }
        }




        //Kiểm tra tuổi
        //private void NgaySinh_selectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DateTime YearNow = DateTime.Now;
        //    int yearNow = int.Parse(YearNow.ToString("yyyy"));
        //    dateTime = (DateTime)NgaySinh.SelectedDate;
        //    int YearCT = int.Parse(dateTime.ToString("yyyy"));
        //    int kt = yearNow - YearCT;
        //    SqlCommand sqlcommand = data.ExcuteQuery("select * from BangThamSo");
        //    SqlDataReader sqldatareader = sqlcommand.ExecuteReader();

        //    while (sqldatareader.Read())
        //    {
        //        luatThiDau.TuoiToiThieu = sqldatareader.GetInt32(2);
        //        luatThiDau.TuoiToiDa = sqldatareader.GetInt32(3);
        //    }

        //    if (kt > luatThiDau.TuoiToiDa || kt < luatThiDau.TuoiToiThieu)
        //    {
        //        MessageBox.Show("Số tuổi cầu thủ phải từ 18 -> 26");
        //    }

        //}


    }
}
