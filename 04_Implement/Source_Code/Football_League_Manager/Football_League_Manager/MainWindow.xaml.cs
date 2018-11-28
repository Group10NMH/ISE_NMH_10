using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataProvider dataProvider = new DataProvider();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // ------- Test ExcuteQuery --------
            //SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from CauThu where TenCT = @ma", new object[] { "Lê Công Hưng" });
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //while (sqlDataReader.Read())
            //{
            //    string ma = sqlDataReader.GetString(0);
            //    string ten = sqlDataReader.GetString(1);
            //}



            // ------- Test ExcuteNonQuery --------
            //int a = dataProvider.ExcuteNonQuery("insert into CauThu values ('005', N'Nguyễn Duy Hậu', N'Tiền Đạo', 9)");
            //MessageBox.Show(a.ToString());



            // ------- Test ExcuteScalar --------
            //object a = dataProvider.ExcuteScalar("select count(*) from CauThu");
            //MessageBox.Show(a.ToString());

        }


    }
}

