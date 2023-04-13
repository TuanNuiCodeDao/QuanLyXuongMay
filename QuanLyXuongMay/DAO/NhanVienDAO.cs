using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;
        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
            private set { instance = value; }
        }
        private NhanVienDAO()
        {
        }
        public List<NhanVien> loadDSNhanVien()
        {
            List<NhanVien> ds = new List<NhanVien>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  NHANVIEN");
            foreach (DataRow item in data.Rows)
            {
                NhanVien b = new NhanVien(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<NhanVien> loadDSThoMay()
        {
            List<NhanVien> ds = new List<NhanVien>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  NHANVIEN WHERE PhanLoai=N'Nhân viên sản phẩm'");
            foreach (DataRow item in data.Rows)
            {
                NhanVien b = new NhanVien(item);
                ds.Add(b);
            }
            return ds;
        }
        public NhanVien getNhanVienByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  NHANVIEN WHERE MaNV=N'"+ma+"'");
            foreach (DataRow item in data.Rows)
            {
                NhanVien b = new NhanVien(item);
                return b;
            }
            return null;
        }
        public List<NhanVien> loadDSTimNhanVien(string tuKhoa)
        {
            List<NhanVien> ds = new List<NhanVien>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  NHANVIEN WHERE HoTen LIKE N'%"+ tuKhoa + "%' OR MaNV LIKE N'%" + tuKhoa + "%' OR SDT LIKE N'%" + tuKhoa + "%' OR ToDoi LIKE N'%" + tuKhoa + "%' OR PhanLoai LIKE N'%" + tuKhoa + "%'");
            foreach (DataRow item in data.Rows)
            {
                NhanVien b = new NhanVien(item);
                ds.Add(b);
            }
            return ds;
        }
    }
}