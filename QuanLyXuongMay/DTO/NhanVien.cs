using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class NhanVien
    {
        private string ma;
        private string hoTen;
        private string sdt;
        private string toDoi;
        private string phanLoai;
        private int luong;
        public NhanVien()
        {
            ma = "";
            hoTen = "";
            luong = 0;
            sdt = "";
        }
        public string Ma { get => ma; set => ma = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public int Luong { get => luong; set => luong = value; }
        public string ToDoi { get => toDoi; set => toDoi = value; }
        public string PhanLoai { get => phanLoai; set => phanLoai = value; }

        public NhanVien(DataRow d)
        {
            this.ma = d["MaNV"].ToString();
            this.hoTen = d["HoTen"].ToString();
            this.sdt = d["SDT"].ToString();
            this.toDoi = d["ToDoi"].ToString();
            this.phanLoai = d["PhanLoai"].ToString();
            this.luong = (int)d["Luong"];
        }
    }
}
