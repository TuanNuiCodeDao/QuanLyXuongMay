using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class PhanCong
    {
        private int maPC;
        private string maSP;
        private string maNV;
        private int tienCong1SP;
        private int soLuongPhanCong;
        private int soLuongHoanThanh;
        private int soLuongCon;
        private string ghiChu;
        private string trangThai;
        public PhanCong()
        {

        }
        public PhanCong(DataRow d)
        {
            MaPC = (int)d["MaPC"];
            maSP = d["MaSP"].ToString();
            MaNV = d["MaNV"].ToString();
            tienCong1SP = (int)d["TienCong1SP"];
            soLuongPhanCong = (int) d["SoLuongPhanCong"];
            soLuongHoanThanh = (int)d["SoLuongHoanThanh"];
            soLuongCon = (int)d["SoLuongCon"];
            GhiChu = d["GhiChu"].ToString();
            TrangThai = d["TrangThai"].ToString();
        }

        public string MaSP { get => maSP; set => maSP = value; }
        public string MaNV { get => maNV; set => maNV = value; }
        public int TienCong1SP { get => tienCong1SP; set => tienCong1SP = value; }
        public int SoLuongPhanCong { get => soLuongPhanCong; set => soLuongPhanCong = value; }
        public int SoLuongHoanThanh { get => soLuongHoanThanh; set => soLuongHoanThanh = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public int MaPC { get => maPC; set => maPC = value; }
        public int SoLuongCon { get => soLuongCon; set => soLuongCon = value; }
    }
}
