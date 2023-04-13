using DocumentFormat.OpenXml.ExtendedProperties;
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

namespace QuanLyXuongMay.InfoForm
{
    public partial class FTraLuongNhanVienSanPham : Form
    {
        private NhanVien nv;
        private int tong;
        public FTraLuongNhanVienSanPham(string maNv)
        {
            InitializeComponent();
            nv = NhanVienDAO.Instance.getNhanVienByMa(maNv);
            load();
        }
        void load()
        {
            tbHoTen.Text = nv.HoTen;
            tbMa.Text = nv.Ma;
            tbPhanLoai.Text = nv.PhanLoai;
            tbToDoi.Text = nv.ToDoi;
            tbSDT.Text = nv.Sdt;
            ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getLanTraLuongCuoiCungTheoMaNV(nv.Ma);
            if (cp == null)
                tblastTime.Text = "Chưa có";
            else
                tblastTime.Text = cp.NgayChi+"";
            tong = 0;
            List<PhanCong> l = PhanCongDAO.Instance.loadDSChuaThanhToanTheoMaThoMay(nv.Ma);
            lvPhanCong.Items.Clear();
            int stt = 0;
            foreach (PhanCong item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.MaPC.ToString());
                lv.SubItems.Add(item.MaSP.ToString());
                lv.SubItems.Add(item.SoLuongPhanCong.ToString());
                lv.SubItems.Add(item.SoLuongHoanThanh.ToString());
                lv.SubItems.Add(item.TienCong1SP.ToString());
                int tien = item.TienCong1SP * item.SoLuongHoanThanh;
                tong += tien;
                lv.SubItems.Add(String.Format("{0:###,###,##0}", tien) + " VNĐ");
                lvPhanCong.Items.Add(lv);
            }
            lvPhanCong.FullRowSelect = true;
            lvPhanCong.Show();
            tbTongTienCong.Text = String.Format("{0:###,###,##0}", tong) + " VNĐ";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(tong==0)
            {
                MessageBox.Show("Không thể trả tiền công (Lương=0) !", "Thông báo");
                return;
            }
            if (MessageBox.Show("Xác nhận trả lương cho thợ may  ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("INSERT dbo.CHIPHIRIENG(TenCP,PhanLoai,SoTien,GhiChu) VALUES(N'" + nv.Ma + "',N'Trả lương thợ may'," + tong + ",N'" + tbGhiChu.Text + "')");
                ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getLast();
                DataProvider.Instance.RunQuery("INSERT dbo.THUCHI(Loai,NoiDung,SoTien,MaNoi) VALUES(N'Chi',N'Trả lương thợ may - "+cp.TenCp+" - " + cp.MaCp+ "'," + tong + ",N'CPR" + cp.MaCp + "')");
                DataProvider.Instance.RunQuery("UPDATE PHANCONG SET TrangThai=N'Đã hoàn tất' WHERE MaNV=N'" + nv.Ma + "' AND TrangThai=N'Chưa thanh toán'");
                MessageBox.Show("Trả lương thành công", "Thông báo");
                load();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
