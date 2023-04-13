using QuanLyXuongMay.DAO;
using QuanLyXuongMay.InfoForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyXuongMay
{
    public partial class FMenu : Form
    {
        public FMenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btDonHang_Click(object sender, EventArgs e)
        {

        }

        private void btSanPham_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FNhanVien nv = new FNhanVien();
            this.Hide();
            nv.ShowDialog();
            this.Show();
        }
        void donHang()
        {
            FDonHang dh = new FDonHang();
            this.Hide();
            dh.ShowDialog();
            this.Show();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            donHang();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FNhanVien nv = new FNhanVien();
            this.Hide();
            nv.ShowDialog();
            this.Show();
        }
        void khachHang()
        {
            FKhachHang kh = new FKhachHang();
            this.Hide();
            kh.ShowDialog();
            this.Show();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            khachHang();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            khachHang();
        }
        void sanPham()
        {
            FSanPham sp = new FSanPham();
            this.Hide();
            sp.ShowDialog();
            this.Show();
        }


        private void label5_Click(object sender, EventArgs e)
        {
            sanPham();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sanPham();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            donHang();
        }
        void runPackUp()
        {
            FBackup fB = new FBackup();
            this.Hide();
            fB.ShowDialog();
            this.Show();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            runPackUp();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            runPackUp();
        }

        void taiKhoan()
        {
            FTaiKhoan fTk = new FTaiKhoan();
            this.Hide();
            fTk.ShowDialog();
            this.Show();
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            taiKhoan();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            taiKhoan();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FPhanCong pc = new FPhanCong();
            this.Hide();
            pc.ShowDialog();
            this.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        void chiPhi()
        {
            FChiPhiVatTu cp = new FChiPhiVatTu();
            this.Hide();
            cp.ShowDialog();
            this.Show();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            chiPhi();
        }
        private void label12_Click(object sender, EventArgs e)
        {
            chiPhi();
        }
        void thongKe()
        {
            FThongKe tk = new FThongKe();
            this.Hide();
            tk.ShowDialog();
            this.Show();
        }    
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            thongKe();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            thongKe();
        }
        void chiPhiRieng()
        {
            FChiPhiRieng cp = new FChiPhiRieng();
            this.Hide();
            cp.ShowDialog();
            this.Show();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            chiPhiRieng();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            chiPhiRieng();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            FXoaDuLieu fx = new FXoaDuLieu();
            this.Hide();
            fx.ShowDialog();
            this.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            FXoaDuLieu fx = new FXoaDuLieu();
            this.Hide();
            fx.ShowDialog();
            this.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
