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
    /// Interaction logic for SapLichThiDauUserControl.xaml
    /// </summary>
    public partial class SapLichThiDauUserControl : UserControl
    {
        public SapLichThiDauUserControl()
        {
            InitializeComponent();
        }

        List<string> dsVongDau;
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
                
                dsTranDau.Add(dt);
            }

            TranDau_ComboBox.ItemsSource = dsTranDau;
        }

        private void ThemLich_Button_Click(object sender, RoutedEventArgs e)
        {
            if (dsTranDau == null)
            {
                MessageBox.Show("Bạn chưa chọn vòng đấu");
            }
            else
            {

                string maTD = "";

                if (vongDau * dsTranDau.Count() < 10)
                {
                    maTD = "TD0" + ((vongDau - 1) * dsTranDau.Count() + TranDau_ComboBox.SelectedIndex).ToString();
                }
                else
                {
                    maTD = "TD" + ((vongDau - 1) * dsTranDau.Count() + TranDau_ComboBox.SelectedIndex).ToString();
                }

                string NgayThiDau = NgayThiDauDP.SelectedDate.ToString().Substring(0,8);
                string GioThiDau = GioThiDauDP.SelectedTime.ToString().Substring(9);

                string result = NgayThiDau + " " + GioThiDau;

                DateTime a = DateTime.Parse(result);
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = "update LichThiDau set ThoiGian = '" + a.ToString(format) + "' where MaTran= @maTran ";
                try
                {
                    DataProvider dataProvider = new DataProvider();
                    int dem = dataProvider.ExcuteNonQuery(query, new object[] { maTD });
                    var i = TranDau_ComboBox.SelectedIndex;
                    dsTranDau[i].ThoiGian = DateTime.Parse(result);
                    MessageBox.Show("Thay đổi thời gian thi đấu thành công!");

                }
                catch (Exception)
                {
                    MessageBox.Show("Ban chưa chọn trận đấu");
                }
            }
        }

        private void TranDau_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
