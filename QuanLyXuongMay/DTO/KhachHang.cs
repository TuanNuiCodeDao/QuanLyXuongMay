using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class KhachHang
    {
        private string ma;
        private string hoTen;
        private string sdt;
        private string diaChi;
        public KhachHang()
        {

        }

        public string Ma { get => ma; set => ma = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public KhachHang(DataRow d)
        {
            Ma = d["MaKH"].ToString();
            HoTen = d["TenKH"].ToString();
            Sdt = d["SDT"].ToString();
            DiaChi = d["DiaChi"].ToString();
        }
    }
}
