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

namespace Football_League_Manager
{
    /// <summary>
    /// Interaction logic for TraCuuKetQuaTran.xaml
    /// </summary>
    public partial class TraCuuKetQuaTran : Window
    {
        public TraCuuKetQuaTran()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new List<string>();

            for (int i = 0; i < 20; i++)
            {
                items.Add(" Vòng " + (i + 1).ToString());
            }

            itemsCombobox.ItemsSource = items;
            itemsCombobox.SelectedIndex = 0;
            
        }

        VongDau vongDau = new VongDau();
        private void itemsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vongDau = new VongDau();
            vongDau.LoadLichThiDau(itemsCombobox.SelectedIndex + 1);

            itemsCombobox_Copy.ItemsSource = vongDau.TranDaus;
            //itemsCombobox_Copy.SelectedIndex = 0;
        }

        private void itemsCombobox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (itemsCombobox_Copy.SelectedIndex != -1)
            {
                DoiNhaText.Text = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].TenDoiA;
                DoiKhachText.Text = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].TenDoiB;

                TranDau tranDau = new TranDau();
                tranDau.MaTranDau = vongDau.TranDaus[itemsCombobox_Copy.SelectedIndex].MaTranDau;
                bool check = tranDau.LoadKQTranDau();

                if (check == true)
                {
                    TiSoA.Content = tranDau.TiSoDoiA.ToString();
                    TiSoB.Content = tranDau.TiSoDoiB.ToString();
                }
                else
                {
                    MessageBox.Show("Trận đấu chưa được diễn ra, vui lòng trở lại sau");
                }
            }
        }
    }
}
