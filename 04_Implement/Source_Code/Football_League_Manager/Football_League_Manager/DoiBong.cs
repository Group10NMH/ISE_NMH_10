using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Football_League_Manager
{
    class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        void LoadAdmin()
        {

        }

        bool Login(string user, string pass)
        {

            return false;
        }
    }

    class DanhSachDoiBong
    {
        public List<DoiBong> DoiBongs { get; set; }

        void LoadDanhSachDoi()
        {
            // select * from DoiBong

            // for () doiBongs[i]->LoadDoiBong();
        }
    }


    public class DoiBong
    {
        public string MaDoiBong { get; set; }
        public string TenDoiBong { get; set; }
        public string SanNha { get; set; }
        public string HuanLuyenVien { get; set; }
        public int SoTran { get; set; }
        public int SoTranThang { get; set; }
        public int SoTranThua { get; set; }

        public List<CauThu> CauThus { get; set; }

        public void LoadDoiBong(string tenDoiBong)
        {
            DataProvider dataProvider = new DataProvider();
            SqlCommand sql;
            sql = dataProvider.ExcuteQuery("select db.TenDB, db.TenHLV, ct.TenCT, db.MaDB from CauThu ct join DoiBong db on ct.MaDB = db.MaDB where db.TenDB = @vongdau", new object[] { tenDoiBong });
            SqlDataReader sqlDataReader = sql.ExecuteReader();

            CauThus = new List<CauThu>();

            while (sqlDataReader.Read())
            {
                CauThu cauThu = new CauThu();
                cauThu.TenCauThu = sqlDataReader.GetString(2);
                CauThus.Add(cauThu);
                TenDoiBong = tenDoiBong;
                HuanLuyenVien = sqlDataReader.GetString(1);
                MaDoiBong = sqlDataReader.GetString(3);
            }

        }
    }

    public class ViTri
    {
        public string MaVT { get; set; }
        public string TenVT { get; set; }
    }

    public class CauThu
    {
        public string MaCauThu { get; set; }
        public string TenCauThu { get; set; }
        public string NgaySinhCauThu { get; set; }
        public string ViTri { get; set; }
        public int SoAo { get; set; }
        public string MaVT { get; set; }
        public string QuocTichCT { get; set; }

        //
        public string MaDB { get; set; }
        public int STT { get; set; }
        public int SoBanThang { get; set; }
        public int SoTheVang { get; set; }
        public string TenDoiBong { get; set; }
        //
    }

    class VongDau
    {
        public List<TranDau> TranDaus { get; set; }

        public void LoadLichThiDau(int vong)
        {
            DataProvider dataProvider = new DataProvider();

            SqlCommand sql = dataProvider.ExcuteQuery("select ltd.MaTran, db1.TenDB, db2.TenDB from LichThiDau ltd join DoiBong db1 on ltd.MaDoi1 = db1.MaDB join DoiBong db2 on ltd.MaDoi2 = db2.MaDB where VongDau = @vongdau", new object[] { vong });

            SqlDataReader sqlDataReader = sql.ExecuteReader();

            TranDaus = new List<TranDau>();

            while (sqlDataReader.Read())
            {
                TranDau tranDau = new TranDau();
                tranDau.MaTranDau = sqlDataReader.GetString(0);
                tranDau.TenDoiA = sqlDataReader.GetString(1);
                tranDau.TenDoiB = sqlDataReader.GetString(2);
                TranDaus.Add(tranDau);
            }
        }
    }

    public class TranDau
    {
        public string MaTranDau { get; set; }
        public string TenDoiA { get; set; }
        public string TenDoiB { get; set; }

        public int TiSoDoiA { get; set; }
        public int TiSoDoiB { get; set; }

        public DateTime ThoiGian { get; set; }

        public List<CauThu> CauThuBiCamsA { get; set; }
        public List<CauThu> CauThuBiCamsB { get; set; }

        public override string ToString()
        {
            return TenDoiA + " - " + TenDoiB;
        }

        public void LoadKQTranDau()
        {
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from KetQua where MaTran = @matran", new object[] { MaTranDau });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TiSoDoiA = sqlDataReader.GetInt32(1);
                TiSoDoiB = sqlDataReader.GetInt32(2);
            }
        }

        public void LoadCTBiCam(int vong)
        {
            CauThuBiCamsA = new List<CauThu>();
            CauThuBiCamsB = new List<CauThu>();
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select TenCT from CauThu ct join DoiBong db on ct.MaDB = db.MaDB where VongCam = @vong and db.TenDB = @tendoi", new object[] { vong, TenDoiA });
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CauThu cauThu = new CauThu();
                cauThu.TenCauThu = sqlDataReader.GetString(0);
                CauThuBiCamsA.Add(cauThu);
            }

            dataProvider = new DataProvider();
            sqlCommand = dataProvider.ExcuteQuery("select TenCT from CauThu ct join DoiBong db on ct.MaDB = db.MaDB where VongCam = @vong and db.TenDB = @tendoi", new object[] { vong, TenDoiB });
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CauThu cauThu = new CauThu();
                cauThu.TenCauThu = sqlDataReader.GetString(0);
                CauThuBiCamsB.Add(cauThu);
            }
            sqlDataReader.Close();
        }
    }



    class DanhSachTrongTai
    {

        public Dictionary<string, string> DanhSachTrongTais { get; set; }
        public void LoadDSTrongTai()
        {
            DataProvider dataProvider = new DataProvider();
            SqlCommand sqlCommand = dataProvider.ExcuteQuery("select * from TrongTai");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DanhSachTrongTais = new Dictionary<string, string>();
            while (sqlDataReader.Read())
            {
                string ma = sqlDataReader.GetString(0);
                string ten = sqlDataReader.GetString(1);
                DanhSachTrongTais.Add(ma, ten);
            }

        }

    }

    class LuatThiDau
    {
        public int DiemThang { get; set; }
        public int DiemThua { get; set; }
        public int DiemHoa { get; set; }
        public int TuoiToiDa { get; set; }
        public int TuoiToiThieu { get; set; }
        public int SoCauThuToiDa { get; set; }
        public int SoCauThuToiThieu { get; set; }
        public int SoTheVang { get; set; }
    }

    class DataProvider
    {
        private static DataProvider instance;

        string ConnectionString = @"Data Source=TBN-PC\SQLEXPRESS;Initial Catalog=GiaiVDQG;Integrated Security=True";

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        /// <summary>
        /// Hàm truy vấn dạng select .....
        /// </summary>
        /// <param name="query"> Câu truy vấn (select * from CauThu) </param>
        /// <param name="parameter"> Danh sách parameter tham số cho query </param>
        /// <returns> SqlCommand để xử lí bên hàm chính </returns>
        public SqlCommand ExcuteQuery(string query, object[] parameter = null)
        {
            // Tạo kết nối tới cơ sở dữ liệu bằng chuỗi kết nối
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                // Mở kết nối
                if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                // Tạo SqlCommand truy xuất dữ liệu: 
                // query - câu truy vấn
                // sqlConnection - cầu nối csdl được tạo ở trên
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Nếu danh sách parameter không rỗng
                if (parameter != null)
                {
                    // Cắt chuỗi query tại những dấu cách để lấy ra những từ có chứa "@"
                    var listparam = query.Split(' ');
                    int i = 0;

                    // Lấy ra từng item trong danh sách trên
                    foreach (var item in listparam)
                    {
                        // Nếu item có chứa "@"
                        if (item.Contains("@"))
                        {
                            // Tạo mới 1 para có tên là item vừa cắt ở trên 
                            SqlParameter para = new SqlParameter(item, null);

                            // Giá trị của para = giá trị được lưu trong object[] parameter tương ứng
                            para.Value = parameter[i];
                            i++;

                            sqlCommand.Parameters.Add(para);
                        }
                    }
                }


                return sqlCommand;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Hàm truy vấn dạng insert, update, delete
        /// </summary>
        /// <param name="query"> Câu truy vấn (select * from CauThu) </param>
        /// <param name="parameter"> Danh sách parameter tham số cho query </param>
        /// <returns> Số dòng xử lý thành công </returns>
        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            int NumberOfLine = 0;
            try
            {
                // Mở kết nối
                if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                if (parameter != null)
                {
                    var listparam = query.Split(' ');
                    int i = 0;
                    foreach (var item in listparam)
                    {
                        if (item.Contains("@"))
                        {
                            SqlParameter par = new SqlParameter(item, null);

                            par.Value = parameter[i];
                            i++;

                            sqlCommand.Parameters.Add(par);
                        }
                    }
                }

                // Hàm truy xuất dạng insert, update, delete trả về số dòng
                NumberOfLine = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                NumberOfLine = 0;
            }
            return NumberOfLine;
        }

        /// <summary>
        /// Hàm truy vấn để lấy 1 giá trị duy nhất ví dụ select count(*) ......
        /// </summary>
        /// <param name="query"> Câu truy vấn (select * from CauThu) </param>
        /// <param name="parameter"> Danh sách parameter tham số cho query </param>
        /// <returns> Trả về đối tượng mình cần lấy (bất kì kiểu nào) </returns>
        public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = null;

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            try
            {
                // Mở kết nối
                if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                if (parameter != null)
                {
                    var listparam = query.Split(' ');
                    int i = 0;
                    foreach (var item in listparam)
                    {
                        if (item.Contains("@"))
                        {
                            SqlParameter par = new SqlParameter(item, null);

                            par.Value = parameter[i];
                            i++;

                            sqlCommand.Parameters.Add(par);
                        }
                    }
                }

                // Hàm truy xuất lấy ra 1 đối tượng data
                data = sqlCommand.ExecuteScalar();
            }
            catch (Exception)
            {
                data = null;
            }
            return data;
        }

    }
}

