using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCCaLamViecDieuChinh : UserControl
    {
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        BALQuanLyCa dbcl;
        bool themMoi = false;
        bool sua = false;

        public UCCaLamViecDieuChinh()
        {
            InitializeComponent();
            dbcl = new BALQuanLyCa();
            LoadDuLieuBuoi();
            LoadDuLieuNhanVien();
            SetControls(false);
            LoadData();
        }
        void LoadData()
        {
            try
            {
                // Cấu hình DGV
                dgv_caLamViec.ReadOnly = true;
                dgv_caLamViec.AllowUserToAddRows = false;
                dgv_caLamViec.MultiSelect = false;

                DataTable dt = dbcl.CaLamViec();
                dgv_caLamViec.DataSource = dt;

                if (dgv_caLamViec.Rows.Count > 0)
                {
                    dgv_caLamViec_CellClick(dgv_caLamViec, new DataGridViewCellEventArgs(0, 0));
                }
                else
                {
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu ca làm việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearControls();
            }
        }

        private void LoadDuLieuBuoi()
        {
            List<string> danhSachBuoi = new List<string> {
                "Sáng", "Trưa", "Chiều"};
            cbb_buoi.DataSource = danhSachBuoi;
            cbb_buoi.SelectedIndex = -1;
        }

        private void LoadDuLieuNhanVien()
        {
            DataTable dtNV = dbcl.LayDSNhanVien();
            cbb_tenNV.DataSource = dtNV;
            cbb_tenNV.DisplayMember = "HoTen";
            cbb_tenNV.ValueMember = "MaNV";
            cbb_tenNV.SelectedIndex = -1;
        }

        private void SetControls(bool status)
        {
            txt_maCa.ReadOnly = true;
            txt_maNV.ReadOnly = true;

            txt_ngay.ReadOnly = !status;
            cbb_buoi.Enabled = status;
            cbb_tenNV.Enabled = status;
        }

        private void ClearControls()
        {
            txt_maCa.Clear();
            txt_ngay.Clear();
            cbb_buoi.SelectedIndex = -1;
            txt_maNV.Clear();
            cbb_tenNV.SelectedIndex = -1;
        }

        // Xử lý khi chọn Tên NV -> Tự động load Mã NV (NVARCHAR)
        private void cbb_tenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_tenNV.SelectedIndex != -1 && cbb_tenNV.SelectedValue != null)
            {
                txt_maNV.Text = cbb_tenNV.SelectedValue.ToString();
            }
            else
            {
                txt_maNV.Clear();
            }
        }

        // Đổ dữ liệu từ DGV lên controls
        private void dgv_caLamViec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_caLamViec.Rows.Count) return;
            DataGridViewRow row = dgv_caLamViec.Rows[e.RowIndex];

            try
            {
                if (row.Cells[0].Value == null || row.Cells[0].Value == DBNull.Value) return;

                txt_maCa.Text = row.Cells[0].Value?.ToString() ?? "";
                txt_ngay.Text = Convert.ToDateTime(row.Cells[1].Value).ToString("dd/MM/yyyy");
                cbb_buoi.Text = row.Cells[2].Value?.ToString() ?? "";

                // FIX: Gán Mã NV (NVARCHAR)
                object maNVValue = row.Cells[3].Value;
                if (maNVValue != null && maNVValue != DBNull.Value)
                {
                    cbb_tenNV.SelectedValue = maNVValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu lên form: " + ex.Message, "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CRUD LOGIC ---

        private void btn_them_Click(object sender, EventArgs e)
        {
            themMoi = true;
            sua = false;
            ClearControls();
            SetControls(true);

            txt_maCa.Text = "(Tự động sinh)";

            btn_them.Enabled = false; btn_sua.Enabled = false; btn_xoa.Enabled = false;
            btn_luu.Enabled = true; btn_huy.Enabled = true;
            txt_ngay.Focus();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dgv_caLamViec.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txt_maCa.Text))
            {
                MessageBox.Show("Vui lòng chọn Ca làm việc cần sửa!", "Lỗi Sửa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sua = true;
            themMoi = false;
            SetControls(true);

            btn_them.Enabled = false; btn_sua.Enabled = false; btn_xoa.Enabled = false;
            btn_luu.Enabled = true; btn_huy.Enabled = true;
            txt_ngay.Focus();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dgv_caLamViec.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txt_maCa.Text))
            {
                MessageBox.Show("Vui lòng chọn Ca làm việc cần xóa!", "Lỗi Xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Xác nhận xóa Ca làm việc Mã {txt_maCa.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string err = "";
                try
                {
                    string maCa = txt_maCa.Text.Trim(); // Mã Ca là NVARCHAR
                    if (dbcl.XoaCa(maCa, ref err))
                    {
                        MessageBox.Show("Xóa Ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_huy_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại. Chi tiết: " + err, "Lỗi Nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: Lỗi kết nối. " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra Validation chung
            if (string.IsNullOrWhiteSpace(txt_ngay.Text) || cbb_buoi.SelectedIndex == -1 || cbb_tenNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin Ngày, Buổi và Tên NV!", "Lỗi Lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string err = "";
            DateTime ngay;

            // Kiểm tra định dạng ngày dd/MM/yyyy
            if (!DateTime.TryParseExact(txt_ngay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay))
            {
                MessageBox.Show("Định dạng Ngày không hợp lệ! Vui lòng dùng định dạng dd/MM/yyyy.", "Lỗi Lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // FIX: Lấy Mã NV là CHUỖI (NVARCHAR)
            string maNV = cbb_tenNV.SelectedValue?.ToString() ?? "";
            string buoi = cbb_buoi.Text;

            // 2. Xử lý Thêm mới hoặc Sửa
            if (themMoi)
            {
                if (dbcl.ThemCa(ngay, buoi, maNV, ref err))
                {
                    MessageBox.Show("Thêm Ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm Ca thất bại. Chi tiết: " + err, "Lỗi Nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (sua)
            {
                string maCa = txt_maCa.Text.Trim();
                if (dbcl.SuaCa(maCa, ngay, buoi, maNV, ref err))
                {
                    MessageBox.Show("Sửa Ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sửa Ca thất bại. Chi tiết: " + err, "Lỗi Nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 3. Reset trạng thái
            btn_huy_Click(sender, e);
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            themMoi = false;
            sua = false;
            ClearControls();
            SetControls(false);

            btn_them.Enabled = true; btn_sua.Enabled = true; btn_xoa.Enabled = true;
            btn_luu.Enabled = false; btn_huy.Enabled = false;

            LoadData();
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucCaLamViec;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
        private void txt_timKiem_Click(object sender, EventArgs e)
        {
            if (txt_timKiem.Text == "Nhập ngày (dd/mm/yyyy)")
            {
                txt_timKiem.Text = "";
                txt_timKiem.ForeColor = Color.Black;
            }
        }

        // 🚨 BỔ SUNG: Hàm xử lý Leave (Placeholder)
        private void txt_timKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_timKiem.Text))
            {
                txt_timKiem.Text = "Nhập ngày (dd/mm/yyyy)";
                txt_timKiem.ForeColor = Color.Gray;
            }
        }
        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgv_caLamViec.DataSource != null)
            {
                DataTable dt = (DataTable)dgv_caLamViec.DataSource;
                string timkiem = txt_timKiem.Text.Trim();
                if (timkiem == "" || timkiem == "Nhập ngày (dd/mm/yyyy)")
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    // Lọc theo NgayThangNam (chuyển sang string), Buoi, hoặc TenNhanVien
                    string filterExpression = string.Format("CONVERT(NgayThangNam, 'System.String') LIKE '%{0}%' OR Buoi LIKE '%{0}%' OR TenNhanVien LIKE '%{0}%'", timkiem);
                    dt.DefaultView.RowFilter = filterExpression;
                }
            }
        }
    }
}