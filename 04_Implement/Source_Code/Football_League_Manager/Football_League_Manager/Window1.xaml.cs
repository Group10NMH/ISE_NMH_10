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

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataProvider dataProvider = new DataProvider();
        public bool isLogin = false;     
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from Admin");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            Admin admin = new Admin();
            while (sqlDataReader.Read())
            {
                admin.Username = sqlDataReader.GetString(0);
                admin.Password = sqlDataReader.GetString(1);
            }
            if(txtFullName.Text == admin.Username.Trim() && txtPassW.Password == admin.Password.Trim())
            {
                MessageBox.Show("Hưng đẹp trai vãi");
                isLogin = true;
            }
            else
            {
                MessageBox.Show("sai");
                isLogin = false;
            }

        }
    }
}
