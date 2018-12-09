using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SapXepLichThiDau.xaml
    /// </summary>
    public partial class SapXepLichThiDau : Window
    {
        List<string> dsVongDau;
        public SapXepLichThiDau()
        {
            InitializeComponent();
        }
        class DataObject
        {
            public string MaTran { get; set; }
            public string DoiA { get; set; }
            public string DoiB { get; set; }
            public DateTime ThoiGian { get; set; }
        };
        List<TranDau> dsTranDau;
        private void VongDau_ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            dsVongDau = new List<string>() { "Vòng 1", "Vòng 2", "Vòng 3", "Vòng 4", "Vòng 5", "Vòng 6", "Vòng 7", "Vòng 8", "Vòng 9" };
            VongDau_ComboBox.ItemsSource = dsVongDau;
            VongDau_ComboBox.FontSize = 16;
        }
        int vongDau;
        private void VongDau_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            vongDau = int.Parse((VongDau_ComboBox.SelectedIndex + 1).ToString());
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select DB1.TenDB, DB2.TenDB, LTD.MaTran, LTD.ThoiGian from DoiBong DB1, " +
                "DoiBong DB2, LichThiDau LTD where DB1.MaDB = LTD.MaDoi1 and DB2.MaDB = LTD.MaDoi2 and VongDau = @vong", new object[] { vongDau });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            dsTranDau = new List<TranDau>();
            while (sqlDataReader.Read())
            {
                var dt = new TranDau();
                
                dt.TenDoiA = sqlDataReader.GetString(0);
                dt.TenDoiB = sqlDataReader.GetString(1);
                dt.MaTranDau = sqlDataReader.GetString(2);
                dt.ThoiGian = sqlDataReader.GetDateTime(3);
                dsTranDau.Add(dt);
            }

            TranDau_ComboBox.ItemsSource = dsTranDau;
        }

        private void ThemLich_Button_Click(object sender, RoutedEventArgs e)
        {
            string maTD = "";
            DataProvider dataProvider = new DataProvider();
            if (vongDau * dsTranDau.Count() < 10)
            {
                maTD = "TD0" + ((vongDau-1) * dsTranDau.Count() + TranDau_ComboBox.SelectedIndex).ToString();
            }
            else
            {
                maTD = "TD" + ((vongDau-1) * dsTranDau.Count() + TranDau_ComboBox.SelectedIndex).ToString();
            }
            string a = TranDau_ComboBox.SelectedIndex.ToString();
           
            dataProvider.ExcuteNonQuery("update LichThiDau set ThoiGian = @thoiGian" +
                " where MaTran= @maTran ", new object[] { Convert.ToDateTime(ThoiGian_TextBox.Text), maTD });
            var i = TranDau_ComboBox.SelectedIndex;
            dsTranDau[i].ThoiGian = Convert.ToDateTime(ThoiGian_TextBox.Text);
            MessageBox.Show("Thay đổi thời gian thi đấu thành công!");

        }

        private void TranDau_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var i = TranDau_ComboBox.SelectedIndex;
            ThoiGian_TextBox.Text = dsTranDau[i].ThoiGian.ToString();
        }
    }
}
