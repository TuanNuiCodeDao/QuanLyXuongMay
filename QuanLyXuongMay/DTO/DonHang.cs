using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DTO
{
    public class DonHang
    {
        private string ma;
        private string maKH;
        private DateTime ngayDat;
        private DateTime ngayHenGiao;
        private string ngayGiao;
        private int tongTien;
        private string trangThai;
        public DonHang()
        {

        }
        public DonHang(DataRow d)
        {
            Ma = d["MaDH"].ToString();
            MaKH = d["MaKH"].ToString();
            NgayDat = (DateTime)d["NgayDat"];
            NgayHenGiao = (DateTime)d["NgayHenGiao"];
            NgayGiao = d["NgayGiao"].ToString();
            TongTien =(int) d["TongTien"];
            TrangThai = d["TrangThai"].ToString();
        }
        public string Ma { get => ma; set => ma = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public DateTime NgayDat { get => ngayDat; set => ngayDat = value; }
        public DateTime NgayHenGiao { get => ngayHenGiao; set => ngayHenGiao = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string NgayGiao { get => ngayGiao; set => ngayGiao = value; }
    }
}
