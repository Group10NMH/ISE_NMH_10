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
    /// Interaction logic for WindowChangePass.xaml
    /// </summary>
    public partial class WindowChangePass : Window
    {

        DataProvider dataProvider = new DataProvider();
        public WindowChangePass()
        {
            InitializeComponent();
        }

        private void Change_Password_click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from Admin");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            int pass = PassNew.Password.Length;

            Admin admin = new Admin();
            while (sqlDataReader.Read())
            {
                admin.Username = sqlDataReader.GetString(0).Trim();
                admin.Password = sqlDataReader.GetString(1).Trim();
            }
            

            if (PassOld.Password != admin.Password)
            {
                MessageBox.Show("Bạn nhập sai mật khẩu cũ");
            }

            else if (PassNew.Password == "")
            {
                MessageBox.Show("Mật khẩu mới không được để trống!!!");
            }
            else if (PassNew.Password != PassNewagain.Password)
            {
                MessageBox.Show("Bạn nhập 2 mật khẩu mới khác nhau");
            }
            else if (PassNew.Password == PassOld.Password)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ");
            }
            // Kiểm tra độ dài của mật khẩu
            else if (pass < 6 || pass > 12)
            {
                MessageBox.Show("Mật khẩu phải từ 6 -> 12 ký tự");
            }
            else
            {
                int a = dataProvider.ExcuteNonQuery("UPDATE Admin Set MatKhau = @p", new object[] { PassNew.Password });
                if (a > 0)
                {
                    MessageBox.Show("Bạn đã thay đổi mật khẩu thành công");
                }
                PassOld.Password = null;
                PassNew.Password = null;
                PassNewagain.Password = null; 

            }



        }
    }
}
