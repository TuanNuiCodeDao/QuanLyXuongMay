using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class ThongKeSanPham
    {
        private string ma;
        private string ten;
        private int soLuongDaGiao;
        private int doanhThu;
        private int donGiaTB;
        private int tongChiPhi;
        private int chiPhiTB;
        private int soLuongChuaGiao;
        private int soLuongDangSX;
        public ThongKeSanPham(string ma, string ten, int soLuongDaGiao, int doanhThu, int donGiaTB, int tongChiPhi, int chiPhiTB, int soLuongChuaGiao, int soLuongDangSX)
        {
            Ma = ma;
            Ten = ten;
            SoLuongDaGiao = soLuongDaGiao;
            DoanhThu = doanhThu;
            DonGiaTB = donGiaTB;
            TongChiPhi = tongChiPhi;
            ChiPhiTB = chiPhiTB;
            SoLuongChuaGiao = soLuongChuaGiao;
            SoLuongDangSX = soLuongDangSX;
        }

        public ThongKeSanPham()
        {

        }

        public string Ma { get => ma; set => ma = value; }
        public string Ten { get => ten; set => ten = value; }
        public int SoLuongDaGiao { get => soLuongDaGiao; set => soLuongDaGiao = value; }
        public int DoanhThu { get => doanhThu; set => doanhThu = value; }
        public int DonGiaTB { get => donGiaTB; set => donGiaTB = value; }
        public int TongChiPhi { get => tongChiPhi; set => tongChiPhi = value; }
        public int ChiPhiTB { get => chiPhiTB; set => chiPhiTB = value; }
        public int SoLuongChuaGiao { get => soLuongChuaGiao; set => soLuongChuaGiao = value; }
        public int SoLuongDangSX { get => soLuongDangSX; set => soLuongDangSX = value; }
    }
}
