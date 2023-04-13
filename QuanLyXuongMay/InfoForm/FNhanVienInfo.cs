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
    public partial class FNhanVienInfo : Form
    {
        private NhanVien nv;
        public FNhanVienInfo(NhanVien n)
        {
            InitializeComponent();
            nv = n;
            loadInfo();
        }
        void loadInfo()
        {
            tbHoTen.Text = nv.HoTen;
            tbMa.Text = nv.Ma;
            tbLuong.Text = String.Format("{0:###,###,##0}",nv.Luong) + " VNĐ";
            tbPhanLoai.Text = nv.PhanLoai;
            tbToDoi.Text = nv.ToDoi;
            tbSDT.Text = nv.Sdt;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMa_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
