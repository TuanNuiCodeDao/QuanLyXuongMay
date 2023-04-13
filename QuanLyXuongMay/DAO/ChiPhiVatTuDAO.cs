using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyXuongMay.DAO
{
    public class ChiPhiVatTuDAO
    {
        private static ChiPhiVatTuDAO instance;
        public static ChiPhiVatTuDAO Instance
        {
            get { if (instance == null) instance = new ChiPhiVatTuDAO(); return instance; }
            private set { instance = value; }
        }
        private ChiPhiVatTuDAO()
        {
        }
        public List<ChiPhiVatTu> loadDS()
        {
            List<ChiPhiVatTu> ds = new List<ChiPhiVatTu>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIVATTU");
            foreach (DataRow item in data.Rows)
            {
                ChiPhiVatTu b = new ChiPhiVatTu(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<ChiPhiVatTu> loadDSTheoMaDH(string ma)
        {
            List<ChiPhiVatTu> ds = new List<ChiPhiVatTu>();
            List<CTDonHang> dsCTDH = CTDonHangDAO.Instance.loadDSByMaDH(ma);
            foreach (CTDonHang i in dsCTDH)
            {
                DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIVATTU WHERE MaD=N'"+i.MaCTDH+"'");
                foreach (DataRow item in data.Rows)
                {
                    ChiPhiVatTu b = new ChiPhiVatTu(item);
                    ds.Add(b);
                }
            }
            DataTable data1 = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIVATTU WHERE MaD=N'" + ma + "'");
            foreach (DataRow item in data1.Rows)
            {
                ChiPhiVatTu b = new ChiPhiVatTu(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<ChiPhiVatTu> loadDSTheoMaCTDH(string ma)
        {
            List<ChiPhiVatTu> ds = new List<ChiPhiVatTu>();
            DataTable data1 = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIVATTU WHERE MaD=N'" + ma + "'");
            foreach (DataRow item in data1.Rows)
            {
                ChiPhiVatTu b = new ChiPhiVatTu(item);
                ds.Add(b);
            }
            return ds;
        }
        public List<ChiPhiVatTu> loadDSChungTheoMaDH(string ma)
        {
            List<ChiPhiVatTu> ds = new List<ChiPhiVatTu>();
            DataTable data1 = DataProvider.Instance.RunQuery("SELECT * FROM  CHIPHIVATTU WHERE MaD=N'" + ma + "'");
            foreach (DataRow item in data1.Rows)
            {
                ChiPhiVatTu b = new ChiPhiVatTu(item);
                ds.Add(b);
            }
            return ds;
        }
        public ChiPhiVatTu getChiPhiVatTuByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM  ChiPhi WHERE MaCP ="+ma);
            foreach (DataRow item in data.Rows)
            {
                ChiPhiVatTu b = new ChiPhiVatTu(item);
                return b;
            }
            return null;
        }
        public ChiPhiVatTu getLast()
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT*FROM CHIPHIVATTU WHERE MaCP=(SELECT MAX(MaCP) FROM CHIPHIVATTU)");
            foreach (DataRow item in data.Rows)
            {
                ChiPhiVatTu b = new ChiPhiVatTu(item);
                return b;
            }
            return null;
        }
    }
}
