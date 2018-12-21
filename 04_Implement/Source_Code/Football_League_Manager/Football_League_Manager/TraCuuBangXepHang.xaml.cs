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
    /// Interaction logic for TraCuuBangXepHang.xaml
    /// </summary>
    public partial class TraCuuBangXepHang : Window
    {
        public TraCuuBangXepHang()
        {
            InitializeComponent();
            bangXepHangs = new List<DataObject>();
        }

        List<TranDau> TranDaus;
        public List<DataObject> bangXepHangs { get; set; }
        public class DataObject
        {
            public int ViTri { get; set; }
            public string TenDoiBong { get; set; }
            public int SoTran { get; set; }
            public int SoTranThang { get; set; }
            public int SoTranThua { get; set; }
            public int SoTranHoa { get; set; }
            public int SoBanThang { get; set; }
            public int SoBanThua { get; set; }
            public int HieuSo { get; set; }
            public int Diem { get; set; }

            public int SoSanh(DataObject d)
            {
                if (Diem > d.Diem)
                {
                    return 1;
                }
                else if (Diem < d.Diem)
                {
                    return -1;
                }
                else
                {
                    if (HieuSo > d.HieuSo)
                    {
                        return 1;
                    }
                    else if (HieuSo < d.HieuSo)
                    {
                        return -1;
                    }
                    else
                    {
                        if (SoBanThang > d.SoBanThang)
                        {
                            return 1;
                        }
                        else if (SoBanThang < d.SoBanThang)
                        {
                            return -1;
                        }
                        else return 0;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select TenDB from DoiBong");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<string> DoiBongs = new List<string>();
            int soDoi = 0;
            while (sqlDataReader.Read())
            {
                soDoi++;
                DoiBongs.Add(sqlDataReader.GetString(0));
            }

            if (soDoi == 0) MessageBox.Show("Hãy thêm đội bóng trước khi tra cứu bảng xếp hạng nhé!");
            else
            {
                sqlCommand = dataProvider.ExcuteQuery("select ltd.MaTran, db1.TenDB, db2.TenDB, kq.SoBanDoi1, kq.SoBanDoi2 from LichThiDau ltd, KetQua kq, DoiBong db1, DoiBong db2 " +
              "where(ltd.MaDoi1 = db1.MaDB  and  ltd.MaDoi2 = db2.MaDB) and kq.MaTran = ltd.MaTran");
                sqlDataReader = sqlCommand.ExecuteReader();

                TranDaus = new List<TranDau>();
                int soTran = 0;
                while (sqlDataReader.Read())
                {
                    soTran++;
                    TranDau tranDau = new TranDau();
                    tranDau.MaTranDau = sqlDataReader.GetString(0);
                    tranDau.TenDoiA = sqlDataReader.GetString(1);
                    tranDau.TenDoiB = sqlDataReader.GetString(2);
                    tranDau.TiSoDoiA = sqlDataReader.GetInt32(3);
                    tranDau.TiSoDoiB = sqlDataReader.GetInt32(4);
                    TranDaus.Add(tranDau);
                }

                var list = new List<DataObject>();
                for (int i = 0; i < DoiBongs.Count; i++)
                {
                    DataObject dataObject = new DataObject();

                    dataObject.TenDoiBong = DoiBongs[i];

                    dataObject.SoTran = 0;
                    foreach (var tranDau in TranDaus)
                    {
                        if (DoiBongs[i] == tranDau.TenDoiA || DoiBongs[i] == tranDau.TenDoiB)
                        {
                            dataObject.SoTran += 1;
                        }
                    }

                    if (dataObject.SoTran != 0)
                    {
                        dataObject.SoTranThang = 0;
                        foreach (var tranDau in TranDaus)
                        {
                            if ((DoiBongs[i] == tranDau.TenDoiA && tranDau.TiSoDoiA > tranDau.TiSoDoiB)
                                || (DoiBongs[i] == tranDau.TenDoiB && tranDau.TiSoDoiB > tranDau.TiSoDoiA))
                            {
                                dataObject.SoTranThang += 1;
                            }
                        }

                        dataObject.SoTranHoa = 0;
                        foreach (var tranDau in TranDaus)
                        {
                            if ((DoiBongs[i] == tranDau.TenDoiA || DoiBongs[i] == tranDau.TenDoiB) && tranDau.TiSoDoiB == tranDau.TiSoDoiA)
                            {
                                dataObject.SoTranHoa += 1;
                            }
                        }

                        dataObject.SoTranThua = dataObject.SoTran - dataObject.SoTranThang - dataObject.SoTranHoa;

                        dataObject.SoBanThang = 0;
                        dataObject.SoBanThua = 0;
                        foreach (var tranDau in TranDaus)
                        {
                            if (DoiBongs[i] == tranDau.TenDoiA)
                            {
                                dataObject.SoBanThang += tranDau.TiSoDoiA;
                                dataObject.SoBanThua += tranDau.TiSoDoiB;
                            }

                            if (DoiBongs[i] == tranDau.TenDoiB)
                            {
                                dataObject.SoBanThang += tranDau.TiSoDoiB;
                                dataObject.SoBanThua += tranDau.TiSoDoiA;
                            }
                        }

                        dataObject.HieuSo = dataObject.SoBanThang - dataObject.SoBanThua;

                        dataObject.Diem = dataObject.SoTranThang * 3 + dataObject.SoTranHoa * 1;

                        list.Add(dataObject);
                    }
                }


                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i; j < list.Count; j++)
                    {
                        if (list[j].SoSanh(list[i]) == 1)
                        {
                            DataObject temp = list[i];
                            list[i] = list[j];
                            list[j] = temp;
                        }
                    }
                    list[i].ViTri = i + 1;
                }

                BXHListView.ItemsSource = list;
                bangXepHangs = list;
            }
        }
    }
}
