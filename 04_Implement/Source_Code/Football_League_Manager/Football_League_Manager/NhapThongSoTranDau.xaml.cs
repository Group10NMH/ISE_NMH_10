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
    /// Interaction logic for NhapThongSoTranDau.xaml
    /// </summary>
    public partial class NhapThongSoTranDau : Window
    {
        public NhapThongSoTranDau()
        {
            InitializeComponent();
        }

        public string TenDoiNha { get; set; }
        public string TenDoiKhach { get; set;}
        public string MaTran { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var iteams = new List<string> { TenDoiNha,TenDoiKhach };
            DBcomboBox.ItemsSource = iteams;
            var thevang = new List<int> { 1, 2 };
            comboBox2.ItemsSource = thevang;
            var sobanthang = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            comboBox3.ItemsSource = sobanthang;
            //DBcomboBox.SelectedIndex = 0;
        }
        List<CauThu> DSCauThu = new List<CauThu>();
        private void DBcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            string ten = DBcomboBox.SelectedItem.ToString();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select CT.TenCT, CT.SoAo,CT.MaCT,CT.MaDB,CT.SoTheVang from DoiBong DB, CauThu CT where DB.MaDB = CT.MaDB and DB.TenDB = @TenDoiBong", new object[] { ten });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
       
            while (sqlDataReader.Read())
            {
                string TenCauThu = sqlDataReader.GetString(0);
                int SoAo = sqlDataReader.GetInt32(1);
                string MaCT = sqlDataReader.GetString(2);
                string MaDB = sqlDataReader.GetString(3);
                var SoTheVang= sqlDataReader.GetInt32(4);
                
                CauThu CT=new CauThu();
                CT.TenCauThu = TenCauThu;
                CT.SoAo = SoAo;
                CT.MaCauThu = MaCT;
                CT.MaDB = MaDB;
                CT.SoTheVang = SoTheVang;
                DSCauThu.Add(CT);
            }
            comboBox1.ItemsSource = DSCauThu;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int SoTheVang = DSCauThu[comboBox1.SelectedIndex].SoTheVang + int.Parse(comboBox2.SelectedItem.ToString());
            string MaCT = DSCauThu[DBcomboBox.SelectedIndex].MaCauThu;
            string MaDB = DSCauThu[DBcomboBox.SelectedIndex].MaDB;
            int SoBanThang =int.Parse(comboBox3.SelectedItem.ToString());
            string maTran = MaTran;
            DataProvider dataProvider = new DataProvider();
            int a = dataProvider.ExcuteNonQuery("insert into GhiBan values ( @MaCT , @MaTran , @MaDB , @soban )",new object[] {MaCT,maTran,MaDB,SoBanThang});
            
            int b = dataProvider.ExcuteNonQuery("update CauThu set SoTheVang= @SoTheVang where MaDB= @MaDB and MaCT= @MaCT ", new object[] { SoTheVang, MaDB, MaCT });
            MessageBox.Show("Lưu thành công");


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

