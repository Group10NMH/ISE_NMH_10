﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
        static public TabControl tab;
        static public MaterialDesignThemes.Wpf.PopupBox st;
        static public TextBlock dn;
        static public WrapPanel ai;

        static int N;
        int[,] a;

        public MainWindow()
        {
            InitializeComponent();
            st = SettingPopup;
            dn = DangNhapTextBlock;
            ai = AcountInfo;
            tab = tabs;

            tabs.SelectedIndex = 17;

            DataProvider dataProvider = new DataProvider();
            int SoTran = (int)dataProvider.ExcuteScalar("select count(*) from LichThiDau");
            if (SoTran == 0) TaoLichThiDauButton.IsEnabled = true;

            N = (int)dataProvider.ExcuteScalar("select count(*) from DoiBong");

            a = new int[N, N + 1];
        }

        private void PowerOffButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        public static bool isLogin = false;
        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {
            if (!isLogin)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn hãy đăng nhập để sử dụng chức năng quản lý nhé", "Thông báo" ,  MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    TittleTextBlock.Text = "Đăng nhập";
                    tabs.SelectedIndex = 0;
                }
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Đăng nhập";
            tabs.SelectedIndex = 0;
        }

        private void ThemDoiBongButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Thêm đội bóng mới vào mùa giải";
            tabs.SelectedIndex = 2;
        }

        private void DangXuatButton_Click(object sender, RoutedEventArgs e)
        {
            isLogin = false;
            SettingPopup.Visibility = Visibility.Hidden;
            DangNhapTextBlock.Visibility = Visibility.Visible;
            AcountInfo.Visibility = Visibility.Hidden;
            tabs.SelectedIndex = 17;
        }

        private void ThemTrongTaiButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Thêm trọng tài";
            tabs.SelectedIndex = 3;
        }

        

        public class TD
        {
            public int d1, d2, vong;
        }

        private void TaoLichThiDauButton_Click(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select MaDB from DoiBong");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<string> MaDB = new List<string>();
            MaDB.Add("");
            while (sqlDataReader.Read())
            {
                MaDB.Add(sqlDataReader.GetString(0));

            }

            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    a[i, j] = GenerateY(N, j, i);
                }
            }

            List<TD> tDs = new List<TD>();
            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (a[i, j] != 0)
                    {
                        TD tD = new TD();
                        tD.d1 = j;
                        tD.d2 = a[i, j];
                        tD.vong = i;
                        tDs.Add(tD);
                    }
                }
            }

            int dem = 0;
            for (int i = 0; i < tDs.Count; i++)
            {
                string maTran = GenerateMaTran(i + 1);
                string doiNha = MaDB[tDs[i].d1];
                string doiKhach = MaDB[tDs[i].d2];
                int vongDau = tDs[i].vong;

                dataProvider = new DataProvider();
                dem += dataProvider.ExcuteNonQuery("insert into LichThiDau values ( @MaTran , @DoiNha , @DoiKhach , @VongDau , @ThoiGian )", new object[] { maTran, doiNha, doiKhach, vongDau, "1-1-1" });

            }
        }

        public string GenerateMaTran(int x)
        {
            string result = "TD";

            if (x < 10) result += "0";

            result += x.ToString();

            return result;
        }

        public int GenerateY(int n, int x, int r)
        {
            bool check = false;

            for (int j = 1; j < x; j++)
            {
                if (a[r, j] == x)
                {
                    return 0;
                }
            }

            for (int i = x; i <= n; i++)
            {
                if ((x + i) % (n - 1) == r)
                {
                    check = true;
                    if (i != x) return i;
                }
            }

            if (check == false && r - x > 0 && r - x <= n)
            {
                return r - x;
            }

            return n;
        }

        private void SapXepLichThiDauButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Sắp xếp lịch thi đấu";
            tabs.SelectedIndex = 4;
        }

        private void PhanBoTrongTaiButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Phẩn bổ trọng tài";
            tabs.SelectedIndex = 5;
        }

        private void NhapKetQuaButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Nhập kết quả trận đấu";
            tabs.SelectedIndex = 6;
        }

        private void TongKetMuaGiaiButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Tổng kết mùa giải";
            tabs.SelectedIndex = 7;
        }

        private void ChinhSuaDoiBongButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Chỉnh sửa đội bóng";
            tabs.SelectedIndex = 8;
        }

        private void ThayDoiLuatButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Thay đổi luật thi đấu";
            tabs.SelectedIndex = 9;
        }

        private void TraCuuDoiBongButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Tra cứu thông tin đội bóng";
            tabs.SelectedIndex = 10;
        }

        private void TraCuuCauThuButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Tra cứu cầu thủ";
            tabs.SelectedIndex = 11;
        }

        private void TraCuuLichButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Tra cứu lịch thi đấu";
            tabs.SelectedIndex = 12;
        }

        private void TraCuuKetQuaButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Tra cứu kết quả thi đấu";
            tabs.SelectedIndex = 13;
        }

        private void TraCuuTruocTranButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Tra cứu thông tin trước trận đấu";
            tabs.SelectedIndex = 14;
        }

        private void TraCuuVuaPhaLuoiButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Bảng xếp hạng vua phá lưới";
            tabs.SelectedIndex = 15;
        }

        private void TraCuuBangXepHangButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Bảng xếp hạng mùa giải";
            tabs.SelectedIndex = 16;

        }

        private void DoiMatKhauButton_Click(object sender, RoutedEventArgs e)
        {
            TittleTextBlock.Text = "Đổi mật khẩu";
            tabs.SelectedIndex = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 17;
        }
    }
}