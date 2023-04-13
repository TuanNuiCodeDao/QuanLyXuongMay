using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class SanPham
    {
        private string ma;
        private string ten;
        private string mau;
        private string size;
        private string url;
        private string ghiChu;
        private int tonKho;

        public string Ma { get => ma; set => ma = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Mau { get => mau; set => mau = value; }
        public string Size { get => size; set => size = value; }
        public string Url { get => url; set => url = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public int TonKho { get => tonKho; set => tonKho = value; }

        public SanPham()
        {
            
        }
        public SanPham(DataRow d)
        {
            this.ma = d["MaSP"].ToString();
            this.ten = d["TenSP"].ToString();
            this.mau = d["Mau"].ToString();
            this.size = d["Size"].ToString();
            this.url = d["AnhURL"].ToString();
            this.ghiChu = d["GhiChu"].ToString();
            this.tonKho = (int)d["TonKho"];
        }
    }
}
