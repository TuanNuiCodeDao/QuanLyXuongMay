using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.CustomUI;
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
    public partial class FPhanCong : Form
    {
        public FPhanCong()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadPC();
            loadCbxMaTho();
            loadCbxSP();
            loadCbxTrangThai();
        }
        void loadCbxSP()
        {
            List<SanPham> dsSp = SanPhamDAO.Instance.loadDS();
            cbxMaSP.DataSource = dsSp;
            cbxMaSP.DisplayMember = "Ma";
            cbxTenSP.DataSource = dsSp;
            cbxTenSP.DisplayMember = "Ten";
        }
        void loadCbxTrangThai()
        {
            cbxTrangThai.Items.Clear();
            cbxTrangThai.Items.Add("Tất cả");
            cbxTrangThai.Items.Add("Chưa hoàn thành");
            cbxTrangThai.Items.Add("Chưa thanh toán");
            cbxTrangThai.Items.Add("Đã hoàn tất");
            cbxTrangThai.Items.Add("Đã hủy");
            cbxTrangThai.Text = "Tất cả";
        }
        void loadCbxMaTho()
        {
            List<NhanVien> dsTho = NhanVienDAO.Instance.loadDSThoMay();
            cbxMaThoMay.DataSource = dsTho;
            cbxMaThoMay.DisplayMember = "Ma";
        }
        void loadPC()
        {
            lamTrong();
            lvPC.Items.Clear();
            int stt = 0; List<PhanCong> l = PhanCongDAO.Instance.loadDS();
            foreach (PhanCong item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaPC.ToString());
                lv.SubItems.Add(item.MaSP.ToString());
                lv.SubItems.Add(item.MaNV.ToString());
                lv.SubItems.Add(item.TienCong1SP.ToString());
                lv.SubItems.Add(item.SoLuongPhanCong.ToString());
                lv.SubItems.Add(item.SoLuongHoanThanh.ToString());
                lv.SubItems.Add(item.TrangThai.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvPC.Items.Add(lv);
            }
            lvPC.FullRowSelect = true;
            lvPC.Show();
        }
        void loadPCDieuKien()
        {
            lamTrong();
            lvPC.Items.Clear();
            int stt = 0; List<PhanCong> l = PhanCongDAO.Instance.loadDSTheoDieuKien(" WHERE TrangThai=N'"+cbxTrangThai.Text+"'");
            foreach (PhanCong item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaPC.ToString());
                lv.SubItems.Add(item.MaSP.ToString());
                lv.SubItems.Add(item.MaNV.ToString());
                lv.SubItems.Add(item.TienCong1SP.ToString());
                lv.SubItems.Add(item.SoLuongPhanCong.ToString());
                lv.SubItems.Add(item.SoLuongHoanThanh.ToString());
                lv.SubItems.Add(item.TrangThai.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvPC.Items.Add(lv);
            }
            lvPC.FullRowSelect = true;
            lvPC.Show();
        }
        void lamTrong()
        {
            tbMaPC.Text = "";
            tbGhiChu.Text = "";
            nudSoLuongHT.Value = 0;
            nudSoLuongPC.Value = 0;
            nudTienCong1SP.Value = 0;
        }
        private void lvCTDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            lamTrong();
            foreach (ListViewItem items in lvPC.SelectedItems)
            {
                tbMaPC.Text = items.SubItems[1].Text;
                cbxMaSP.Text = items.SubItems[2].Text;
                cbxMaThoMay.Text = items.SubItems[3].Text;
                nudTienCong1SP.Value =int.Parse(items.SubItems[4].Text);
                nudSoLuongPC.Value = int.Parse(items.SubItems[5].Text);
                nudSoLuongHT.Value = int.Parse(items.SubItems[6].Text);
                tbGhiChu.Text = items.SubItems[8].Text;
                tbTrangThai.Text = items.SubItems[7].Text;
            }
        }

        private void cbxMaPhanDon_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nudSoLuongPC.Value <= 0)
                return;
            if (string.IsNullOrEmpty(cbxMaThoMay.Text)||string.IsNullOrEmpty(cbxMaSP.Text))
            {
                MessageBox.Show("Hãy chọn sản phẩm và thợ may trước !", "Thông báo");
                return;
            }    
            DataProvider.Instance.RunQuery("INSERT dbo.PHANCONG(MaSP,MaNV,TienCong1SP,SoLuongPhanCong,GhiChu) VALUES(N'" + cbxMaSP.Text + "',N'" + cbxMaThoMay.Text + "'," + nudTienCong1SP.Value + "," + nudSoLuongPC.Value + ",N'" + tbGhiChu.Text + "')");
            loadPC();
            MessageBox.Show("Phân công thành công", "Thông báo");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMaPC.Text))
            {
                MessageBox.Show("Hãy chọn phân công cần done !", "Thông báo");
                return;
            }
            if (nudSoLuongHT.Value<=0)
            {
                MessageBox.Show("Số lượng hoàn thành phải lớn hơn 0 !", "Thông báo");
                return;
            }
            if (tbTrangThai.Text != "Chưa hoàn thành")
            {
                MessageBox.Show("Phân công này không thể done !", "Thông báo");
                return;
            }
            SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(cbxMaSP.Text);
            if (sp == null)
                return;
            int slNew = (int)nudSoLuongHT.Value + sp.TonKho;
            DataProvider.Instance.RunQuery("UPDATE PHANCONG SET TrangThai=N'Chưa thanh toán',SoLuongHoanThanh="+nudSoLuongHT.Value+ ",SoLuongCon="+nudSoLuongHT.Value+ ",TienCong1SP=" + nudTienCong1SP.Value+" WHERE MaPC=" + tbMaPC.Text);
            DataProvider.Instance.RunQuery("UPDATE SANPHAM SET TonKho="+ slNew + " WHERE MaSP=N'" + sp.Ma+"'");
            loadPC();
            MessageBox.Show("Done phân công thành công", "Thông báo");
        }

        private void cbxMaThoMay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NhanVien nv = NhanVienDAO.Instance.getNhanVienByMa(cbxMaThoMay.Text);
                if (nv == null)
                    return;
                FNhanVienInfo iNv = new FNhanVienInfo(nv);
                iNv.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cbxMaThoMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxTenSP_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                FSanPhamInfo iSp = new FSanPhamInfo(cbxMaSP.Text);
                iSp.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lamTrong();
            if (cbxTrangThai.Text == "Tất cả")
                loadPC();
                    else loadPCDieuKien();
        }

        private void cbxTenSP_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FSanPhamInfo iSp = new FSanPhamInfo(cbxMaSP.Text);
                iSp.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMaPC.Text))
            {
                MessageBox.Show("Hãy chọn phân công cần hủy !", "Thông báo");
                return;
            }
            if (tbTrangThai.Text != "Chưa hoàn thành")
            {
                MessageBox.Show("Phân công này không thể hủy !", "Thông báo");
                return;
            }
            DataProvider.Instance.RunQuery("UPDATE PHANCONG SET TrangThai=N'Đã hủy',SoLuongHoanThanh=0 WHERE MaPC=" + tbMaPC.Text);
            loadPC();
            MessageBox.Show("Hủy phân công thành công", "Thông báo");
        }

        private void cbxTrangThai_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lamTrong();
            if (cbxTrangThai.Text == "Tất cả")
                loadPC();
            else loadPCDieuKien();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMaPC.Text))
            {
                MessageBox.Show("Hãy chọn phân công cần cập nhật !", "Thông báo");
                return;
            }
            if (tbTrangThai.Text != "Chưa hoàn thành")
            {
                MessageBox.Show("Phân công này không thể thay đổi thông tin !", "Thông báo");
                return;
            }
            DataProvider.Instance.RunQuery("UPDATE PHANCONG SET SoLuongHoanThanh=" + nudSoLuongHT.Value + ",TienCong1SP=" + nudTienCong1SP.Value + " WHERE MaPC=" + tbMaPC.Text);
            loadPC();
            MessageBox.Show("Cập nhật phân công thành công", "Thông báo");
        }

        private void cbxMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
