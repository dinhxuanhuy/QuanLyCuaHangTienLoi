using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCNhanVien : UserControl
    {
        // Biến toàn cục
        bool isLoading, Them;
        bool txtTimKiem_DaClick = false;
        string gioiTinhChon;
        string chucVuChon;

        public BALNhanVien dbnv = null;
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        public UCNhanVien()
        {
            InitializeComponent();
            UIStyles.ApplyUIStyle(this);
            dbnv = new BALNhanVien();
            LoadGioiTinh();
            LoadChucVu();
            LoadData();
        }

        // --- CÁC HÀM HỖ TRỢ LOAD DỮ LIỆU (Giữ nguyên) ---

        void LoadGioiTinh()
        {
            isLoading = true;
            DataTable dtGT = new DataTable();
            dtGT.Columns.Add("GioiTinh");
            dtGT.Rows.Add("Nam");
            dtGT.Rows.Add("Nữ");
            cboGioiTinh.DataSource = dtGT;
            cboGioiTinh.DisplayMember = "GioiTinh";
            cboGioiTinh.ValueMember = "GioiTinh";
            cboGioiTinh.SelectedIndex = -1;
            isLoading = false;
        }

        void LoadChucVu()
        {
            isLoading = true;
            try
            {
                DataTable dtCV = dbnv.LayChucVu();
                if (dtCV.Rows.Count > 0)
                {
                    cboChucVu.DataSource = dtCV;
                    cboChucVu.DisplayMember = "ChucVu";
                    cboChucVu.ValueMember = "ChucVu";
                    cboChucVu.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chức vụ: " + ex.Message);
            }
            isLoading = false;
        }

        void LoadData()
        {
            try
            {
                dgvNhanVien.ReadOnly = true;
                dgvNhanVien.AllowUserToAddRows = false;
                dgvNhanVien.AllowUserToDeleteRows = false;
                dgvNhanVien.MultiSelect = false;

                DataTable dtNhanVien = dbnv.LayNhanVien();
                dgvNhanVien.DataSource = dtNhanVien;

                ResetText();

                cboGioiTinh.Enabled = false;
                cboChucVu.Enabled = false;

                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                if (dgvNhanVien.Rows.Count > 0)
                {
                    var args = new DataGridViewCellEventArgs(0, 0);
                    dgvNhanVien_CellClick(dgvNhanVien, args);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong bảng Nhân Viên!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ResetText()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtNgaySinh.Clear();
            cboGioiTinh.SelectedIndex = -1;
            txtDiaChi.Clear();
            txtSDT.Clear();
            cboChucVu.SelectedIndex = -1;
            txtNgayTuyenDung.Clear();
        }

        // --- XỬ LÝ SỰ KIỆN CELL CLICK (Đồng bộ hiển thị ngày) ---

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.RowCount) return;

            int r = e.RowIndex;

            try
            {
                if (dgvNhanVien.Rows[r].DataBoundItem == null || dgvNhanVien.Rows[r].IsNewRow) return;

                DataRow row = ((DataRowView)dgvNhanVien.Rows[r].DataBoundItem).Row;

                // Load TextBoxes
                txtMaNV.Text = row["MaNV"]?.ToString() ?? ""; // MaNV giờ là chuỗi
                txtHoTen.Text = row["HoTen"]?.ToString() ?? "";
                txtDiaChi.Text = row["DiaChi"]?.ToString() ?? "";
                txtSDT.Text = row["SDT"]?.ToString() ?? "";

                // Load Ngày Tuyển Dụng (dd/MM/yyyy)
                object ngayTD = row["NgayTuyenDung"];
                txtNgayTuyenDung.Text = (ngayTD != null && ngayTD != DBNull.Value)
                    ? Convert.ToDateTime(ngayTD).ToString("dd/MM/yyyy") : "";

                // Load Ngày Sinh (dd/MM/yyyy)
                object ngaySinhObj = row["NgaySinh"];
                txtNgaySinh.Text = (ngaySinhObj != null && ngaySinhObj != DBNull.Value)
                    ? Convert.ToDateTime(ngaySinhObj).ToString("dd/MM/yyyy") : "";

                // Load ComboBox (Giữ nguyên logic FindStringExact)
                string gioiTinh = row["GioiTinh"]?.ToString().Trim();
                if (!string.IsNullOrEmpty(gioiTinh))
                {
                    int idx = cboGioiTinh.FindStringExact(gioiTinh);
                    cboGioiTinh.SelectedIndex = idx;
                }
                else cboGioiTinh.SelectedIndex = -1;

                string chucVu = row["ChucVu"]?.ToString().Trim();
                if (!string.IsNullOrEmpty(chucVu))
                {
                    int idx = cboChucVu.FindStringExact(chucVu);
                    cboChucVu.SelectedIndex = idx;
                }
                else cboChucVu.SelectedIndex = -1;

                gioiTinhChon = cboGioiTinh.Text;
                chucVuChon = cboChucVu.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        // --- XỬ LÝ CÁC NÚT BẤM CRUD ---

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            ResetText();

            // Mở khóa nhập liệu
            cboGioiTinh.Enabled = true;
            cboChucVu.Enabled = true;

            txtMaNV.Enabled = false; // ⬅️ KHÓA NHẬP MÃ NV (TRIGGER sẽ sinh)
            txtMaNV.Text = "(Tự động sinh)";

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentCell == null || dgvNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            Them = false;
            var args = new DataGridViewCellEventArgs(dgvNhanVien.CurrentCell.ColumnIndex, dgvNhanVien.CurrentCell.RowIndex);
            dgvNhanVien_CellClick(dgvNhanVien, args);

            // Mở khóa nhập liệu (TRỪ MÃ NV)
            cboGioiTinh.Enabled = true;
            cboChucVu.Enabled = true;
            txtMaNV.Enabled = false; // Mã NV luôn bị khóa

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy Mã NV trực tiếp từ TextBox (đã được load từ DGV)
                string maNV = txtMaNV.Text.Trim();

                // 🚨 Kiểm tra an toàn
                if (string.IsNullOrWhiteSpace(maNV) || maNV.Equals("(Tự động sinh)", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên hợp lệ có Mã để xóa.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Hỏi xác nhận
                if (MessageBox.Show($"Chắc chắn xóa nhân viên {maNV}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string err = "";
                    bool f = dbnv.XoaNhanVien(ref err, maNV);
                    if (f)
                    {
                        LoadData();
                        MessageBox.Show("Đã xóa thành công!");
                    }
                    else
                    {
                        // Nếu Xóa thất bại, lỗi có thể là FK hoặc lỗi SP chưa nhận NVARCHAR
                        MessageBox.Show("Không xóa được!\nLỗi: " + (string.IsNullOrEmpty(err) ? "Lỗi SQL: SP chưa được cập nhật thành NVARCHAR." : err), "Lỗi Hệ thống");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validation cơ bản
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Thiếu họ tên!"); txtHoTen.Focus(); return; }
            // Bỏ kiểm tra Mã NV khi Them=true
            if (!Them && string.IsNullOrWhiteSpace(txtMaNV.Text)) { MessageBox.Show("Thiếu mã NV khi cập nhật!"); txtMaNV.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text)) { MessageBox.Show("Thiếu địa chỉ!"); txtDiaChi.Focus(); return; }

            // Validate SĐT
            if (!decimal.TryParse(txtSDT.Text, out decimal soDT) || soDT < 0)
            {
                MessageBox.Show("SĐT sai định dạng!"); txtSDT.Focus(); return;
            }

            // --- ĐỒNG BỘ LOGIC NGÀY THÁNG ---
            DateTime ngaySinh;
            DateTime ngayTuyenDung;
            string ngaySinhStr = txtNgaySinh.Text.Trim();
            string ngayTuyenDungStr = txtNgayTuyenDung.Text.Trim();

            // 1. NGÀY SINH (TryParseExact)
            if (!DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
            {
                MessageBox.Show("Định dạng Ngày Sinh không hợp lệ! Vui lòng dùng dd/MM/yyyy.", "Lỗi Lưu");
                txtNgaySinh.Focus();
                return;
            }

            // 2. NGÀY TUYỂN DỤNG (TryParseExact)
            if (!DateTime.TryParseExact(ngayTuyenDungStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayTuyenDung))
            {
                MessageBox.Show("Định dạng Ngày Tuyển Dụng không hợp lệ! Vui lòng dùng dd/MM/yyyy.", "Lỗi Lưu");
                txtNgayTuyenDung.Focus();
                return;
            }

            // 3. Validate logic ngày tháng
            if (ngayTuyenDung.Date <= ngaySinh.Date)
            {
                MessageBox.Show("Ngày tuyển dụng phải lớn hơn ngày sinh!");
                txtNgayTuyenDung.Focus();
                return;
            }

            // --- XỬ LÝ BAL ---
            string err = "";
            bool f;

            if (Them)
            {
                // 🚨 KHÔNG TRUYỀN MA NV KHI THÊM
                f = dbnv.ThemNhanVien(ref err,
                    txtHoTen.Text.Trim(),
                    ngaySinh,
                    cboGioiTinh.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSDT.Text.Trim(),
                    cboChucVu.Text.Trim(),
                    ngayTuyenDung
                );
                if (f) MessageBox.Show("Thêm thành công!");
            }
            else // Sửa
            {
                // TRUYỀN MA NV KHI SỬA
                f = dbnv.CapNhatNhanVien(ref err,
                    txtMaNV.Text.Trim(), // ⬅️ Lấy Mã NV (chuỗi)
                    txtHoTen.Text.Trim(),
                    ngaySinh,
                    cboGioiTinh.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSDT.Text.Trim(),
                    cboChucVu.Text.Trim(),
                    ngayTuyenDung
                );
                if (f) MessageBox.Show("Cập nhật thành công!");
            }

            if (f) LoadData();
            else MessageBox.Show("Thất bại: " + err);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetText();
            txtTimKiem.Text = "Nhập tên nhân viên";
            txtTimKiem.ForeColor = Color.Gray;
            txtTimKiem_DaClick = false;
            LoadData();
        }

        // --- CÁC SỰ KIỆN KHÁC ---

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucQuanLyDuLieu;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            if (dgvNhanVien.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.Format("HoTen LIKE '%{0}%'", txtTimKiem.Text.Trim());
            }
        }
    }
}