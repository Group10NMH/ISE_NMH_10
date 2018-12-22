//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace Football_League_Manager
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        DataProvider dataProvider = new DataProvider();

//        int[,] a = new int[N, N + 1];
//        const int N = 10;
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        public class TD
//        {
//            public int d1, d2, vong;
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {

//            // ------- Test ExcuteQuery --------

//            //SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from CauThu where TenCT = @ten or MaCT = @ma", new object[] { "Lê Công Hưng", "002" });
//            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

//            //while (sqlDataReader.Read())
//            //{
//            //    string ma = sqlDataReader.GetString(0);
//            //    string ten = sqlDataReader.GetString(1);
//            //}



//            //-------Test ExcuteNonQuery--------
//            int a = dataProvider.ExcuteNonQuery("insert into CauThu values ( @ma , @madb , @ten , @ns , @qt , @soao , @mavt , @a , @b , @c )", new object[] { "CT99", "DB01", "Trần Bá Ngọc", DateTime.Parse("08/12/1998"), "Việt Nam", 5, "TD", 0, 0, 0 });
//            MessageBox.Show(a.ToString());



//            // ------- Test ExcuteScalar --------
//            //object a = dataProvider.ExcuteScalar("select count(*) from CauThu");
//            //MessageBox.Show(a.ToString());


//            //SqlCommand sqlCommand = dataProvider.ExcuteQuery("select MaDB from DoiBong");
//            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
//            //List<string> MaDB = new List<string>();
//            //MaDB.Add("");
//            //while (sqlDataReader.Read())
//            //{
//            //    MaDB.Add(sqlDataReader.GetString(0));

//            //}

//            //for (int i = 1; i < N; i++)
//            //{
//            //    for (int j = 1; j < N + 1; j++)
//            //    {
//            //        GenerateY(N, j, i);
//            //        a[i, j] = GenerateY(N, j, i);
//            //    }
//            //}

//            //List<TD> tDs = new List<TD>();
//            //for (int i = 1; i < N; i++)
//            //{
//            //    for (int j = 1; j < N + 1; j++)
//            //    {
//            //        if (a[i, j] != 0)
//            //        {
//            //            TD tD = new TD();
//            //            tD.d1 = j;
//            //            tD.d2 = a[i, j];
//            //            tD.vong = i;
//            //            tDs.Add(tD);
//            //        }
//            //    }
//            //}

//            //int dem = 0;
//            //for (int i = 0; i < tDs.Count; i++)
//            //{
//            //    string maTran = GenerateMaTran(i + 1);
//            //    string doiNha = MaDB[tDs[i].d1];
//            //    string doiKhach = MaDB[tDs[i].d2];
//            //    int vongDau = tDs[i].vong;

//            //    DataProvider dataProvider = new DataProvider();
//            //    dem += dataProvider.ExcuteNonQuery("insert into LichThiDau values ( @MaTran , @DoiNha , @DoiKhach , @VongDau , @ThoiGian )", new object[] { maTran, doiNha, doiKhach, vongDau, "1-1-1" });

//        }


//        public string GenerateMaTran(int x)
//        {
//            string result = "TD";

//            if (x < 10) result += "0";

//            result += x.ToString();

//            return result;
//        }

//        public int GenerateY(int n, int x, int r)
//        {
//            bool check = false;

//            for (int j = 1; j < x; j++)
//            {
//                if (a[r, j] == x)
//                {
//                    return 0;
//                }
//            }

//            for (int i = x; i <= n; i++)
//            {
//                if ((x + i) % (n - 1) == r)
//                {
//                    check = true;
//                    if (i != x) return i;
//                }
//            }

//            if (check == false && r - x > 0 && r - x <= n)
//            {
//                return r - x;
//            }

//            return n;
//        }

//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            Admin admin = new Admin();
//            admin.LoadAdmin();

//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TabControl tab;

        DispatcherTimer timerTraCuu = new DispatcherTimer();
        private bool isCollapsedTraCuu = true;

        DispatcherTimer timerQuanLy = new DispatcherTimer();
        private bool isCollapsedQuanLy = true;

        public MainWindow()
        {
            InitializeComponent();

            timerTraCuu.Tick += new EventHandler(timerTraCuu_Tick);
            timerTraCuu.Interval = new TimeSpan(10000);

            timerQuanLy.Tick += new EventHandler(timerQuanLy_Tick);
            timerQuanLy.Interval = new TimeSpan(10000);

            tab = tabs;
        }

        private void timerQuanLy_Tick(object sender, EventArgs e)
        {
            if (isCollapsedQuanLy)
            {
                //button5.Image = Resources.Collapse_Arrow_20px;
                PanelQuanLy.Height += 20;
                if (PanelQuanLy.Height == PanelQuanLy.MaxHeight)
                {
                    timerQuanLy.Stop();
                    isCollapsedQuanLy = false;
                }
            }
            else
            {
                //button5.Image = Resources.Expand_Arrow_20px;
                PanelQuanLy.Height -= 20;
                if (PanelQuanLy.Height == PanelQuanLy.MinHeight)
                {
                    timerQuanLy.Stop();
                    isCollapsedQuanLy = true;
                }
            }
        }

        private void QuanLyButton_Click(object sender, RoutedEventArgs e)
        {
            //TraCuuButton_Click(null, null);

            var lo = QuanLyButton.TranslatePoint(new Point(0, 0), this);
            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = QuanLyButton.Height;

            timerQuanLy.Start();
        }

        private void timerTraCuu_Tick(object sender, EventArgs e)
        {
            if (isCollapsedTraCuu)
            {
                //button5.Image = Resources.Collapse_Arrow_20px;
                PanelTraCuu.Height += 20;
                if (PanelTraCuu.Height == PanelTraCuu.MaxHeight)
                {
                    timerTraCuu.Stop();
                    isCollapsedTraCuu = false;
                }
            }
            else
            {
                //button5.Image = Resources.Expand_Arrow_20px;
                PanelTraCuu.Height -= 20;
                if (PanelTraCuu.Height == PanelTraCuu.MinHeight)
                {
                    timerTraCuu.Stop();
                    isCollapsedTraCuu = true;
                }
            }
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void TraCuuButton_Click(object sender, RoutedEventArgs e)
        {
            //QuanLyButton_Click(null, null);
            timerTraCuu.Start();
            Point lo = TraCuuButton.TranslatePoint(new Point(0, 0), this);

            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = TraCuuButton.Height;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckMuc.Margin = new Thickness() { Top = QuanLyButton.TranslatePoint(new Point(0, 0), this).Y };
        }

        private void TraCuuCauThuButton_Click(object sender, RoutedEventArgs e)
        {
            var lo = TraCuuCauThuButton.TranslatePoint(new Point(0, 0), this);
            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = TraCuuCauThuButton.Height;

            TraCuuCauThu traCuuCauThu = new TraCuuCauThu();
            traCuuCauThu.ShowDialog();
        }

        private void TraCuuDoiBongButton_Click(object sender, RoutedEventArgs e)
        {
            var lo = TraCuuDoiBongButton.TranslatePoint(new Point(0, 0), this);
            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = TraCuuDoiBongButton.Height;

            Tra_cuu_thong_tin_doi_bong tra_Cuu_Thong_Tin_Doi_Bong = new Tra_cuu_thong_tin_doi_bong();
            tra_Cuu_Thong_Tin_Doi_Bong.ShowDialog();
        }

        private void TraCuuLichThiDauButton_Click(object sender, RoutedEventArgs e)
        {
            var lo = TraCuuLichThiDauButton.TranslatePoint(new Point(0, 0), this);
            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = TraCuuLichThiDauButton.Height;

            TraCuuLichThiDau traCuuLichThiDau = new TraCuuLichThiDau();
            traCuuLichThiDau.ShowDialog();
        }

        private void TraCuuKetQuaButton_Click(object sender, RoutedEventArgs e)
        {
            var lo = TraCuuKetQuaButton.TranslatePoint(new Point(0, 0), this);
            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = TraCuuKetQuaButton.Height;

            TraCuuKetQuaTran traCuuKetQuaTran = new TraCuuKetQuaTran();
            traCuuKetQuaTran.ShowDialog();
        }

        private void TraCuuTruocTranButton_Click(object sender, RoutedEventArgs e)
        {
            var lo = TraCuuTruocTranButton.TranslatePoint(new Point(0, 0), this);
            CheckMuc.Margin = new Thickness() { Top = lo.Y };
            CheckMuc.Height = TraCuuTruocTranButton.Height;

            TraCuuTruocTran traCuuTruocTran = new TraCuuTruocTran();
            traCuuTruocTran.ShowDialog();
        }

        private void ThemDoiBongButton_Click(object sender, RoutedEventArgs e)
        {
            ReceiveRecords receiveRecords = new ReceiveRecords();
            receiveRecords.ShowDialog();
        }

        private void ChinhSuaDoiBongButton_Click(object sender, RoutedEventArgs e)
        {
            ChinhSuaDoiBong chinhSuaDoiBong = new ChinhSuaDoiBong();
            chinhSuaDoiBong.ShowDialog();
        }

        private void NhapDanhSachTrongTaiButton_Click(object sender, RoutedEventArgs e)
        {
            NhapDanhSachTrongTai nhapDanhSachTrongTai = new NhapDanhSachTrongTai();
            nhapDanhSachTrongTai.ShowDialog();
        }

        private void NhapKetQuaTranButton_Click(object sender, RoutedEventArgs e)
        {
            NhapKetQuaTranDau nhapKetQuaTranDau = new NhapKetQuaTranDau();
            nhapKetQuaTranDau.ShowDialog();
        }

        private void PhanBoTrongTaiButton_Click(object sender, RoutedEventArgs e)
        {
            PhanBoTrongTai phanBoTrongTai = new PhanBoTrongTai();
            phanBoTrongTai.ShowDialog();
        }

        private void SapXepLichThiDauButton_Click(object sender, RoutedEventArgs e)
        {
            SapXepLichThiDau sapXepLichThiDau = new SapXepLichThiDau();
            sapXepLichThiDau.ShowDialog();
        }

        private void ThayDoiLuatButton_Click(object sender, RoutedEventArgs e)
        {
            ThayDoiLuatThiDau thayDoiLuatThiDau = new ThayDoiLuatThiDau();
            thayDoiLuatThiDau.ShowDialog();
        }

        private void TongKetMuaGiaiButton_Click(object sender, RoutedEventArgs e)
        {
            TongKetMuaGiai tongKetMuaGiai = new TongKetMuaGiai();
            tongKetMuaGiai.ShowDialog();
        }

        private void VuaPhaLuoiButton_Click(object sender, RoutedEventArgs e)
        {
            TraCuuVuaPhaLuoi traCuuVuaPhaLuoi = new TraCuuVuaPhaLuoi();
            traCuuVuaPhaLuoi.ShowDialog();
        }

        private void BangXepHangButton_Click(object sender, RoutedEventArgs e)
        {
            TraCuuBangXepHang traCuuBangXepHang = new TraCuuBangXepHang();
            traCuuBangXepHang.ShowDialog();
        }

        private void ThongTinTruocTran_Click(object sender, RoutedEventArgs e)
        {
            TraCuuTruocTran traCuuTruocTran = new TraCuuTruocTran();
            traCuuTruocTran.ShowDialog();
        }

        bool isLogin = false;
        private void Hyperlink1_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

            isLogin = window1.isLogin;

            if (isLogin)
            {
                DangNhapTextBlock.Visibility = Visibility.Hidden;
                DangXuatTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void Hyperlink2_Click(object sender, RoutedEventArgs e)
        {
            isLogin = false;
            DangNhapTextBlock.Visibility = Visibility.Visible;
            DangXuatTextBlock.Visibility = Visibility.Hidden;
        }
    }
}