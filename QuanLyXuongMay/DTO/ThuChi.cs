using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class ThuChi
    {
        private int ma;
        private string phanLoai;
        private string noiDung;
        private DateTime thoiGian;
        private int soTien;
        private string maNoi;
        public ThuChi()
        { }
        public ThuChi(DataRow d)
        {
            ma = (int)d["Ma"];
            phanLoai = d["Loai"].ToString();
            noiDung = d["NoiDung"].ToString();
            MaNoi = d["MaNoi"].ToString();
            thoiGian = (DateTime)d["ThoiGian"];
            soTien = (int)d["SoTien"];
        }
        public int Ma { get => ma; set => ma = value; }
        public string PhanLoai { get => phanLoai; set => phanLoai = value; }
        public string NoiDung { get => noiDung; set => noiDung = value; }
        public DateTime ThoiGian { get => thoiGian; set => thoiGian = value; }
        public int SoTien { get => soTien; set => soTien = value; }
        public string MaNoi { get => maNoi; set => maNoi = value; }
    }
}
