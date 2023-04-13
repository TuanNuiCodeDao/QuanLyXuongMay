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
    public partial class FKhachHang : Form
    {
        public FKhachHang()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadKH();
        }
        void loadKH()
        {
            lvKhachHang.Items.Clear();
            List<KhachHang> l = KhachHangDAO.Instance.loadDS();
            int stt = 0;
            foreach (KhachHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.HoTen.ToString());
                lv.SubItems.Add(item.Sdt.ToString());
                lv.SubItems.Add(item.DiaChi.ToString());
                lvKhachHang.Items.Add(lv);
            }
            lvKhachHang.FullRowSelect = true;
            lvKhachHang.Show();
        }

        bool ktrSDT(string sdt)
        {
            for (int i = 0; i < sdt.Length; i++)
                if (sdt[i] < '0' || sdt[i] > '9')
                    return false;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hoTen = tbHoTen.Text;
            string sdt = tbSDT.Text;
            string diaChi = tbDiaChi.Text;
            if (diaChi == null || diaChi == "")
                diaChi = "Empty";
            if (ktrSDT(sdt) == false)
                sdt = "Empty";
            if (sdt != "Empty")
            {
                DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM KHACHHANG WHERE SDT=N'" + sdt + "'");
                int i = 0;
                foreach (DataRow item in d.Rows)
                    i++;
                if (i > 0)
                {
                    MessageBox.Show("Số điện thoại " + sdt + " đã được khách hàng khác sử dụng !", "Thông báo");
                    return;
                }
            }
            if (hoTen != null && hoTen != "")
            {
                DataProvider.Instance.RunQuery("INSERT dbo.KHACHHANG(TenKH,SDT,DiaChi) VALUES(N'" + hoTen + "',N'" + sdt + "',N'" + diaChi + "')");
                loadKH();
            }
            else MessageBox.Show("Vui lòng nhập đủ họ tên !", "Thông báo");
        }

        private void lvKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem items in lvKhachHang.SelectedItems)
            {
                tbMa.Text = items.SubItems[1].Text;
                tbHoTen.Text = items.SubItems[2].Text;
                tbSDT.Text = items.SubItems[3].Text;
                tbDiaChi.Text = items.SubItems[4].Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvKhachHang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn khách hàng cần xóa !", "Thông báo");
                return;
            }
            ListViewItem items = lvKhachHang.SelectedItems[0];
            if (MessageBox.Show("Xác nhận xóa khách hàng có mã " + items.SubItems[1].Text + "?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("DELETE FROM KHACHHANG WHERE MaKH = N'" + items.SubItems[1].Text + "'");
                loadKH();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ma = tbMa.Text;
            if (ma == null || ma == "")
            {
                MessageBox.Show("Hãy chọn khách hàng trước !", "Thông báo");
                return;
            }
            string hoTen = tbHoTen.Text;
            string sdt = tbSDT.Text;
            string diaChi = tbDiaChi.Text;
            if (diaChi == null || diaChi == "")
                diaChi = "Empty";
            if (ktrSDT(sdt) == false)
                sdt = "Empty";
            if (sdt != "Empty")
            {
                DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM KHACHHANG WHERE SDT=N'" + sdt + "' AND MaKH!=N'" + ma + "'");
                int i = 0;
                foreach (DataRow item in d.Rows)
                    i++;
                if (i > 0)
                {
                    MessageBox.Show("Số điện thoại " + sdt + " đã được khách hàng khác sử dụng !", "Thông báo");
                    return;
                }
            }
            if (hoTen != null && hoTen != "")
            {
                DataProvider.Instance.RunQuery("UPDATE KHACHHANG SET TenKH=N'" + hoTen + "',SDT=N'" + sdt + "',DiaChi=N'" + diaChi + "' WHERE MaKH=N'" + ma + "'");
                loadKH();
            }
            else MessageBox.Show("Vui lòng nhập đủ họ tên !", "Thông báo");
        }
        private void tbTim_TextChanged(object sender, EventArgs e)
        {
            if (tbTim.Text == " " || tbTim.Text == "All" || tbTim.Text == "ALL" || tbTim.Text == "all" || tbTim.Text == "")
            {
                loadKH();
                return;
            }
            lvKhachHang.Items.Clear();
            List<KhachHang> l = KhachHangDAO.Instance.loadDSTim(tbTim.Text);
            int stt = 0;
            foreach (KhachHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.HoTen.ToString());
                lv.SubItems.Add(item.Sdt.ToString());
                lv.SubItems.Add(item.DiaChi.ToString());
                lvKhachHang.Items.Add(lv);
            }
            lvKhachHang.FullRowSelect = true;
            lvKhachHang.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
