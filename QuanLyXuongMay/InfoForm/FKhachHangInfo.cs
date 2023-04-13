using QuanLyXuongMay.DAO;
using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyXuongMay.InfoForm
{
    public partial class FKhachHangInfo : Form
    {
        public FKhachHangInfo(string ma)
        {
            InitializeComponent();
            loadInfo(ma);
        }
        void loadInfo(string ma)
        {
            KhachHang kh = KhachHangDAO.Instance.getKhachHangByMa(ma);
            if (kh == null)
                return;
            tbDiaChi.Text = kh.DiaChi;
            tbHoTen.Text = kh.HoTen;
            tbMa.Text = kh.Ma;
            tbSDT.Text = kh.Sdt;
        }
    }
}
