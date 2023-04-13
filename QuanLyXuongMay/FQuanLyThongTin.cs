using QuanLyXuongMay.DAO;
using QuanLyXuongMay.DTO;
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
    public partial class FQuanLyThongTin : Form
    {
        BindingSource dsNVLC = new BindingSource();
        BindingSource dsNVSP = new BindingSource();
        BindingSource dsSP = new BindingSource();
        String dd = "";
        public FQuanLyThongTin()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadDSNVLC();
            loadDSNVSP();
            loadDSSP();
            dgvNhanVienLuongCung.DataSource = dsNVLC;
            dgvNVSP.DataSource = dsNVSP;
            dgvSanPham.DataSource = dsSP;
            loadThongTinNVLC();
            loadThongTinNVSP();
            loadThongTinSP();
            loadMau();
            loadSize();
        }
        void loadMau()
        {
            List<DTO.Mau> lm = MauDAO.Instance.loadDSMau();
            cbxMau.DataSource = lm;
            cbxMau.DisplayMember = "TenMau";
            cbxMauSP.DataSource = lm;
        }
        void loadSize()
        {
            List<DTO.Size> lm = SizeDAO.Instance.loadDSSize();
            cbxSize.DataSource = lm;
            cbxSize.DisplayMember = "TenSize";
            cbxSizeSP.DataSource = lm;
            cbxSizeSP.DisplayMember = "TenSize";
        }
        void loadThongTinSP()
        {
            tbMaSP.DataBindings.Add(new Binding("Text", dgvSanPham.DataSource, "Mã"));
            tbTenSP.DataBindings.Add(new Binding("Text", dgvSanPham.DataSource, "Tên"));
            cbxMauSP.DataBindings.Add(new Binding("Text", dgvSanPham.DataSource, "Màu"));
            cbxSizeSP.DataBindings.Add(new Binding("Text", dgvSanPham.DataSource, "Size"));
            nudGiaSP.DataBindings.Add(new Binding("Value", dgvSanPham.DataSource, "Giá"));
            tbUrl.DataBindings.Add(new Binding("Text", dgvSanPham.DataSource, "URL"));
            ptbAnhSP.DataBindings.Add(new Binding("Text", dgvSanPham.DataSource, "URL"));
        }
        void loadThongTinNVSP()
        {
            tbMaNVSP.DataBindings.Add(new Binding("Text", dgvNVSP.DataSource, "Mã"));
            tbHoTenNVSP.DataBindings.Add(new Binding("Text", dgvNVSP.DataSource, "Họ tên"));
            tbSDTNVSP.DataBindings.Add(new Binding("Text", dgvNVSP.DataSource, "SĐT"));
        }
        void loadThongTinNVLC()
        {
            tbMaNVLC.DataBindings.Add(new Binding("Text", dgvNhanVienLuongCung.DataSource, "Mã"));
            TBHoTenNVLC.DataBindings.Add(new Binding("Text", dgvNhanVienLuongCung.DataSource, "Họ tên"));
            tbSDTNVLC.DataBindings.Add(new Binding("Text", dgvNhanVienLuongCung.DataSource, "SĐT"));
            nudLuongNVLC.DataBindings.Add(new Binding("Value", dgvNhanVienLuongCung.DataSource, "Lương"));
        }
        void loadDSSP()
        {
            string query = "SELECT MaSP AS[Mã],TenSP AS[Tên],Mau AS[Màu],Size AS[Size] ,DonGia AS[Giá],AnhURL AS[URL] FROM SANPHAM";
            dsSP.DataSource = DataProvider.Instance.RunQuery(query);
        }
        public void loadDSNVLC()
        {
            string query = "SELECT MaNVLC AS[Mã],HoTen AS[Họ tên],SDT AS[SĐT],Luong AS[Lương] FROM NV_LUONGCUNG";
            dsNVLC.DataSource = DataProvider.Instance.RunQuery(query);
        }
        public void loadDSNVSP()
        {
            string query = "SELECT MaNVSP AS[Mã],HoTen AS[Họ tên],SDT AS[SĐT] FROM NV_SANPHAM";
            dsNVSP.DataSource = DataProvider.Instance.RunQuery(query);
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btCapNhatNV_Click(object sender, EventArgs e)
        {
            string ma = tbMaNVLC.Text;
            if(ma==null||ma=="")
            {
                MessageBox.Show("Hãy chọn nhân viên cần cập nhật thông tin trước !", "Thông báo");
            }
            else
            {
                string hoTen = TBHoTenNVLC.Text;
                string sdt = tbSDTNVLC.Text;
                int luong = (int)nudLuongNVLC.Value;
                if(hoTen==null||hoTen==""||sdt == null || sdt == "")
                {
                    MessageBox.Show("Vui lòng nhập thông tin cần cập nhật !", "Thông báo");
                }
                else
                {
                    NhanVienDAO.Instance.CapNhatThongTinNV(ma, hoTen, sdt, luong);
                    MessageBox.Show("Cập nhật thông tin thành công !", "Thông báo");
                    loadDSNVLC();
                }
            } 
        }

        private void btThemNVLC_Click(object sender, EventArgs e)
        {
            string hoTen = TBHoTenNVLC.Text;
            string sdt = tbSDTNVLC.Text;
            int luong = (int)nudLuongNVLC.Value;
            if (hoTen != null && hoTen != "" && sdt != null && sdt != "")
            {
                NhanVienDAO.Instance.themNhanVien(hoTen, sdt, luong);
                MessageBox.Show("Thêm nhân viên lương cứng thành công !", "Thông báo");
                loadDSNVLC();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin !", "Thông báo");
        }

        private void btXoaNVLC_Click(object sender, EventArgs e)
        {
            string ma = tbMaNVLC.Text;
            if (ma == null || ma == "")
            {
                MessageBox.Show("Hãy chọn nhân viên cần xóa thông tin trước !", "Thông báo");
            }
            else
            {
                if (MessageBox.Show("Xác nhận xóa nhân viên ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    NhanVienDAO.Instance.XoaNhanVienLC(ma);
                    MessageBox.Show("Xóa nhân viên thành công !", "Thông báo");
                    loadDSNVLC();
                }
            }
        }

        

        private void btThemSP_Click(object sender, EventArgs e)
        {
            string ten = tbTenSP.Text;
            string mau = cbxMauSP.Text;
            string size = cbxSizeSP.Text;
            int gia = (int)nudGiaSP.Value;
            if (ten==null||mau==null||size==null)
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin trước !", "Thông báo");
            }
            else
            {
                String temp = tbUrl.Text;
                String newFilename = @"C:\\AnhNhanVienQuanLyXuongMay\temp" + Path.GetExtension(tbUrl.Text);
                string ma = "";
                DataTable d=DataProvider.Instance.RunQuery("SELECT *FROM SANPHAM WHERE MaSP = (SELECT MAX(MaSP) FROM SANPHAM)");
                foreach(DataRow item in d.Rows)
                {
                    ma = item["MaSP"].ToString();
                }
                newFilename = @"C:\\AnhNhanVienQuanLyXuongMay\" + ma + Path.GetExtension(temp);
                if (File.Exists(newFilename))
                {

                    File.Delete(newFilename);
                }
                if (File.Exists(temp))
                {
                    if (newFilename != String.Empty)
                    {
                        File.Move(temp, newFilename);
                    }
                }
                string q = "UPDATE dbo.SANPHAM SET AnhURL=N'" + newFilename + "' WHERE MaSP = N'" + ma + "'";
                int kq = DataProvider.Instance.RunNonQuery(q);
                MessageBox.Show("Thêm sản phẩm thành công !", "Thông báo");
                loadDSSP();
            }
        }

        private void btChonAnh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn ảnh";
            openFileDialog1.Filter = "Windows Bitmap|*.bmp|JPEG Image|*.jpg|All Files|*.*";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbUrl.Text = openFileDialog1.FileName;
            }
        }

        private void btCapNhatSP_Click(object sender, EventArgs e)
        {
            string ma = tbMaSP.Text;
            if (ma == null || ma == "")
            {
                MessageBox.Show("Hãy chọn nhân viên cần cập nhật thông tin trước !", "Thông báo");
            }
            else
            {
                string ten = tbTenSP.Text;
                string mau = cbxMauSP.Text;
                string size = cbxSizeSP.Text;
                int gia = (int)nudGiaSP.Value;
                if (ten == null || mau == null || size == null)
                {
                    MessageBox.Show("Vui lòng nhập thông tin cần cập nhật !", "Thông báo");
                }
                else
                {
                    String newFilename = @"C:\\AnhNhanVienQuanLyXuongMay\" + tbMaSP.Text + Path.GetExtension(tbUrl.Text);
                    if (File.Exists(newFilename))
                    {

                        File.Delete(newFilename);
                    }
                    chuyenFile();
                    SanPhamDAO.Instance.CapNhatThongTinSP(ma, ten, mau, size, newFilename, gia);
                    MessageBox.Show("Cập nhật thông tin thành công !", "Thông báo");
                    loadDSSP();
                }
            }
        }
        private void btXoaSP_Click(object sender, EventArgs e)
        {
            string ma = tbMaSP.Text;
            if (ma == null || ma == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm cần xóa thông tin trước !", "Thông báo");
            }
            else
            {
                if (MessageBox.Show("Xác nhận xóa sản phẩm ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String Filename = @"C:\\AnhNhanVienQuanLyXuongMay\" + tbMaSP.Text + Path.GetExtension(tbUrl.Text);
                    if (File.Exists(Filename))
                    {
                        
                        File.Delete(Filename);
                    }
                    MessageBox.Show("Xóa sản phẩm thành công !", "Thông báo");
                    loadDSSP();
                }
            }
        }
        private void chuyenFile()
        {
            String filePath = tbUrl.Text;
            if (File.Exists(filePath))
            {
                String newFilename = @"C:\\AnhNhanVienQuanLyXuongMay\" + tbMaSP.Text + Path.GetExtension(tbUrl.Text);
                if (newFilename != String.Empty)
                {
                    File.Move(filePath, newFilename);
                }
            }
        }
        private void btXemAnh_Click(object sender, EventArgs e)
        {
            FAnhSanPham fa = new FAnhSanPham(tbUrl.Text);
            this.Hide();
            fa.ShowDialog();
            this.Show();
        }
    }
}
