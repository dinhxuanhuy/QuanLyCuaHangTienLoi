using BusinessAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCSanPham : UserControl
    {
        // Biến toàn cục
        bool Them;
        public BALSanPham dbsp = null;

        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // Định nghĩa các Column Names (Cập nhật theo View vSanPham mới)
        private const string COL_MaSP = "MaSP";
        private const string COL_TenSP = "TenSP";
        private const string COL_SoLuong = "SoLuong";
        // Trong View vSanPham, cột giá bán tên là "GiaBan"
        private const string COL_GiaBan = "GiaBan";
        // Lưu ý: Cột GiaNhap đã bị ẩn khỏi View nên không khai báo ở đây để tránh lỗi
        private const string COL_GiaNhap = "GiaNhap";
        private const string COL_NgaySX = "NgaySX";
        private const string COL_HanSD = "HanSD";
        private const string COL_DonVi = "DonVi";
        private const string COL_TinhTrang = "TinhTrang";
        private const string COL_NVQuanLy = "NVQuanLy";
        private const string COL_MaNCC = "MaNCC";

        public UCSanPham()
        {
            InitializeComponent();
            UIStyles.ApplyUIStyle(this);
            dbsp = new BALSanPham();
        }

        private void UCSanPham_Load(object sender, EventArgs e)
        {
            LoadNVQuanLy();
            LoadMaNCC();
            LoadData();
        }

        // --- HÀM HỖ TRỢ ---

        new void ResetText()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
            txtGiaNhap.Clear(); // <--- MỚI: Reset ô Giá Nhập
            txtNgaySX.Clear();
            txtHanSD.Clear();
            txtDonVi.Clear();
            txtTinhTrang.Clear();
            cboNVQuanLy.SelectedIndex = -1;
            cboMaNCC.SelectedIndex = -1;
            txtMaSP.Text = "(Tự động sinh)";
        }

        void LoadData()
        {
            try
            {
                dgvSanPham.ReadOnly = true;
                dgvSanPham.AllowUserToAddRows = false;
                dgvSanPham.AllowUserToDeleteRows = false;
                dgvSanPham.MultiSelect = false;

                // Lấy dữ liệu từ View (Không có Giá Nhập)
                DataTable dtSanPham = dbsp.LaySanPham();
                dgvSanPham.DataSource = dtSanPham;

                ResetText();

                // Khóa các control mặc định
                cboNVQuanLy.Enabled = false;
                cboMaNCC.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                txtMaSP.Enabled = false;

                // Khóa luôn các ô nhập liệu khi chưa nhấn nút
                txtTenSP.Enabled = false;
                txtGiaNhap.Enabled = false;
                txtDonGia.Enabled = false;
                txtSoLuong.Enabled = false;
                // ... (Các ô khác tương tự nếu cần thiết kế chặt chẽ)

                if (dgvSanPham.Rows.Count > 0)
                {
                    dgvSanPham_CellClick(dgvSanPham, new DataGridViewCellEventArgs(0, 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadNVQuanLy()
        {
            DataTable dtNVQL = dbsp.LayMaNV();
            cboNVQuanLy.DataSource = dtNVQL;
            cboNVQuanLy.DisplayMember = "MaNV";
            cboNVQuanLy.ValueMember = "MaNV";
            cboNVQuanLy.SelectedIndex = -1;
        }

        void LoadMaNCC()
        {
            DataTable dtNCC = dbsp.LayMaNCC();
            cboMaNCC.DataSource = dtNCC;
            cboMaNCC.DisplayMember = "MaNCC";
            cboMaNCC.ValueMember = "MaNCC";
            cboMaNCC.SelectedIndex = -1;
        }

        // --- SỰ KIỆN NÚT ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            ResetText();

            // --- MỞ KHÓA TOÀN BỘ CÁC TRƯỜNG NHẬP LIỆU ---

            // 1. Các trường quan trọng (bạn đã có)
            cboNVQuanLy.Enabled = true;
            cboMaNCC.Enabled = true;
            txtTenSP.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtDonGia.Enabled = true;
            txtSoLuong.Enabled = true;

            // 2. CÁC TRƯỜNG BỊ THIẾU (Cần bổ sung để mở lại)
            txtDonVi.Enabled = true;     // <--- Bổ sung
            txtTinhTrang.Enabled = true; // <--- Bổ sung
            txtNgaySX.Enabled = true;    // <--- Bổ sung
            txtHanSD.Enabled = true;     // <--- Bổ sung

            // 3. Khóa mã (đúng rồi)
            txtMaSP.Enabled = false;
            txtMaSP.Text = "(Tự động sinh)";

            // 4. Trạng thái nút
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;

            txtTenSP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có dòng nào được chọn không
            if (dgvSanPham.CurrentCell == null || dgvSanPham.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần nhập thêm hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LoadData(); // Load lại dữ liệu để reset trạng thái form
                return;
            }

            Them = false; // Chế độ Cập nhật (Sửa/Nhập hàng)

            // 2. Load lại dữ liệu dòng đang chọn để đảm bảo thông tin hiển thị chính xác
            dgvSanPham_CellClick(dgvSanPham, new DataGridViewCellEventArgs(dgvSanPham.CurrentCell.ColumnIndex, dgvSanPham.CurrentCell.RowIndex));

            // =================================================================================
            // 3. LOGIC KHÓA FORM: Tắt hết tất cả, chỉ chừa lại Số Lượng
            // =================================================================================

            // --- Nhóm ComboBox (Tắt hết) ---
            cboNVQuanLy.Enabled = false; // Khóa luôn người quản lý (Lấy theo dòng cũ hoặc user đăng nhập)
            cboMaNCC.Enabled = false;    // Khóa Nhà cung cấp
                                         // cboLoaiSP.Enabled = false;// Nếu có ComboBox loại sản phẩm, khóa luôn

            // --- Nhóm TextBox (Tắt hết các thông tin định danh/giá) ---
            txtMaSP.Enabled = false;     // Khóa Mã SP
            txtTenSP.Enabled = false;    // Khóa Tên SP
            txtGiaNhap.Enabled = false;  // Khóa Giá nhập
            txtDonGia.Enabled = false;   // Khóa Giá bán
            txtTinhTrang.Enabled = false;// Khóa Tình trạng (Hệ thống tự cập nhật dựa trên số lượng > 0)
            txtDonVi.Enabled = false;
            txtHanSD.Enabled = false;  // Khóa Hạn sử dụng
            txtNgaySX.Enabled = false; // Khóa Ngày sản xuất
            // txtDonVi.Enabled = false; // Khóa đơn vị tính (nếu có)

            // --- CHỈ MỞ DUY NHẤT SỐ LƯỢNG ---
            txtSoLuong.Enabled = true;
            txtSoLuong.Text = "0";       // Reset về 0 để người dùng nhập số lượng CỘNG THÊM
            txtSoLuong.Focus();          // Đưa con trỏ chuột vào ngay ô số lượng

            // =================================================================================

            // 4. Thông báo hướng dẫn
            MessageBox.Show("Chế độ NHẬP HÀNG:\nCác thông tin khác đã bị khóa.\nVui lòng chỉ nhập số lượng muốn CỘNG THÊM.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 5. Cập nhật trạng thái các nút chức năng
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // === KIỂM TRA VALIDATION CHUNG ===
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!"); txtSoLuong.Focus(); return;
            }

            // Xử lý ngày tháng
            DateTime ngaySX, hanSD;
            string format = "d/M/yyyy";
            CultureInfo culture = CultureInfo.InvariantCulture;

            if (!DateTime.TryParseExact(txtNgaySX.Text.Trim(), format, culture, DateTimeStyles.None, out ngaySX))
            {
                MessageBox.Show("Ngày SX sai định dạng (dd/MM/yyyy)!"); txtNgaySX.Focus(); return;
            }
            if (!DateTime.TryParseExact(txtHanSD.Text.Trim(), format, culture, DateTimeStyles.None, out hanSD))
            {
                MessageBox.Show("Hạn SD sai định dạng (dd/MM/yyyy)!"); txtHanSD.Focus(); return;
            }
            if (hanSD.Date <= ngaySX.Date)
            {
                MessageBox.Show("Hạn sử dụng phải sau ngày sản xuất!"); txtHanSD.Focus(); return;
            }

            if (string.IsNullOrWhiteSpace(txtDonVi.Text)) { MessageBox.Show("Thiếu Đơn vị!"); txtDonVi.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtTinhTrang.Text)) { MessageBox.Show("Thiếu Tình trạng!"); txtTinhTrang.Focus(); return; }

            // LƯU Ý: Khi nhập hàng (Sửa), cboNVQuanLy bị disable nên SelectedValue có thể null
            // Ta sẽ lấy mã người đăng nhập (frmTrangChu.MaNV) ở dưới, không cần check cboNVQuanLy ở đây.
            if (Them && cboNVQuanLy.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn Nhân viên quản lý!"); return;
            }

            string err = "";
            bool kq = false;

            // -------------------------------------------------------------
            // FIX LỖI: Lấy mã người đăng nhập từ biến toàn cục
            // -------------------------------------------------------------
            string maNVDangNhap = frmTrangChu.MaNV;

            // Kiểm tra an toàn (nếu chưa đăng nhập hoặc biến bị null)
            if (string.IsNullOrEmpty(maNVDangNhap))
            {
                MessageBox.Show("Không xác định được người đang đăng nhập! Hệ thống sẽ lấy mã mặc định 'NV0001' để test.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maNVDangNhap = "NV0001"; // Mã fallback để tránh crash
            }
            // -------------------------------------------------------------

            // === XỬ LÝ THEM ===
            if (Them)
            {
                if (string.IsNullOrWhiteSpace(txtTenSP.Text)) { MessageBox.Show("Thiếu tên sản phẩm!"); txtTenSP.Focus(); return; }
                if (cboMaNCC.SelectedValue == null) { MessageBox.Show("Chưa chọn Nhà cung cấp!"); return; }

                // Validate Giá
                if (!decimal.TryParse(txtDonGia.Text, out decimal giaBan) || giaBan < 0)
                { MessageBox.Show("Giá bán không hợp lệ!"); txtDonGia.Focus(); return; }

                if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap < 0)
                { MessageBox.Show("Giá nhập không hợp lệ!"); txtGiaNhap.Focus(); return; }

                string maNVQuanLyMoi = cboNVQuanLy.SelectedValue.ToString();

                // Gọi BAL ThemSanPham
                kq = dbsp.ThemSanPham(ref err,
                    txtTenSP.Text.Trim(),
                    soLuong,
                    giaNhap,
                    giaBan,
                    ngaySX, hanSD,
                    txtDonVi.Text.Trim(), txtTinhTrang.Text.Trim(),
                    maNVQuanLyMoi, // Người được chọn làm quản lý SP này
                    cboMaNCC.SelectedValue.ToString()
                );
            }
            // === XỬ LÝ SUA (NHẬP HÀNG) ===
            else
            {
                string maSP = txtMaSP.Text.Trim();
                if (maSP == "(Tự động sinh)") return;

                // Gọi hàm nhập hàng mới
                // TRUYỀN ĐÚNG mã người đang đăng nhập (maNVDangNhap)
                kq = dbsp.NhapHang(ref err, maSP, soLuong, maNVDangNhap);
            }

            if (kq)
            {
                LoadData();
                string msg = Them ? "Đã thêm sản phẩm mới!" : "Đã nhập thêm hàng thành công!\nNgười thực hiện: " + maNVDangNhap;
                MessageBox.Show(msg);

                // Reset UI
                cboNVQuanLy.Enabled = false;
                cboMaNCC.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lỗi thao tác!\n" + err);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetText();
            LoadData(); // Load lại để reset trạng thái enable/disable chuẩn
            txtTimKiem.Text = "Nhập tên sản phẩm";
            txtTimKiem.ForeColor = Color.Gray;
        }

        // --- SỰ KIỆN CELL CLICK ---
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSanPham.RowCount) return;
            int r = e.RowIndex;

            try
            {
                DataRowView rowView = dgvSanPham.Rows[r].DataBoundItem as DataRowView;
                if (rowView == null) return;
                DataRow row = rowView.Row;

                Func<string, string> getRowValue = (colName) =>
                {
                    if (!row.Table.Columns.Contains(colName)) return ""; // Check cột tồn tại
                    object val = row[colName];
                    return (val == null || val == DBNull.Value) ? "" : val.ToString();
                };

                txtMaSP.Text = getRowValue(COL_MaSP);
                txtTenSP.Text = getRowValue(COL_TenSP);
                txtSoLuong.Text = getRowValue(COL_SoLuong);

                // Load Giá Bán lên ô Đơn Giá
                string giaBanStr = getRowValue(COL_GiaBan);
                if (decimal.TryParse(giaBanStr, out decimal gb))
                    txtDonGia.Text = gb.ToString("N0"); // Format số cho đẹp
                else
                    txtDonGia.Text = "0";

                // Date handling
                if (DateTime.TryParse(getRowValue(COL_NgaySX), out DateTime nsx))
                    txtNgaySX.Text = nsx.ToString("d/M/yyyy");
                else txtNgaySX.Clear();

                if (DateTime.TryParse(getRowValue(COL_HanSD), out DateTime hsd))
                    txtHanSD.Text = hsd.ToString("d/M/yyyy");
                else txtHanSD.Clear();

                txtDonVi.Text = getRowValue(COL_DonVi);
                txtTinhTrang.Text = getRowValue(COL_TinhTrang);

                if (cboNVQuanLy.Items.Count > 0) cboNVQuanLy.SelectedValue = getRowValue(COL_NVQuanLy);
                if (cboMaNCC.Items.Count > 0) cboMaNCC.SelectedValue = getRowValue(COL_MaNCC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucQuanLyDuLieu;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.DataSource is DataTable dt)
                dt.DefaultView.RowFilter = string.Format("TenSP LIKE '%{0}%'", txtTimKiem.Text);
        }
    }
}