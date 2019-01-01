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
    /// Interaction logic for DoiMatKhauUserControl.xaml
    /// </summary>
    public partial class DoiMatKhauUserControl : UserControl
    {
        public DoiMatKhauUserControl()
        {
            InitializeComponent();
        }

        DataProvider dataProvider = new DataProvider();
        private void Change_Password_click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from Admin");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            int pass = PassNew.Password.Length;
            int kt = 1;
            int ktKyTu = 1;
            Admin admin = new Admin();
            while (sqlDataReader.Read())
            {
                admin.Username = sqlDataReader.GetString(0).Trim();
                admin.Password = sqlDataReader.GetString(1).Trim();
            }
            for (int i = 0; i < pass; i++)
            {
                if (PassNew.Password[i] == ' ')
                {
                    kt = 0;
                    break;
                }
            }
            for (int i = 0; i < pass; i++)
            {
                if (PassNew.Password[i] == '^' || PassNew.Password[i] == '.' || PassNew.Password[i] == '-' || PassNew.Password[i] == '?' || PassNew.Password[i] == ',' ||
                                PassNew.Password[i] == '@' || PassNew.Password[i] == '#' || PassNew.Password[i] == '%' || PassNew.Password[i] == '&' || PassNew.Password[i] == '*')
                {
                    ktKyTu = 0;
                    break;
                }
            }


            if (PassOld.Password != admin.Password)
            {
                MessageBox.Show("Bạn nhập sai mật khẩu cũ");
            }
            else if (kt == 0)
            {
                MessageBox.Show("Mật khẩu không được có khoảng trắng");
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

            else if (pass < 6 || pass > 12)
            {
                MessageBox.Show("Mật khẩu lớn hơn 6 ký tự");
            }
            else if (pass > 12)
            {
                MessageBox.Show("Mật khẩu bé hơn 12 ký tự");
            }
            else if (ktKyTu == 0)
            {
                MessageBox.Show("Mật khẩu mới không được tồn tại ký tự đặc biệt");
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
