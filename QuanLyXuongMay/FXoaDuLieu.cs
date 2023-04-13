using DocumentFormat.OpenXml.Spreadsheet;
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

namespace QuanLyXuongMay
{
    public partial class FXoaDuLieu : Form
    {
        public FXoaDuLieu()
        {
            InitializeComponent();
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap dn = DangNhapDAO.Instance.getDangNhap();
            if(dn.MatKhau!=tbMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu không chính xác !", "Thông báo");
                return;
            }
            if (MessageBox.Show("Xác nhận reset dữ liệu ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.saoLuuTuDong();
                DataProvider.Instance.resetDuLieu();
                MessageBox.Show("Reset dữ liệu thành công !", "Thông báo");
            }
        }
    }
}
