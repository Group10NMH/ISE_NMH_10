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
using System.Data;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for ReceiveRecords.xaml
    /// </summary>
    public partial class ReceiveRecords : Window
    {
        public ReceiveRecords()
        {
            InitializeComponent();
           
        }
        AddListCT listCT = new AddListCT();
        public List<DoiBong> doiBongs = new List<DoiBong>();
        DoiBong doiBong = new DoiBong();
        
        public void Hoan_tat(object sender, RoutedEventArgs e)
        {
            string maDB = "DB";
            string madb = "";
            int dem = 0;
            int demdb = 0;
            string TenDBTemp = "";
            bool kt = true;
            int Ma = 0;
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlcommand = dataProvider.ExcuteQuery("select * from DoiBong");
            SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();

            if (TenDB.Text == "")
            {
                MessageBox.Show("Thêm tên đội bóng");

            }
            else if (HLV.Text == "")
            {
                MessageBox.Show("Thêm tên huấn luyện viên của đội bóng");
            }
            else if (SanNha.Text == "")
            {
                MessageBox.Show("Thêm sân nhà của đội bóng");
            }
            else
            {
                while (sqlDataReader.Read())
                {
                    TenDBTemp = sqlDataReader.GetString(1);
                    if (TenDB.Text == TenDBTemp)
                    {
                        kt = false;
                        break;
                    }
                }
                if (kt == false)
                {
                    MessageBox.Show("Tên đội bóng đã tồn tại");
                }
                else
                {
                    if (listCT.cauThus.Count == 0)
                    {
                        MessageBox.Show("Bạn phải thêm cầu thủ cho đội");
                    }
                    else
                    {
                        SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from DoiBong");
                        SqlDataReader sqldataReader = sqlCommand.ExecuteReader();
                        while (sqldataReader.Read())
                        {
                            doiBong.MaDoiBong = sqldataReader.GetString(0);
                        }
                        if (doiBong.MaDoiBong != null)
                        {
                            Ma = int.Parse(doiBong.MaDoiBong.Substring(2, 2));
                        }
                        doiBong.SanNha = SanNha.Text;
                        doiBong.HuanLuyenVien = HLV.Text;
                        doiBong.TenDoiBong = TenDB.Text;
                        Ma += 1;
                        if (Ma < 10) { maDB = "DB0"; }
                        
                        madb = maDB + Ma.ToString();
                        doiBong.MaDoiBong = madb;
                        doiBongs.Add(doiBong);

                        demdb = dataProvider.ExcuteNonQuery("INSERT INTO DoiBong (MaDB,TenDB,TenHLV,SanNha,isDelete) VALUES ( @a , @b , @c , @d , '0' )"
                        , new object[] { madb, doiBong.TenDoiBong, doiBong.HuanLuyenVien, doiBong.SanNha });

                        foreach (var ct in doiBong.CauThus)
                        {
                       
                            dem = dataProvider.ExcuteNonQuery("INSERT INTO CauThu (MaCT, MaDB,TenCT,NgaySinh,SoAo,MaVT,QuocTich,SoTheCamDa,SoTheVang,isDelete) VALUES ( @a , @b , @c , @d , @e , @f , @g , '0' , '0' , '0' )", new object[] {
                            ct.MaCauThu, doiBong.MaDoiBong ,ct.TenCauThu, DateTime.Parse(ct.NgaySinhCauThu) , ct.SoAo, ct.MaVT , ct.QuocTichCT });
                        }
                        MessageBox.Show(dem.ToString() + demdb.ToString());
                        SanNha.Text = "";
                        TenDB.Text = "";
                        HLV.Text = "";
                        doiBong = new DoiBong();
                        listCT.Close(); 
                    }
                }
            }
        }
        private void Them_cau_thu_Click(object sender, RoutedEventArgs e)
        {
            if (TenDB.Text == "")
            {
                MessageBox.Show("Thêm tên đội bóng");

            }
            else if (HLV.Text == "")
            {
                MessageBox.Show("Thêm tên huấn luyện viên của đội bóng");
            }
            else if (SanNha.Text == "")
            {
                MessageBox.Show("Thêm sân nhà của đội bóng");
            }
            else
            {
                this.Hide();
                listCT = new AddListCT();
                listCT.ShowDialog();
                this.Show();
                doiBong.CauThus = new List<CauThu>(listCT.cauThus);
                listCT.Close();
            }
        }
    }
}
