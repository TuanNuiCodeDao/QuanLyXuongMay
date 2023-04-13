using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class FHoaDon : Form
    {
        private DonHang dh;
        private List<CTDonHang> l;
        public FHoaDon(DonHang dh)
        {
            InitializeComponent();
            this.dh = dh;
            l = new List<CTDonHang>();
            loadInfo();
        }
        void lamTrong()
        {
            tbSoTT.Text = "";
            tbTongTien.Text="0";
            tbSoLuongDat.Text = "0";
            nudSoLuongGiao.Value = 0;
            nudDonGia.Value = 0;
            tbThanhTien.Text = "0";
        }
        void reLoadCT()
        {
            lamTrong();
            lvCTDonHang.Items.Clear();
            int stt = 0;
            int tong = 0;
            foreach (CTDonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(item.MaSP.ToString());
                lv.SubItems.Add(sp.Ten);
                lv.SubItems.Add(item.Mau.ToString());
                lv.SubItems.Add(item.Size.ToString());
                lv.SubItems.Add(item.SoLuongDat.ToString());
                lv.SubItems.Add(item.SoLuongGiao.ToString());
                lv.SubItems.Add(item.DonGia.ToString());
                int thanhTien = item.DonGia * item.SoLuongGiao;
                tong += thanhTien;
                lv.SubItems.Add(thanhTien + "");
                lvCTDonHang.Items.Add(lv);
            }
            lvCTDonHang.FullRowSelect = true;
            lvCTDonHang.Show();
            KhachHang kh = KhachHangDAO.Instance.getKhachHangByMa(dh.MaKH);
            tbMaDH.Text = dh.Ma;
            tbTongTien.Text = tong + "";
            tbMaKH.Text = kh.Ma;
        }
        void loadInfo()
        {
            KhachHang kh = KhachHangDAO.Instance.getKhachHangByMa(dh.MaKH);
            if (kh != null)
                tbTenKH.Text = kh.HoTen;
            lamTrong();   
            lvCTDonHang.Items.Clear();
            l = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
            int stt = 0;
            int tong = 0;
            foreach (CTDonHang item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(item.MaSP.ToString());
                lv.SubItems.Add(sp.Ten);
                lv.SubItems.Add(item.Mau.ToString());
                lv.SubItems.Add(item.Size.ToString());
                lv.SubItems.Add(item.SoLuongDat.ToString());
                lv.SubItems.Add(item.SoLuongGiao.ToString());
                lv.SubItems.Add(item.DonGia.ToString());
                int thanhTien = item.DonGia * item.SoLuongGiao;
                tong += thanhTien;
                lv.SubItems.Add(thanhTien+"");
                lvCTDonHang.Items.Add(lv);
            }
            lvCTDonHang.FullRowSelect = true;
            lvCTDonHang.Show();
            KhachHang kh1 = KhachHangDAO.Instance.getKhachHangByMa(dh.MaKH);
            tbMaDH.Text = dh.Ma;
            tbTongTien.Text = tong + "";
            tbMaKH.Text = kh1.Ma;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dh.TrangThai != "Chưa hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành hoặc đã hủy", "Nhắc nhở");
                return;
            }
            if (MessageBox.Show("Sau khi giao sẽ không được thay đổi dữ liệu, xác nhận giao đơn hàng ?", "Xác nhận", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DateTime ngayGiao = DateTime.Now;
                DataProvider.Instance.RunQuery("UPDATE DONHANG SET TrangThai=N'Đã hoàn thành',NgayGiao=CONVERT(DATE,N'" + ngayGiao.ToString("s") + "'),TongTien="+tbTongTien.Text+" WHERE MaDH=N'" + dh.Ma + "'");
                List<string> lMaSP = new List<string>();
                List<int> lCpThoMay = new List<int>();
                for (int i = 0; i < l.Count; i++)
                {
                    DataProvider.Instance.RunQuery("UPDATE CT_DONHANG SET SoLuongGiao=" + l[i].SoLuongGiao + ",DonGia=" + l[i].DonGia + " WHERE MaCTDH=N'" + l[i].MaCTDH + "'");
                    int j=0;
                    for (j = 0; j < lMaSP.Count; j++)
                        if (l[i].MaSP == lMaSP[j])
                            break;
                    if(j==lMaSP.Count)
                    {
                        lMaSP.Add(l[i].MaSP);
                        int tong = 0,tongTien=0;
                        for (int dem = i; dem < l.Count; dem++)
                            if (l[i].MaSP == l[dem].MaSP)
                                tong += l[dem].SoLuongGiao;
                        int maPc = 0;
                        PhanCong pcTam;
                        int sl = tong;
                        while (tong>0)
                        {
                            maPc++;
                            pcTam = PhanCongDAO.Instance.getPhanCongByMa(maPc+""); 
                            if(pcTam != null && pcTam.MaSP == l[i].MaSP&&pcTam.SoLuongCon>0)
                            {
                                int slLay = pcTam.SoLuongCon;
                                if (tong > slLay)
                                    slLay = tong;
                                tong -= slLay;
                                tongTien = slLay * pcTam.TienCong1SP;
                                DataProvider.Instance.RunQuery("UPDATE PHANCONG SET SoLuongCon=" + (pcTam.SoLuongCon - slLay) + " WHERE MaPC=" + pcTam.MaPC);
                            }  
                        }
                        int tienCongThoMay1SP = tongTien / sl;
                        lCpThoMay.Add(tienCongThoMay1SP);
                        SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(l[i].MaSP);
                        DataProvider.Instance.RunQuery("UPDATE SANPHAM SET TonKho=" + (sp.TonKho-sl) + " WHERE MaSP=N'" + sp.Ma+"'");
                        DataProvider.Instance.RunQuery("UPDATE CT_DONHANG SET SoLuongGiao=" + l[i].SoLuongGiao + ",DonGia=" + l[i].DonGia + ",ChiPhiThoMay="+ tienCongThoMay1SP + " WHERE MaCTDH=N'" + l[i].MaCTDH + "'");
                    }
                    else
                    {
                        DataProvider.Instance.RunQuery("UPDATE CT_DONHANG SET SoLuongGiao=" + l[i].SoLuongGiao + ",DonGia=" + l[i].DonGia + ",ChiPhiThoMay=" + lCpThoMay[j] + " WHERE MaCTDH=N'" + l[i].MaCTDH + "'");
                    }
                }
                dh = DonHangDAO.Instance.getDonHangByMa(dh.Ma);
                DataProvider.Instance.RunQuery("INSERT dbo.THUCHI(Thu,NoiDung,SoTien,MaNoi) VALUES(N'Chi',N'Thanh toán đơn hàng - " + dh.Ma + "'," + dh.TongTien + ",N'" + dh.Ma + "')");
                MessageBox.Show("Giao đơn hàng thành công", "Thông báo");
            }
        }

        private void lvCTDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lvCTDonHang.SelectedItems)
            {
                tbSoTT.Text = items.SubItems[0].Text;
                tbSoLuongDat.Text= items.SubItems[4].Text;
                nudDonGia.Value = int.Parse(items.SubItems[6].Text);
                nudSoLuongGiao.Value = int.Parse(items.SubItems[5].Text);
                tbThanhTien.Text = items.SubItems[7].Text;
            }
        }

        private void tbMaKH_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FKhachHangInfo iKh = new FKhachHangInfo(tbMaKH.Text);
                iKh.ShowDialog();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            loadInfo();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(dh.TrangThai=="Đã hủy")
            {
                MessageBox.Show("Đơn hàng đã hủy !", "Nhắc nhở");
                return;
            }
            if (dh.TrangThai == "Đã hoàn thành")
            {
                MessageBox.Show("Đơn hàng đã hoàn thành !", "Nhắc nhở");
                return;
            }
            if (lvCTDonHang.SelectedItems.Count==0)
            {
                MessageBox.Show("Hãy chọn phân đơn trước !", "Nhắc nhở");
                return;
            }
            int tong = 0;
            ListViewItem items =lvCTDonHang.SelectedItems[0];
            SanPham sp = SanPhamDAO.Instance.getSanPhamByTen(items.SubItems[1].Text);
            int slNew = (int)nudSoLuongGiao.Value;
            for(int i=0;i<l.Count;i++)
                if (int.Parse(items.SubItems[0].Text) != i + 1 && sp.Ma == l[i].MaSP)
                {
                    tong += l[i].SoLuongGiao;
                }
            if(tong+slNew>sp.TonKho)
            {
                MessageBox.Show("Sản phẩm này chỉ còn lại "+(sp.TonKho-tong)+" tồn kho !", "Nhắc nhở");
                return;
            }
            int stt = int.Parse(tbSoTT.Text);
            l[stt - 1].SoLuongGiao = slNew;
            int giaNew=(int)nudDonGia.Value;
            l[stt - 1].DonGia = giaNew;
            reLoadCT();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dh.TrangThai=="Đã hủy")
            {
                MessageBox.Show("Đơn hàng này đã huy, không thể phân tích !", "Nhắc nhở");
                return;
            }    
            FPhanTichChiPhi pt = new FPhanTichChiPhi(dh, l);
            this.Hide();
            pt.ShowDialog();
            this.Show();
        }
    }
}
