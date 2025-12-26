using BusinessAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCNhaCungCap : UserControl
    {
        bool Them;
        // Loại bỏ biến không dùng
        BALNhaCungCap dbncc = null;

        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // Định nghĩa các Column Names (Đảm bảo khớp với SELECT * FROM NhaCungCap)
        private const string COL_MaNCC = "MaNCC";
        private const string COL_TenNCC = "TenNCC";
        private const string COL_DiaChi = "DiaChi";
        private const string COL_SDT = "SDT";

        public UCNhaCungCap()
        {
            InitializeComponent();
            dbncc = new BALNhaCungCap();
        }

        private void UCNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        new void ResetText()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtMaNCC.Text = "(Tự động sinh)"; // Mã tự sinh
        }

        void LoadData()
        {
            try
            {
                dgvNhaCungCap.ReadOnly = true;
                dgvNhaCungCap.AllowUserToAddRows = false;
                dgvNhaCungCap.AllowUserToDeleteRows = false;
                dgvNhaCungCap.MultiSelect = false;

                dgvNhaCungCap.DataSource = dbncc.LayNCC();

                ResetText();

                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMaNCC.Enabled = false; // Luôn khóa Mã NCC

                // Tự động load dữ liệu dòng đầu tiên nếu có
                if (dgvNhaCungCap.Rows.Count > 0)
                {
                    dgvNhaCungCap_CellClick(dgvNhaCungCap, new DataGridViewCellEventArgs(0, 0));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu Nhà Cung Cấp! Lỗi: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Hàm chính xử lý logic load data lên controls khi click DGV (Dùng DataRow an toàn)
        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhaCungCap.RowCount) return;
            int r = e.RowIndex;

            try
            {
                // Lấy DataRow an toàn
                DataRowView rowView = dgvNhaCungCap.Rows[r].DataBoundItem as DataRowView;
                if (rowView == null) return;
                DataRow row = rowView.Row;

                Func<string, string> getRowValue = (colName) =>
                {
                    object val = row[colName];
                    return (val == null || val == DBNull.Value) ? "" : val.ToString();
                };

                // Load dữ liệu bằng TÊN CỘT (Sử dụng DataRow)
                txtMaNCC.Text = getRowValue(COL_MaNCC);
                txtTenNCC.Text = getRowValue(COL_TenNCC);
                txtDiaChi.Text = getRowValue(COL_DiaChi);
                txtSDT.Text = getRowValue(COL_SDT);
                txtMaNCC.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu Nhà Cung Cấp.\nChi tiết: " + ex.Message, "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            ResetText();

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaNCC.Enabled = false;
            txtMaNCC.Text = "(Tự động sinh)";
            txtTenNCC.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentCell == null || dgvNhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Them = false;

            // Load dữ liệu của hàng đang chọn
            dgvNhaCungCap_CellClick(dgvNhaCungCap, new DataGridViewCellEventArgs(dgvNhaCungCap.CurrentCell.ColumnIndex, dgvNhaCungCap.CurrentCell.RowIndex));

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaNCC.Enabled = false;
            txtTenNCC.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhaCungCap.CurrentCell == null) return;
                int r = dgvNhaCungCap.CurrentCell.RowIndex;

                // Lấy Mã NCC an toàn từ DataRow
                DataRowView rowView = dgvNhaCungCap.Rows[r].DataBoundItem as DataRowView;
                if (rowView == null) return;
                string maNCC = rowView.Row[COL_MaNCC]?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(maNCC))
                {
                    MessageBox.Show("Không tìm thấy Mã NCC hợp lệ để xóa!", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult traloi = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhà cung cấp này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                string err = "";
                if (traloi == DialogResult.Yes)
                {
                    bool f = dbncc.XoaNCC(ref err, maNCC); // MaNCC là NVARCHAR
                    if (f)
                    {
                        LoadData();
                        MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa!\nLỗi: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu!\n" + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // === VALIDATION ===
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text)) { MessageBox.Show("Thiếu Tên nhà cung cấp!"); txtTenNCC.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text)) { MessageBox.Show("Thiếu Địa chỉ!"); txtDiaChi.Focus(); return; }

            // Kiểm tra SDT
            if (!txtSDT.Text.All(char.IsDigit) || txtSDT.Text.Length < 9)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!"); txtSDT.Focus(); return;
            }

            string err = "";
            bool f;

            // Lấy dữ liệu
            string tenNCC = txtTenNCC.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            if (Them)
            {
                // Thêm: Không truyền Mã NCC
                f = dbncc.ThemNCC(ref err, tenNCC, diaChi, sdt);
                if (f) MessageBox.Show("Thêm mới nhà cung cấp thành công!");
                else MessageBox.Show("Không thể thêm!\nLỗi: " + err);
            }
            else
            {
                // Cập nhật: Cần truyền Mã NCC
                string maNCC = txtMaNCC.Text.Trim();
                f = dbncc.CapNhatNCC(ref err, maNCC, tenNCC, diaChi, sdt);
                if (f) MessageBox.Show("Cập nhật thông tin thành công!");
                else MessageBox.Show("Không thể cập nhật!\nLỗi: " + err);
            }

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadData(); // Tải lại dữ liệu
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetText();

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            txtTimKiem.Text = "Nhập tên nhà cung cấp";
            txtTimKiem.ForeColor = Color.Gray;

            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.DataSource is DataTable dt)
                dt.DefaultView.RowFilter = string.Format("TenNCC LIKE '%{0}%'", txtTimKiem.Text);
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucQuanLyDuLieu;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
    }
}