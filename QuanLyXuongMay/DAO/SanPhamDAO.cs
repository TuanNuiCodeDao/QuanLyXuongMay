using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class SanPhamDAO
    {
        private static SanPhamDAO instance;
        public static SanPhamDAO Instance
        {
            get { if (instance == null) instance = new SanPhamDAO(); return instance; }
            private set { instance = value; }
        }
        private SanPhamDAO()
        {
        }
        public List<SanPham> loadDS()
        {
            List<SanPham> ds = new List<SanPham>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM SANPHAM");
            foreach (DataRow item in data.Rows)
            {
                SanPham b = new SanPham(item);
                ds.Add(b);
            }
            return ds;
        }
        public string getUrlByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT *FROM SANPHAM WHERE MaSP= N'"+ma+"'");
            foreach (DataRow item in data.Rows)
            {
                SanPham b = new SanPham(item);
                return item["AnhURL"].ToString();
            }
            return null;
        }
        public SanPham getSanPhamByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT *FROM SANPHAM WHERE MaSP= N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                SanPham b = new SanPham(item);
                return b;
            }
            return null;
        }
        public SanPham getSanPhamByTen(string ten)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT *FROM SANPHAM WHERE TenSP= N'" + ten + "'");
            foreach (DataRow item in data.Rows)
            {
                SanPham b = new SanPham(item);
                return b;
            }
            return null;
        }
        public List<SanPham> loadDSTim(string tuKhoa)
        {
            List<SanPham> ds = new List<SanPham>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  SANPHAM WHERE TenSP LIKE N'%" + tuKhoa + "%' OR MaSP LIKE N'%" + tuKhoa + "%' OR Size LIKE N'%" + tuKhoa + "%' OR Mau LIKE N'%" + tuKhoa + "%' OR GhiChu LIKE N'%" + tuKhoa + "%'");
            foreach (DataRow item in data.Rows)
            {
                SanPham b = new SanPham(item);
                ds.Add(b);
            }
            return ds;
        }
    }
}
