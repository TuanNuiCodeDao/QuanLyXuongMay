using QuanLyXuongMay.DAO;
using QuanLyXuongMay.DTO;
using QuanLyXuongMay.InfoForm;
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
    public partial class FCTDonHang : Form
    {
        private DonHang dh;
        public FCTDonHang(string maDH)
        {
            InitializeComponent();
            dh = DonHangDAO.Instance.getDonHangByMa(maDH);
            loadInfo();
        }
        void loadInfo()
        {
            loadCTDonHang();
            loadCbxMaSP();
            loadCbxTenSP();
        }
        void loadCbxMaSP()
        {
            List<SanPham> dsSp = SanPhamDAO.Instance.loadDS();
            cbxMaSP.DataSource = dsSp;
            cbxMaSP.DisplayMember = "Ma";
            
        }
        void loadCbxTenSP()
        {
            List<SanPham> dsSp = SanPhamDAO.Instance.loadDS();
            cbxTenSP.DataSource = dsSp;
            cbxTenSP.DisplayMember = "Ten";

        }
        void loadCbxMauSize()
        {
            SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(cbxMaSP.Text);
            cbxMau.Items.Clear();
            cbxSize.Items.Clear();
            if(sp!=null)
            {
                string[] aMau = sp.Mau.Split(new char[] { ',' });
                string[] aSize = sp.Size.Split(new char[] { ',' });
                for (int i = 0; i < aMau.Length; i++)
                    cbxMau.Items.Add(aMau[i]);
                for (int i = 0; i < aSize.Length; i++)
                    cbxSize.Items.Add(aSize[i]);
                cbxMau.Text = aMau[0];
                cbxSize.Text = aSize[0];
            }    
        }
        void loadCTDonHang()
        {
            lvCTDonHang.Items.Clear();
            List<CTDonHang> l = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
            int stt = 0;
            foreach (CTDonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaCTDH.ToString());
                lv.SubItems.Add(item.MaSP.ToString());
                lv.SubItems.Add(item.Mau.ToString());
                lv.SubItems.Add(item.Size.ToString());
                lv.SubItems.Add(item.DonGia.ToString());
                lv.SubItems.Add(item.SoLuongDat.ToString());
                lv.SubItems.Add(item.SoLuongGiao.ToString());
                lvCTDonHang.Items.Add(lv);
            }
            lvCTDonHang.FullRowSelect = true;
            lvCTDonHang.Show();
        }
        private void lvDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lvCTDonHang.SelectedItems)
            {
                tbMaCTDH.Text = items.SubItems[1].Text;
                CTDonHang ct = CTDonHangDAO.Instance.getCTDonHangByMa(tbMaCTDH.Text);
                tbGhiChu.Text = ct.GhiChu;
                cbxMaSP.Text = items.SubItems[2].Text;
                cbxMau.Text = items.SubItems[3].Text;
                cbxSize.Text = items.SubItems[4].Text;
                tbDonGia.Text = String.Format("{0:###,###,##0}",int.Parse(items.SubItems[5].Text)) ;
                nudSoLuongDat.Text = items.SubItems[6].Text;
                tbSoLuongGiao.Text= items.SubItems[7].Text;
                SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(cbxMaSP.Text);
                if (File.Exists(sp.Url))
                {
                    ptAnhSP.Image = Image.FromFile(sp.Url);
                }
                else ptAnhSP.Image = null;
                cbxTenSP.Text = sp.Ten;
            }
        }

        private void cbxMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maSP = cbxMaSP.Text;
            SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(maSP);
            cbxTenSP.Text = "";
            ptAnhSP.Image = null;
            if (sp != null)
            {
                cbxTenSP.Text = sp.Ten;
                if (File.Exists(sp.Url))
                {
                    ptAnhSP.Image = Image.FromFile(sp.Url);
                }
                else ptAnhSP.Image = null;
            }
            loadCbxMauSize();
            tbMaCTDH.Clear();
            tbGhiChu.Clear();
            tbDonGia.Clear();
            nudSoLuongDat.Value = 0;
            tbSoLuongGiao.Clear();
        }
        void tinhTien()
        {
            List<CTDonHang> l = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
            int tong = 0;
            foreach(CTDonHang ct in l)
            {
                tong += ct.DonGia * ct.SoLuongDat;
            }    
            DataProvider.Instance.RunQuery("UPDATE DONHANG SET TongTien=" + tong + " WHERE MaDH=N'" + dh.Ma + "'");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            string maSP = cbxMaSP.Text;
            if(maSP==null||maSP=="")
            {
                MessageBox.Show("Hãy chọn sản phẩm trước", "Nhắc nhở");
                return;
            }
            string mau = cbxMau.Text;
            string size = cbxSize.Text;
            int soLuongDat = (int)nudSoLuongDat.Value;
            if (soLuongDat == 0)
                return;
            DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM CT_DONHANG WHERE MaSP=N'" + maSP + "' AND MaDH=N'" + dh.Ma + "' AND Mau=N'" + mau + "' AND Size=N'" + size + "'");
            CTDonHang ct = null;
            foreach (DataRow item in d.Rows)
            {
                ct = new CTDonHang(item);
            }
            if (ct != null)
            {
                int soLuongNew = ct.SoLuongDat + soLuongDat;
                DataProvider.Instance.RunQuery("UPDATE CT_DONHANG SET SoLuongDat=" + soLuongNew + " WHERE MaCTDH=N'" + ct.MaCTDH + "'");
            }
            else
            {
                if (string.IsNullOrEmpty(tbGhiChu.Text))
                    tbGhiChu.Text = "Empty";
                DataProvider.Instance.RunQuery("INSERT CT_DONHANG(MaDH,MaSP,Mau,Size,SoLuongDat,GhiChu) VALUES(N'" + dh.Ma + "',N'" + maSP + "',N'" + mau + "',N'" + size + "'," + soLuongDat + ",N'"+tbGhiChu.Text+"')");
            }
                tinhTien();
            loadCTDonHang();
            MessageBox.Show("Thêm phân đơn thành công", "Thông báo");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tbGhiChu.Text, "Ghi chú");
        }
        private void cbxMaSP_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        private void cbxTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenSP = cbxTenSP.Text;
            SanPham sp = SanPhamDAO.Instance.getSanPhamByTen(tenSP);
            cbxMaSP.Text = "";
            ptAnhSP.Image = null;
            if (sp != null)
            {
                cbxMaSP.Text = sp.Ma;
                if (File.Exists(sp.Url))
                {
                    ptAnhSP.Image = Image.FromFile(sp.Url);
                }
                else ptAnhSP.Image = null;
            }
            loadCbxMauSize();
            tbMaCTDH.Clear();
            tbGhiChu.Clear();
            tbDonGia.Clear();
            nudSoLuongDat.Value = 0;
            tbSoLuongGiao.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            if (lvCTDonHang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn phân đơn cần hủy !", "Thông báo");
                return;
            }
            ListViewItem items = lvCTDonHang.SelectedItems[0];
            if (MessageBox.Show("Xác nhận hủy phân đơn có mã " + items.SubItems[1].Text + "?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("DELETE FROM CT_DONHANG WHERE MaCTDH = N'" + items.SubItems[1].Text + "'");
                tinhTien();
                loadCTDonHang();
                MessageBox.Show("Xóa phân đơn thành công !", "Thông báo");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }
        private void cbxMaSP_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FSanPhamInfo iSp = new FSanPhamInfo(cbxMaSP.Text);
                iSp.ShowDialog();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            FHoaDon hd = new FHoaDon(dh);
            this.Hide();
            hd.ShowDialog();
            this.Show();
            dh = DonHangDAO.Instance.getDonHangByMa(dh.Ma);
            loadInfo();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            CTDonHang ct = CTDonHangDAO.Instance.getCTDonHangByMa(tbMaCTDH.Text);
            if (ct == null)
            {
                MessageBox.Show("Hãy chọn phân đơn cần thêm ghi chú !", "Thông báo");
                return;
            }
            if (string.IsNullOrEmpty(tbGhiChu.Text))
                tbGhiChu.Text = "Empty";
            DataProvider.Instance.RunQuery("UPDATE CT_DONHANG SET GhiChu=N'" +tbGhiChu.Text+ "' WHERE MaCTDH=N'" + ct.MaCTDH + "'");
            MessageBox.Show("Thêm thành công", "Thông báo");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            tbGhiChu.Text = "";
        }

        private void tbDonGia_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
