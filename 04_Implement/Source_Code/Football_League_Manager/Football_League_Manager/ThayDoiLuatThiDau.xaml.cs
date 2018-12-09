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
using System.Windows.Shapes;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for ThayDoiLuatThiDau.xaml
    /// </summary>
    public partial class ThayDoiLuatThiDau : Window
    {
        public ThayDoiLuatThiDau()
        {
            InitializeComponent();
        }
        LuatThiDau luatThiDau = new LuatThiDau();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from BangThamSo ");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                luatThiDau.DiemThang = sqlDataReader.GetInt32(5);
                luatThiDau.DiemThua = sqlDataReader.GetInt32(6);
                luatThiDau.DiemHoa = sqlDataReader.GetInt32(7);
                luatThiDau.TuoiToiDa = sqlDataReader.GetInt32(3);
                luatThiDau.TuoiToiThieu = sqlDataReader.GetInt32(2);
                luatThiDau.SoCauThuToiDa = sqlDataReader.GetInt32(1);
                luatThiDau.SoCauThuToiThieu = sqlDataReader.GetInt32(0);
                luatThiDau.SoTheVang = sqlDataReader.GetInt32(4);
            }
            DiemThang_TextBox.Text = luatThiDau.DiemThang.ToString();
            DiemThang_TextBox.IsEnabled = false;
            DiemHoa_TextBox.Text = luatThiDau.DiemHoa.ToString();
            DiemHoa_TextBox.IsEnabled = false;
            DiemThua_TextBox.Text = luatThiDau.DiemThua.ToString();
            DiemThua_TextBox.IsEnabled = false;
            TuoiMax_TextBox.Text = luatThiDau.TuoiToiDa.ToString();
            TuoiMax_TextBox.IsEnabled = false;
            TuoiMin_TextBox.Text = luatThiDau.TuoiToiThieu.ToString();
            TuoiMin_TextBox.IsEnabled = false;
            SoCauThuMax_TextBox.Text = luatThiDau.SoCauThuToiDa.ToString();
            SoCauThuMax_TextBox.IsEnabled = false;
            SoCauThuMin_TextBox.Text = luatThiDau.SoCauThuToiThieu.ToString();
            SoCauThuMin_TextBox.IsEnabled = false;
            SoThe_TextBox.Text = luatThiDau.SoTheVang.ToString();
            SoThe_TextBox.IsEnabled = false;
        }

        private void Diem_CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            DiemThang_TextBox.IsEnabled = true;
            DiemThua_TextBox.IsEnabled = true;
            DiemHoa_TextBox.IsEnabled = true;

        }

        private void Tuoi_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TuoiMin_TextBox.IsEnabled = true;
            TuoiMax_TextBox.IsEnabled = true;
        }

        private void SoCauThu_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SoCauThuMax_TextBox.IsEnabled = true;
            SoCauThuMin_TextBox.IsEnabled = true;
        }

        private void The_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SoThe_TextBox.IsEnabled = true;
        }

        private void Luu_Button_Click(object sender, RoutedEventArgs e)
        {
            luatThiDau.DiemThang = int.Parse(DiemThang_TextBox.Text);
            luatThiDau.DiemHoa = int.Parse(DiemHoa_TextBox.Text);
            luatThiDau.DiemThua = int.Parse(DiemThua_TextBox.Text);
            luatThiDau.TuoiToiDa = int.Parse(TuoiMax_TextBox.Text);
            luatThiDau.TuoiToiThieu = int.Parse(TuoiMin_TextBox.Text);
            luatThiDau.SoCauThuToiDa = int.Parse(SoCauThuMax_TextBox.Text);
            luatThiDau.SoCauThuToiThieu = int.Parse(SoCauThuMin_TextBox.Text);
            luatThiDau.SoTheVang = int.Parse(SoThe_TextBox.Text);

            DataProvider dataProvider = new DataProvider();
            int dem = dataProvider.ExcuteNonQuery(" update LuatThiDau set DiemThang= @DiemThang , DiemThua= @b , DiemHoa= @c , TuoiToiDa= @d ," +
                " TuoiToiThieu = @e , SoCauThuToiDa = @f ,SoCauThuToiThieu = @g , SoTheVang = @h ", new object[] { luatThiDau.DiemThang, luatThiDau.DiemThua, luatThiDau.DiemHoa, luatThiDau.TuoiToiDa, luatThiDau.TuoiToiThieu, luatThiDau.SoCauThuToiDa, luatThiDau.SoCauThuToiThieu, luatThiDau.SoTheVang });

            MessageBox.Show("Thay đổi thành công" + dem.ToString());
            Diem_CheckBox.IsChecked = false;
            The_CheckBox.IsChecked = false;
            SoCauThu_CheckBox.IsChecked = false;
            Tuoi_CheckBox.IsChecked = false;

        }

        private void HoanTat_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Diem_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            DiemThang_TextBox.Text = luatThiDau.DiemThang.ToString();
            DiemThang_TextBox.IsEnabled = false;
            DiemHoa_TextBox.Text = luatThiDau.DiemHoa.ToString();
            DiemHoa_TextBox.IsEnabled = false;
            DiemThua_TextBox.Text = luatThiDau.DiemThua.ToString();
            DiemThua_TextBox.IsEnabled = false;

        }

        private void Tuoi_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TuoiMax_TextBox.Text = luatThiDau.TuoiToiDa.ToString();
            TuoiMax_TextBox.IsEnabled = false;
            TuoiMin_TextBox.Text = luatThiDau.TuoiToiThieu.ToString();
            TuoiMin_TextBox.IsEnabled = false;
        }

        private void SoCauThu_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TuoiMin_TextBox.IsEnabled = false;
            SoCauThuMax_TextBox.Text = luatThiDau.SoCauThuToiDa.ToString();
            SoCauThuMax_TextBox.IsEnabled = false;
            SoCauThuMin_TextBox.Text = luatThiDau.SoCauThuToiThieu.ToString();
            SoCauThuMin_TextBox.IsEnabled = false;
        }

        private void The_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SoThe_TextBox.Text = luatThiDau.SoTheVang.ToString();
            SoThe_TextBox.IsEnabled = false;
        }
    }
}
