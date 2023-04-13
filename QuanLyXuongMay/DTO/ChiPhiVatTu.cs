using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class ChiPhiVatTu
    {
        private int maCP;
        private string tenCP;
        private string maD;
        private string phanLoai;
        private int soTien;
        private string ghiChu;
        public ChiPhiVatTu(DataRow d)
        {
            maCP = (int)d["MaCP"];
            tenCP = d["TenCP"].ToString();
            maD = d["MaD"].ToString();
            ghiChu = d["GhiChu"].ToString();
            phanLoai = d["PhanLoai"].ToString();
            soTien = (int)d["SoTien"];
        }
        public int MaCP { get => maCP; set => maCP = value; }
        public string TenCP { get => tenCP; set => tenCP = value; }
        public string MaD { get => maD; set => maD = value; }
        public string PhanLoai { get => phanLoai; set => phanLoai = value; }
        public int SoTien { get => soTien; set => soTien = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
    }
}
