using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class ThongKeKhachHang
    {
        private string ma;
        private string hoTen;
        private string sdt;
        private int tongSoDon;
        private int soDonDaXong;
        private int soDonChuaXong;
        private int soDonDaHuy;
        private int tongSoTienDaTT;
        public ThongKeKhachHang()
        { }
        public ThongKeKhachHang(string ma, string hoTen, string sdt, int tongSoDon, int soDonDaXong, int soDonChuaXong, int soDonDaHuy, int tongSoTienDaTT)
        {
            Ma = ma;
            HoTen = hoTen;
            Sdt = sdt;
            TongSoDon = tongSoDon;
            SoDonDaXong = soDonDaXong;
            SoDonChuaXong = soDonChuaXong;
            SoDonDaHuy = soDonDaHuy;
            TongSoTienDaTT = tongSoTienDaTT;
        }

        public string Ma { get => ma; set => ma = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public int TongSoDon { get => tongSoDon; set => tongSoDon = value; }
        public int SoDonDaXong { get => soDonDaXong; set => soDonDaXong = value; }
        public int SoDonChuaXong { get => soDonChuaXong; set => soDonChuaXong = value; }
        public int SoDonDaHuy { get => soDonDaHuy; set => soDonDaHuy = value; }
        public int TongSoTienDaTT { get => tongSoTienDaTT; set => tongSoTienDaTT = value; }
    }
}
