using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class DangNhapDAO
    {
        private static DangNhapDAO instance;
        public static DangNhapDAO Instance
        {
            get { if (instance == null) instance = new DangNhapDAO(); return instance; }
            private set { instance = value; }
        }
        private DangNhapDAO()
        {
        }
        public bool ktrDangNhap(string taiKhoan,string matKhau)
        {
            List<KhachHang> ds = new List<KhachHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DANGNHAP WHERE TaiKhoan=N'"+taiKhoan+"' AND MatKhau=N'"+matKhau+"'");
            foreach (DataRow item in data.Rows)
            {
                DangNhap b = new DangNhap(item);
                return true;
            }
            return false;
        }
        public DangNhap getDangNhap()
        {
            List<KhachHang> ds = new List<KhachHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  DANGNHAP");
            foreach (DataRow item in data.Rows)
            {
                DangNhap b = new DangNhap(item);
                return b;
            }
            return null;
        }
    }
}
