using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class DangNhap
    {
        private string taiKhoan;
        private string matKhau;
        public DangNhap()
        {

        }
        public DangNhap(DataRow d)
        {
            taiKhoan = d["TaiKhoan"].ToString();
            matKhau = d["MatKhau"].ToString();
        }
        public string TaiKhoan { get => taiKhoan; set => taiKhoan = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
    }
}
