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
    public partial class FPhanTichChiPhi : Form
    {
        private DonHang dh;
        private List<CTDonHang> l;
        public FPhanTichChiPhi(DonHang dh, List<CTDonHang> l)
        {
            InitializeComponent();
            this.dh = dh;
            this.l = l;
            load();
        }
        void load()
        {
            loadCbxMaCTDH();
            loadDS();
        }
        void lamTrong()
        {
            tbTBGop.Text = "0";
            tbTBRieng.Text = "0";
            tbSLRieng.Text = "0";
            tbSLGop.Text = "0";
            tbTongRieng.Text = "0";
            tbTongGop.Text = "0";
        }
        void loadDS()
        {
            lamTrong();
            int tong = 0,tongTienChung=0;
            for (int i = 0; i < l.Count; i++)
                tong += l[i].SoLuongGiao;
            tbSLChung.Text = tong + "";
            lvCP.Items.Clear();
            List<ChiPhiVatTu> lCp = ChiPhiVatTuDAO.Instance.loadDSTheoMaDH(dh.Ma);
            int stt = 0,tongChi=0;
            if(lCp.Count > 0)
            {
                foreach (ChiPhiVatTu item in lCp)
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
                    if (item.PhanLoai == "Đơn hàng")
                        tongTienChung += item.SoTien;
                    tongChi += item.SoTien;
                }
            }
            tbTongChi.Text = tongChi + " VNĐ";
            tbTongChung.Text = tongTienChung + "";
            if (tong == 0)
                tong = 1;
            tbTBChung.Text = (tongTienChung / tong) + "";
            lvCP.FullRowSelect = true;
            lvCP.Show();
        }
        void loadCbxMaCTDH()
        {
            cbxMaCTDH.DataSource = l;
            cbxMaCTDH.DisplayMember = "MaCTDH";
            if (l.Count > 0)
                cbxMaCTDH.Text = l[0].MaCTDH;
        }
        private void lvCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxMaCTDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            lamTrong();
            lvCP.Items.Clear();
            List<ChiPhiVatTu> lCp = ChiPhiVatTuDAO.Instance.loadDSChungTheoMaDH(dh.Ma);
            int stt = 0,tongRieng=0,slRieng=0;
            foreach (ChiPhiVatTu item in lCp)
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

            lCp = ChiPhiVatTuDAO.Instance.loadDSTheoMaCTDH(cbxMaCTDH.Text);
            foreach (ChiPhiVatTu item in lCp)
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
                tongRieng += item.SoTien;
            }
            int i = 0;
            for (i = 0; i < l.Count; i++)
                if (l[i].MaCTDH == cbxMaCTDH.Text)
                    break;
            if (i == l.Count)
                return;
            CTDonHang ct = l[i];
            if (dh.TrangThai == "Chưa hoàn thành")
            {
                int  cpThoMay = 0;
                
                if (ct != null)
                {
                    slRieng = ct.SoLuongGiao;
                    SanPham sp = SanPhamDAO.Instance.getSanPhamByMa(ct.MaSP);
                    int tong = 0;
                    for (int j = 0; j < l.Count; j++)
                        if (sp.Ma == l[j].MaSP)
                        {
                            tong += l[j].SoLuongGiao;
                        }
                    if (tong > 0)
                    {
                        int tong1 = tong;
                        PhanCong pcTam = PhanCongDAO.Instance.getPhanCongByMa(1 + "");
                        while (pcTam != null && (pcTam.SoLuongCon == 0 || pcTam.MaSP != sp.Ma))
                        {
                            pcTam = PhanCongDAO.Instance.getPhanCongByMa((pcTam.MaPC + 1) + "");
                        }
                        if (pcTam != null)
                        {
                            while (tong1 > 0)
                            {
                                if (pcTam.MaSP == sp.Ma)
                                {
                                    int slLay = 0;
                                    if (tong1 < pcTam.SoLuongCon)
                                    {
                                        slLay = tong1;
                                        tong1 = 0;
                                    }
                                    else
                                    {
                                        slLay = pcTam.SoLuongCon;
                                        tong1 -= slLay;
                                    }
                                    cpThoMay += pcTam.TienCong1SP * slLay;
                                }
                                int maNew = pcTam.MaPC + 1;
                                pcTam = PhanCongDAO.Instance.getPhanCongByMa(maNew + "");
                            }
                        }

                    }
                    tongRieng += cpThoMay;
                    tbTongRieng.Text = tongRieng + "";
                    tbSLGop.Text = slRieng + "";
                    tbSLRieng.Text = slRieng + "";
                    tbTongGop.Text = (int.Parse(tbTBChung.Text) * slRieng + tongRieng) + "";
                    if (slRieng == 0)
                    {
                        tbTBRieng.Text = "0";
                        tbTBGop.Text = "0";
                    }
                    else
                    {
                        tbTBRieng.Text = (tongRieng / slRieng) + "";
                        tbTBGop.Text = (int.Parse(tbTongGop.Text) / slRieng) + "";
                    }
                }
                stt++;
                ListViewItem lv1 = new ListViewItem(stt.ToString());
                lv1.SubItems.Add("TM");
                lv1.SubItems.Add("Thợ may");
                lv1.SubItems.Add(ct.MaCTDH + "");
                lv1.SubItems.Add(cpThoMay + "");
                lv1.SubItems.Add("Phân đơn");
                lv1.SubItems.Add("");
                lvCP.Items.Add(lv1);
            }
            {
                tongRieng += ct.SoLuongGiao * ct.ChiPhiThoMay;
                stt++;
                ListViewItem lv1 = new ListViewItem(stt.ToString());
                lv1.SubItems.Add("TM");
                lv1.SubItems.Add("Thợ may");
                lv1.SubItems.Add(ct.MaCTDH + "");
                lv1.SubItems.Add((ct.SoLuongGiao * ct.ChiPhiThoMay) + "");
                lv1.SubItems.Add("Phân đơn");
                lv1.SubItems.Add("");
                lvCP.Items.Add(lv1);
                tbTongRieng.Text = tongRieng + "";
                tbSLGop.Text = ct.SoLuongGiao + "";
                tbSLRieng.Text = ct.SoLuongGiao + "";
                tbTongGop.Text = (int.Parse(tbTBChung.Text) * ct.SoLuongGiao + tongRieng) + "";
                if (ct.SoLuongGiao == 0)
                {
                    tbTBRieng.Text = "0";
                    tbTBGop.Text = "0";
                }
                else
                {
                    tbTBRieng.Text = (tongRieng / ct.SoLuongGiao) + "";
                    tbTBGop.Text = (int.Parse(tbTongGop.Text) / ct.SoLuongGiao) + "";
                }
            }
            lvCP.FullRowSelect = true;
            lvCP.Show();
        }

        private void cbxMaCTDH_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int i = 0;
                for (i = 0; i < l.Count; i++)
                    if (l[i].MaCTDH == cbxMaCTDH.Text)
                        break;
                if (i == l.Count)
                    return;
                FCTDonHangInfo iCT = new FCTDonHangInfo(l[i]);
                iCT.ShowDialog();
            }
        }
    }
}
