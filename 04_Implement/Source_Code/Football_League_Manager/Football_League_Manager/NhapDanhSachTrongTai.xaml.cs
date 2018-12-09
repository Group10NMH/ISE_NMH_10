using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NhapDanhSachTrongTai.xaml
    /// </summary>
    public partial class NhapDanhSachTrongTai : Window
    {
        public NhapDanhSachTrongTai()
        {
            InitializeComponent();
        }
        class DataObject
        {
            public string MaTT { get; set; }
            public string TenTT { get; set; }
        };
        private void TrongTaiDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DataObject> list = new ObservableCollection<DataObject>();
            DanhSachTrongTai dstt = new DanhSachTrongTai();
            dstt.LoadDSTrongTai();

            foreach (KeyValuePair<string, string> item in dstt.DanhSachTrongTais)
            {
                list.Add(new DataObject() { MaTT = item.Key, TenTT = item.Value });
            }
  
            TrongTaiDataGrid.ItemsSource = list;

        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DataObject> list = new ObservableCollection<DataObject>();
            DanhSachTrongTai dstt = new DanhSachTrongTai();
            dstt.LoadDSTrongTai();
            int i = 0;
            if ( TenTrongTai_TextBox.Text != null )
            {
                foreach (KeyValuePair<string, string> item in dstt.DanhSachTrongTais)
                {
                    list.Add(new DataObject() { MaTT = item.Key, TenTT = item.Value });
                }
                i = dstt.DanhSachTrongTais.Count();
                string maTT;
                if (i<10)
                {
                    maTT = "TT0" + i.ToString();
                }
                else { maTT = "TT" + i.ToString(); }
                list.Add(new DataObject() { MaTT = maTT, TenTT = TenTrongTai_TextBox.Text });
                TrongTaiDataGrid.ItemsSource = list;
                DataProvider dataProvider = new DataProvider();
                dataProvider.ExcuteNonQuery("insert into TrongTai values ( @MaTT , @TenTT )", new object[] {maTT, TenTrongTai_TextBox.Text});
                TenTrongTai_TextBox.Text = "";
            }
        }
       

       
        private void HoanTat_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
