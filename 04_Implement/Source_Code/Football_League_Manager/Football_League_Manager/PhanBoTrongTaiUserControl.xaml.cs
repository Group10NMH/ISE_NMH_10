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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for PhanBoTrongTaiUserControl.xaml
    /// </summary>
    public partial class PhanBoTrongTaiUserControl : UserControl
    {
        public PhanBoTrongTaiUserControl()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var iteams = new List<string> { "Vòng 1", "Vòng 2", "Vòng 3", "Vòng 4", "Vòng 5", "Vòng 6", "Vòng 7", "Vòng 8", "Vòng 9", "Vòng 10" };
            VDComboBox.ItemsSource = iteams;
        }
        class DataObject
        {
            public string TenDoiNha { get; set; }
            public string TenDoiKhach { get; set; }
            public string MaTran { get; set; }
        }


        private void VDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DataObject> DSTranDau;
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select DB.TenDB,DB1.TenDB,LTD.MaTran from DoiBong DB, LichThiDau LTD, DoiBong DB1 where DB.MaDB = LTD.MaDoi1 and DB1.MaDB = LTD.MaDoi2 and LTD.VongDau = @Vong ", new object[] { VDComboBox.SelectedIndex + 1 });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DSTranDau = new List<DataObject>();

            while (sqlDataReader.Read())
            {
                string TenDoiNha = sqlDataReader.GetString(0);
                string TenDoiKhach = sqlDataReader.GetString(1);
                string MaTran = sqlDataReader.GetString(2);
                DataObject data = new DataObject();
                data.TenDoiNha = TenDoiNha;
                data.TenDoiKhach = TenDoiKhach;
                data.MaTran = MaTran;
                DSTranDau.Add(data);
            }
            TDComboBox.ItemsSource = DSTranDau;
        }
        DanhSachTrongTai TT;
        string MaTran;
        private void TDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TDComboBox.SelectedIndex != -1)
            {
                DataObject data = (DataObject)TDComboBox.SelectedItem;
                MaTran = data.MaTran;

                DataProvider dataProvider = new DataProvider();
                SqlCommand sqlCommand = dataProvider.ExcuteQuery("select distinct(MaTran) from LichCamCoi");
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    if (MaTran == sqlDataReader.GetString(0))
                    {
                        MessageBox.Show("Trận đấu đã có trọng tài điều khiển, hãy chọn trận đấu khác");
                        TDComboBox.SelectedIndex = -1;
                        return;
                    }
                }

                TT = new DanhSachTrongTai();
                TT.LoadDSTrongTai();

                TT1comboBox.ItemsSource = TT.DanhSachTrongTais.Values;

                
            }
        }


        private void TT1comboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            TT = new DanhSachTrongTai();
            TT.LoadDSTrongTai();
            string matt = TT.DanhSachTrongTais.ElementAt(TT1comboBox.SelectedIndex).Key;
            TT.DanhSachTrongTais.Remove(matt);
            TT2comboBox.ItemsSource = TT.DanhSachTrongTais.Values;
        }

        private void TT2comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TT = new DanhSachTrongTai();
            TT.LoadDSTrongTai();
            string matt = TT.DanhSachTrongTais.ElementAt(TT1comboBox.SelectedIndex).Key;
            TT.DanhSachTrongTais.Remove(matt);
            string matt1 = TT.DanhSachTrongTais.ElementAt(TT2comboBox.SelectedIndex).Key;
            TT.DanhSachTrongTais.Remove(matt1);
            TT3comboBox.ItemsSource = TT.DanhSachTrongTais.Values;
        }

        string[] matt = new string[4];

        private void TT3comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TT = new DanhSachTrongTai();
            TT.LoadDSTrongTai();

            matt[0] = TT.DanhSachTrongTais.ElementAt(TT1comboBox.SelectedIndex).Key;
            TT.DanhSachTrongTais.Remove(matt[0]);
            matt[1] = TT.DanhSachTrongTais.ElementAt(TT2comboBox.SelectedIndex).Key;
            TT.DanhSachTrongTais.Remove(matt[1]);
            matt[2] = TT.DanhSachTrongTais.ElementAt(TT3comboBox.SelectedIndex).Key;
            TT.DanhSachTrongTais.Remove(matt[2]);

            TT4comboBox.ItemsSource = TT.DanhSachTrongTais.Values;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            int a = 0;
            if (VDComboBox.SelectedIndex == -1 || TDComboBox.SelectedIndex == -1 || TT1comboBox.SelectedIndex == -1 || TT2comboBox.SelectedIndex == -1 || TT3comboBox.SelectedIndex == -1 || TT4comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đủ thông tin!");
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    a += dataProvider.ExcuteNonQuery("insert into LichCamCoi values ( @MaTT , @MaTran )", new object[] { matt[i], MaTran });
                }
                if(a==4)
                {
                    MessageBox.Show("Lưu thành công.");
                }
            }
        }
        

        private void TT4comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            matt[3] = TT.DanhSachTrongTais.ElementAt(TT4comboBox.SelectedIndex).Key;
        }
        
    }
}
