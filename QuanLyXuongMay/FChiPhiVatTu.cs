using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
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
    public partial class FChiPhiVatTu : Form
    {
        public FChiPhiVatTu()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadCbxTrangThai();
            loadCbxMaDH();
            loadCbxCTDH();
            loadPhanLoai();
            loadDSCPTheoDH();
        }
        void lamTrong()
        {
            tbGhiChu.Text = "";
            tbTenCP.Text = "";
            tbMaCP.Text = "";
            nudSoTien.Value = 0;
        }
        void loadDSCPTheoDH()
        {
            lamTrong();
            lvCP.Items.Clear();
            int stt = 0;
            List<ChiPhiVatTu> l = ChiPhiVatTuDAO.Instance.loadDSTheoMaDH(cbxMaDH.Text);
            foreach (ChiPhiVatTu item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaCP.ToString());
                lv.SubItems.Add(item.TenCP.ToString());
                lv.SubItems.Add(item.MaD.ToString());
                lv.SubItems.Add(item.SoTien.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvCP.Items.Add(lv);
            }
            lvCP.FullRowSelect = true;
            lvCP.Show();
        }
        void loadDSCPTheoCTDH()
        {
            lamTrong();
            lvCP.Items.Clear();
            int stt = 0;
            List<ChiPhiVatTu> l = ChiPhiVatTuDAO.Instance.loadDSTheoMaCTDH(cbxMaCTDH.Text);
            foreach (ChiPhiVatTu item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaCP.ToString());
                lv.SubItems.Add(item.TenCP.ToString());
                lv.SubItems.Add(item.MaD.ToString());
                lv.SubItems.Add(item.SoTien.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvCP.Items.Add(lv);
            }
            lvCP.FullRowSelect = true;
            lvCP.Show();
        }
        void loadPhanLoai()
        {
            cbxPhanLoai.Items.Clear();
            cbxPhanLoai.Items.Add("Đơn hàng");
            cbxPhanLoai.Items.Add("Phân đơn");
            cbxPhanLoai.Text = cbxPhanLoai.Items[0].ToString();
        }
        void loadCbxTrangThai()
        {
            cbxTrangThai.Items.Clear();
            cbxTrangThai.Items.Add("Tất cả");
            cbxTrangThai.Items.Add("Đơn chưa hoành thành");
            cbxTrangThai.Items.Add("Đơn đã hoàn thành");
            cbxTrangThai.Items.Add("Đơn đã hủy");
            cbxTrangThai.Text = cbxTrangThai.Items[0].ToString();
        }
        void loadCbxMaDH()
        {
            List<DonHang> l=new List<DonHang>();
            if (cbxTrangThai.Text == "Tất cả")
                l = DonHangDAO.Instance.loadDS();
            if (cbxTrangThai.Text == "Đơn chưa hoành thành")
                l = DonHangDAO.Instance.loadDSChuaHT();
            if (cbxTrangThai.Text == "Đơn đã hoàn thành")
                l = DonHangDAO.Instance.loadDSDaHT();
            if (cbxTrangThai.Text == "Đơn đã hủy")
                l = DonHangDAO.Instance.loadDSDaHuy();
            cbxMaDH.DataSource = l;
            cbxMaDH.DisplayMember = "Ma";
            if (l.Count > 0)
                cbxMaDH.Text = l[0].Ma;
        }
        void loadCbxCTDH()
        {
            List<CTDonHang> l = CTDonHangDAO.Instance.loadDSByMaDH(cbxMaDH.Text);
            cbxMaCTDH.DataSource = l;
            cbxMaCTDH.DisplayMember = "MaCTDH";
        }    
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            lamTrong();
            foreach (ListViewItem items in lvCP.SelectedItems)
            {
                tbMaCP.Text = items.SubItems[1].Text;
                tbTenCP.Text = items.SubItems[2].Text;
                cbxPhanLoai.Text= items.SubItems[5].Text;
                if(cbxPhanLoai.Text=="Phân đơn")
                {
                    cbxMaCTDH.Text = items.SubItems[3].Text;
                }    
                nudSoTien.Value = int.Parse(items.SubItems[4].Text);
                tbGhiChu.Text = items.SubItems[6].Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadCbxMaDH();
        }

        private void cbxTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCbxMaDH();
            loadCbxCTDH();
            loadDSCPTheoDH();
        }

        private void cbxMaDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCbxCTDH();
            loadDSCPTheoDH();
        }

        private void cbxMaCTDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDSCPTheoCTDH();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DonHang dh = null;
            CTDonHang ct = null;
            dh = DonHangDAO.Instance.getDonHangByMa(cbxMaDH.Text);
            if (cbxPhanLoai.Text == "Phân đơn")
                ct = CTDonHangDAO.Instance.getCTDonHangByMa(cbxMaCTDH.Text);
            if(dh==null)
            {
                MessageBox.Show("Hãy chọn đơn hàng hoặc phân đơn cần thêm chi phí !", "Nhắc nhở");
                return;
            }
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            if (cbxPhanLoai.Text == "Phân đơn"&&ct==null)
            {
                MessageBox.Show("Đơn hàng chưa có phân đơn nào !", "Nhắc nhở");
                return;
            }
            string tenCP = tbTenCP.Text;
            string ghiChu = tbGhiChu.Text;
            int soTien = (int)nudSoTien.Value;
            string maD = cbxMaDH.Text;
            if (string.IsNullOrEmpty(tenCP))
            {
                MessageBox.Show("Tên chi phí không được để trống !", "Nhắc nhở");
                return;
            }
            if (cbxPhanLoai.Text == "Phân đơn")
                maD = cbxMaCTDH.Text;
            DataProvider.Instance.RunQuery("INSERT dbo.CHIPHIVATTU(MaD,TenCP,PhanLoai,SoTien,GhiChu) VALUES(N'" + maD + "',N'" + tenCP + "',N'" + cbxPhanLoai.Text + "'," + soTien + ",N'" + ghiChu + "')");
            ChiPhiVatTu cp = ChiPhiVatTuDAO.Instance.getLast();
            DataProvider.Instance.RunQuery("INSERT dbo.THUCHI(Loai,NoiDung,SoTien,MaNoi) VALUES(N'Chi',N'Chi phí đơn hàng - " + cp.TenCP + " - " + cp.MaCP + "'," + soTien + ",N'CPVT" + cp.MaCP + "')");
            MessageBox.Show("Thêm chi phí đơn hàng thành công", "Thông báo");
            loadDSCPTheoDH();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbMaCP.Text))
            {
                MessageBox.Show("Hãy chọn phần chi phí cần xóa !", "Nhắc nhở");
                return;
            }    
            DonHang dh = null;
            if (cbxPhanLoai.Text == "Đơn hàng")
                dh = DonHangDAO.Instance.getDonHangByMa(cbxMaDH.Text);
            CTDonHang ct = null;
            if (cbxPhanLoai.Text == "Phân đơn")
                ct = CTDonHangDAO.Instance.getCTDonHangByMa(cbxMaCTDH.Text);
            if (dh == null)
                dh = DonHangDAO.Instance.getDonHangByMa(ct.MaDH);
            if (ct == null && dh == null)
            {
                MessageBox.Show("Hãy chọn đơn hàng hoặc phân đơn trước !", "Nhắc nhở");
                return;
            }
            if (dh.TrangThai == "Đã hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành !", "Nhắc nhở");
                return;
            }
            if (MessageBox.Show("Xác nhận xóa chi phí đơn hàng ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("DELETE FROM dbo.CHIPHIVATTU WHERE MaCP="+tbMaCP.Text);
                DataProvider.Instance.RunQuery("DELETE FROM dbo.THUCHI WHERE MaNoi=N'CPVT" + tbMaCP.Text + "'");
                MessageBox.Show("Xóa chi phí đơn hàng thành công", "Thông báo");
                loadDSCPTheoDH();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(tbMaCP.Text))
            {
                MessageBox.Show("Hãy chọn phần chi phí cần cập nhật !", "Nhắc nhở");
                return;
            }
            DonHang dh = null;
            if (cbxPhanLoai.Text == "Đơn hàng")
                dh = DonHangDAO.Instance.getDonHangByMa(cbxMaDH.Text);
            CTDonHang ct = null;
            if (cbxPhanLoai.Text == "Phân đơn")
                ct = CTDonHangDAO.Instance.getCTDonHangByMa(cbxMaCTDH.Text);
            if (dh == null)
                dh = DonHangDAO.Instance.getDonHangByMa(ct.MaDH);
            if (ct == null && dh == null)
            {
                MessageBox.Show("Hãy chọn đơn hàng hoặc phân đơn trước !", "Nhắc nhở");
                return;
            }
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            if (cbxPhanLoai.Text == "Phân đơn" && ct == null)
            {
                MessageBox.Show("Đơn hàng chưa có phân đơn nào !", "Nhắc nhở");
                return;
            }
            string tenCP = tbTenCP.Text;
            string ghiChu = tbGhiChu.Text;
            int soTien = (int)nudSoTien.Value;
            string maD = cbxMaDH.Text;
            if (string.IsNullOrEmpty(tenCP))
            {
                MessageBox.Show("Tên chi phí không được để trống !", "Nhắc nhở");
                return;
            }
            if (cbxPhanLoai.Text == "Phân đơn")
                maD = cbxMaCTDH.Text;
            DataProvider.Instance.RunQuery("UPDATE CHIPHIVATTU SET MaD = N'" + maD + "',TenCP = N'" + tenCP + "',PhanLoai = N'" + cbxPhanLoai.Text + "',SoTien="+soTien+",GhiChu=N'"+ghiChu+"' WHERE MaCP="+tbMaCP.Text);
            DataProvider.Instance.RunQuery("UPDATE THUCHI SET SoTien = " + soTien + "WHERE MaNoi=N'CPVT" + tbMaCP.Text + "'");
            MessageBox.Show("Cập nhật chi phí đơn hàng thành công", "Thông báo");
            loadDSCPTheoDH();
        }

        private void cbxMaCTDH_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CTDonHang ct = CTDonHangDAO.Instance.getCTDonHangByMa(cbxMaCTDH.Text);
                if (ct == null)
                    return;
                FCTDonHangInfo iCT = new FCTDonHangInfo(ct);
                iCT.ShowDialog();
            }
        }
    }
}
