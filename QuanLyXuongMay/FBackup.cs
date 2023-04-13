using QuanLyXuongMay.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ClosedXML.Excel;
using Excel =Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Runtime.InteropServices;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;
using QuanLyXuongMay.DTO;
using static System.Windows.Forms.LinkLabel;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.Security.Policy;

namespace QuanLyXuongMay
{
    public partial class FBackup : Form
    {
        public FBackup()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbDuongDan.Text))
            {
                MessageBox.Show("Hãy chọn nơi lưu !", "Nhắc nhở");
                return;
            }    
            if(DataProvider.Instance.saoLuuThuCong(tbDuongDan.Text)==true);
                MessageBox.Show("Sao lưu thành công","Thông báo");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbTenFile.Text))
            {
                MessageBox.Show("Hãy chọn File BackUp !", "Nhắc nhở");
                return;
            }
            if (DataProvider.Instance.phucHoi(tbTenFile.Text) == true);
                MessageBox.Show("Phục hồi dữ liệu thành công", "Thông báo");
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f=new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                tbDuongDan.Text = f.SelectedPath;

            }
            else
                tbDuongDan.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbDuongDan.Text))
            {
                MessageBox.Show("Hãy chọn nơi lưu !", "Nhắc nhở");
                return;
            }
            DateTime dt = DateTime.Now;
            string s = dt.ToString(), duongDan = "";
            int j = 0;
            while (j < s.Length)
            {
                if (s[j] == '/' || s[j] == ' ' || s[j] == ':')
                    duongDan += '_';
                else duongDan += s[j];
                j++;
            }
            string filepath = tbDuongDan.Text+"\\DataXuongMay"+ duongDan + ".xlsx";
            XLWorkbook wbook = new XLWorkbook();
            var ws = wbook.Worksheets.Add("Nhân viên");
            List<NhanVien> lNV = NhanVienDAO.Instance.loadDSNhanVien();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã nhân viên";
            ws.Cell(1, 3).Value = "Họ tên";
            ws.Cell(1, 4).Value = "SĐT";
            ws.Cell(1, 5).Value = "Phân loại";
            ws.Cell(1, 6).Value = "Tổ đội";
            ws.Cell(1, 7).Value = "Lương";
            for (int i = 0; i < lNV.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lNV[i].Ma;
                ws.Cell(i + 2, 3).Value = lNV[i].HoTen;
                ws.Cell(i + 2, 4).Value = lNV[i].Sdt;
                ws.Cell(i + 2, 5).Value = lNV[i].PhanLoai;
                ws.Cell(i + 2, 6).Value = lNV[i].ToDoi;
                ws.Cell(i + 2, 7).Value = lNV[i].Luong;
            }

            ws = wbook.Worksheets.Add("Khách hàng");
            List<KhachHang> lKH = KhachHangDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã khách hàng";
            ws.Cell(1, 3).Value = "Họ tên";
            ws.Cell(1, 4).Value = "SĐT";
            ws.Cell(1, 5).Value = "Địa chỉ";
            for (int i = 0; i < lKH.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lKH[i].Ma;
                ws.Cell(i + 2, 3).Value = lKH[i].HoTen;
                ws.Cell(i + 2, 4).Value = lKH[i].Sdt;
                ws.Cell(i + 2, 5).Value = lKH[i].DiaChi;
            }

            ws = wbook.Worksheets.Add("Sản phẩm");
            List<SanPham> lSP = SanPhamDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã sản phẩm";
            ws.Cell(1, 3).Value = "Tên sản phẩm";
            ws.Cell(1, 4).Value = "Size";
            ws.Cell(1, 5).Value = "Màu";
            ws.Cell(1, 6).Value = "Tồn kho";
            ws.Cell(1, 7).Value = "URL ảnh";
            ws.Cell(1, 8).Value = "Ghi chú";
            for (int i = 0; i < lSP.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lSP[i].Ma;
                ws.Cell(i + 2, 3).Value = lSP[i].Ten;
                ws.Cell(i + 2, 4).Value = lSP[i].Size;
                ws.Cell(i + 2, 5).Value = lSP[i].Mau;
                ws.Cell(i + 2, 6).Value = lSP[i].TonKho;
                ws.Cell(i + 2, 8).Value = lSP[i].Url;
                ws.Cell(i + 2, 9).Value = lSP[i].GhiChu;
            }

            ws = wbook.Worksheets.Add("Đơn hàng");
            List<DonHang> lDH = DonHangDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã đơn hàng";
            ws.Cell(1, 3).Value = "Mã khách hàng";
            ws.Cell(1, 4).Value = "Ngày đặt";
            ws.Cell(1, 5).Value = "Ngày hẹn giao";
            ws.Cell(1, 6).Value = "Ngày giao";
            ws.Cell(1, 7).Value = "Tổng tiền";
            ws.Cell(1, 8).Value = "Trạng thái";
            for (int i = 0; i < lDH.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lDH[i].Ma;
                ws.Cell(i + 2, 3).Value = lDH[i].MaKH;
                ws.Cell(i + 2, 4).Value = lDH[i].NgayDat;
                ws.Cell(i + 2, 5).Value = lDH[i].NgayHenGiao;
                ws.Cell(i + 2, 6).Value = lDH[i].NgayGiao;
                ws.Cell(i + 2, 7).Value = lDH[i].TongTien;
                ws.Cell(i + 2, 8).Value = lDH[i].TrangThai;
            }

            ws = wbook.Worksheets.Add("Chi tiết đơn hàng");
            List<CTDonHang> lCTDH = CTDonHangDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã chi tiết đơn hàng";
            ws.Cell(1, 3).Value = "Mã đơn hàng";
            ws.Cell(1, 4).Value = "Mã sản phẩm";
            ws.Cell(1, 5).Value = "Màu";
            ws.Cell(1, 6).Value = "Size";
            ws.Cell(1, 7).Value = "Đơn giá";
            ws.Cell(1, 8).Value = "Số lượng đặt";
            ws.Cell(1, 9).Value = "Số lượng giao";
            ws.Cell(1, 10).Value = "Chi phí thợ may 1SP";
            ws.Cell(1, 11).Value = "Ghi chú";
            for (int i = 0; i < lCTDH.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lCTDH[i].MaCTDH;
                ws.Cell(i + 2, 3).Value = lCTDH[i].MaDH;
                ws.Cell(i + 2, 4).Value = lCTDH[i].MaSP;
                ws.Cell(i + 2, 5).Value = lCTDH[i].Mau;
                ws.Cell(i + 2, 6).Value = lCTDH[i].Size;
                ws.Cell(i + 2, 7).Value = lCTDH[i].DonGia;
                ws.Cell(i + 2, 8).Value = lCTDH[i].SoLuongDat;
                ws.Cell(i + 2, 9).Value = lCTDH[i].SoLuongGiao;
                ws.Cell(i + 2, 10).Value = lCTDH[i].ChiPhiThoMay;
                ws.Cell(i + 2, 11).Value = lCTDH[i].GhiChu;
            }

            ws = wbook.Worksheets.Add("Phân công");
            List<PhanCong> lPC = PhanCongDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã phân công";
            ws.Cell(1, 3).Value = "Mã sản phẩm";
            ws.Cell(1, 4).Value = "Mã thợ may";
            ws.Cell(1, 5).Value = "Tiền công 1 sản phẩm";
            ws.Cell(1, 6).Value = "Số lượng phân công";
            ws.Cell(1, 7).Value = "Số lượng hoàn thành";
            ws.Cell(1, 8).Value = "Số lượng còn";
            ws.Cell(1, 9).Value = "Ghi chú";
            ws.Cell(1, 10).Value = "Trạng thái";
            for (int i = 0; i < lPC.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lPC[i].MaPC;
                ws.Cell(i + 2, 3).Value = lPC[i].MaSP;
                ws.Cell(i + 2, 4).Value = lPC[i].MaNV;
                ws.Cell(i + 2, 5).Value = lPC[i].TienCong1SP;
                ws.Cell(i + 2, 6).Value = lPC[i].SoLuongPhanCong;
                ws.Cell(i + 2, 7).Value = lPC[i].SoLuongHoanThanh;
                ws.Cell(i + 2, 8).Value = lPC[i].SoLuongCon;
                ws.Cell(i + 2, 9).Value = lPC[i].GhiChu;
                ws.Cell(i + 2, 10).Value = lPC[i].TrangThai;
            }

            ws = wbook.Worksheets.Add("Chi phí đơn hàng");
            List<ChiPhiVatTu> lCPDH = ChiPhiVatTuDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã chi phí";
            ws.Cell(1, 3).Value = "Mã đơn/Phân đơn";
            ws.Cell(1, 4).Value = "Tên chi phí";
            ws.Cell(1, 5).Value = "Phân loại";
            ws.Cell(1, 6).Value = "Số tiền";
            ws.Cell(1, 7).Value = "Ghi chú";
            for (int i = 0; i < lCPDH.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lCPDH[i].MaCP;
                ws.Cell(i + 2, 3).Value = lCPDH[i].MaD;
                ws.Cell(i + 2, 4).Value = lCPDH[i].TenCP;
                ws.Cell(i + 2, 5).Value = lCPDH[i].PhanLoai;
                ws.Cell(i + 2, 6).Value = lCPDH[i].SoTien;
                ws.Cell(i + 2, 7).Value = lCPDH[i].GhiChu;
            }

            ws = wbook.Worksheets.Add("Chi phí khác");
            List<ChiPhiRieng> lCPK = ChiPhiRiengDAO.Instance.loadDS();
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Mã chi phí";
            ws.Cell(1, 3).Value = "Tên chi phí";
            ws.Cell(1, 4).Value = "Phân loại";
            ws.Cell(1, 5).Value = "Số tiền";
            ws.Cell(1, 6).Value = "Ngày chi";
            ws.Cell(1, 7).Value = "Ghi chú";
            for (int i = 0; i < lCPK.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = i + 1;
                ws.Cell(i + 2, 2).Value = lCPK[i].MaCp;
                ws.Cell(i + 2, 3).Value = lCPK[i].TenCp;
                ws.Cell(i + 2, 4).Value = lCPK[i].PhanLoai;
                ws.Cell(i + 2, 5).Value = lCPK[i].SoTien;
                ws.Cell(i + 2, 6).Value = lCPK[i].NgayChi;
                ws.Cell(i + 2, 7).Value = lCPK[i].GhiChu;
            }
            try
            {
                wbook.SaveAs(filepath);
                MessageBox.Show("Xuất file Excel thành công\nĐường dẫn : "+filepath, "Thông báo");
            }
            catch
            {
                MessageBox.Show("Xuất file Excel thất bại !", "Thông báo");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn File BackUp";
            openFileDialog1.Filter = "Windows Bitmap|*.bak|All Files|*.*";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbTenFile.Text = openFileDialog1.FileName;
            }
        }
    }

}