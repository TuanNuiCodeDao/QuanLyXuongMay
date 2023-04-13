using QuanLyXuongMay.DAO;
using QuanLyXuongMay.DTO;
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
    public partial class FDonHang : Form
    {
        private int dk;
        public FDonHang()
        {
            InitializeComponent();
            load();
            dk = 1;
        }
        void load()
        {
            loadCBXMaKH();
            loadCBXDonHang();
            loadDonHang();
        }
        void loadDonHang()
        {
            dk = 1;
            lvDonHang.Items.Clear();
            List<DonHang> l = DonHangDAO.Instance.loadDS();
            int stt = 0;
            foreach (DonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.MaKH.ToString());
                lv.SubItems.Add(item.NgayDat.ToString());
                lv.SubItems.Add(item.NgayHenGiao.ToString());
                lv.SubItems.Add(item.NgayGiao.ToString());
                lv.SubItems.Add(item.TongTien.ToString());
                lv.SubItems.Add(item.TrangThai.ToString());
                lvDonHang.Items.Add(lv);
            }
            lvDonHang.FullRowSelect = true;
            lvDonHang.Show();
        }
        void loadCBXMaKH()
        {
            List<KhachHang> dsKh = KhachHangDAO.Instance.loadDS();
            cbxMaKH.DataSource = dsKh;
            cbxMaKH.DisplayMember = "Ma";
        }
        void loadCBXDonHang()
        {
            cbxLoadDH.Items.Clear();
            cbxLoadDH.Items.Add("Tất cả");
            cbxLoadDH.Items.Add("Đơn chưa hoành thành");
            cbxLoadDH.Items.Add("Đơn đã hoàn thành");
            cbxLoadDH.Items.Add("Đơn đã hủy");
            cbxLoadDH.Text = cbxLoadDH.Items[0].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM SANPHAM");
            int i = 0;
            foreach (DataRow item in d.Rows)
                i++;
            if (i == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào, hãy thêm sản phẩm trước !", "Thông báo");
                return;
            }
            string maKH = cbxMaKH.Text;
            if(maKH==null||maKH=="")
            {
                MessageBox.Show("Hãy chọn khách hàng trước !", "Nhắc nhở");
                return;
            }
            DateTime ngayDat = dateDat.Value;
            DateTime ngayGiao = dateHenGiao.Value;
            if (ngayGiao < ngayDat.AddDays(-1))
            {
                MessageBox.Show("Ngày giao không được trước ngày đặt !", "Nhắc nhở");
                return;
            }
            DataProvider.Instance.RunQuery("INSERT DONHANG(MaKH,NgayDat,NgayHenGiao) VALUES(N'" + maKH + "',CONVERT(DATE,N'"+ ngayDat.ToString("s")+"'),CONVERT(DATE,N'"+ngayGiao.ToString("s")+"'))");
            loadDonHang();
        }

        private void lvDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lvDonHang.SelectedItems)
            {
                tbMa.Text = items.SubItems[1].Text;
                cbxMaKH.Text = items.SubItems[2].Text;
                dateDat.Value =DateTime.Parse(items.SubItems[3].Text);
                dateHenGiao.Value = DateTime.Parse(items.SubItems[4].Text);
                dateGiao.Text = items.SubItems[5].Text;
                tbTongTien.Text = items.SubItems[6].Text + " VNĐ";
                tbTrangThai.Text = items.SubItems[7].Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvDonHang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn đơn hàng cần xóa !", "Thông báo");
                return;
            }
            ListViewItem items = lvDonHang.SelectedItems[0];
            if (MessageBox.Show("Xác nhận hủy đơn hàng có mã " + items.SubItems[1].Text + "?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("UPDATE DONHANG SET TrangThai=N'Đã hủy' WHERE MaDH=N'" + items.SubItems[1].Text + "'");
                reloadDH();
                MessageBox.Show("Hủy đơn hàng thành công !", "Thông báo");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListViewItem items;
            if (lvDonHang.SelectedItems.Count == 0)
            {
                if (lvDonHang.Items.Count== 0)
                {
                    MessageBox.Show("Danh sách đơn hàng trống !", "Thông báo");
                    return;
                }
                items = lvDonHang.Items[0];
            }
            else items = lvDonHang.SelectedItems[0];
            FCTDonHang cTdh = new FCTDonHang(items.SubItems[1].Text);
            this.Hide();
            cTdh.ShowDialog();
            this.Show();
            load();
        }

        private void cbxMaKH_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FKhachHangInfo iKh = new FKhachHangInfo(cbxMaKH.Text);
                iKh.ShowDialog();
            }
        }
        void loadChuaHT()
        {
            dk = 2;
            lvDonHang.Items.Clear();
            List<DonHang> l = DonHangDAO.Instance.loadDSChuaHT();
            int stt = 0;
            foreach (DonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.MaKH.ToString());
                lv.SubItems.Add(item.NgayDat.ToString());
                lv.SubItems.Add(item.NgayHenGiao.ToString());
                lv.SubItems.Add(item.NgayGiao.ToString());
                lv.SubItems.Add(item.TongTien.ToString());
                lv.SubItems.Add(item.TrangThai.ToString());
                lvDonHang.Items.Add(lv);
            }
            lvDonHang.FullRowSelect = true;
            lvDonHang.Show();
        }
        void loadDaHT()
        {
            dk = 3;
            lvDonHang.Items.Clear();
            List<DonHang> l = DonHangDAO.Instance.loadDSDaHT();
            int stt = 0;
            foreach (DonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.MaKH.ToString());
                lv.SubItems.Add(item.NgayDat.ToString());
                lv.SubItems.Add(item.NgayHenGiao.ToString());
                lv.SubItems.Add(item.NgayGiao.ToString());
                lv.SubItems.Add(item.TongTien.ToString());
                lv.SubItems.Add(item.TrangThai.ToString());
                lvDonHang.Items.Add(lv);
            }
            lvDonHang.FullRowSelect = true;
            lvDonHang.Show();
        }
        void loadDaHuy()
        {
            dk = 4;
            lvDonHang.Items.Clear();
            List<DonHang> l = DonHangDAO.Instance.loadDSDaHuy();
            int stt = 0;
            foreach (DonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.MaKH.ToString());
                lv.SubItems.Add(item.NgayDat.ToString());
                lv.SubItems.Add(item.NgayHenGiao.ToString());
                lv.SubItems.Add(item.NgayGiao.ToString());
                lv.SubItems.Add(item.TongTien.ToString());
                lv.SubItems.Add(item.TrangThai.ToString());
                lvDonHang.Items.Add(lv);
            }
            lvDonHang.FullRowSelect = true;
            lvDonHang.Show();
        }
        void reloadDH()
        {
            if (dk==1)
                loadDonHang();
            if (dk==2)
                loadChuaHT();
            if (dk==3)
                loadDaHT();
            if (dk==4)
                loadDaHuy();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (cbxLoadDH.Text == cbxLoadDH.Items[0].ToString())
                loadDonHang();
            if (cbxLoadDH.Text == cbxLoadDH.Items[1].ToString())
                loadChuaHT();
            if (cbxLoadDH.Text == cbxLoadDH.Items[2].ToString())
                loadDaHT();
            if (cbxLoadDH.Text == cbxLoadDH.Items[3].ToString())
                loadDaHuy();
        }

        private void cbxMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxLoadDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLoadDH.Text == cbxLoadDH.Items[0].ToString())
                loadDonHang();
            if (cbxLoadDH.Text == cbxLoadDH.Items[1].ToString())
                loadChuaHT();
            if (cbxLoadDH.Text == cbxLoadDH.Items[2].ToString())
                loadDaHT();
            if (cbxLoadDH.Text == cbxLoadDH.Items[3].ToString())
                loadDaHuy();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DonHang dh = DonHangDAO.Instance.getDonHangByMa(tbMa.Text);
            if (dh == null)
                return;
            if (dh.TrangThai == "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng chưa hoàn thành !", "Nhắc nhở");
                return;
            }
            if (dh.TrangThai == "Đã hủy")
            {
                MessageBox.Show("Đơn hàng đã hủy !", "Nhắc nhở");
                return;
            }
            FXuatHoaDon fXhd = new FXuatHoaDon(dh.Ma);
            fXhd.ShowDialog();
        }
    }
}
