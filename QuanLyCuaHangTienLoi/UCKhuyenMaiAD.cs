using BusinessAccessLayer;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCKhuyenMaiAD : UserControl
    {
        BALKhuyenMaiAD dbkmad = null;
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;
        public UCKhuyenMaiAD()
        {
            InitializeComponent();
            dbkmad = new BALKhuyenMaiAD();

            // Load dữ liệu ban đầu
            LoadComboBoxMaSP();
            LoadComboBoxMaKM();
            LoadGridSPKM(); // Gọi hàm load bảng
            Dock = DockStyle.Fill;
            // Vào màn hình -> Chế độ xem (Khóa nhập liệu)
            DoiTrangThai(false);
        }

        // ========================================================
        // 1. CÁC HÀM LOAD DỮ LIỆU (DATABASE)
        // ========================================================

        // Hàm load dữ liệu lên DataGridView (Sửa lỗi LoadGridSPKM not exist tại đây)
        private void LoadGridSPKM()
        {
            try
            {
                dgvKMAD.DataSource = dbkmad.LayDanhSachSPKM();

                // Định dạng tên cột hiển thị
                if (dgvKMAD.Columns["MaSP"] != null) dgvKMAD.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                if (dgvKMAD.Columns["TenSP"] != null) dgvKMAD.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                if (dgvKMAD.Columns["MaKM"] != null) dgvKMAD.Columns["MaKM"].HeaderText = "Mã Khuyến Mãi";
                if (dgvKMAD.Columns["LoaiKM"] != null) dgvKMAD.Columns["LoaiKM"].HeaderText = "Loại KM";

                dgvKMAD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void LoadComboBoxMaSP()
        {
            try
            {
                var dt = dbkmad.LayDanhSachMaSP();
                if (dt != null)
                {
                    cbb_maSP.DataSource = dt;
                    cbb_maSP.DisplayMember = "TenSP";
                    cbb_maSP.ValueMember = "MaSP";
                    cbb_maSP.SelectedIndex = -1;
                }
            }
            catch { }
        }

        private void LoadComboBoxMaKM()
        {
            try
            {
                var dt = dbkmad.LayDanhSachMaKM();
                if (dt != null)
                {
                    cbb_maKM.DataSource = dt;
                    cbb_maKM.DisplayMember = "LoaiKM";
                    cbb_maKM.ValueMember = "MaKM";
                    cbb_maKM.SelectedIndex = -1;
                }
            }
            catch { }
        }

        // ========================================================
        // 2. QUẢN LÝ TRẠNG THÁI GIAO DIỆN (Logic UI)
        // ========================================================

        // Hàm Reset dữ liệu nhập về rỗng
        private void ResetInput()
        {
            cbb_maSP.SelectedIndex = -1;
            cbb_maKM.SelectedIndex = -1;
        }

        // Hàm Bật/Tắt các nút
        private void DoiTrangThai(bool isAdding)
        {
            // isAdding = true  => Đang bấm Thêm (Nhập liệu)
            // isAdding = false => Đang xem bình thường

            // 1. Khu vực nhập liệu
            cbb_maSP.Enabled = isAdding;
            cbb_maKM.Enabled = isAdding;

            // 2. Khu vực nút bấm
            btn_luu.Enabled = isAdding;

            // Nếu bạn có nút Hủy thì mở dòng dưới ra, không thì thôi
            if (btn_huy != null) btn_huy.Enabled = isAdding;

            if (btn_themAD != null) btn_themAD.Enabled = !isAdding; // Nút Thêm sáng khi KHÔNG thêm
            if (btn_xoaAD != null) btn_xoaAD.Enabled = !isAdding;   // Nút Xóa sáng khi KHÔNG thêm

            // 3. Khu vực Bảng (Grid): Khóa khi đang thêm để tránh click nhầm
            dgvKMAD.Enabled = !isAdding;
        }

        // ========================================================
        // 3. CÁC SỰ KIỆN NÚT BẤM (EVENTS)
        // ========================================================

        // NÚT THÊM
        private void btn_themAD_Click(object sender, EventArgs e)
        {
            ResetInput();       // Xóa trắng ô cũ
            DoiTrangThai(true); // Bật chế độ thêm (Mở khóa nhập liệu)
            cbb_maSP.Focus();   // Đưa con trỏ vào ô đầu
        }

        // NÚT HỦY (Nếu bạn đã tạo nút này trong Designer)
        private void btn_huy_Click(object sender, EventArgs e)
        {
            ResetInput();
            DoiTrangThai(false); // Quay về chế độ xem (Khóa lại)
        }

        // NÚT LƯU
        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (cbb_maSP.SelectedIndex == -1 || cbb_maSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (cbb_maKM.SelectedIndex == -1 || cbb_maKM.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Mã khuyến mãi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            try
            {
                string maSP = cbb_maSP.SelectedValue.ToString();
                string maKM = cbb_maKM.SelectedValue.ToString();

                // Kiểm tra trùng lặp
                if (dbkmad.KiemTraSanPhamDaCoKM(maSP))
                {
                    MessageBox.Show($"Sản phẩm '{maSP}' đã có khuyến mãi rồi!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // Thực hiện lưu
                string err = "";
                if (dbkmad.ThemKhuyenMaiChoSP(maSP, maKM, ref err))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadGridSPKM();      // Tải lại bảng ngay lập tức
                    ResetInput();        // Xóa trắng ô nhập
                    DoiTrangThai(false); // Quay về chế độ xem (Khóa lại)
                }
                else
                {
                    MessageBox.Show("Lỗi: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message); }
        }

        // NÚT XÓA
        private void btn_xoaAD_Click(object sender, EventArgs e)
        {
            string maSPCanXoa = "";

            // Lấy ID từ dòng đang chọn hoặc từ combobox
            if (dgvKMAD.SelectedRows.Count > 0 && dgvKMAD.CurrentRow != null)
                maSPCanXoa = dgvKMAD.CurrentRow.Cells["MaSP"].Value.ToString();
            else if (cbb_maSP.SelectedIndex != -1 && cbb_maSP.SelectedValue != null)
                maSPCanXoa = cbb_maSP.SelectedValue.ToString();

            if (string.IsNullOrEmpty(maSPCanXoa))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            if (MessageBox.Show($"Xác nhận gỡ khuyến mãi khỏi sản phẩm '{maSPCanXoa}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string err = "";
                    if (dbkmad.XoaKhuyenMaiCuaSP(maSPCanXoa, ref err))
                    {
                        MessageBox.Show("Đã xóa thành công!");
                        LoadGridSPKM(); // Tải lại bảng ngay
                        ResetInput();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa: " + err);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // SỰ KIỆN CLICK VÀO BẢNG
        private void dgvKMAD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ chạy khi bảng đang ở trạng thái cho phép click (Enabled = true)
            if (dgvKMAD.Enabled && e.RowIndex >= 0 && e.RowIndex < dgvKMAD.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvKMAD.Rows[e.RowIndex];
                    cbb_maSP.SelectedValue = row.Cells["MaSP"].Value.ToString();
                    cbb_maKM.SelectedValue = row.Cells["MaKM"].Value.ToString();
                }
                catch { }
            }
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucKhuyenMai;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
    }
}