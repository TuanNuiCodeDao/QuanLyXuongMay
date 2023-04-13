using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class CTDonHang
    {
        private string maCTDH;
        private string maDH;
        private string maSP;
        private string mau;
        private string size;
        private string ghiChu;
        private int donGia;
        private int chiPhiThoMay;
        private int soLuongDat;
        private int soLuongGiao;
        public CTDonHang()
        {

        }
        public CTDonHang(DataRow d)
        {
            MaCTDH = d["MaCTDH"].ToString();
            MaDH = d["MaDH"].ToString();
            MaSP = d["MaSP"].ToString();
            Mau = d["Mau"].ToString();
            Size = d["Size"].ToString();
            GhiChu = d["GhiChu"].ToString();
            DonGia = (int)d["DonGia"];
            chiPhiThoMay = (int)d["ChiPhiThoMay"];
            SoLuongDat = (int)d["SoLuongDat"];
            SoLuongGiao = (int)d["SoLuongGiao"];
        }

        public string MaCTDH { get => maCTDH; set => maCTDH = value; }
        public string MaDH { get => maDH; set => maDH = value; }
        public string MaSP { get => maSP; set => maSP = value; }
        public string Mau { get => mau; set => mau = value; }
        public string Size { get => size; set => size = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public int SoLuongDat { get => soLuongDat; set => soLuongDat = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public int SoLuongGiao { get => soLuongGiao; set => soLuongGiao = value; }
        public int ChiPhiThoMay { get => chiPhiThoMay; set => chiPhiThoMay = value; }
    }
}
