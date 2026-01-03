using BusinessAccessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class frmChiTietHD : Form
    {
        string _maHD;
        BALHoaDon balHD = new BALHoaDon();

        // --- SỬA TẠI ĐÂY: Thêm tham số vào ngoặc ---
        public frmChiTietHD(string maHD, string tenNV, string ngayLap, string tongTien)
        {
            InitializeComponent();
            _maHD = maHD;

            // Gán dữ liệu nhận được lên các Label
            lblMaHD.Text = "Mã HĐ: " + maHD;
            lblTenNV.Text = "Nhân viên: " + tenNV;
            lblNgayLap.Text = "Ngày lập: " + ngayLap;
            lblTongTien.Text = "Tổng tiền: " + tongTien;

            // Gọi tải dữ liệu Grid
            LoadData();
        }

        void LoadData()
        {
            // 1. QUAN TRỌNG: Chặn việc tự tạo cột, bắt buộc dùng cột bạn đã Design
            dgvChiTiet.AutoGenerateColumns = false;

            try
            {
                // 2. Lấy dữ liệu từ BAL
                DataTable dt = balHD.LayChiTietHDBanHang(_maHD);

                // 3. Đổ dữ liệu vào (Tự động khớp nhờ DataPropertyName)
                dgvChiTiet.DataSource = dt;

                // 4. Các thiết lập thẩm mỹ cho Guna2 (Màu sắc)
                dgvChiTiet.ThemeStyle.RowsStyle.ForeColor = Color.Black;
                dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dgvChiTiet.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;

                // 5. (Tùy chọn) Format số tiền nếu bạn QUÊN chỉnh trong Design
                // Nếu trong Design bạn đã chỉnh DefaultCellStyle.Format = "N0" rồi thì xóa đoạn này đi cũng được
                if (dgvChiTiet.Columns["Thành Tiền"] != null)
                    dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}