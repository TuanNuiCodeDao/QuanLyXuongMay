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
    public partial class FNhanVien : Form
    {
        public FNhanVien()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            loadCBXLoai();
            loadNV();
        }
        void loadCBXLoai()
        {
            cbxLoai.Items.Clear();
            cbxLoai.Items.Add("Nhân viên lương cứng");
            cbxLoai.Items.Add("Nhân viên sản phẩm");
            cbxLoai.Text = "Nhân viên lương cứng";
        }
        void loadNV()
        {
            lvNhanVien.Items.Clear();
            List<NhanVien> l = NhanVienDAO.Instance.loadDSNhanVien();
            int stt = 0;
            foreach (NhanVien item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.HoTen.ToString());
                lv.SubItems.Add(item.Sdt.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(item.ToDoi.ToString());
                lv.SubItems.Add(item.Luong.ToString());
                lvNhanVien.Items.Add(lv);
            }
            lvNhanVien.FullRowSelect = true;
            lvNhanVien.Show();
        }

        private void lvNhanVien_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void lvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lvNhanVien.SelectedItems)
            {
                tbMa.Text = items.SubItems[1].Text;
                tbHoTen.Text = items.SubItems[2].Text;
                tbSDT.Text = items.SubItems[3].Text;
                cbxLoai.Text = items.SubItems[4].Text;
                tbToDoi.Text = items.SubItems[5].Text;
                string luong = items.SubItems[6].Text.ToString();
                nudLuong.Value = int.Parse(luong);
            }
        }
        bool ktrSDT(string sdt)
        {
            for (int i = 0; i < sdt.Length; i++)
                if (sdt[i] <'0'|| sdt[i] > '9')
                    return false;
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string hoTen = tbHoTen.Text;
            string sdt = tbSDT.Text;
            string phanLoai = cbxLoai.Text;
            string toDoi = tbToDoi.Text;
            int luong = (int)nudLuong.Value;
            if (toDoi == null || toDoi == "")
                toDoi = "Empty";
            if (ktrSDT(sdt)==false)
                sdt = "Empty";
            if (sdt != "Empty")
            {
                DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM NHANVIEN WHERE SDT=N'" + sdt + "'");
                int i = 0;
                foreach (DataRow item in d.Rows)
                    i++;
                if (i >0)
                {
                    MessageBox.Show("Số điện thoại " + sdt + " đã được nhân viên khác sử dụng !", "Thông báo");
                    return;
                }
            }
            if (phanLoai == "Nhân viên sản phẩm")
                luong = 0;
            if (hoTen != null && hoTen != "")
            {
                DataProvider.Instance.RunQuery("INSERT dbo.NHANVIEN(HoTen,SDT,PhanLoai,ToDoi,Luong) VALUES(N'" + hoTen + "',N'" + sdt + "',N'"+phanLoai+"',N'" + toDoi + "'," + luong + ")");
                loadNV();
            }
            else MessageBox.Show("Vui lòng nhập đủ họ tên !", "Thông báo");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(lvNhanVien.SelectedItems.Count==0)
            {
                MessageBox.Show("Hãy chọn nhân viên cần xóa !", "Thông báo");
                return;
            }
            ListViewItem items = lvNhanVien.SelectedItems[0];
            if (MessageBox.Show("Xác nhận xóa nhân viên có mã "+ items.SubItems[1].Text + "?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.RunQuery("DELETE FROM NHANVIEN WHERE MaNV = N'" + items.SubItems[1].Text + "'");
                loadNV();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ma = tbMa.Text;
            if (ma == null || ma == "")
            {
                MessageBox.Show("Hãy chọn nhân viên trước !", "Thông báo");
                return;
            }
            string hoTen = tbHoTen.Text;
            string sdt = tbSDT.Text;
            string phanLoai = cbxLoai.Text;
            string toDoi = tbToDoi.Text;
            int luong = (int)nudLuong.Value;
            if (toDoi == null || toDoi == "")
                toDoi = "Empty";
            if (ktrSDT(sdt) == false)
                sdt = "Empty";
            if (sdt != "Empty")
            {
                DataTable d = DataProvider.Instance.RunQuery("SELECT * FROM NHANVIEN WHERE SDT=N'" + sdt + "' AND MaNV!=N'"+ma+"'");
                int i = 0;
                foreach (DataRow item in d.Rows)
                    i++;
                if (i > 0)
                {
                    MessageBox.Show("Số điện thoại " + sdt + " đã được nhân viên khác sử dụng !", "Thông báo");
                    return;
                }
            }
            if (phanLoai == "Nhân viên sản phẩm")
                luong = 0;
            if (hoTen != null && hoTen != "")
            {
                DataProvider.Instance.RunQuery("UPDATE NHANVIEN SET HoTen=N'" + hoTen + "',SDT=N'" + sdt + "',PhanLoai=N'" + phanLoai + "',ToDoi=N'" + toDoi + "',Luong=" + luong + " WHERE MaNV=N'"+ma+"'");
                loadNV();
            }
            else MessageBox.Show("Vui lòng nhập đủ họ tên !", "Thông báo");
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void nudLuong_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbxLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbToDoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            NhanVien nv = NhanVienDAO.Instance.getNhanVienByMa(tbMa.Text);
            if (nv==null)
            {
                MessageBox.Show("Hãy chọn nhân viên cần trả lương !", "Thông báo");
                return;
            }
            if(nv.PhanLoai=="Nhân viên lương cứng")
            {
                FTraLuongNhanVienLuongCung fTl = new FTraLuongNhanVienLuongCung(nv.Ma);
                fTl.ShowDialog();
            }
            else
            {
                FTraLuongNhanVienSanPham fTl = new FTraLuongNhanVienSanPham(nv.Ma);
                fTl.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbTim_Resize(object sender, EventArgs e)
        {
            
        }

        private void tbTim_TextChanged(object sender, EventArgs e)
        {
            if (tbTim.Text == " " || tbTim.Text == "All" || tbTim.Text == "ALL" || tbTim.Text == "all" || tbTim.Text == "")
            {
                loadNV();
                return;
            }
            lvNhanVien.Items.Clear();
            List<NhanVien> l = NhanVienDAO.Instance.loadDSTimNhanVien(tbTim.Text);
            int stt = 0;
            foreach (NhanVien item in l)
            {
                stt++;
                ListViewItem lv = new ListViewItem(stt.ToString());
                lv.SubItems.Add(item.Ma.ToString());
                lv.SubItems.Add(item.HoTen.ToString());
                lv.SubItems.Add(item.Sdt.ToString());
                lv.SubItems.Add(item.PhanLoai.ToString());
                lv.SubItems.Add(item.ToDoi.ToString());
                lv.SubItems.Add(item.Luong.ToString());
                lvNhanVien.Items.Add(lv);
            }
            lvNhanVien.FullRowSelect = true;
            lvNhanVien.Show();
        }
    }
}
