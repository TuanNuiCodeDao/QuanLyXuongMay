using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace QuanLyXuongMay.DAO
{
    public class DataProvider
    {
        private string severname;
        private static DataProvider instance;
        private string connectionStr = @"Data Source=DESKTOP-LRLPN55;Initial Catalog=DataXuongMay;Integrated Security=True";
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }
        public string Severname { get => severname; set => severname = value; }
        private DataProvider()
        {
        }
        public void resetDuLieu()
        {
            DataProvider.instance.RunQuery("DELETE FROM THUCHI");
            DataProvider.instance.RunQuery("DELETE FROM CHIPHIRIENG");
            DataProvider.instance.RunQuery("DELETE FROM CHIPHIVATTU");
            DataProvider.instance.RunQuery("DELETE FROM PHANCONG");
            DataProvider.instance.RunQuery("DELETE FROM CT_DONHANG");
            DataProvider.instance.RunQuery("DELETE FROM DONHANG");
            DataProvider.instance.RunQuery("DELETE FROM KHACHHANG");
            DataProvider.instance.RunQuery("DELETE FROM SANPHAM");
            DataProvider.instance.RunQuery("DELETE FROM NHANVIEN");
        }
        public bool saoLuuThuCong(string noiLuu)
        {
            try
            {
                DateTime dt=DateTime.Now;
                string s= dt.ToString(),duongDan="";
                int i = 0;
                while(i<s.Length)
                {
                    if (s[i] == '/'|| s[i] == ' '|| s[i] == ':')
                            duongDan += '_';
                        else duongDan += s[i];
                    i++;
                }
                string Sql = "BACKUP DATABASE DataXuongMay TO DISK = '" + noiLuu + "\\ThuCong_DataXuongMay_" + duongDan + ".bak'";
                using (SqlConnection CON = new SqlConnection(connectionStr))
                using (SqlCommand cmdBackup = new SqlCommand(Sql, CON))
                {
                    CON.Open();
                    cmdBackup.ExecuteNonQuery();
                    CON.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi");
            }
            return true;
        }
        public void saoLuuTuDong()
        {
            string directoryPath = "C:\\BackUpDataXuongMay\\";
            try
            { 
                if (!System.IO.Directory.Exists(directoryPath))
                    System.IO.Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString(),"Lỗi");
                return;
            }
            try
            {
                DateTime dt = DateTime.Now;
                string s = dt.ToString(), tenFile = "";
                int i = 0;
                while (i < s.Length)
                {
                    if (s[i] == '/' || s[i] == ' ' || s[i] == ':')
                        tenFile += '_';
                    else tenFile += s[i];
                    i++;
                }
                string noiLuu = directoryPath + "TuDong_DataXuongMay_" + tenFile;
                string Sql = "BACKUP DATABASE DataXuongMay TO DISK = '" + noiLuu+ ".bak'";
                using (SqlConnection CON = new SqlConnection(connectionStr))
                using (SqlCommand cmdBackup = new SqlCommand(Sql, CON))
                {
                    CON.Open();
                    cmdBackup.ExecuteNonQuery();
                    CON.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi");
            }
        }
        public bool phucHoi(string duongDan)
        {
            try
            {
                using (SqlConnection CON = new SqlConnection(connectionStr))
                {
                    CON.Open();
                    string sql1 = "ALTER DATABASE DataXuongMay SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    SqlCommand cmd1 = new SqlCommand(sql1, CON);
                    cmd1.ExecuteNonQuery();

                    string sql2 = "USE MASTER RESTORE DataBase DataXuongMay FROM DISK='"+duongDan+"' WITH REPLACE";
                    SqlCommand cmd2 = new SqlCommand(sql2, CON);
                    cmd2.ExecuteNonQuery();

                    string sql3 = "ALTER DataXuongMay DatabaseName SET MULTI_USER";
                    SqlCommand cmd3 = new SqlCommand(sql3, CON);
                    cmd2.ExecuteNonQuery();
                    CON.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi");
            }
            return true;
        }
        public DataTable RunQuery(string query, object[] parameter = null)
        {

            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi");
            }
            return data;
        }
    }
}
