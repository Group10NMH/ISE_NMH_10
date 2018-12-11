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
    /// Interaction logic for NhapKetQuaTranDau.xaml
    /// </summary>
    public partial class NhapKetQuaTranDau : Window
    {
        public NhapKetQuaTranDau()
        {
            InitializeComponent();
        }
        List<DataObject> DSTranDau;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            NhapThongSoTranDau nhapThongSoTranDau = new NhapThongSoTranDau();
            nhapThongSoTranDau.TenDoiNha = DoiNha.Text;
            nhapThongSoTranDau.TenDoiKhach = DoiKhach.Text;
            nhapThongSoTranDau.MaTran = DSTranDau[TDcomboBox.SelectedIndex].MaTran;
            nhapThongSoTranDau.ShowDialog();
            Show();
        }
        class DataObject
        {
            public string TenDoiNha { get; set;}
            public string TenDoiKhach { get; set;}
            public string MaTran { get; set; }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var iteams = new List<string> { "Vòng 1", "Vòng 2", "Vòng 3" ,"Vòng 4","Vòng 5","Vòng 6","Vòng 7","Vòng 8","Vòng 9"};
            VDCombobox.ItemsSource = iteams;
        }
       
        private void VDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           DataProvider dataProvider = new DataProvider();

            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select DB.TenDB,DB1.TenDB,LTD.MaTran from DoiBong DB, LichThiDau LTD, DoiBong DB1 where DB.MaDB = LTD.MaDoi1 and DB1.MaDB = LTD.MaDoi2 and LTD.VongDau = @Vong ", new object[] {VDCombobox.SelectedIndex+1});
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DSTranDau = new List<DataObject>();

            while (sqlDataReader.Read())
            {
                string TenDoiNha = sqlDataReader.GetString(0);
                string TenDoiKhach = sqlDataReader.GetString(1);
                string MaTran = sqlDataReader.GetString(2);
                DataObject data = new DataObject();
                data.TenDoiNha = TenDoiNha;
                data.TenDoiKhach = TenDoiKhach;
                data.MaTran = MaTran;
                DSTranDau.Add(data);
            }
            TDcomboBox.ItemsSource = DSTranDau;
        }

        private void TDcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoiNha.Text= DSTranDau[TDcomboBox.SelectedIndex].TenDoiNha;
            DoiKhach.Text= DSTranDau[TDcomboBox.SelectedIndex].TenDoiKhach;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string maTran = DSTranDau[TDcomboBox.SelectedIndex].MaTran ;
            int SoBanNha = int.Parse(SBTNTextBox.Text);
            int SoBanKhach = int.Parse(SBTKTextBox.Text);
            DataProvider dataProvider = new DataProvider();
            int a = dataProvider.ExcuteNonQuery("insert into KetQua values ( @MaTran , @SoBanNha , @SoBanKhach )", new object[] {maTran,SoBanNha,SoBanKhach});
            if (a==1) MessageBox.Show("Lưu thành công");

        }

        private void SBTNTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int a = int.Parse(SBTNTextBox.Text);
              

            }
            catch(Exception)
            {
                if(SBTNTextBox.Text!="" )MessageBox.Show("Số bàn thắng phải là số tự nhiên");
                SBTNTextBox.Text = "";
            }
        }

        private void SBTKTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int a = int.Parse(SBTKTextBox.Text);


            }
            catch (Exception)
            {
                if (SBTKTextBox.Text != "") MessageBox.Show("Số bàn thắng phải là số tự nhiên");
                SBTKTextBox.Text = "";
            }
        }
    }
}
