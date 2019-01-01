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
    /// Interaction logic for TCVuaPhaLuoiUserControl.xaml
    /// </summary>
    public partial class TCVuaPhaLuoiUserControl : UserControl
    {
        public List<CauThu> cauThus { get; set; }
        public TCVuaPhaLuoiUserControl()
        {
            InitializeComponent();
            string query = "select ct.TenCT, db.TenDB, vt.TenVT, ct.SoTheVang, sum(SoBan) as SoBanThang " +
                           "from GhiBan gb join CauThu ct on gb.MaCT = ct.MaCT and gb.MaDB = ct.MaDB " +
                           "join DoiBong db on ct.MaDB = db.MaDB " +
                           "join ViTri vt on ct.MaVT = vt.MaVT " +
                           "group by gb.MaCT, gb.MaDB, ct.TenCT, db.TenDB, vt.TenVT, ct.SoTheVang " +
                           "order by SoBanThang desc";

            cauThus = new List<CauThu>();
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery(query);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            int i = 1;
            while (sqlDataReader.Read() && i <= 10)
            {
                CauThu cauThu = new CauThu();
                cauThu.TenCauThu = sqlDataReader.GetString(0);
                cauThu.TenDoiBong = sqlDataReader.GetString(1);
                cauThu.ViTri = sqlDataReader.GetString(2);
                cauThu.SoTheVang = sqlDataReader.GetInt32(3);
                cauThu.SoBanThang = sqlDataReader.GetInt32(4);
                cauThu.STT = i++;
                cauThus.Add(cauThu);
            }

            if (i != 1)
            {
                BXHListView.ItemsSource = cauThus;
            }
            else
            {
                MessageBox.Show("Chưa có cầu thủ nào ghi bàn nên chưa thể thống kê vua phá lưới!");
            }
        }
    }
}
