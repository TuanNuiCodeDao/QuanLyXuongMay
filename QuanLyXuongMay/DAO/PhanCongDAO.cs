using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class PhanCongDAO
    {
        private static PhanCongDAO instance;
        public static PhanCongDAO Instance
        {
            get { if (instance == null) instance = new PhanCongDAO(); return instance; }
            private set { instance = value; }
        }
        private PhanCongDAO()
        {
        }
        public List<PhanCong> loadDS()
        {
            List<PhanCong> ds = new List<PhanCong>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  PhanCong ");
            foreach (DataRow item in data.Rows)
            {
                PhanCong b = new PhanCong(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<PhanCong> loadDSTheoDieuKien(string dieuKien)
        {
            List<PhanCong> ds = new List<PhanCong>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  PhanCong " + dieuKien);
            foreach (DataRow item in data.Rows)
            {
                PhanCong b = new PhanCong(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<PhanCong> loadDSChuaThanhToanTheoMaThoMay(string ma)
        {
            List<PhanCong> ds = new List<PhanCong>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  PhanCong WHERE MaNV=N'"+ma+"' AND TrangThai=N'Chưa thanh toán'");
            foreach (DataRow item in data.Rows)
            {
                PhanCong b = new PhanCong(item);
                ds.Add(b);
            }
            return ds;
        }
        public PhanCong getPhanCongByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  PhanCong WHERE MaPC =N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                PhanCong b = new PhanCong(item);
                return b;
            }
            return null;
        }
        public void xoaPC(string ma)
        {
            DataProvider.Instance.RunQuery("DELETE FROM PHANCONG WHERE MaPC = N'" + ma + "'");
        }
        public PhanCong getPhanCongMaxByMaSP(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM PHANCONG WHERE MaPC=(SELECT MAX(MaPC) FROM PHANCONG) AND PHANCONG.MaSP = N'"+ma+"'");
            foreach (DataRow item in data.Rows)
            {
                PhanCong b = new PhanCong(item);
                return b;
            }
            return null;
        }
    }
}
