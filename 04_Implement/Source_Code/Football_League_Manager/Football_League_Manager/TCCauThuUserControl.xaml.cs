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
    /// Interaction logic for TCCauThuUserControl.xaml
    /// </summary>
    public partial class TCCauThuUserControl : UserControl
    {
        public TCCauThuUserControl()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TenSearch.Text != "")
            {
                DataProvider dataProvider = new DataProvider();
                SqlCommand sqlCommand = dataProvider.ExcuteQuery("select ct.TenCT, db.TenDB, ct.NgaySinh, vt.TenVT, ct.QuocTich from CauThu ct join DoiBong db on ct.MaDB = db.MaDB join ViTri vt on ct.MaVT = vt.MaVT where ct.TenCT LIKE @ten", new object[] { "%" + TenSearch.Text + "%" });
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<CauThu> cauThus = new List<CauThu>();

                while (sqlDataReader.Read())
                {
                    CauThu cauThu = new CauThu();
                    cauThu.TenCauThu = sqlDataReader.GetString(0);
                    cauThu.TenDoiBong = sqlDataReader.GetString(1);
                    cauThu.NgaySinhCauThu = sqlDataReader.GetDateTime(2).ToString("dd/MM/yyyy");
                    cauThu.ViTri = sqlDataReader.GetString(3);
                    cauThu.QuocTichCT = sqlDataReader.GetString(4);

                    cauThus.Add(cauThu);
                }

                BXHListView.ItemsSource = cauThus;
            }
            else
            {
                BXHListView.ItemsSource = null;
            }
        }
    }
}
