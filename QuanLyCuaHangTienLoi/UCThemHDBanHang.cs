using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessAccessLayer; // Đảm bảo đã using BAL

namespace QuanLyCuaHangTienLoi
{
    public partial class UCThemHDBanHang : UserControl
    {
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // 1. Khai báo các biến dùng chung
        private BALHoaDon bal = new BALHoaDon();
        private DataTable dtGioHang; // Bảng tạm chứa dữ liệu giỏ hàng

        // LOẠI BỎ PROPERTY MaNhanVienDangNhap CỨNG

        public UCThemHDBanHang()
        {
            InitializeComponent();
        }

        // Sự kiện khi UserControl được load
        private void UC_ThemHDBanHang_Load(object sender, EventArgs e)
        {
            KhoiTaoGioHang();
            LoadDanhSachSanPham();
        }

        // =================================================================================
        // PHẦN 1: KHỞI TẠO DỮ LIỆU (Giữ nguyên)
        // =================================================================================

        //private void KhoiTaoGioHang()
        //{
        //    // 1. Khởi tạo DataTable (Nguồn dữ liệu)
        //    dtGioHang = new DataTable();
        //    dtGioHang.Columns.Add("MaSP", typeof(string));
        //    dtGioHang.Columns.Add("TenSP", typeof(string));
        //    dtGioHang.Columns.Add("DonGia", typeof(decimal));
        //    dtGioHang.Columns.Add("SoLuong", typeof(int));
        //    dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

        //    // 2. TẮT TẠO CỘT TỰ ĐỘNG (RẤT QUAN TRỌNG)
        //    // Điều này buộc DataGridView chỉ sử dụng các cột đã được thiết lập thủ công trong Designer.
        //    dgvGioHang.AutoGenerateColumns = false; // <--- DÒNG BỔ SUNG

        //    // 3. Gán nguồn dữ liệu
        //    dgvGioHang.DataSource = dtGioHang;

        //    // 4. Định dạng cột (Chỉ hoạt động nếu bạn đã thêm cột thủ công trong Designer với NAME: DonGia và ThanhTien)
        //    if (dgvGioHang.Columns["DonGia"] != null)
        //        dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";

        //    if (dgvGioHang.Columns["ThanhTien"] != null)
        //        dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        //}
        private void KhoiTaoGioHang()
        {
            // 1. Khởi tạo DataTable
            dtGioHang = new DataTable();
            dtGioHang.Columns.Add("MaSP", typeof(string));
            dtGioHang.Columns.Add("TenSP", typeof(string));
            dtGioHang.Columns.Add("DonGia", typeof(decimal));
            dtGioHang.Columns.Add("SoLuong", typeof(int));

            // --- MỚI: Thêm cột Giảm Giá ---
            dtGioHang.Columns.Add("GiamGia", typeof(decimal));

            dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

            // 2. Tắt tạo cột tự động
            dgvGioHang.AutoGenerateColumns = false;

            // 3. Gán nguồn dữ liệu
            dgvGioHang.DataSource = dtGioHang;

            // 4. Định dạng cột số
            if (dgvGioHang.Columns["DonGia"] != null)
                dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";

            if (dgvGioHang.Columns["ThanhTien"] != null)
                dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            // --- MỚI: Định dạng cột Giảm Giá ---
            if (dgvGioHang.Columns["GiamGia"] != null)
                dgvGioHang.Columns["GiamGia"].DefaultCellStyle.Format = "N0";
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                dgvSanPham.DataSource = bal.LayDanhSachSanPham();
                if (dgvSanPham.Columns["GiaBan"] != null)
                    dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách SP: " + ex.Message);
            }
        }

        // =================================================================================
        // PHẦN 2: CÁC NÚT CHỨC NĂNG (Giữ nguyên)
        // =================================================================================

        //private void btnThemVaoGio_Click(object sender, EventArgs e)
        //{
        //    // 1. Kiểm tra đã chọn sản phẩm chưa
        //    if (dgvSanPham.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách bên trái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // 2. Lấy thông tin dòng đang chọn từ DataGridView Sản Phẩm
        //    DataGridViewRow row = dgvSanPham.SelectedRows[0];
        //    string maSP = row.Cells["MaSP"].Value.ToString();
        //    string tenSP = row.Cells["TenSP"].Value.ToString();
        //    decimal donGia = Convert.ToDecimal(row.Cells["GiaBan"].Value);
        //    int soLuongTonKho = Convert.ToInt32(row.Cells["SoLuong"].Value); // Lấy tồn kho

        //    int soLuongThem = (int)numSoLuong.Value; // Lấy số lượng muốn mua thêm

        //    // 2.1. Cấm nhập giá trị âm hoặc bằng 0
        //    if (soLuongThem <= 0)
        //    {
        //        MessageBox.Show("Số lượng mua phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // 3. Tính tổng số lượng sản phẩm này ĐÃ CÓ trong giỏ hàng
        //    int soLuongTrongGio = 0;
        //    DataRow rowTrongGio = null;

        //    foreach (DataRow dr in dtGioHang.Rows)
        //    {
        //        if (dr["MaSP"].ToString() == maSP)
        //        {
        //            soLuongTrongGio = Convert.ToInt32(dr["SoLuong"]);
        //            rowTrongGio = dr;
        //            break;
        //        }
        //    }

        //    // --- LOGIC KIỂM TRA SỐ LƯỢNG TỐI ĐA ---

        //    // Tính số lượng CÒN LẠI có thể mua thêm (Tồn kho - Đã có trong giỏ)
        //    int soLuongCoTheMuaThem = soLuongTonKho - soLuongTrongGio;

        //    // Nếu người dùng muốn mua thêm nhiều hơn số lượng còn lại
        //    if (soLuongThem > soLuongCoTheMuaThem)
        //    {
        //        if (soLuongCoTheMuaThem <= 0)
        //        {
        //            MessageBox.Show($"Bạn đã lấy hết {soLuongTonKho} sản phẩm '{tenSP}' vào giỏ rồi. Không thể thêm nữa.", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //        else
        //        {
        //            // Hiển thị chính xác con số còn lại có thể mua
        //            MessageBox.Show(
        //                $"Kho còn {soLuongTonKho}, trong giỏ đã có {soLuongTrongGio}.\nBạn chỉ có thể mua thêm tối đa {soLuongCoTheMuaThem} sản phẩm nữa thôi.",
        //                "Quá số lượng tồn kho",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Warning
        //            );
        //        }
        //        return; // Dừng lại, không thêm
        //    }

        //    // --- KẾT THÚC KIỂM TRA, TIẾN HÀNH THÊM VÀO GIỎ ---

        //    if (rowTrongGio != null)
        //    {
        //        // Nếu đã có -> Cộng dồn
        //        rowTrongGio["SoLuong"] = soLuongTrongGio + soLuongThem;
        //        rowTrongGio["ThanhTien"] = (soLuongTrongGio + soLuongThem) * donGia;
        //    }
        //    else
        //    {
        //        // Nếu chưa có -> Thêm mới
        //        DataRow newRow = dtGioHang.NewRow();
        //        newRow["MaSP"] = maSP;
        //        newRow["TenSP"] = tenSP;
        //        newRow["DonGia"] = donGia;
        //        newRow["SoLuong"] = soLuongThem;
        //        newRow["ThanhTien"] = donGia * soLuongThem;
        //        dtGioHang.Rows.Add(newRow);
        //    }

        //    // 4. Cập nhật tổng tiền hiển thị
        //    CapNhatTongTien();
        //}
        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            // ========================================================================
            // BƯỚC 1: KIỂM TRA ĐẦU VÀO (GIỮ NGUYÊN)
            // ========================================================================
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách bên trái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuongMuonMua = (int)numSoLuong.Value;
            if (soLuongMuonMua <= 0)
            {
                MessageBox.Show("Số lượng mua phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ========================================================================
            // BƯỚC 2: LẤY THÔNG TIN SẢN PHẨM & TỒN KHO
            // ========================================================================
            DataGridViewRow row = dgvSanPham.SelectedRows[0];
            string maSP = row.Cells["MaSP"].Value.ToString();
            string tenSP = row.Cells["TenSP"].Value.ToString();
            decimal donGia = Convert.ToDecimal(row.Cells["GiaBan"].Value);
            int soLuongTonKho = Convert.ToInt32(row.Cells["SoLuong"].Value);

            // Kiểm tra hàng đã có trong giỏ
            int soLuongDaCoTrongGio = 0;
            DataRow rowTrongGio = null;

            foreach (DataRow dr in dtGioHang.Rows)
            {
                if (dr["MaSP"].ToString() == maSP)
                {
                    soLuongDaCoTrongGio = Convert.ToInt32(dr["SoLuong"]);
                    rowTrongGio = dr;
                    break;
                }
            }

            // Kiểm tra tồn kho
            int soLuongCoTheMuaThem = soLuongTonKho - soLuongDaCoTrongGio;
            if (soLuongMuonMua > soLuongCoTheMuaThem)
            {
                MessageBox.Show($"Kho chỉ còn {soLuongTonKho}. Bạn đã có {soLuongDaCoTrongGio} trong giỏ.\nChỉ có thể mua thêm {soLuongCoTheMuaThem}.", "Quá tồn kho");
                return;
            }

            // ========================================================================
            // BƯỚC 3: TÍNH TOÁN KHUYẾN MÃI (LOGIC MỚI)
            // ========================================================================

            // 3.1. Tính TỔNG SỐ LƯỢNG sau khi thêm
            int tongSoLuong = soLuongDaCoTrongGio + soLuongMuonMua;

            // 3.2. Lấy thông tin khuyến mãi từ BAL
            decimal tienGiamGia = 0;
            DataTable dtKM = bal.LayThongTinKhuyenMai(maSP);

            if (dtKM != null && dtKM.Rows.Count > 0)
            {
                // Lấy dữ liệu từ dòng đầu tiên tìm được
                DataRow drKM = dtKM.Rows[0];

                // Mức KM (Số tiền sẽ giảm)
                decimal mucKM = Convert.ToDecimal(drKM["MucKM"]);

                // Điều kiện (Số lượng tối thiểu để được giảm)
                int dieuKienSL = Convert.ToInt32(drKM["DieuKien"]);

                // 3.3. So sánh điều kiện
                if (tongSoLuong >= dieuKienSL)
                {
                    // Đạt điều kiện -> Áp dụng giảm giá (Trừ thẳng MucKM vào hóa đơn cho dòng này)
                    tienGiamGia = mucKM;
                }
                else
                {
                    // Chưa đủ số lượng -> Không giảm
                    tienGiamGia = 0;
                }
            }

            // 3.4. Tính Thành Tiền
            // Công thức: Tổng tiền hàng - Tiền được giảm
            // Lưu ý: Nếu tiền giảm > Tổng tiền hàng thì Thành tiền = 0 (tránh âm tiền)
            decimal tongTienHang = donGia * tongSoLuong;
            decimal thanhTien = tongTienHang - tienGiamGia;

            if (thanhTien < 0) thanhTien = 0;

            // ========================================================================
            // BƯỚC 4: CẬP NHẬT GIỎ HÀNG
            // ========================================================================

            if (rowTrongGio != null)
            {
                // Cập nhật dòng cũ
                rowTrongGio["SoLuong"] = tongSoLuong;
                rowTrongGio["GiamGia"] = tienGiamGia; // Cập nhật mức giảm giá mới
                rowTrongGio["ThanhTien"] = thanhTien;
            }
            else
            {
                // Thêm dòng mới
                DataRow newRow = dtGioHang.NewRow();
                newRow["MaSP"] = maSP;
                newRow["TenSP"] = tenSP;
                newRow["DonGia"] = donGia;
                newRow["SoLuong"] = tongSoLuong; // Lúc này tongSoLuong = soLuongMuonMua
                newRow["GiamGia"] = tienGiamGia;
                newRow["ThanhTien"] = thanhTien;

                dtGioHang.Rows.Add(newRow);
            }

            // ========================================================================
            // BƯỚC 5: HOÀN TẤT
            // ========================================================================
            CapNhatTongTien();
            numSoLuong.Value = 1;
        }
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvGioHang.SelectedRows)
                {
                    string maSPCanXoa = row.Cells["MaSanPham"].Value.ToString();
                    DataRow rowToDelete = null;

                    foreach (DataRow dr in dtGioHang.Rows)
                    {
                        if (dr["MaSP"].ToString() == maSPCanXoa)
                        {
                            rowToDelete = dr;
                            break;
                        }
                    }

                    if (rowToDelete != null) dtGioHang.Rows.Remove(rowToDelete);
                }
                CapNhatTongTien();
            }
            else
            {
                MessageBox.Show("Chọn món trong giỏ hàng để xóa!", "Thông báo");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dtGioHang.Clear();
            CapNhatTongTien();
            numSoLuong.Value = 1;
        }

        private void CapNhatTongTien()
        {
            decimal tongTien = 0;
            foreach (DataRow dr in dtGioHang.Rows)
            {
                tongTien += Convert.ToDecimal(dr["ThanhTien"]);
            }
            lblTongTien.Text = string.Format("{0:N0} VNĐ", tongTien);
        }

        // =================================================================================
        // PHẦN 3: XỬ LÝ THANH TOÁN (LẤY ID TỪ FORM CHA)
        // =================================================================================

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra giỏ hàng
            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn thanh toán hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // --- ĐOẠN CODE FIX START ---
            // Lấy MaNV từ biến static (đã được gán giá trị từ labMaNV sau khi đăng nhập)
            string maNVBanHang = frmTrangChu.MaNV;

            if (string.IsNullOrEmpty(maNVBanHang))
            {
                MessageBox.Show("Lỗi: Không tìm thấy Mã Nhân Viên đang đăng nhập! Vui lòng đăng nhập lại.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // --- ĐOẠN CODE FIX END ---

            string err = "";
            try
            {
                // BƯỚC 1: TẠO HÓA ĐƠN (VỎ) -> LẤY MÃ HD
                string maHDMoi = bal.TaoHDBanHang(maNVBanHang, ref err); // Dùng MaNV lấy được

                if (maHDMoi == null)
                {
                    MessageBox.Show("Lỗi tạo hóa đơn: " + err, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // BƯỚC 2: DUYỆT GIỎ HÀNG VÀ ĐẨY CHI TIẾT XUỐNG DB
                int soDongThanhCong = 0;
                foreach (DataRow dr in dtGioHang.Rows)
                {
                    string maSP = dr["MaSP"].ToString();
                    int soLuong = Convert.ToInt32(dr["SoLuong"]);

                    bool ketQua = bal.ThemChiTietHDBanHang(maHDMoi, maSP, soLuong, ref err);

                    if (ketQua) soDongThanhCong++;
                    else
                    {
                        MessageBox.Show($"Lỗi thêm SP {maSP}: {err}");
                    }
                }

                // BƯỚC 3: HOÀN TẤT
                if (soDongThanhCong == dtGioHang.Rows.Count)
                {
                    MessageBox.Show($"Thanh toán thành công!\nMã HĐ: {maHDMoi}\nTổng tiền: {lblTongTien.Text}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnLamMoi_Click(null, null);
                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show($"Thanh toán có lỗi! Chỉ thêm được {soDongThanhCong}/{dtGioHang.Rows.Count} sản phẩm.", "Cảnh báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        // Sự kiện tìm kiếm sản phẩm (Chức năng phụ)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.DataSource is DataTable dt)
            {
                string key = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(key))
                    dt.DefaultView.RowFilter = "";
                else
                    dt.DefaultView.RowFilter = $"TenSP LIKE '%{key}%' OR MaSP LIKE '%{key}%'";
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucHoaDonBan;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
    }
}