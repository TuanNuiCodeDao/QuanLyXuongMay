using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class ChiPhiRiengDAO
    {
        private static ChiPhiRiengDAO instance;
        public static ChiPhiRiengDAO Instance
        {
            get { if (instance == null) instance = new ChiPhiRiengDAO(); return instance; }
            private set { instance = value; }
        }
        private ChiPhiRiengDAO()
        {
        }
        public List<ChiPhiRieng> loadDS()
        {
            List<ChiPhiRieng> ds = new List<ChiPhiRieng>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIRIENG");
            foreach (DataRow item in data.Rows)
            {
                ChiPhiRieng b = new ChiPhiRieng(item);
                ds.Add(b);
            }
            return ds;
        }
        public ChiPhiRieng getLanTraLuongCuoiCungTheoMaNV(string ma)
        {
            List<ChiPhiRieng> ds = new List<ChiPhiRieng>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIRIENG WHERE TenCP=N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                ChiPhiRieng b = new ChiPhiRieng(item);
                ds.Add(b);
            }
            if (ds == null || ds.Count == 0)
                return null;
            return ds[ds.Count-1];
        }
        public ChiPhiRieng getLast()
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT*FROM CHIPHIRIENG WHERE MaCP=(SELECT MAX(MaCP) FROM CHIPHIRIENG)");
            foreach (DataRow item in data.Rows)
            {
                ChiPhiRieng b = new ChiPhiRieng(item);
                return b;
            }
            return null;
        }
        public ChiPhiRieng getChiPhiRiengTheoMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIRIENG WHERE MaCP=" + ma);
            foreach (DataRow item in data.Rows)
            {
                ChiPhiRieng b = new ChiPhiRieng(item);
                return b;
            }
            return null;
        }
        public List<ChiPhiRieng> loadDSTheoDieuKien(string dk)
        {
            List<ChiPhiRieng> ds = new List<ChiPhiRieng>();
            DataTable data1 = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIRIENG "+dk);
            foreach (DataRow item in data1.Rows)
            {
                ChiPhiRieng b = new ChiPhiRieng(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<ChiPhiRieng> loadDSTim(string tuKhoa)
        {
            List<ChiPhiRieng> ds = new List<ChiPhiRieng>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIRIENG WHERE TenCP LIKE N'%" + tuKhoa + "%' OR PhanLoai LIKE N'%" + tuKhoa + "%' OR GhiChu LIKE N'%" + tuKhoa + "%'");
            foreach (DataRow item in data.Rows)
            {
                ChiPhiRieng b = new ChiPhiRieng(item);
                ds.Add(b);
            }
            return ds;
        }
    }
}
