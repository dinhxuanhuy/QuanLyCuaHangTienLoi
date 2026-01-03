using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using BusinessAccessLayer;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCHoaDonBan : UserControl
    {
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;
        // ----- KẾT THÚC THÊM MỚI -----
        private DataGridView dgvHoaDon;
        private BALHoaDon dbhd = null;

        //// Định nghĩa tên cột (PHẢI KHỚP VỚI VIEW SQL view_HoaDonBanHang)
        //private const string COL_MA_HD = "MaHD";
        //private const string COL_NV_BAN_HANG_ID = "MaNVBanHang"; // ID Mã NV bán hàng (Cho DoubleClick)
        //private const string COL_TEN_NV = "TenNVBanHang";     // Tên NV bán hàng (Dùng cho hiển thị và lọc)
        //private const string COL_TONG_GIA_TRI = "TongGiaTri";
        //private const string COL_NGAY_LAP = "NgayLap"; // Tên cột Ngày Bán/Lập trong VIEW SQL
        // Định nghĩa tên cột (PHẢI KHỚP VỚI VIEW SQL view_HoaDonBanHang)
        private const string COL_MA_HD = "Column1";
        
        private const string COL_TEN_NV = "Column2";     // Tên NV bán hàng (Dùng cho hiển thị và lọc)
        
        private const string COL_NGAY_LAP = "Column3"; // Tên cột Ngày Bán/Lập trong VIEW SQL
        private const string COL_TONG_GIA_TRI = "Column4";
        private const string COL_NV_BAN_HANG_ID = "Column5"; // ID Mã NV bán hàng (Cho DoubleClick)
        public UCHoaDonBan()
        {
            InitializeComponent();
            dbhd = new BALHoaDon();
            // 🚨 Gán DataGridView thực tế
            this.dgvHoaDon = this.guna2DataGridView1;

            LoadData();
        }

        // HÀM LOAD CHÍNH
        public void LoadData()
        {
            try
            {
                dgvHoaDon.DataSource = dbhd.LayHoaDonBan();

                // Tự động chọn dòng đầu tiên nếu có dữ liệu
                if (dgvHoaDon.Rows.Count > 0)
                {
                    // Giả lập chọn ô đầu tiên để kích hoạt CellClick
                    dgvHoaDon.CurrentCell = dgvHoaDon.Rows[0].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucQuanLyHoaDon;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        // 1. XỬ LÝ TÌM KIẾM (Đã FIX đồng bộ tìm theo Ngày Lập)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (dgvHoaDon.DataSource is DataTable dt)
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    // FIX: Lọc theo Mã HD, Tên NV, HOẶC Ngày Lập (CONVERT DATE -> STRING)
                    string filter = string.Format(
                        "({0} LIKE '%{1}%') OR ({2} LIKE '%{1}%') OR (CONVERT({3}, 'System.String') LIKE '%{1}%')",

                        COL_MA_HD,      // Mã hóa đơn
                        keyword,
                        COL_TEN_NV,     // Tên nhân viên
                        COL_NGAY_LAP    // Ngày lập (Convert sang String)
                    );

                    dt.DefaultView.RowFilter = filter;
                }
            }
        }

        // 2. XỬ LÝ HIỂN THỊ DỮ LIỆU CƠ BẢN KHI CLICK DGV (Sử dụng DataRow an toàn)
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0 || e.RowIndex >= dgvHoaDon.Rows.Count) return;

            //try
            //{
            //    DataRowView rowView = dgvHoaDon.Rows[e.RowIndex].DataBoundItem as DataRowView;
            //    if (rowView == null) return;

            //    DataRow row = rowView.Row;

            //    // FIX: Dùng tên cột đã định nghĩa để truy cập DataRow
            //    txtTongGiaTri.Text = row[COL_TONG_GIA_TRI]?.ToString() ?? "";
            //    txtMaHD.Text = row[COL_MA_HD]?.ToString() ?? "";
            //    txtNVBanHang.Text = row[COL_TEN_NV]?.ToString() ?? ""; // Hiển thị Tên NV
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi hiển thị chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            // Kiểm tra dòng hợp lệ (không click vào tiêu đề)
            if (e.RowIndex < 0 || e.RowIndex >= dgvHoaDon.Rows.Count) return;

            try
            {
                // Lấy dòng hiện tại trên giao diện
                DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];

                // --- SỬA ĐỔI: Dùng .Cells[Tên_Cột].Value thay vì DataRow[...] ---
                // Cách này an toàn nhất vì nó dùng chung tên cột "Column..." mà bạn vừa khai báo

                // 1. Lấy Tổng giá trị
                txtTongGiaTri.Text = row.Cells[COL_TONG_GIA_TRI].Value?.ToString() ?? "";

                // 2. Lấy Mã Hóa Đơn
                txtMaHD.Text = row.Cells[COL_MA_HD].Value?.ToString() ?? "";

                // 3. Lấy Tên NV
                txtNVBanHang.Text = row.Cells[COL_TEN_NV].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. XỬ LÝ DOUBLE CLICK (MỞ FORM CHI TIẾT)
        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHoaDon.Rows.Count) return;

            try
            {
                DataRowView rowView = dgvHoaDon.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (rowView == null) return;
                DataRow row = rowView.Row;

                // Lấy Mã HD và ID NV bán hàng bằng tên cột của DataRow
                string maHD = row[COL_MA_HD]?.ToString() ?? "";
                string nvBanHangID = row[COL_NV_BAN_HANG_ID]?.ToString() ?? ""; // ⬅️ Lấy ID NV

                if (string.IsNullOrEmpty(maHD)) return;
                ;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Clear inputs
            txtTimKiem.Text = string.Empty;
            txtMaHD.Text = string.Empty;
            txtNVBanHang.Text = string.Empty;
            txtTongGiaTri.Text = string.Empty;
            // Xóa chọn trong DGV và tải lại data (để reset filter)
            dgvHoaDon.ClearSelection();
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy UserControl Doanh Thu mà bạn đã tạo sẵn trong Program.cs
            UserControl ucCanChuyenToi = Program.ucThemHDBanHang;

            // 4. Kích hoạt sự kiện và gửi UserControl đi
            //    Dấu ? (null-conditional operator) để kiểm tra xem có ai (frmTrangChu)
            //    đang lắng nghe sự kiện này không. Nếu có, nó sẽ gọi Invoke.
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra chọn dòng
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy dòng đang chọn
                DataGridViewRow row = dgvHoaDon.SelectedRows[0];

                // 3. Lấy dữ liệu từ các cột (Sử dụng các hằng số tên cột bạn đã khai báo ở đầu class)
                // Lưu ý: Đảm bảo tên cột khớp với View SQL hoặc Design DataGridView của bạn
                string maHD = row.Cells[COL_MA_HD].Value?.ToString() ?? "";

                // Lấy Tên NV (hoặc Mã NV tùy ý bạn, ở đây mình lấy Tên cho đẹp)
                string tenNV = row.Cells[COL_TEN_NV].Value?.ToString() ?? "";

                // Lấy ngày và format gọn lại (chỉ lấy ngày tháng năm)
                string ngayRaw = row.Cells[COL_NGAY_LAP].Value?.ToString() ?? "";
                string ngayLap = "";
                if (DateTime.TryParse(ngayRaw, out DateTime dateValue))
                {
                    ngayLap = dateValue.ToString("dd/MM/yyyy HH:mm");
                }
                else
                {
                    ngayLap = ngayRaw;
                }

                // Lấy tổng tiền và format có dấu phẩy
                string tienRaw = row.Cells[COL_TONG_GIA_TRI].Value?.ToString() ?? "0";
                string tongTien = "";
                if (decimal.TryParse(tienRaw, out decimal tienValue))
                {
                    tongTien = tienValue.ToString("N0") + " VNĐ";
                }
                else
                {
                    tongTien = tienRaw;
                }

                // 4. KHỞI TẠO FORM VỚI NHIỀU THAM SỐ
                frmChiTietHD frm = new frmChiTietHD(maHD, tenNV, ngayLap, tongTien);

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form: " + ex.Message);
            }
        }
    }
}