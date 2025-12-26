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

namespace QuanLyCuaHangTienLoi
{
    public partial class UC_caLamViec : UserControl
    {
        // ----- BẮT ĐẦU THÊM MỚI -----
        // 1. Định nghĩa khuôn mẫu (delegate) cho sự kiện
        //    Sự kiện này sẽ gửi đi một UserControl
        public delegate void NavigateRequestEventHandler(UserControl uc);

        // 2. Định nghĩa sự kiện (event) dựa trên khuôn mẫu đó
        //    Form cha (frmTrangChu) sẽ "lắng nghe" sự kiện này
        public event NavigateRequestEventHandler NavigateRequest;
        // ----- KẾT THÚC THÊM MỚI -----
        BALQuanLyCa dbcl;
        public UC_caLamViec()
        {
            InitializeComponent();
            dbcl = new BALQuanLyCa();
            DataTable dt = dbcl.CaLamViec();
            dgv_caLamViec.DataSource = dt;
        }

        private void btn_dieuChinh_Click(object sender, EventArgs e)
        {
            // Lấy UserControl Doanh Thu mà bạn đã tạo sẵn trong Program.cs
            UserControl ucCanChuyenToi = Program.ucCaLamViecDieuChinh;
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

        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            // Bỏ qua nếu đang là placeholder hoặc nguồn dữ liệu rỗng
            if (dgv_caLamViec.DataSource == null || txt_timKiem.Text == "Nhập ngày (dd/mm/yyyy)")
            {
                return;
            }

            DataTable dt = (DataTable)dgv_caLamViec.DataSource;
            string timkiem = txt_timKiem.Text.Trim().Replace("'", "''"); // Xử lý dấu nháy đơn

            if (string.IsNullOrWhiteSpace(timkiem))
            {
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                string filterExpression = string.Format(
                    "CONVERT(NgayThangNam, 'System.String') LIKE '%{0}%' OR Buoi LIKE '%{0}%' OR TenNhanVien LIKE '%{0}%'",
                    timkiem
                );

                // Chạy filter (dùng CONVERT sang chuỗi để tìm kiếm trong chuỗi ngày)
                dt.DefaultView.RowFilter = filterExpression;
            }
        }

        private void txt_timKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_timKiem.Text))
            {
                txt_timKiem.Text = "Nhập ngày (dd/mm/yyyy)";
                txt_timKiem.ForeColor = Color.Gray; // Dùng màu xám cho placeholder
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            DataTable dt = dbcl.CaLamViec();
            dgv_caLamViec.DataSource = dt;
        }
    }
}
