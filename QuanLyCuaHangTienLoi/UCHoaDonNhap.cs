using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BusinessAccessLayer;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCHoaDonNhap : UserControl
    {
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        private DataGridView dgvHoaDon;
        private BALHoaDon dbhd = null;
        private const string COL_MA_PHIEU = "Mã Phiếu";   // Trước là "MaDonNhap"
        private const string COL_NGAY_NHAP = "Ngày Nhập"; // Trước là "NgayNhap"
        private const string COL_THANH_TIEN = "Thành Tiền"; // Trước là "TongTien"
        private const string COL_NGUOI_NHAP = "Người Nhập"; // Cột hiển thị tên nhân viên
        private const string COL_TEN_SP = "Tên Sản Phẩm";

        public UCHoaDonNhap()
        {
            InitializeComponent();
            UIStyles.ApplyUIStyle(this);
            dbhd = new BALHoaDon();

            this.dgvHoaDon = this.guna2DataGridView1;
            this.txtTimKiem.PlaceholderText = "Nhập mã phiếu hoặc tên SP...";

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                // Gọi hàm lấy lịch sử (đảm bảo BAL đã có hàm này)
                dgvHoaDon.DataSource = dbhd.LayLichSuNhapHang();

                // Format hiển thị tiền tệ
                if (dgvHoaDon.Columns[COL_THANH_TIEN] != null)
                    dgvHoaDon.Columns[COL_THANH_TIEN].DefaultCellStyle.Format = "N0";

                if (dgvHoaDon.Columns["Giá Nhập"] != null)
                    dgvHoaDon.Columns["Giá Nhập"].DefaultCellStyle.Format = "N0";

                if (dgvHoaDon.Rows.Count > 0)
                {
                    dgvHoaDon.CurrentCell = dgvHoaDon.Rows[0].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucQuanLyHoaDon;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

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
                    string filter = string.Format("[{0}] LIKE '%{2}%' OR [{1}] LIKE '%{2}%'",
                        COL_MA_PHIEU, COL_TEN_SP, keyword);

                    dt.DefaultView.RowFilter = filter;
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}