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

            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select TenCT from CauThu where TenCT LIKE @ten", new object[] { "%" + TenSearch.Text + "%" });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<string> CauThus = new List<string>();

            while (sqlDataReader.Read())
            {
                CauThus.Add(sqlDataReader.GetString(0));
            }

            BXHListView.ItemsSource = CauThus;
        }
    }
}
