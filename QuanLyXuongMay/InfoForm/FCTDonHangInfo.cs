using QuanLyXuongMay.DAO;
using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyXuongMay.InfoForm
{
    public partial class FCTDonHangInfo : Form
    {
        private CTDonHang ct;
        public FCTDonHangInfo(CTDonHang ct)
        {
            InitializeComponent();
            this.ct = ct;
            if (ct != null)
                loadInfo();
        }
        void loadInfo()
        {
            tbMaCTDH.Text = ct.MaCTDH;
            tbMau.Text = ct.Mau;
            tbSize.Text = ct.Size;
            tbSoLuongGiao.Text = ct.SoLuongGiao+"";
            tbSoLuongDat.Text = ct.SoLuongDat + "";
            tbDonGia.Text = ct.DonGia+" VNĐ";
            tbGhiChu.Text = ct.GhiChu;
            SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(ct.MaSP);
            if (sp == null)
                return;
            tbTenSP.Text = sp.Ten;
            tbMaSP.Text = sp.Ma;
            if (File.Exists(sp.Url))
            {
                ptAnhSP.Image = Image.FromFile(sp.Url);
            }
            else ptAnhSP.Image = null;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ptAnhSP_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
