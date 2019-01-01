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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for TCTruocTranUserControl.xaml
    /// </summary>
    public partial class TCTruocTranUserControl : UserControl
    {
        public TCTruocTranUserControl()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new List<string>();

            for (int i = 0; i < 9; i++)
            {
                items.Add(" Vòng " + (i + 1).ToString());
            }

            itemsCombobox.ItemsSource = items;
            itemsCombobox.SelectedIndex = 0;

        }

        VongDau vongDau;
        private void itemsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vongDau = new VongDau();
            vongDau.LoadLichThiDau(itemsCombobox.SelectedIndex + 1);

            itemsCombobox_Copy.ItemsSource = vongDau.TranDaus;
            itemsCombobox_Copy.SelectedIndex = 0;
        }

        class DataObject
        {
            public string CTDoiA { get; set; }
            public string CTDoiB { get; set; }
        }

        DoiBong doiBongA;
        DoiBong doiBongB;
        ObservableCollection<DataObject> CauThuDuocThiDau = new ObservableCollection<DataObject>();

        private void itemsCombobox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (itemsCombobox_Copy.SelectedIndex >= 0)
            {
                doiBongA = new DoiBong();
                doiBongB = new DoiBong();


                string[] tokens = itemsCombobox_Copy.SelectedItem.ToString().Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(token => token.Trim())
                    .ToArray<string>();

                doiBongA.LoadDoiBong(tokens[0]);
                doiBongB.LoadDoiBong(tokens[1]);

                DataProvider dataProvider = new DataProvider();
                SqlCommand sqlCommand = dataProvider.ExcuteQuery("select tt.TenTT from LichCamCoi lcc join TrongTai tt on lcc.MaTT = tt.MaTT where lcc.MaTran = @MaTran", new object[] { vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].MaTranDau });
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                TT1TextBox.Text = TT2TextBox.Text = TT3TextBox.Text = TT4TextBox.Text = "";


                int temp = 0;
                while (sqlDataReader.Read())
                {
                    if (temp == 0) TT1TextBox.Text = "  " + sqlDataReader.GetString(0);
                    if (temp == 1) TT2TextBox.Text = "  " + sqlDataReader.GetString(0);
                    if (temp == 2) TT3TextBox.Text = "  " + sqlDataReader.GetString(0);
                    if (temp == 3) TT4TextBox.Text = "  " + sqlDataReader.GetString(0);
                    temp++;
                }

                HLVATextBox.Text = doiBongA.HuanLuyenVien;
                HLVBTextBox.Text = doiBongB.HuanLuyenVien;


                vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].LoadCTBiCam(itemsCombobox.SelectedIndex + 1);

                for (int i = 0; i < doiBongA.CauThus.Count; i++)
                {
                    for (int j = 0; j < vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsA.Count; j++)
                    {
                        if (doiBongA.CauThus[i].TenCauThu == vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsA[j].TenCauThu)
                        {
                            doiBongA.CauThus.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }

                for (int i = 0; i < doiBongB.CauThus.Count; i++)
                {
                    for (int j = 0; j < vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsB.Count; j++)
                    {
                        if (doiBongB.CauThus[i].TenCauThu == vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsB[j].TenCauThu)
                        {
                            doiBongB.CauThus.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }

                CheckDuocThiDau.IsChecked = true;
                CheckDuocThiDau_Checked(sender, e);

            }
            else
            {
                HLVATextBox.Text = HLVBTextBox.Text = TT4TextBox.Text = TT3TextBox.Text = TT2TextBox.Text = TT1TextBox.Text = "";
                CauThuDuocThiDau.Clear();
                CauThuDataGrid.ItemsSource = CauThuDuocThiDau;
            }



        }

        // Chưa xong hàm này
        private void CheckBiCamThiDau_Checked(object sender, RoutedEventArgs e)
        {
            CauThuDuocThiDau.Clear();

            int soCTA = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsA.Count;
            int soCTB = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsB.Count;

            if (soCTA > soCTB)
            {
                for (int i = 0; i < soCTA; i++)
                {
                    if (i >= soCTB)
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsA[i].TenCauThu, CTDoiB = "" });
                    }
                    else
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsA[i].TenCauThu, CTDoiB = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsB[i].TenCauThu });
                    }

                }
            }
            else
            {
                for (int i = 0; i < soCTB; i++)
                {
                    if (i >= soCTA)
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = "", CTDoiB = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsB[i].TenCauThu });
                    }
                    else
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsA[i].TenCauThu, CTDoiB = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].CauThuBiCamsB[i].TenCauThu });
                    }

                }
            }
            if (CauThuDuocThiDau.Count != 0) CauThuDataGrid.ItemsSource = CauThuDuocThiDau;
        }

        private void CheckDuocThiDau_Checked(object sender, RoutedEventArgs e)
        {
            CauThuDuocThiDau.Clear();

            int soCTA = doiBongA.CauThus.Count;
            int soCTB = doiBongB.CauThus.Count;
            if (soCTA > soCTB)
            {
                for (int i = 0; i < soCTA; i++)
                {
                    if (i >= soCTB)
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = doiBongA.CauThus[i].TenCauThu, CTDoiB = "" });
                    }
                    else
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = doiBongA.CauThus[i].TenCauThu, CTDoiB = doiBongB.CauThus[i].TenCauThu });
                    }

                }
            }
            else
            {
                for (int i = 0; i < soCTB; i++)
                {
                    if (i >= soCTA)
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = "", CTDoiB = doiBongB.CauThus[i].TenCauThu });
                    }
                    else
                    {
                        CauThuDuocThiDau.Add(new DataObject() { CTDoiA = doiBongA.CauThus[i].TenCauThu, CTDoiB = doiBongB.CauThus[i].TenCauThu });
                    }

                }
            }

            CauThuDataGrid.ItemsSource = CauThuDuocThiDau;

        }
    }
}
