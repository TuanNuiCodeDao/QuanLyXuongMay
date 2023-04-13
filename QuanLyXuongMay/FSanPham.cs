using QuanLyXuongMay.DAO;
using QuanLyXuongMay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuanLyXuongMay
{
    public partial class FSanPham : Form
    {
        public FSanPham()
        {
            InitializeComponent();
            loadInfo();
        }
        void loadInfo()
        {
            loadSP();
        }
        void loadSP()
        {
            lvSanPham.Items.Clear();
            List<SanPham> l = SanPhamDAO.Instance.loadDS();
            int stt = 0;
            foreach (SanPham item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.Ten.ToString());
                lv.SubItems.Add(item.Size.ToString());
                lv.SubItems.Add(item.Mau.ToString());
                lv.SubItems.Add(item.TonKho.ToString());
                lv.SubItems.Add(item.Url.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvSanPham.Items.Add(lv);
            }

            lvSanPham.FullRowSelect = true;
            lvSanPham.Show();
        }

        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lvSanPham.SelectedItems)
            {
                tbMa.Text = items.SubItems[1].Text;
                tbTenSP.Text = items.SubItems[2].Text;
                tbSize.Text = items.SubItems[3].Text;
                tbMau.Text = items.SubItems[4].Text;
                tbTonKho.Text= items.SubItems[5].Text;
                tbUrl.Text = items.SubItems[6].Text;
                tbGhiChu.Text= items.SubItems[7].Text;
                if (File.Exists(tbUrl.Text))
                {
                    Image img = Image.FromFile(tbUrl.Text);
                    ptAnh.Image = img;
                }
                else ptAnh.Image = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn ảnh";
            openFileDialog1.Filter = "Windows Bitmap|*.png|JPEG Image|*.jpg|All Files|*.*";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbUrl.Text = openFileDialog1.FileName;
                String filePath = tbUrl.Text;
                if (File.Exists(filePath))
                {
                    ptAnh.Image = Image.FromFile(filePath);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(tbMa.Text);
            MessageBox.Show(sp.GhiChu, "Ghi chú");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tenSP = tbTenSP.Text;
            string size = tbSize.Text;
            string mau = tbMau.Text;
            string ghiChu = tbGhiChu.Text;
            string url = tbUrl.Text;
            if (ghiChu == null || ghiChu == ""|| ghiChu == " ")
                ghiChu = "Empty";
            if (File.Exists(url) == false)
                url = "Empty";
            if (size == null || size == "")
                size = "Empty";
            if (mau == null || mau == "")
                mau = "Empty";
            DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM SANPHAM WHERE TenSP=N'" + tenSP + "'");
            int i = 0;
            foreach (DataRow item in d.Rows)
                i++;
            if (i > 0)
            {
                    MessageBox.Show("Tên sản phẩm '" + tenSP + "' đã được sử dụng !", "Thông báo");
                    return;
            }
            if (tenSP != null && tenSP != ""&& size!="Empty"&& mau != "Empty")
            {
                DataProvider.Instance.RunQuery("INSERT dbo.SANPHAM(TenSP,AnhURL,Mau,Size,GhiChu) VALUES(N'" + tenSP + "',N'" + url + "',N'" + mau + "',N'" + size + "',N'" + ghiChu + "')");
                MessageBox.Show("Thêm sản phẩm thành công !", "Thông báo");
                loadSP();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin !", "Thông báo");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn sản phẩm cần xóa !", "Thông báo");
                return;
            }
            ListViewItem items = lvSanPham.SelectedItems[0];
            if (MessageBox.Show("Xác nhận xóa sản phẩm có mã " + items.SubItems[1].Text + "?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("DELETE FROM SANPHAM WHERE MaSP = N'" + items.SubItems[1].Text + "'");
                loadSP();
                MessageBox.Show("Xóa sản phẩm thành công !", "Thông báo");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn sản phẩm cần cập nhật !", "Thông báo");
                return;
            }
            string tenSP = tbTenSP.Text;
            string size = tbSize.Text;
            string mau = tbMau.Text;
            string ghiChu = tbGhiChu.Text;
            string url = tbUrl.Text;
            if (ghiChu == null || ghiChu == "" || ghiChu == " ")
                ghiChu = "Empty";
            if (File.Exists(url) == false)
                url = "Empty";
            if (size == null || size == "")
                size = "Empty";
            if (mau == null || mau == "")
                mau = "Empty";
            DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM SANPHAM WHERE TenSP=N'" + tenSP + "' AND MaSP!=N'"+tbMa.Text+"'");
            int i = 0;
            foreach (DataRow item in d.Rows)
                i++;
            if (i > 0)
            {
                MessageBox.Show("Tên sản phẩm '" + tenSP + "' đã được sử dụng ở sản phẩm khác !", "Thông báo");
                return;
            }
            if (tenSP != null && tenSP != "" && size != "Empty" && mau != "Empty")
            {
                DataProvider.Instance.RunQuery("UPDATE dbo.SANPHAM SET TenSP=N'" + tenSP + "',AnhURL=N'" + url + "',Mau=N'" + mau + "',Size=N'" + size + "',GhiChu=N'" + ghiChu + "' WHERE MaSP=N'"+tbMa.Text+"'");
                MessageBox.Show("Cập nhật sản phẩm thành công !", "Thông báo");
                loadSP();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin !", "Thông báo");
        }

        private void tbTim_TextChanged(object sender, EventArgs e)
        {
            if (tbTim.Text == " " || tbTim.Text == "All" || tbTim.Text == "ALL" || tbTim.Text == "all" || tbTim.Text == "")
            {
                loadSP();
                return;
            }
            lvSanPham.Items.Clear();
            List<SanPham> l = SanPhamDAO.Instance.loadDSTim(tbTim.Text);
            int stt = 0;
            foreach (SanPham item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.Ten.ToString());
                lv.SubItems.Add(item.Size.ToString());
                lv.SubItems.Add(item.Mau.ToString());
                lv.SubItems.Add(item.TonKho.ToString());
                lv.SubItems.Add(item.Url.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvSanPham.Items.Add(lv);
            }
            lvSanPham.FullRowSelect = true;
            lvSanPham.Show();
        }
    }
}
