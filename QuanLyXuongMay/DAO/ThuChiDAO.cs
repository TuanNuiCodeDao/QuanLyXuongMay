using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class ThuChiDAO
    {
        private static ThuChiDAO instance;
        public static ThuChiDAO Instance
        {
            get { if (instance == null) instance = new ThuChiDAO(); return instance; }
            private set { instance = value; }
        }
        private ThuChiDAO()
        {
        }
        public List<ThuChi> loadDS()
        {
            List<ThuChi> ds = new List<ThuChi>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  ThuChi");
            foreach (DataRow item in data.Rows)
            {
                ThuChi b = new ThuChi(item);
                ds.Add(b);
            }
            return ds;
        }

        public List<ThuChi> loadDSTim(string dieuKien)
        {
            List<ThuChi> ds = new List<ThuChi>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  THUCHI "+dieuKien);
            foreach (DataRow item in data.Rows)
            {
                ThuChi b = new ThuChi(item);
                ds.Add(b);
            }
            return ds;
        }
    }
}
