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
    public partial class FSanPhamInfo : Form
    {
        public FSanPhamInfo(string ma)
        {
            InitializeComponent();
            loadInfo(ma);
        }
        void loadInfo(string ma)
        {
            SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(ma);
            if (sp == null)
                return;
            tbGhiChu.Text = sp.GhiChu;
            tbMa.Text = sp.Ma;
            tbMau.Text = sp.Mau;
            tbSize.Text = sp.Size;
            tbTenSP.Text = sp.Ten;
            tbTonKho.Text = sp.TonKho+"";
            if (File.Exists(sp.Url))
            {
                ptAnhSP.Image = Image.FromFile(sp.Url);
            }
            else ptAnhSP.Image = null;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
