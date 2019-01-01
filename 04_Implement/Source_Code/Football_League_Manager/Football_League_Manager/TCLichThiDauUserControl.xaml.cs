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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for TCLichThiDauUserControl.xaml
    /// </summary>
    public partial class TCLichThiDauUserControl : UserControl
    {
        public TCLichThiDauUserControl()
        {
            InitializeComponent();
        }

        List<string> dsVongDau;
        class DataObject
        {

            public string DoiA { get; set; }
            public string DoiB { get; set; }
            public DateTime ThoiGian { get; set; }
        };
        List<DataObject> dsTranDau;

        private void VongDau_ComboBox_Loaded(object sender, RoutedEventArgs e)
        {

            dsVongDau = new List<string>() { "Vòng 1", "Vòng 2", "Vòng 3", "Vòng 4", "Vòng 5", "Vòng 6", "Vòng 7", "Vòng 8", "Vòng 9" };
            VongDau_ComboBox.ItemsSource = dsVongDau;
            VongDau_ComboBox.FontSize = 16;

        }
        int vongDau;
        private void TraCuu_Button_Click(object sender, RoutedEventArgs e)
        {
            vongDau = int.Parse((VongDau_ComboBox.SelectedIndex + 1).ToString());
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select DB1.TenDB, DB2.TenDB,  LTD.ThoiGian from DoiBong DB1, " +
                "DoiBong DB2, LichThiDau LTD where DB1.MaDB = LTD.MaDoi1 and DB2.MaDB = LTD.MaDoi2 and VongDau = @vong", new object[] { vongDau });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            dsTranDau = new List<DataObject>();
            while (sqlDataReader.Read())
            {
                DataObject dt = new DataObject();
                dt.DoiA = sqlDataReader.GetString(0);
                dt.DoiB = sqlDataReader.GetString(1);
                dt.ThoiGian = sqlDataReader.GetDateTime(2);
                dsTranDau.Add(dt);
            }
            ObservableCollection<DataObject> list = new ObservableCollection<DataObject>();
            for (int i = 0; i < dsTranDau.Count(); i++)
            {
                list.Add(dsTranDau[i]);
            }
            LichThiDauDataGrid.ItemsSource = list;
        }


    }
}
