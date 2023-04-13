using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class FTraLuongNhanVienLuongCung : Form
    {
        private NhanVien nv;
        public FTraLuongNhanVienLuongCung(string maNv)
        {
            InitializeComponent();
            nv = NhanVienDAO.Instance.getNhanVienByMa(maNv);
            load();
        }
        void load()
        {
            tbHoTen.Text = nv.HoTen;
            tbMa.Text = nv.Ma;
            tbLuong.Text = String.Format("{0:###,###,##0}", nv.Luong) + " VNĐ";
            tbPhanLoai.Text = nv.PhanLoai;
            tbToDoi.Text = nv.ToDoi;
            tbSDT.Text = nv.Sdt;
            ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getLanTraLuongCuoiCungTheoMaNV(nv.Ma);
            if (cp == null)
                tblastTime.Text = "Chưa có";
                else
                tblastTime.Text = cp.NgayChi+"";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận trả lương cho nhân viên  ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("INSERT dbo.CHIPHIRIENG(TenCP,PhanLoai,SoTien,GhiChu) VALUES(N'"+nv.Ma+"',N'Trả lương nhân viên',"+nv.Luong+",N'"+tbGhiChu.Text+"')");
                ChiPhiRieng cp = ChiPhiRiengDAO.Instance.getLast();
                DataProvider.Instance.RunQuery("INSERT dbo.THUCHI(Loai,NoiDung,SoTien,MaNoi) VALUES(N'Chi',N'Trả lương nhân viên - "+cp.TenCp+" - " + cp.MaCp + "'," + nv.Luong + ",N'CPR" + cp.MaCp + "')");
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
