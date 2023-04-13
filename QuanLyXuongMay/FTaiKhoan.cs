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

namespace QuanLyXuongMay
{
    public partial class FTaiKhoan : Form
    {
        private DangNhap dn;
        public FTaiKhoan()
        {
            InitializeComponent();
            LoadTT();
        }
        void LoadTT()
        {
            dn = DangNhapDAO.Instance.getDangNhap();
            if (dn == null)
                return;
            tbTaiKhoan.Text = dn.TaiKhoan;
            tbMatKhau.Text = dn.MatKhau;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMatKhau.Text) || string.IsNullOrEmpty(tbTaiKhoan.Text))
            {
                MessageBox.Show("Tài khoản và mật khẩu không được để trống !", "Thông báo");
                return;
            }
            DataProvider.Instance.RunQuery("UPDATE dbo.DANGNHAP SET TaiKhoan=N'" + tbTaiKhoan.Text + "',MatKhau=N'" + tbMatKhau.Text + "' WHERE TaiKhoan=N'" + dn.TaiKhoan + "'");
            dn = DangNhapDAO.Instance.getDangNhap();
            MessageBox.Show("Câp nhật tài khoản thành công", "Thông báo");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
