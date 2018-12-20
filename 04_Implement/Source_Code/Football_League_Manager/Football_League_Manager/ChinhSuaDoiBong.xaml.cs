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
    /// Interaction logic for ChinhSuaDoiBong.xaml
    /// </summary>
    public partial class ChinhSuaDoiBong : Window
    {
        public List<DoiBong> doiBongs { get; set; }

        public ChinhSuaDoiBong()
        {
            InitializeComponent();

            doiBongs = new List<DoiBong>();

            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from DoiBong");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                DoiBong doiBong = new DoiBong();
                doiBong.MaDoiBong = sqlDataReader.GetString(0);
                doiBong.TenDoiBong = sqlDataReader.GetString(1);
                doiBong.HuanLuyenVien = sqlDataReader.GetString(2);
                doiBong.SanNha = sqlDataReader.GetString(3);

                doiBongs.Add(doiBong);
            }

            itemsCombobox.ItemsSource = doiBongs;
        }

        private void itemsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TenHLVText.Text = doiBongs[itemsCombobox.SelectedIndex].HuanLuyenVien;
            TenSanNhaText.Text = doiBongs[itemsCombobox.SelectedIndex].SanNha;
        }

        private void DanhSachCauThuButton_Click(object sender, RoutedEventArgs e)
        {
            //doiBongs[itemsCombobox.SelectedIndex].CauThus = new List<CauThu>();

            Hide();

            ChinhSuaCauThu chinhSuaCauThu = new ChinhSuaCauThu();
            chinhSuaCauThu.MaDoiBong = doiBongs[itemsCombobox.SelectedIndex].MaDoiBong;

            chinhSuaCauThu.ShowDialog();

            Show();
        }

        private void LuuButton_Click(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            int check = dataProvider.ExcuteNonQuery("update DoiBong set TenHLV = @hlv , SanNha = @sannha where MaDB = @madb", new object[] { TenHLVText.Text, TenSanNhaText.Text, doiBongs[itemsCombobox.SelectedIndex].MaDoiBong });
            if (check == 1) MessageBox.Show("Ok");
        }

        private void XoaButton_Click(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ExcuteNonQuery("delete DoiBong where MaDB = @madb", new object[] { doiBongs[itemsCombobox.SelectedIndex].MaDoiBong });
            doiBongs.RemoveAt(itemsCombobox.SelectedIndex);
        }

    }
}
