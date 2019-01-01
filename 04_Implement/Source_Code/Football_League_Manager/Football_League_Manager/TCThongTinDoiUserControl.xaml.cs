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
    /// Interaction logic for TCThongTinDoiUserControl.xaml
    /// </summary>
    public partial class TCThongTinDoiUserControl : UserControl
    {
        public TCThongTinDoiUserControl()
        {
            InitializeComponent();
        }

        DataProvider dataProvider = new DataProvider();

        public void TenDB_loaded(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlcommand = dataProvider.ExcuteQuery("select * from DoiBong");
            SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
            // DoiBong Tendb = new DoiBong();
            List<string> dsDb = new List<string>();
            while (sqldatareader.Read())
            {
                dsDb.Add(sqldatareader.GetString(1));
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = dsDb;

        }
        string MaDB = "";
        public class cauThu
        {
            public int STT { get; set; }
            public string TenCT { get; set; }
            public string NgaySinhCT { get; set; }
            public string SoAo { get; set; }
            public string ViTri { get; set; }
            public string QuocTichCT { get; set; }
        }
        ObservableCollection<cauThu> listCT;
        public void TenDB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cauThu ct = new cauThu();
            listCT = new ObservableCollection<cauThu>();
            CauThuDataGrid.ItemsSource = listCT;
            string tendb = TenDB.SelectedItem.ToString();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from DoiBong");
            SqlDataReader sqldatareader = sqlCommand.ExecuteReader();
            SqlCommand sqlcommand = dataProvider.ExcuteQuery("select * from CauThu");
            SqlDataReader sqlDatareader = sqlcommand.ExecuteReader();


            while (sqldatareader.Read())
            {
                if (tendb == sqldatareader.GetString(1))
                {
                    MaDB = sqldatareader.GetString(0);
                    HLV.Text = sqldatareader.GetString(2);
                    SanNha.Text = sqldatareader.GetString(3);
                }
            }
            while (sqlDatareader.Read())
            {
                string vtri = "";
                ct = new cauThu();
                if (MaDB == sqlDatareader.GetString(1))
                {
                    ct.STT = int.Parse(sqlDatareader.GetString(0).Substring(2, 2));
                    ct.TenCT = sqlDatareader.GetString(2);
                    ct.NgaySinhCT = sqlDatareader.GetDateTime(3).ToString();
                    ct.QuocTichCT = sqlDatareader.GetString(4);
                    ct.SoAo = sqlDatareader.GetInt32(5).ToString();
                    vtri = sqlDatareader.GetString(6);
                    if (vtri == "TM") { ct.ViTri = "Thủ môn"; }
                    if (vtri == "TV") { ct.ViTri = "Tiền vệ"; }
                    if (vtri == "HV") { ct.ViTri = "Hậu vệ"; }
                    if (vtri == "TD") { ct.ViTri = "Tiền đạo"; }

       
                    listCT.Add(ct);
                }
            }
            
            CauThuDataGrid.ItemsSource = listCT;

        }

    }
}
