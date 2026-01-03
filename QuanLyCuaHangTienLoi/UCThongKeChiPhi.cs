using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCThongKeChiPhi : UserControl
    {
        // Sự kiện chuyển trang
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // Khai báo lớp xử lý nghiệp vụ
        private BALThongKe dbtk;

        public UCThongKeChiPhi()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            dbtk = new BALThongKe();

            // Cấu hình giao diện và tải dữ liệu
            CauHinhGiaoDien();
            LoadDuLieuTuDatabase();
        }

        // Cấu hình giao diện TextBox hiển thị báo cáo
        private void CauHinhGiaoDien()
        {
            txt_thongKe.Multiline = true;
            txt_thongKe.ScrollBars = ScrollBars.Both;
            txt_thongKe.WordWrap = false;
            txt_thongKe.ReadOnly = true;

            txt_thongKe.BackColor = Color.White;
            txt_thongKe.ForeColor = Color.Black;
            txt_thongKe.Font = new Font("Consolas", 11, FontStyle.Regular);

            txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
        }

        // Tải dữ liệu từ database lên lưới và tạo báo cáo
        private void LoadDuLieuTuDatabase()
        {
            try
            {
                DataTable dt = dbtk.DoanhThuTheoThang();
                dgv_doanhThuTheoThang.DataSource = dt;
                txt_thongKe.Text = PhanTichChiPhiTheoThang(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Sự kiện nút Quay lại
        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucThongKe;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        // Sự kiện nút Làm mới dữ liệu
        private void btn_chiPhiTheoThang_Click(object sender, EventArgs e)
        {
            LoadDuLieuTuDatabase();
            dgv_doanhThuTheoThang.Visible = true;
            txt_thongKe.Visible = false;

            txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
        }

        // Sự kiện nút Xem Thống kê
        private void btn_thongKe_Click(object sender, EventArgs e)
        {
            dgv_doanhThuTheoThang.Visible = false;
            txt_thongKe.Visible = true;
            txt_thongKe.BringToFront();

            if (string.IsNullOrWhiteSpace(txt_thongKe.Text))
            {
                DataTable dt = (DataTable)dgv_doanhThuTheoThang.DataSource;
                if (dt != null)
                {
                    txt_thongKe.Text = PhanTichChiPhiTheoThang(dt);
                }
            }
        }

        // Xử lý sự kiện click ô tìm kiếm
        private void txt_timKiem_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_timKiem.Text == "Nhập thời gian bắt đầu (dd/mm/yyyy)")
            {
                txt_timKiem.Text = "";
                txt_timKiem.ForeColor = Color.Black;
            }
        }

        // Xử lý sự kiện nhập liệu ô tìm kiếm
        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgv_doanhThuTheoThang.DataSource != null)
            {
                DataTable dt = (DataTable)dgv_doanhThuTheoThang.DataSource;
                string timkiem = txt_timKiem.Text.Trim();

                try
                {
                    if (timkiem == "" || timkiem == "Nhập thời gian bắt đầu (dd/mm/yyyy)")
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        string filterExpression = string.Format("CONVERT(ThoiGianBatDau, 'System.String') LIKE '%{0}%'", timkiem);
                        dt.DefaultView.RowFilter = filterExpression;
                    }
                }
                catch
                {
                    // Bỏ qua lỗi cú pháp
                }
            }
        }

        // Hàm tạo báo cáo phân tích chi phí
        private string PhanTichChiPhiTheoThang(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return "⚠️ Không có dữ liệu chi phí để lập báo cáo.";

            var dataSorted = dt.AsEnumerable()
                               .Select(row => new
                               {
                                   ThoiGian = row.Field<DateTime>("ThoiGianBatDau"),
                                   ChiPhi = row.Field<decimal>("ChiPhiNhapHang")
                               })
                               .OrderBy(x => x.ThoiGian)
                               .ToList();

            decimal tongChiPhi = dataSorted.Sum(x => x.ChiPhi);
            decimal trungBinh = tongChiPhi / dataSorted.Count;

            var maxItem = dataSorted.OrderByDescending(x => x.ChiPhi).First();
            var minItem = dataSorted.OrderBy(x => x.ChiPhi).First();

            int totalWidth = 100;
            string lineTop = "╔" + new string('═', totalWidth) + "╗";
            string lineMiddle = "╟" + new string('─', totalWidth) + "╢";
            string lineBottom = "╚" + new string('═', totalWidth) + "╝";
            string sep = "│";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(lineTop);
            sb.AppendLine("║" + CenterString("BÁO CÁO QUẢN TRỊ CHI PHÍ NHẬP HÀNG", totalWidth) + "║");
            sb.AppendLine("║" + CenterString($"Kỳ báo cáo: {dataSorted.First().ThoiGian:MM/yyyy} - {dataSorted.Last().ThoiGian:MM/yyyy}", totalWidth) + "║");
            sb.AppendLine(lineMiddle);

            sb.AppendLine("║  1. TỔNG HỢP CHI PHÍ" + new string(' ', totalWidth - 21) + "║");
            sb.AppendLine(lineMiddle);

            sb.AppendLine(FormatTwoColumnRow("• TỔNG CHI PHÍ TOÀN KỲ", $"{tongChiPhi:N0} VNĐ", totalWidth));
            sb.AppendLine(FormatTwoColumnRow("• Trung bình mỗi tháng", $"{trungBinh:N0} VNĐ", totalWidth));
            sb.AppendLine(FormatTwoColumnRow("• Tháng chi nhiều nhất", $"{maxItem.ChiPhi:N0} VNĐ (T{maxItem.ThoiGian:MM})", totalWidth));
            sb.AppendLine(FormatTwoColumnRow("• Tháng chi ít nhất", $"{minItem.ChiPhi:N0} VNĐ (T{minItem.ThoiGian:MM})", totalWidth));

            sb.AppendLine(lineMiddle);

            sb.AppendLine("║  2. BIỂU ĐỒ CHI TIẾT" + new string(' ', totalWidth - 21) + "║");
            sb.AppendLine(lineMiddle);

            string headerTable = string.Format("║ {0,-15} {1} {2,25} {3} {4,-52} ║",
                "THÁNG", sep, "SỐ TIỀN (VNĐ)", sep, "   MỨC ĐỘ CHI TIÊU"); // Thêm khoảng trắng đầu tiêu đề

            sb.AppendLine(headerTable);

            string lineTable = string.Format("║{0}┼{1}┼{2}║",
                new string('─', 17), new string('─', 27), new string('─', 54));
            sb.AppendLine(lineTable);

            foreach (var item in dataSorted)
            {
                int maxBarLen = 48; // Giảm độ dài tối đa để chừa chỗ cho spacing
                double tyle = (double)item.ChiPhi / (double)maxItem.ChiPhi;
                int barLen = (int)(tyle * maxBarLen);
                if (barLen <= 0) barLen = 1;

                // Thêm 3 khoảng trắng vào trước thanh biểu đồ để tạo spacing bên trái
                string bar = "   " + new string('█', barLen);

                string row = string.Format("║ {0,-15} {1} {2,25:N0} {3} {4} ║",
                    "Tháng " + item.ThoiGian.ToString("MM/yyyy"),
                    sep,
                    item.ChiPhi,
                    sep,
                    bar.PadRight(52) // PadRight với độ rộng cột là 52
                    );

                if (row.Length < lineTop.Length) row += " ";

                sb.AppendLine(row);
            }

            sb.AppendLine(lineBottom);
            sb.AppendLine(" Ghi chú: Thanh biểu đồ thể hiện tỷ lệ chi phí so với tháng cao nhất.");

            return sb.ToString();
        }

        // Hàm căn giữa chuỗi
        private string CenterString(string text, int width)
        {
            if (text.Length >= width) return text;
            int leftPadding = (width - text.Length) / 2;
            int rightPadding = width - text.Length - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }

        // Hàm định dạng dòng 2 cột
        private string FormatTwoColumnRow(string label, string value, int width)
        {
            int contentWidth = width - 4;
            int labelWidth = 40;
            int valueWidth = contentWidth - labelWidth;

            return "║ " + label.PadRight(labelWidth) + value.PadLeft(valueWidth) + " ║";
        }
    }
}