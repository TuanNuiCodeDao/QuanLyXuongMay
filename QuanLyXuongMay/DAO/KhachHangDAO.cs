using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;
        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return instance; }
            private set { instance = value; }
        }
        private KhachHangDAO()
        {
        }
        public List<KhachHang> loadDS()
        {
            List<KhachHang> ds = new List<KhachHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  KHACHHANG");
            foreach (DataRow item in data.Rows)
            {
                KhachHang b = new KhachHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<KhachHang> loadDSTim(string tuKhoa)
        {
            List<KhachHang> ds = new List<KhachHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  KHACHHANG WHERE TenKH LIKE N'%" + tuKhoa + "%' OR MaKH LIKE N'%" + tuKhoa + "%' OR SDT LIKE N'%" + tuKhoa + "%' OR DiaChi LIKE N'%" + tuKhoa + "%'");
            foreach (DataRow item in data.Rows)
            {
                KhachHang b = new KhachHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public KhachHang getKhachHangByMa(string ma)
        {
            List<KhachHang> ds = new List<KhachHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  KHACHHANG WHERE MaKH =N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                KhachHang b = new KhachHang(item);
                return b;
            }
            return null;
        }
    }
}
