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
    public partial class FXuatHoaDon : Form
    {
        private DonHang dh;
        public FXuatHoaDon(string maDh)
        {
            InitializeComponent();
            dh = DonHangDAO.Instance.getDonHangByMa(maDh);
            loadTT();
        }
        void loadTT()
        {
            KhachHang kh = KhachHangDAO.Instance.getKhachHangByMa(dh.MaKH);
            tbDiaChi.Text = kh.DiaChi;
            tbMaDH.Text = dh.Ma;
            tbMaKH.Text = kh.Ma;
            tbHoTen.Text = kh.HoTen;
            tbSDT.Text = kh.Sdt;
            tbTongCong.Text= String.Format("{0:###,###,##0}",dh.TongTien) + " VNĐ";
            dateDat.Text = dh.NgayDat + "";
            dateHenGiao.Text = dh.NgayHenGiao + ""; 
            dateGiao.Text = dh.NgayGiao + "";
            List<CTDonHang> l = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
            lvCTDonHang.Items.Clear();
            int stt = 0;
            foreach (CTDonHang i in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(i.MaSP.ToString());
                SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(i.MaSP);
                lv.SubItems.Add(sp.Ten.ToString());
                lv.SubItems.Add(i.Mau.ToString());
                lv.SubItems.Add(i.Size.ToString());
                lv.SubItems.Add(String.Format("{0:###,###,##0}", i.DonGia) + " VNĐ");
                lv.SubItems.Add(i.SoLuongGiao.ToString());
                int tien = i.DonGia * i.SoLuongGiao;
                lv.SubItems.Add(String.Format("{0:###,###,##0}", tien) + " VNĐ");
                lvCTDonHang.Items.Add(lv);
            }
            lvCTDonHang.FullRowSelect = true;
            lvCTDonHang.Show();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateDat_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
