using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class CTDonHangDAO
    {
        private static CTDonHangDAO instance;
        public static CTDonHangDAO Instance
        {
            get { if (instance == null) instance = new CTDonHangDAO(); return instance; }
            private set { instance = value; }
        }
        private CTDonHangDAO()
        {
        }
        public List<CTDonHang> loadDS()
        {
            List<CTDonHang> ds = new List<CTDonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CT_DONHANG");
            foreach (DataRow item in data.Rows)
            {
                CTDonHang b = new CTDonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<CTDonHang> loadDSByMaDH(string maDh)
        {
            List<CTDonHang> ds = new List<CTDonHang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CT_DONHANG WHERE MaDH=N'"+maDh+"'");
            foreach (DataRow item in data.Rows)
            {
                CTDonHang b = new CTDonHang(item);
                ds.Add(b);
            }
            return ds;
        }
        public CTDonHang getCTDonHangByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CT_DONHANG WHERE MaCTDH =N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                CTDonHang b = new CTDonHang(item);
                return b;
            }
            return null;
        }
        public void xoaCTDon(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  PHANCONG WHERE MaCTDH =N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                PhanCong b = new PhanCong(item);
                PhanCongDAO.Instance.xoaPC(b.MaPC+"");
            }
            DataProvider.Instance.RunQuery("DELETE FROM CT_DonHang WHERE MaCTDH = N'" + ma + "'");
        }
    }
}
