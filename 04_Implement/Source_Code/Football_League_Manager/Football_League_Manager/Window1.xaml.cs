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
        
        public Window1()
        {
            InitializeComponent();
        }

        DataProvider dataProvider = new DataProvider();
        public bool isLogin = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool ktTK = true;
            bool KTPass = true;
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from Admin");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            Admin admin = new Admin();
            while (sqlDataReader.Read())
            {
                admin.Username = sqlDataReader.GetString(0);
                admin.Password = sqlDataReader.GetString(1);
            }
            for (int i = 0; i < txtFullName.Text.Length; i++)
            {
                if (txtFullName.Text[i] == ' ')
                {
                    ktTK = false;
                    break;
                }
            }
            for (int i = 0; i < txtPassW.Password.Length; i++)
            {
                if (txtPassW.Password[i] == ' ')
                {
                    KTPass = false;
                    break;
                }
            }
            if (txtFullName.Text == "")
            {
                MessageBox.Show("Tên tài khoản không được để trống");
            }
            else if (txtPassW.Password == "")
            {
                MessageBox.Show("Mật khẩu không được để trống");
            }
            else if (txtFullName.Text == admin.Username.Trim() && txtPassW.Password == admin.Password.Trim())
            {
                MessageBox.Show("Đăng nhập thành công");
                isLogin = true;
            }
            else if (ktTK == false)
            {
                MessageBox.Show("Tài khoảng không được có khoảng trắng");
            }
            else if (KTPass == false)
            {
                MessageBox.Show("Mật khẩu không được có khoảng trắng");
            }

            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
                isLogin = false;
            }

        }
    }
}
