using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TongKetMuaGiai.xaml
    /// </summary>
    public partial class TongKetMuaGiai : Window
    {
        public TongKetMuaGiai()
        {
            InitializeComponent();
        }
        public class Data
        {
            public int ViTri { get; set; }
            public string TenDoi { get; set; }
        }

        public class Data1
        {
            public int SoBan { get; set; }
            public string MaCT { get; set; }
            public string MaDB { get; set; }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TraCuuBangXepHang traCuuBangXepHang = new TraCuuBangXepHang();
            traCuuBangXepHang.Show();
            traCuuBangXepHang.Hide();
            if (traCuuBangXepHang.bangXepHangs.Count() == 10)
            {
                DoiVoDich_TextBlock.Text = traCuuBangXepHang.bangXepHangs[0].TenDoiBong;
                DoiXuongHang_TextBlock.Text = traCuuBangXepHang.bangXepHangs[traCuuBangXepHang.bangXepHangs.Count() - 1].TenDoiBong;
                ObservableCollection<Data> list = new ObservableCollection<Data>();
                for (int i = 0; i < 3; i++)
                {
                    Data data = new Data();
                    data.ViTri = i + 1;
                    data.TenDoi = traCuuBangXepHang.bangXepHangs[i].TenDoiBong;
                    list.Add(data);
                }
                Top4_DataGrid.ItemsSource = list;
                traCuuBangXepHang.Close();
                TraCuuVuaPhaLuoi traCuuVuaPhaLuoi = new TraCuuVuaPhaLuoi();
                VuaPhaLuoi_TextBlock.Text = traCuuVuaPhaLuoi.cauThus[0].TenCauThu;
                //DataProvider dataProvider = new DataProvider();
                //SqlCommand sql = dataProvider.ExcuteQuery("select GhiBan.MaCT, GhiBan.MaDB, GhiBan.SoBan from GhiBan");
                //SqlDataReader sqlDataReader = sql.ExecuteReader();
                //List<Data1> DanhSachGhiBan = new List<Data1>();
                //while (sqlDataReader.Read())
                //{
                //    Data1 dt = new Data1();
                //    dt.MaCT = sqlDataReader.GetString(0);
                //    dt.MaDB = sqlDataReader.GetString(1);
                //    dt.SoBan = sqlDataReader.GetInt32(2);
                //    DanhSachGhiBan.Add(dt);
                //}
                //for (int i = 0; i < DanhSachGhiBan.Count(); i++)
                //{

                //}
            }
            else
            {
                ChuaKetThuc_TextBlock.Text = "Mùa giải chưa kết thúc!!";
                ChuaKetThuc_TextBlock.Height = 420;
                ChuaKetThuc_TextBlock.Width = 380;

            }
        }
    }
}
