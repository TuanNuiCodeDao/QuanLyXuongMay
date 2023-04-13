using QuanLyXuongMay.DAO;
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

namespace QuanLyXuongMay
{
    public partial class FDangNhap : Form
    {
        public FDangNhap()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.saoLuuTuDong();
            Application.Exit();
        }

        private void FDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Xác nhận thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            if (DangNhapDAO.Instance.getDangNhap()== null)
            {
                MessageBox.Show("Hãy tạo tài khoản trước !", "Thông báo");
                return;
            }
            if (DangNhapDAO.Instance.ktrDangNhap(tbTaiKhoan.Text,tbMatKhau.Text) == false)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác !", "Thông báo");
                return;
            }
            FMenu fc = new FMenu();
            this.Hide();
            fc.ShowDialog();
            this.Show();
            DataProvider.Instance.saoLuuTuDong();
            tbMatKhau.Text = "";
            tbTaiKhoan.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DangNhapDAO.Instance.getDangNhap() != null)
            {
                MessageBox.Show("Đã tạo tài khoản trước đây !", "Thông báo");
                return;
            }
            if (string.IsNullOrEmpty(tbMatKhau.Text) || string.IsNullOrEmpty(tbTaiKhoan.Text))
            {
                MessageBox.Show("Tài khoản và mật khẩu không được để trống !", "Thông báo");
                return;
            }
            DataProvider.Instance.RunQuery("INSERT dbo.DANGNHAP(TaiKhoan,MatKhau) VALUES(N'" + tbTaiKhoan.Text + "',N'" + tbMatKhau.Text + "')");
            MessageBox.Show("Tạo tài khoản thành công\nTài khoản : "+tbTaiKhoan.Text+"\nMật khẩu : "+tbMatKhau.Text, "Thông báo");
        }
    }
}
