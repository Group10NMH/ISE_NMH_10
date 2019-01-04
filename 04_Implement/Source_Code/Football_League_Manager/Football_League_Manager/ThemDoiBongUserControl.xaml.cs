using Aspose.Cells;
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
    /// Interaction logic for ThemDoiBongUserControl.xaml
    /// </summary>
    public partial class ThemDoiBongUserControl : UserControl
    {
        public ThemDoiBongUserControl()
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
            bool checkTenDB = true;
            bool checkTenHLV = true;
            bool checkTenSanNha = true;
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlcommand = dataProvider.ExcuteQuery("select * from DoiBong");
            SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();

            for (int j = 0; j < TenDB.Text.Length; j++)
            {
                if (TenDB.Text[j] == '0' || TenDB.Text[j] == '1' || TenDB.Text[j] == '2' || TenDB.Text[j] == '3' || TenDB.Text[j] == '4' || TenDB.Text[j] == '5' ||
                       TenDB.Text[j] == '6' || TenDB.Text[j] == '7' || TenDB.Text[j] == '8' || TenDB.Text[j] == '9')
                {
                    checkTenDB = false;
                    break;
                }
            }

            for (int j = 0; j < HLV.Text.Length; j++)
            {
                if (HLV.Text[j] == '0' || HLV.Text[j] == '1' || HLV.Text[j] == '2' || HLV.Text[j] == '3' || HLV.Text[j] == '4' || HLV.Text[j] == '5' ||
                       HLV.Text[j] == '6' || HLV.Text[j] == '7' || HLV.Text[j] == '8' || HLV.Text[j] == '9')
                {

                    checkTenHLV = false;
                    break;
                }
            }

            for (int j = 0; j < SanNha.Text.Length; j++)
            {
                if (SanNha.Text[j] == '0' || SanNha.Text[j] == '1' || SanNha.Text[j] == '2' || SanNha.Text[j] == '3' || SanNha.Text[j] == '4' || SanNha.Text[j] == '5' ||
                      SanNha.Text[j] == '6' || SanNha.Text[j] == '7' || SanNha.Text[j] == '8' || SanNha.Text[j] == '9')
                {
                    checkTenSanNha = false;
                    break;
                }
            }


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
            else if (checkTenDB == false)
            {
                MessageBox.Show("Tên đội bóng không được để số");
            }
            else if (checkTenHLV == false)
            {
                MessageBox.Show("Tên huấn luyện viên không được để số");
            }
            else if (checkTenSanNha == false)
            {
                MessageBox.Show("Tên sân nhà không được để số");
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
                    if (doiBong.CauThus.Count == 0)
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

                        demdb = dataProvider.ExcuteNonQuery("INSERT INTO DoiBong (MaDB,TenDB,TenHLV,SanNha) VALUES ( @a , @b , @c , @d )"
                                                            , new object[] { madb, doiBong.TenDoiBong, doiBong.HuanLuyenVien, doiBong.SanNha });

                        foreach (var ct in doiBong.CauThus)
                        {
                            dem = dataProvider.ExcuteNonQuery("INSERT INTO CauThu (MaCT, MaDB,TenCT,NgaySinh,SoAo,MaVT,QuocTich,SoTheCamDa,SoTheVang) VALUES ( @a , @b , @c , @d , @e , @f , @g , '0' , '0' )", new object[] {
                            ct.MaCauThu, doiBong.MaDoiBong ,ct.TenCauThu, DateTime.Parse(ct.NgaySinhCauThu) , ct.SoAo, ct.MaVT , ct.QuocTichCT });
                        }

                        if (demdb == 1) MessageBox.Show("Thêm đội bóng và cầu thủ thành công");

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
            bool checkTenDB = true;
            bool checkTenHLV = true;
            bool checkTenSanNha = true;
            for (int j = 0; j < TenDB.Text.Length; j++)
            {
                if (TenDB.Text[j] == '0' || TenDB.Text[j] == '1' || TenDB.Text[j] == '2' || TenDB.Text[j] == '3' || TenDB.Text[j] == '4' || TenDB.Text[j] == '5' ||
                       TenDB.Text[j] == '6' || TenDB.Text[j] == '7' || TenDB.Text[j] == '8' || TenDB.Text[j] == '9')
                {
                    checkTenDB = false;
                    break;
                }
            }

            for (int j = 0; j < HLV.Text.Length; j++)
            {
                if (HLV.Text[j] == '0' || HLV.Text[j] == '1' || HLV.Text[j] == '2' || HLV.Text[j] == '3' || HLV.Text[j] == '4' || HLV.Text[j] == '5' ||
                       HLV.Text[j] == '6' || HLV.Text[j] == '7' || HLV.Text[j] == '8' || HLV.Text[j] == '9')
                {

                    checkTenHLV = false;
                    break;
                }
            }

            for (int j = 0; j < SanNha.Text.Length; j++)
            {
                if (SanNha.Text[j] == '0' || SanNha.Text[j] == '1' || SanNha.Text[j] == '2' || SanNha.Text[j] == '3' || SanNha.Text[j] == '4' || SanNha.Text[j] == '5' ||
                      SanNha.Text[j] == '6' || SanNha.Text[j] == '7' || SanNha.Text[j] == '8' || SanNha.Text[j] == '9')
                {
                    checkTenSanNha = false;
                    break;
                }
            }


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
            else if (checkTenDB == false)
            {
                MessageBox.Show("Tên đội bóng không được để số");
            }
            else if (checkTenHLV == false)
            {
                MessageBox.Show("Tên huấn luyện viên không được để số");
            }
            else if (checkTenSanNha == false)
            {
                MessageBox.Show("Tên sân nhà không được để số");
            }

            else
            {
                //this.Hide();
                listCT = new AddListCT();
                listCT.ShowDialog();
                //this.Show();
                doiBong.CauThus = new List<CauThu>(listCT.cauThus);
                listCT.Close();
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            
            dlg.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                TenFileTextBlock.Text = dlg.FileName;
                DongYImport.Visibility = Visibility.Visible;
            }
        }

        private void DongYImport_Click(object sender, RoutedEventArgs e)
        {
            doiBong = new DoiBong();
            doiBong.CauThus = new List<CauThu>();
            Workbook wb = new Workbook(TenFileTextBlock.Text);
            Worksheet sheet = wb.Worksheets[0];

            // Usually we read based on our template
            Cell cell = sheet.Cells[$"B1"];
            TenDB.Text = cell.Value.ToString();

            cell = sheet.Cells[$"B2"];
            HLV.Text = cell.Value.ToString();

            cell = sheet.Cells[$"B3"];
            SanNha.Text = cell.Value.ToString();

            cell = sheet.Cells[$"A6"];

            string maCT = "CT";

            int row = 6;
            int ID = 1;
            while (cell.Value != null)
            {
                

                CauThu cauThu = new CauThu();

                if (ID < 10) cauThu.MaCauThu = maCT + "0" + ID.ToString();
                else cauThu.MaCauThu = maCT + ID.ToString();
                ID++;

                cell = sheet.Cells[$"A{row}"];
                cauThu.TenCauThu = cell.Value.ToString();

                cell = sheet.Cells[$"B{row}"];
                string ns = cell.Value.ToString();
                cauThu.NgaySinhCauThu = DateTime.Parse(ns).ToString("dd/MM/yyyy");

                cell = sheet.Cells[$"C{row}"];
                cauThu.SoAo = int.Parse(cell.Value.ToString());


                cell = sheet.Cells[$"D{row}"];

                if (cell.Value.ToString() == "Hậu vệ") { cauThu.MaVT = "HV"; }
                if (cell.Value.ToString() == "Tiền đạo") { cauThu.MaVT = "TĐ"; }
                if (cell.Value.ToString() == "Tiền vệ") { cauThu.MaVT = "TV"; }
                if (cell.Value.ToString() == "Thủ môn") { cauThu.MaVT = "TM"; }

                cell = sheet.Cells[$"E{row}"];
                cauThu.QuocTichCT = cell.Value.ToString();

                doiBong.CauThus.Add(cauThu);

                row++;
                cell = sheet.Cells[$"A{row}"];
            }

            ThemCauThuButton.Visibility = Visibility.Hidden;
        }
    }
}
