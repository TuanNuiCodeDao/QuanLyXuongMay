using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class DonHangDAO
    {
        private static DonHangDAO instance;
        public static DonHangDAO Instance
        {
            get { if (instance == null) instance = new DonHangDAO(); return instance; }
            private set { instance = value; }
        }
        private DonHangDAO()
        {
        }
        public List<DonHang> loadDS()
        {
            List<DonHang> ds = new List<DonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG");
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public DonHang getDonHangByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG WHERE MaDH =N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                return b;
            }
            return null;
        }
        public List<DonHang> getDonHangByMaKH(string ma)
        {
            List<DonHang> ds = new List<DonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG WHERE MaKH =N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<DonHang> loadDSChuaHT()
        {
            List<DonHang> ds = new List<DonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG WHERE TrangThai=N'Chưa hoàn thành'");
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<DonHang> loadDSTheoDieuKien(string dieuKien)
        {
            List<DonHang> ds = new List<DonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG "+ dieuKien);
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<DonHang> loadDSDaHT()
        {
            List<DonHang> ds = new List<DonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG WHERE TrangThai!=N'Chưa hoàn thành' AND TrangThai!=N'Đã hủy'");
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<DonHang> loadDSDaHuy()
        {
            List<DonHang> ds = new List<DonHang>();
             DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DONHANG WHERE TrangThai=N'Đã hủy'");
            foreach (DataRow item in data.Rows)
            {
                DonHang b = new DonHang(item);
                ds.Add(b);
            }
            return ds;
        }
    }
}
