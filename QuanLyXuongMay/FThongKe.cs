using DocumentFormat.OpenXml.Bibliography;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyXuongMay
{
    public partial class FThongKe : Form
    {
        public FThongKe()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadDonHang();
            loadSanPham();
            loadKhachHang();
            loadThuChi();
        }
        void loadTimThuChi()
        {
            lvThuChi.Items.Clear();
            string dieuKien="WHERE ";
            string tim = tbTimThuChi.Text;
            if(string.IsNullOrEmpty(tim)||tim=="ALL"||tim=="all")
            {

            }   else
            {
                dieuKien+="(NoiDung LIKE N'%" + tim + "%' OR Loai LIKE N'%" + tim + "%') AND ";
            }
            DateTime d1 = dateThuChi1.Value;
            DateTime d2 = dateThuChi2.Value;
            d2 = d2.AddDays(1);
            dieuKien += "ThoiGian >= CONVERT(DATE, N'" + d1.ToString("s") + "') AND ThoiGian <= CONVERT(DATE, N'" + d2.ToString("s") + "')";
            List<ThuChi> l = ThuChiDAO.Instance.loadDSTim(dieuKien);
            int stt = 0, thu = 0, chi = 0;
            foreach (ThuChi item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(item.NoiDung.ToString());
                lv.SubItems.Add(item.SoTien.ToString());
                lv.SubItems.Add(item.ThoiGian.ToString());
                lvThuChi.Items.Add(lv);
                if (item.PhanLoai == "Thu")
                    thu += item.SoTien;
                else
                    chi += item.SoTien;
            }
            lvThuChi.FullRowSelect = true;
            lvThuChi.Show();
            tbDTTC.Text = String.Format("{0:###,###,##0}", thu) + " VNĐ";
            tbCPTC.Text = String.Format("{0:###,###,##0}", chi) + " VNĐ";
            tbLNTC.Text = String.Format("{0:###,###,##0}", thu - chi) + " VNĐ";
        }
        void loadThuChi()
        {
            lvThuChi.Items.Clear();
            List<ThuChi> l = ThuChiDAO.Instance.loadDS();
            int stt = 0,thu=0,chi=0;
            foreach (ThuChi item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(item.NoiDung.ToString());
                lv.SubItems.Add(item.SoTien.ToString());
                lv.SubItems.Add(item.ThoiGian.ToString());
                lvThuChi.Items.Add(lv);
                if (item.PhanLoai == "Thu")
                    thu += item.SoTien;
                else
                    chi+= item.SoTien;
            }
            lvThuChi.FullRowSelect = true;
            lvThuChi.Show();
            tbDTTC.Text= String.Format("{0:###,###,##0}", thu) + " VNĐ";
            tbCPTC.Text = String.Format("{0:###,###,##0}", chi) + " VNĐ";
            tbLNTC.Text = String.Format("{0:###,###,##0}", thu-chi) + " VNĐ";

        }
        void loadDonHang()
        {
            loadDSDonHang();
            loadCbx();
        }
        void loadCbx()
        {
            loadCbxMaKH();
            loadCbxTrangThai();
            loadCbxLoadDH();
        }    
        void loadCbxTrangThai()
        {
            cbxTrangThai.Items.Clear();
            cbxTrangThai.Items.Add("Chưa hoàn thành");
            cbxTrangThai.Items.Add("Đã hoàn thành");
            cbxTrangThai.Items.Add("Đã hủy");
            cbxTrangThai.Text = cbxTrangThai.Items[0].ToString();
        }    
        void loadCbxLoadDH()
        {
            cbxLoadDH.Items.Clear();
            cbxLoadDH.Items.Add("Tất cả");
            cbxLoadDH.Items.Add("Theo trạng thái");
            cbxLoadDH.Items.Add("Theo khách hàng");
            cbxLoadDH.Items.Add("Theo ngày đặt");
            cbxLoadDH.Items.Add("Theo tổng tiền");
            cbxLoadDH.Text = cbxLoadDH.Items[0].ToString();
        }    
        void loadCbxMaKH()
        {
            cbxMaKH.Items.Clear();
            List<KhachHang> l = KhachHangDAO.Instance.loadDS();
            if (l.Count == 0)
                return;
            cbxMaKH.DataSource = l;
            cbxMaKH.DisplayMember="Ma";
            cbxMaKH.Text = l[0].Ma;
        }
        void loadDSDonHang()
        {
            lamTRongTbDonHang();
            lvDonHang.Items.Clear();
            List<DonHang> l = DonHangDAO.Instance.loadDS();
            int stt = 0,chuaHT=0,daHuy=0,daHT=0,tong=0;
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
                tong += item.TongTien;
                if (item.TrangThai == "Chưa hoàn thành")
                    chuaHT++;
                else
                    if (item.TrangThai == "Đã hủy")
                    daHuy++;
                    else daHT++;
            }
            lvDonHang.FullRowSelect = true;
            lvDonHang.Show();
            tbChuaHT.Text = chuaHT + "";
            tbDaHT.Text = daHT + "";
            tbDaHuy.Text = daHuy + "";
            tbTongTien.Text = String.Format("{0:###,###,##0}", tong) + " VNĐ";
        }
        void lamTRongTbDonHang()
        {
            tbChuaHT.Text = "0";
            tbDaHT.Text = "0";
            tbDaHuy.Text = "0";
            tbTongTien.Text = 0+" VNĐ";
        }
        private void lvDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cbxMaKH_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                KhachHang kh = KhachHangDAO.Instance.getKhachHangByMa(cbxMaKH.Text);
                if (kh == null)
                    return;
                FKhachHangInfo iKh = new FKhachHangInfo(cbxMaKH.Text);
                iKh.ShowDialog();
            }
        }

        private void cbxMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void loadTKHoaDon()
        {
            if (cbxLoadDH.Text == "Tất cả")
            {
                loadDSDonHang();
                return;
            }
            lamTRongTbDonHang();
            lvDonHang.Items.Clear();
            string dieuKien = "";
            int stt = 0, chuaHT = 0, daHuy = 0, daHT = 0, tong = 0;
            if (cbxLoadDH.Text == "Theo trạng thái")
            {
                dieuKien = " WHERE TrangThai=N'" + cbxTrangThai.Text + "'";
            }
            else
            if (cbxLoadDH.Text == "Theo khách hàng")
            {
                dieuKien = " WHERE MaKH=N'" + cbxMaKH.Text + "'";
            }
            else
            if (cbxLoadDH.Text == "Theo ngày đặt")
            {
                DateTime mocDuoi = dateDuoi.Value;
                DateTime mocTren = dateTren.Value;
                dieuKien = " WHERE NgayDat>=CONVERT(DATE,N'" + mocDuoi.ToString("s") + "') AND NgayDat <= CONVERT(DATE, N'" + mocTren.ToString("s") + "')";
            }
            else
            if (cbxLoadDH.Text == "Theo tổng tiền")
            {
                dieuKien = " WHERE TongTien>="+nudTienDuoi.Value+" AND TongTien<="+nudTienTren.Value;

            }
            List<DonHang> l = DonHangDAO.Instance.loadDSTheoDieuKien(dieuKien);
            if (l != null)
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
                    tong += item.TongTien;
                    if (item.TrangThai == "Chưa hoàn thành")
                        chuaHT++;
                    else
                        if (item.TrangThai == "Đã hủy")
                        daHuy++;
                    else daHT++;
                }
            lvDonHang.FullRowSelect = true;
            lvDonHang.Show();
            tbChuaHT.Text = chuaHT + "";
            tbDaHT.Text = daHT + "";
            tbDaHuy.Text = daHuy + "";
            tbTongTien.Text = String.Format("{0:###,###,##0}", tong) + " VNĐ";
        }
        private void cbxLoadDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTKHoaDon();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadTKHoaDon();
        }

        private void nudTienDuoi_ValueChanged(object sender, EventArgs e)
        {
        }
        void loadSanPham()
        {
            lamMoiSP();
            List<SanPham> lSanPham = SanPhamDAO.Instance.loadDS();
            List<ThongKeSanPham> lTk = new List<ThongKeSanPham>();
            foreach (SanPham sanPham in lSanPham)
            {
                ThongKeSanPham tk = new ThongKeSanPham(sanPham.Ma, sanPham.Ten, 0, 0, 0, 0, 0, 0, 0);
                lTk.Add(tk);
            }
            string dieuKien = " WHERE TrangThai=N'Đã hoàn thành'";
            List<DonHang> lDh = DonHangDAO.Instance.loadDSTheoDieuKien(dieuKien);
            foreach (DonHang dh in lDh)
            {
                int tongCpDh = 0;
                List<CTDonHang> lCt = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
                if (lCt == null || lCt.Count == 0)
                    continue;
                List<ChiPhiVatTu> lCpp = ChiPhiVatTuDAO.Instance.loadDSTheoMaCTDH(dh.Ma);
                foreach (ChiPhiVatTu cp in lCpp)
                    tongCpDh += cp.SoTien;
                int tongSl = 0;
                for (int i = 0; i < lCt.Count; i++)
                    tongSl += lCt[i].SoLuongGiao;
                int cpTB = tongCpDh / tongSl;
                for (int i = 0; i < lCt.Count; i++)
                {
                    int j = 0;
                    for (j = 0; j < lTk.Count; j++)
                        if (lTk[j].Ma == lCt[i].MaSP)
                            break;
                    if (j < lTk.Count)
                    {
                        lTk[j].SoLuongDaGiao += lCt[i].SoLuongGiao;
                        lTk[j].DoanhThu += lCt[i].DonGia * lCt[i].SoLuongGiao;
                        lTk[j].TongChiPhi += (lCt[i].ChiPhiThoMay + cpTB) * lCt[i].SoLuongGiao;
                        List<ChiPhiVatTu> lCp = ChiPhiVatTuDAO.Instance.loadDSTheoMaCTDH(lCt[i].MaCTDH);
                        foreach (ChiPhiVatTu cp in lCp)
                            lTk[j].TongChiPhi += cp.SoTien;
                    }
                }
            }
            int tongSlGiao = 0, tongDoanhThu = 0, tongChiPhi = 0, tongSlChuaGiao = 0, tongSlDangSX = 0;
            for (int i = 0; i < lTk.Count; i++)
            {
                tongSlGiao += lTk[i].SoLuongDaGiao;
                tongDoanhThu += lTk[i].DoanhThu;
                tongChiPhi += lTk[i].TongChiPhi;
                if (lTk[i].SoLuongDaGiao > 0)
                {
                    lTk[i].DonGiaTB = lTk[i].DoanhThu / lTk[i].SoLuongDaGiao;
                    lTk[i].ChiPhiTB = lTk[i].TongChiPhi / lTk[i].SoLuongDaGiao;
                }
                List<DonHang> lDH = DonHangDAO.Instance.loadDSChuaHT();
                int dem = 0;
                foreach (DonHang dh in lDH)
                {
                    List<CTDonHang> lct = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
                    foreach (CTDonHang ct in lct)
                        dem += ct.SoLuongDat;
                }
                lTk[i].SoLuongChuaGiao = dem;
                tongSlChuaGiao += dem;
                List<PhanCong> lPc = PhanCongDAO.Instance.loadDSTheoDieuKien(" WHERE TrangThai=N'Chưa hoàn thành' AND MaSP=N'" + lTk[i].Ma + "'");
                dem = 0;
                foreach (PhanCong pc in lPc)
                    dem += pc.SoLuongPhanCong;
                lTk[i].SoLuongDangSX = dem;
                tongSlDangSX += dem;
            }
            int loiNhuan = tongDoanhThu - tongChiPhi;
            tbSPSoLuongDangSX.Text = tongSlDangSX + "";
            tbSPTongChiPhi.Text = String.Format("{0:###,###,##0}", tongChiPhi) + " VNĐ" + "";
            tbSPTongChưaGiao.Text = tongSlChuaGiao + "";
            tbSPTongDaGiao.Text = tongSlGiao + "";
            tbSPTongDoanhThu.Text = String.Format("{0:###,###,##0}", tongDoanhThu) + " VNĐ" + "";
            tbSPTongLoiNhuan.Text = String.Format("{0:###,###,##0}", loiNhuan) + " VNĐ" + "";
            int stt = 0;
            lvTkSP.Items.Clear();
            if (lTk != null)
                foreach (ThongKeSanPham item in lTk)
                {
                    stt++;
                    ListViewItem lv = new ListViewItem(stt.ToString());
                    lv.SubItems.Add(item.Ma.ToString());
                    lv.SubItems.Add(item.Ten.ToString());
                    lv.SubItems.Add(item.SoLuongDaGiao.ToString());
                    lv.SubItems.Add(item.DoanhThu.ToString());
                    lv.SubItems.Add(item.DonGiaTB.ToString());
                    lv.SubItems.Add(item.TongChiPhi.ToString());
                    lv.SubItems.Add(item.ChiPhiTB.ToString());
                    lv.SubItems.Add(item.SoLuongChuaGiao.ToString());
                    lv.SubItems.Add(item.SoLuongDangSX.ToString());
                    lvTkSP.Items.Add(lv);
                }
            lvTkSP.FullRowSelect = true;
            lvTkSP.Show();
        }
        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void lamMoiSP()
        {
            tbSPTongDoanhThu.Text = "0";
            tbSPTongLoiNhuan.Text = "0";
            tbSPTongDaGiao.Text = "0";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d1 = date1.Value;
            DateTime d2 = date2.Value;
            lamMoiSP();
            List<SanPham> lSanPham = SanPhamDAO.Instance.loadDS();
            List<ThongKeSanPham> lTk = new List<ThongKeSanPham>();
            foreach (SanPham sanPham in lSanPham)
            {
                ThongKeSanPham tk = new ThongKeSanPham(sanPham.Ma, sanPham.Ten,0,0,0,0,0,0,0);
                lTk.Add(tk);
            }  
            string dieuKien = " WHERE NgayDat>=CONVERT(DATE,N'" + d1.ToString("s") + "') AND NgayDat <= CONVERT(DATE, N'" + d2.ToString("s") + "') AND TrangThai=N'Đã hoàn thành'";
            List<DonHang> lDh = DonHangDAO.Instance.loadDSTheoDieuKien(dieuKien);
            foreach(DonHang dh in lDh)
            {
                int tongCpDh = 0;
                List<CTDonHang> lCt = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
                if (lCt == null || lCt.Count == 0)
                    continue;
                List<ChiPhiVatTu> lCpp = ChiPhiVatTuDAO.Instance.loadDSTheoMaCTDH(dh.Ma);
                foreach (ChiPhiVatTu cp in lCpp)
                    tongCpDh += cp.SoTien;
                int tongSl=0;
                for (int i = 0; i < lCt.Count; i++)
                    tongSl += lCt[i].SoLuongGiao;
                int cpTB = tongCpDh / tongSl;
                for (int i = 0; i < lCt.Count; i++)
                {
                    int j = 0;
                    for (j = 0; j < lTk.Count; j++)
                        if (lTk[j].Ma == lCt[i].MaSP)
                            break;
                    if (j < lTk.Count)
                    {
                        lTk[j].SoLuongDaGiao += lCt[i].SoLuongGiao;
                        lTk[j].DoanhThu += lCt[i].DonGia * lCt[i].SoLuongGiao;
                        lTk[j].TongChiPhi += (lCt[i].ChiPhiThoMay+cpTB) * lCt[i].SoLuongGiao;
                        List<ChiPhiVatTu> lCp = ChiPhiVatTuDAO.Instance.loadDSTheoMaCTDH(lCt[i].MaCTDH);
                        foreach(ChiPhiVatTu cp in lCp)
                            lTk[j].TongChiPhi += cp.SoTien;
                    }
                }
            }
            int tongSlGiao=0,tongDoanhThu=0,tongChiPhi=0,tongSlChuaGiao=0,tongSlDangSX=0;
            for (int i = 0; i < lTk.Count;i++)
            {
                tongSlGiao += lTk[i].SoLuongDaGiao;
                tongDoanhThu += lTk[i].DoanhThu;
                tongChiPhi += lTk[i].TongChiPhi;
                if (lTk[i].SoLuongDaGiao > 0)
                {
                    lTk[i].DonGiaTB = lTk[i].DoanhThu / lTk[i].SoLuongDaGiao;
                    lTk[i].ChiPhiTB = lTk[i].TongChiPhi / lTk[i].SoLuongDaGiao;
                }
                List<DonHang> lDH = DonHangDAO.Instance.loadDSChuaHT();
                int dem = 0;
                foreach(DonHang dh in lDH)
                {
                    List<CTDonHang> lct = CTDonHangDAO.Instance.loadDSByMaDH(dh.Ma);
                    foreach (CTDonHang ct in lct)
                        dem += ct.SoLuongDat;
                }
                lTk[i].SoLuongChuaGiao = dem;
                tongSlChuaGiao += dem;
                List<PhanCong> lPc = PhanCongDAO.Instance.loadDSTheoDieuKien(" WHERE TrangThai=N'Chưa hoàn thành' AND MaSP=N'" + lTk[i].Ma +"'");
                dem = 0;
                foreach (PhanCong pc in lPc)
                    dem += pc.SoLuongPhanCong;
                lTk[i].SoLuongDangSX = dem;
                tongSlDangSX += dem;
            }
            int loiNhuan = tongDoanhThu - tongChiPhi;
            tbSPSoLuongDangSX.Text = tongSlDangSX + "";
            tbSPTongChiPhi.Text = String.Format("{0:###,###,##0}", tongChiPhi) + " VNĐ" + "";
            tbSPTongChưaGiao.Text = tongSlChuaGiao + "";
            tbSPTongDaGiao.Text = tongSlGiao + "";
            tbSPTongDoanhThu.Text = String.Format("{0:###,###,##0}", tongDoanhThu) + " VNĐ" + "";
            tbSPTongLoiNhuan.Text = String.Format("{0:###,###,##0}", loiNhuan) + " VNĐ" + "";
            int stt = 0;
            lvTkSP.Items.Clear();
            if (lTk != null)
                foreach (ThongKeSanPham item in lTk)
                {
                    stt++;
                    ListViewItem lv = new ListViewItem(stt.ToString());
                    lv.SubItems.Add(item.Ma.ToString());
                    lv.SubItems.Add(item.Ten.ToString());
                    lv.SubItems.Add(item.SoLuongDaGiao.ToString());
                    lv.SubItems.Add(item.DoanhThu.ToString());
                    lv.SubItems.Add(item.DonGiaTB.ToString());
                    lv.SubItems.Add(item.TongChiPhi.ToString());
                    lv.SubItems.Add(item.ChiPhiTB.ToString());
                    lv.SubItems.Add(item.SoLuongChuaGiao.ToString());
                    lv.SubItems.Add(item.SoLuongDangSX.ToString());
                    lvTkSP.Items.Add(lv);
                }
            lvTkSP.FullRowSelect = true;
            lvTkSP.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        void loadKhachHang()
        {
            lvTkKh.Items.Clear();
            List<ThongKeKhachHang> lTk = new List<ThongKeKhachHang>();
            List<KhachHang> lKh = KhachHangDAO.Instance.loadDS();
            int tongSoDon = 0, tongXong = 0, tongChua = 0, tongHuy = 0, tongTien = 0;
            foreach(KhachHang kh in lKh)
            {
                int tongSd = 0, sdXong = 0, sdChua = 0, sdHuy = 0, tien = 0;
                List<DonHang> lDh = DonHangDAO.Instance.loadDSTheoDieuKien(" WHERE TrangThai=N'Chưa hoàn thành' AND MaKH=N'" + kh.Ma + "'");
                if (lDh != null)
                {
                    tongSd += lDh.Count;
                    sdChua = lDh.Count;
                }
                lDh = DonHangDAO.Instance.loadDSTheoDieuKien(" WHERE TrangThai=N'Đã hoàn thành' AND MaKH=N'" + kh.Ma + "'");
                if (lDh != null)
                {
                    tongSd += lDh.Count;
                    sdXong = lDh.Count;
                    foreach (DonHang dh in lDh)
                        tien += dh.TongTien;
                }
                lDh = DonHangDAO.Instance.loadDSTheoDieuKien(" WHERE TrangThai=N'Đã hủy' AND MaKH=N'" + kh.Ma + "'");
                if (lDh != null)
                {
                    tongSd += lDh.Count;
                    sdHuy = lDh.Count;
                }
                tongChua += sdChua;
                tongHuy += sdHuy;
                tongXong += sdXong;
                tongSoDon += tongSd;
                tongTien += tien;
                ThongKeKhachHang tk = new ThongKeKhachHang(kh.Ma, kh.HoTen, kh.Sdt,tongSd,sdXong,sdChua, sdHuy, tien);
                lTk.Add(tk);
            }
            int stt = 0;
            foreach (ThongKeKhachHang item in lTk)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.HoTen.ToString());
                lv.SubItems.Add(item.Sdt.ToString());
                lv.SubItems.Add(item.TongSoDon.ToString());
                lv.SubItems.Add(item.SoDonDaXong.ToString());
                lv.SubItems.Add(item.SoDonChuaXong.ToString());
                lv.SubItems.Add(item.SoDonDaHuy.ToString());
                lv.SubItems.Add(String.Format("{0:###,###,##0}", item.TongSoTienDaTT) + " VNĐ");
                lvTkKh.Items.Add(lv);
            }
            lvTkKh.FullRowSelect = true;
            lvTkKh.Show();
            tbKHSoDonChuaGiao.Text = tongChua + "";
            tbKHSoDonDaHuy.Text = tongHuy + "";
            tbKHSoDonDaXong.Text = tongXong + "";
            tbKHTongSoDon.Text = tongSoDon + "";
            tbKHTongTien.Text = String.Format("{0:###,###,##0}", tongTien) + " VNĐ";
        }

        private void lvTkSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            loadTimThuChi();
        }

        private void lvThuChi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbTimThuChi_TextChanged(object sender, EventArgs e)
        {
            loadTimThuChi();
        }

        private void dateThuChi2_ValueChanged(object sender, EventArgs e)
        {
            loadTimThuChi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadTimThuChi();
        }
    }
}
