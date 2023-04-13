using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class ChiPhiRieng
    {
        private int maCp;
        private string tenCp;
        private string phanLoai;
        private int soTien;
        private DateTime ngayChi;
        private string ghiChu;
        public ChiPhiRieng()
        {

        }
        public ChiPhiRieng(int maCp, string tenCp, string phanLoai, int soTien, DateTime ngayChi, string ghiChu)
        {
            MaCp = maCp;
            TenCp = tenCp;
            PhanLoai = phanLoai;
            SoTien = soTien;
            NgayChi = ngayChi;
            GhiChu = ghiChu;
        }
        public ChiPhiRieng(DataRow d)
        {
            maCp = (int)d["MaCP"];
            tenCp = d["TenCP"].ToString();
            phanLoai = d["PhanLoai"].ToString();
            ghiChu = d["GhiChu"].ToString();
            soTien = (int)d["SoTien"];
            NgayChi = (DateTime)d["NgayChi"];

        }
        public int MaCp { get => maCp; set => maCp = value; }
        public string TenCp { get => tenCp; set => tenCp = value; }
        public string PhanLoai { get => phanLoai; set => phanLoai = value; }
        public int SoTien { get => soTien; set => soTien = value; }
        public DateTime NgayChi { get => ngayChi; set => ngayChi = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
    }
}
