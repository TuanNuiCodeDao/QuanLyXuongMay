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
    public partial class FChiPhiRieng : Form
    {
        public FChiPhiRieng()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadPhanLoai();
            loadDS();
        }
        void loadPhanLoai()
        {
            cbxPhanLoai.Items.Clear();
            cbxPhanLoai.Items.Add("Khác");
            cbxPhanLoai.Items.Add("Máy móc");
            cbxPhanLoai.Items.Add("Điện");
            cbxPhanLoai.Items.Add("Nước");
            cbxPhanLoai.Items.Add("Bảo trì");
            cbxPhanLoai.Items.Add("Trả lương nhân viên");
            cbxPhanLoai.Items.Add("Trả lương thợ may");
            cbxPhanLoai.Text = cbxPhanLoai.Items[0].ToString();
        }
        void lamTrong()
        {
            tbGhiChu.Text = "";
            tbTenCP.Text = "";
            tbMaCP.Text = "";
            nudSoTien.Value = 0;
            tbNgayChi.Text = "";
            tbTongCong.Text = "0 VNĐ";
            cbxPhanLoai.Text = cbxPhanLoai.Items[0].ToString();
        }
        void loadDS()
        {
            lamTrong();
            lvCP.Items.Clear();
            int stt = 0;
            int tong = 0;
            List<ChiPhiRieng> l = ChiPhiRiengDAO.Instance.loadDS();
            foreach (ChiPhiRieng item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaCp.ToString());
                lv.SubItems.Add(item.TenCp.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(String.Format("{0:###,###,##0}", item.SoTien) + " VNĐ");
                lv.SubItems.Add(item.NgayChi.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvCP.Items.Add(lv);
                tong += item.SoTien;
            }
            lvCP.FullRowSelect = true;
            lvCP.Show();
            tbTongCong.Text = String.Format("{0:###,###,##0}", tong) + " VNĐ";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string tenCP = tbTenCP.Text;
            string ghiChu = tbGhiChu.Text;
            int soTien = (int)nudSoTien.Value;
            string phanLoai = cbxPhanLoai.Text;
            if (cbxPhanLoai.Text == "Trả lương nhân viên" || cbxPhanLoai.Text == "Trả lương thợ may")
            {
                MessageBox.Show("Hãy đổi phân loại chi phí !", "Nhắc nhở");
                return;
            }
            if (string.IsNullOrEmpty(tenCP))
            {
                MessageBox.Show("Tên chi phí không được để trống !", "Nhắc nhở");
                return;
            }
            DataProvider.Instance.RunQuery("INSERT dbo.CHIPHIRIENG(TenCP,PhanLoai,SoTien,GhiChu) VALUES(N'" + tenCP + "',N'" + phanLoai + "'," + soTien + ",N'" + ghiChu + "')");
            ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getLast();
            DataProvider.Instance.RunQuery("INSERT dbo.THUCHI(Loai,NoiDung,SoTien,MaNoi) VALUES(N'Chi',N'Chi phí riêng - "+cp.TenCp+" - "+cp.MaCp+"'," + soTien + ",N'CPR" +cp.MaCp+ "')");
            MessageBox.Show("Thêm chi phí thành công", "Thông báo");
            loadDS();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getChiPhiRiengTheoMa(tbMaCP.Text);
            if(cp==null)
            {
                MessageBox.Show("Hãy chọn chi phí cần xóa !", "Nhắc nhở");
                return;
            }
            if (cp.PhanLoai == "Trả lương nhân viên" || cp.PhanLoai == "Trả lương thợ may")
            {
                MessageBox.Show("Không thể xóa chi phí này !", "Nhắc nhở");
                return;
            }
            if (MessageBox.Show("Xác nhận xóa chi phí này ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("DELETE FROM dbo.CHIPHIRIENG WHERE MaCP=" + tbMaCP.Text);
                DataProvider.Instance.RunQuery("DELETE FROM dbo.THUCHI WHERE MaNoi=N'CPR" + cp.MaCp+"'");
                MessageBox.Show("Xóa chi phí thành công", "Thông báo");
                loadDS();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getChiPhiRiengTheoMa(tbMaCP.Text);
            if (cp == null)
            {
                MessageBox.Show("Hãy chọn chi phí cần cập nhật !", "Nhắc nhở");
                return;
            }
            if (cbxPhanLoai.Text == "Trả lương nhân viên" || cbxPhanLoai.Text == "Trả lương thợ may")
            {
                MessageBox.Show("Hãy đổi phân loại chi phí !", "Nhắc nhở");
                return;
            }
            if (cp.PhanLoai == "Trả lương nhân viên" || cp.PhanLoai == "Trả lương thợ may")
            {
                MessageBox.Show("Không thể cập nhật chi phí này !", "Nhắc nhở");
                return;
            }
            string tenCP = tbTenCP.Text;
            string ghiChu = tbGhiChu.Text;
            int soTien = (int)nudSoTien.Value;
            string phanLoai = cbxPhanLoai.Text;
            if (string.IsNullOrEmpty(tenCP))
            {
                MessageBox.Show("Tên chi phí không được để trống !", "Nhắc nhở");
                return;
            }
            DataProvider.Instance.RunQuery("UPDATE CHIPHIRIENG SET TenCP = N'" + tenCP + "',PhanLoai = N'" + cbxPhanLoai.Text + "',SoTien=" + soTien + ",GhiChu=N'" + ghiChu + "' WHERE MaCP=" + tbMaCP.Text);
            DataProvider.Instance.RunQuery("UPDATE THUCHI SET SoTien = " + soTien + "WHERE MaNoi=N'CPR" + cp.MaCp+"'");
            MessageBox.Show("Cập nhật chi phí thành công", "Thông báo");
            loadDS();
        }

        private void tbTim_TextChanged(object sender, EventArgs e)
        {
            if (tbTim.Text == " " || tbTim.Text == "All" || tbTim.Text == "ALL" || tbTim.Text == "all" || tbTim.Text == "")
            {
                loadDS();
                return;
            }
            lamTrong();
            lvCP.Items.Clear();
            int stt = 0,tong=0;
            List<ChiPhiRieng> l = ChiPhiRiengDAO.Instance.loadDSTim(tbTim.Text);
            foreach (ChiPhiRieng item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaCp.ToString());
                lv.SubItems.Add(item.TenCp.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(String.Format("{0:###,###,##0}", item.SoTien) + " VNĐ");
                lv.SubItems.Add(item.NgayChi.ToString());
                lv.SubItems.Add(item.GhiChu.ToString());
                lvCP.Items.Add(lv);
                tong += item.SoTien;
            }
            lvCP.FullRowSelect = true;
            lvCP.Show();
            tbTongCong.Text = String.Format("{0:###,###,##0}", tong) + " VNĐ";
        }

        private void lvCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lvCP.SelectedItems)
            {
                ChiPhiRieng cp=ChiPhiRiengDAO.Instance.getChiPhiRiengTheoMa(items.SubItems[1].Text);
                if (cp == null)
                    return;
                tbTenCP.Text = cp.TenCp;
                tbMaCP.Text = cp.MaCp+"";
                tbGhiChu.Text = cp.GhiChu;
                tbNgayChi.Text = cp.NgayChi + "";
                nudSoTien.Value = cp.SoTien;
                cbxPhanLoai.Text = cp.PhanLoai;
            }
        }
    }
}
