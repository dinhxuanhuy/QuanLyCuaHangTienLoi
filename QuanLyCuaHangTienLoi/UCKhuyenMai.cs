using BusinessAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCKhuyenMai : UserControl
    {
        // Biến toàn cục
        bool Them;
        public BALKhuyenMai dbkm = null;

        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // Định nghĩa Tên Cột (Phải khớp với tên cột trong Database/SQL Query)
        private const string COL_MaKM = "MaKM";
        private const string COL_LoaiKM = "LoaiKM";
        private const string COL_MucKM = "MucKM";
        private const string COL_DieuKien = "DieuKien";
        private const string COL_ThoiGianBatDau = "ThoiGianBatDau";
        private const string COL_ThoiGianKetThuc = "ThoiGianKetThuc";

        public UCKhuyenMai()
        {
            InitializeComponent();
            dbkm = new BALKhuyenMai();
        }

        private void UCKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        new void ResetText()
        {
            this.txtMaKM.Clear();
            this.txtLoaiKM.Clear();
            this.txtMucKM.Clear();
            this.txtDieuKien.Clear();

            // 🚨 FIX: Clear TextBox thay vì gán DateTime.Now cho DTP
            this.txtThoiGianBD.Clear();
            this.txtThoiGianKT.Clear();

            this.txtMaKM.Text = "(Tự động sinh)";
        }

        void LoadData()
        {
            try
            {
                dgvKhuyenMai.ReadOnly = true;
                dgvKhuyenMai.AllowUserToAddRows = false;
                dgvKhuyenMai.AllowUserToDeleteRows = false;
                dgvKhuyenMai.MultiSelect = false;

                DataTable dtKhuyenMai = dbkm.LayKhuyenMai();
                dgvKhuyenMai.DataSource = dtKhuyenMai;

                ResetText();

                // Cấu hình trạng thái nút
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                txtMaKM.Enabled = false; // Luôn khóa Mã KM

                // Tự động chọn dòng đầu tiên
                if (dgvKhuyenMai.Rows.Count > 0)
                {
                    dgvKhuyenMai_CellClick(dgvKhuyenMai, new DataGridViewCellEventArgs(0, 0));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu Khuyến Mãi. Lỗi: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhuyenMai.RowCount) return;
            int r = e.RowIndex;

            try
            {
                // 🚨 FIX: Lấy DataRowView an toàn
                DataRowView rowView = dgvKhuyenMai.Rows[r].DataBoundItem as DataRowView;
                if (rowView == null) return;
                DataRow row = rowView.Row; // Lấy DataRow thực tế

                Func<string, string> getRowValue = (colName) =>
                {
                    object val = row[colName]; // Truy cập cột bằng tên qua DataRow
                    return (val == null || val == DBNull.Value) ? "" : val.ToString();
                };

                // Load dữ liệu bằng tên cột qua DataRow
                this.txtMaKM.Text = getRowValue(COL_MaKM);
                this.txtLoaiKM.Text = getRowValue(COL_LoaiKM);
                this.txtMucKM.Text = getRowValue(COL_MucKM);
                this.txtDieuKien.Text = getRowValue(COL_DieuKien);

                // Load Ngày Tháng (TextBox)
                if (DateTime.TryParse(getRowValue(COL_ThoiGianBatDau), out DateTime bd))
                    this.txtThoiGianBD.Text = bd.ToString("dd/MM/yyyy");
                else
                    this.txtThoiGianBD.Clear();

                if (DateTime.TryParse(getRowValue(COL_ThoiGianKetThuc), out DateTime kt))
                    this.txtThoiGianKT.Text = kt.ToString("dd/MM/yyyy");
                else
                    this.txtThoiGianKT.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu (Kiểm tra tên cột SQL): " + ex.Message, "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            txtMaKM.Enabled = false; // Khóa nhập Mã
            txtMaKM.Text = "(Tự động sinh)";

            txtLoaiKM.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhuyenMai.CurrentCell == null || dgvKhuyenMai.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Them = false;

            // Load lại dữ liệu để chắc chắn
            dgvKhuyenMai_CellClick(dgvKhuyenMai, new DataGridViewCellEventArgs(dgvKhuyenMai.CurrentCell.ColumnIndex, dgvKhuyenMai.CurrentCell.RowIndex));

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaKM.Enabled = false; // Khóa nhập Mã
            txtLoaiKM.Focus();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhuyenMai.CurrentCell == null) return;
            int r = dgvKhuyenMai.CurrentCell.RowIndex;

            try
            {
                // 🚨 FIX 1: Lấy Mã KM bằng DataBoundItem (An toàn nhất)
                DataRowView rowView = dgvKhuyenMai.Rows[r].DataBoundItem as DataRowView;
                if (rowView == null) return;

                // Lấy Mã KM bằng tên cột từ DataRow (COL_MaKM đã được định nghĩa là "MaKM")
                object maKMValue = rowView.Row[COL_MaKM];
                string strKhuyenMai = (maKMValue == null || maKMValue == DBNull.Value) ? "" : maKMValue.ToString().Trim();

                if (string.IsNullOrWhiteSpace(strKhuyenMai))
                {
                    MessageBox.Show("Không tìm thấy Mã Khuyến Mãi hợp lệ để xóa.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Hỏi xác nhận và gọi BAL
                if (MessageBox.Show($"Chắc chắn xóa khuyến mãi {strKhuyenMai}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string err = "";
                    bool f = dbkm.XoaKhuyenMai(ref err, strKhuyenMai);
                    if (f)
                    {
                        LoadData();
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else
                        MessageBox.Show("Không xóa được!\nLỗi: " + err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: Lỗi truy cập dữ liệu không xác định. " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // === VALIDATION ===
            if (string.IsNullOrWhiteSpace(txtLoaiKM.Text)) { MessageBox.Show("Thiếu loại khuyến mãi!"); txtLoaiKM.Focus(); return; }

            if (!decimal.TryParse(txtMucKM.Text, out decimal mucKM) || mucKM < 0)
            {
                MessageBox.Show("Mức khuyến mãi sai!"); txtMucKM.Focus(); return;
            }

            if (!int.TryParse(txtDieuKien.Text, out int dieuKien) || dieuKien < 0)
            {
                MessageBox.Show("Điều kiện sai!"); txtDieuKien.Focus(); return;
            }

            // === XỬ LÝ NGÀY THÁNG (TryParseExact) ===
            DateTime ngayBatDau, ngayKetThuc;
            string bdStr = txtThoiGianBD.Text.Trim();
            string ktStr = txtThoiGianKT.Text.Trim();

            // Hỗ trợ định dạng dd/MM/yyyy hoặc d/M/yyyy
            string format = "d/M/yyyy";
            CultureInfo culture = CultureInfo.InvariantCulture;

            if (!DateTime.TryParseExact(bdStr, format, culture, DateTimeStyles.None, out ngayBatDau))
            {
                MessageBox.Show("Ngày Bắt Đầu sai định dạng dd/MM/yyyy!", "Lỗi Ngày", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtThoiGianBD.Focus(); return;
            }
            if (!DateTime.TryParseExact(ktStr, format, culture, DateTimeStyles.None, out ngayKetThuc))
            {
                MessageBox.Show("Ngày Kết Thúc sai định dạng dd/MM/yyyy!", "Lỗi Ngày", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtThoiGianKT.Focus(); return;
            }

            if (ngayKetThuc <= ngayBatDau)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu!");
                txtThoiGianKT.Focus();
                return;
            }

            // === GỌI BAL ===
            string err = "";
            bool f;

            if (Them)
            {
                // ThemKhuyenMai (5 tham số, KHÔNG có MaKM)
                f = dbkm.ThemKhuyenMai(ref err,
                    txtLoaiKM.Text.Trim(),
                    mucKM,
                    dieuKien,
                    ngayBatDau,
                    ngayKetThuc
                );
            }
            else
            {
                // CapNhatKhuyenMai (6 tham số, CÓ MaKM)
                string maKM = txtMaKM.Text.Trim();
                if (maKM == "(Tự động sinh)")
                {
                    MessageBox.Show("Lỗi: Không có Mã KM để cập nhật."); return;
                }

                f = dbkm.CapNhatKhuyenMai(ref err,
                    maKM,
                    txtLoaiKM.Text.Trim(),
                    mucKM,
                    dieuKien,
                    ngayBatDau,
                    ngayKetThuc
                );
            }

            if (f)
            {
                LoadData();
                MessageBox.Show(Them ? "Đã thêm xong!" : "Đã cập nhật xong!");
            }
            else
                MessageBox.Show("Thất bại!\nLỗi: " + err);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetText();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            txtTimKiem.Text = "Nhập loại khuyến mãi";
            txtTimKiem.ForeColor = Color.Gray;
            LoadData();
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucQuanLyDuLieu;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvKhuyenMai.DataSource is DataTable dt)
            {
                // Filter theo LoaiKM
                dt.DefaultView.RowFilter = string.Format("LoaiKM LIKE '%{0}%'", txtTimKiem.Text.Trim());
            }
        }

        // Xử lý Placeholder tìm kiếm (nếu cần)
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập loại khuyến mãi")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập loại khuyến mãi";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void btn_KMAD_Click(object sender, EventArgs e)
        {
            // Lấy UserControl Doanh Thu mà bạn đã tạo sẵn trong Program.cs
            UserControl ucCanChuyenToi = Program.ucKhuyenMaiAD;

            // 4. Kích hoạt sự kiện và gửi UserControl đi
            //    Dấu ? (null-conditional operator) để kiểm tra xem có ai (frmTrangChu)
            //    đang lắng nghe sự kiện này không. Nếu có, nó sẽ gọi Invoke.
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
    }
}