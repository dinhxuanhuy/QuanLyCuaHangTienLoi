using BusinessAccessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCThongKeLoiNhuan : UserControl
    {
        // Sự kiện chuyển trang
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // Khai báo biến
        private BALThongKe dbtk;

        public UCThongKeLoiNhuan()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            dbtk = new BALThongKe();

            // Cấu hình giao diện và tải dữ liệu mặc định
            CauHinhGiaoDien();
            LoadDuLieuMacDinh();
        }

        // Cấu hình định dạng cho khung thống kê text
        private void CauHinhGiaoDien()
        {
            txt_thongKe.Multiline = true;
            txt_thongKe.ScrollBars = ScrollBars.Both;
            txt_thongKe.WordWrap = false;
            txt_thongKe.ReadOnly = true;

            txt_thongKe.BackColor = Color.White;
            txt_thongKe.ForeColor = Color.Black;
            // Font Consolas bắt buộc để căn cột và vẽ biểu đồ ký tự thẳng hàng
            txt_thongKe.Font = new Font("Consolas", 11, FontStyle.Regular);

            txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
        }

        private void LoadDuLieuMacDinh()
        {
            btn_chiPhiTheoThang_Click(null, null);
        }

        // Sự kiện nút Quay lại
        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            NavigateRequest?.Invoke(Program.ucThongKe);
        }

        // Sự kiện nút Cập Nhật / Xem Lợi Nhuận (Kiêm Refresh)
        private void btn_chiPhiTheoThang_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Tải lại dữ liệu từ DB
                DataTable dt = dbtk.DoanhThuTheoThang();

                // 2. Reset DataSource
                dgv_doanhThuTheoThang.DataSource = null;
                dgv_doanhThuTheoThang.DataSource = dt;

                // 3. Cập nhật giao diện
                dgv_doanhThuTheoThang.Visible = true;
                txt_thongKe.Visible = false;

                // 4. Reset tìm kiếm
                txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
                txt_timKiem.ForeColor = Color.Gray;

                // 5. Tính toán báo cáo chi tiết
                txt_thongKe.Text = PhanTichLoiNhuanChiTiet(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Sự kiện nút Thống Kê (Hiển thị báo cáo Text)
        private void btn_thongKe_Click(object sender, EventArgs e)
        {
            dgv_doanhThuTheoThang.Visible = false;
            txt_thongKe.Visible = true;
            txt_thongKe.BringToFront();
        }

        // Xử lý tìm kiếm
        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgv_doanhThuTheoThang.DataSource != null)
            {
                DataTable dt = (DataTable)dgv_doanhThuTheoThang.DataSource;
                string timkiem = txt_timKiem.Text.Trim();
                try
                {
                    if (string.IsNullOrEmpty(timkiem) || timkiem.StartsWith("Nhập"))
                        dt.DefaultView.RowFilter = "";
                    else
                        dt.DefaultView.RowFilter = string.Format("CONVERT(ThoiGianBatDau, 'System.String') LIKE '%{0}%'", timkiem);
                }
                catch { }
            }
        }

        private void txt_timKiem_Click(object sender, EventArgs e)
        {
            if (txt_timKiem.Text.StartsWith("Nhập"))
            {
                txt_timKiem.Text = "";
                txt_timKiem.ForeColor = Color.Black;
            }
        }

        // --- HÀM TẠO BÁO CÁO LỢI NHUẬN CHI TIẾT (Stack Bar Chart) ---
        private string PhanTichLoiNhuanChiTiet(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "⚠️ Không có dữ liệu.";

            // 1. Chuẩn bị dữ liệu đầy đủ 3 chỉ số
            var dataList = dt.AsEnumerable().Select(row => new
            {
                ThoiGian = row.Field<DateTime>("ThoiGianBatDau"),
                DoanhThu = row.Table.Columns.Contains("DoanhThu") ? row.Field<decimal>("DoanhThu") : 0,
                ChiPhi = row.Table.Columns.Contains("ChiPhiNhapHang") ? row.Field<decimal>("ChiPhiNhapHang") : 0,
                LoiNhuan = row.Table.Columns.Contains("LoiNhuan") ? row.Field<decimal>("LoiNhuan") : 0
            }).OrderBy(x => x.ThoiGian).ToList();

            // 2. Tính toán tổng quan
            decimal tongDoanhThu = dataList.Sum(x => x.DoanhThu);
            decimal tongChiPhi = dataList.Sum(x => x.ChiPhi);
            decimal tongLoiNhuan = dataList.Sum(x => x.LoiNhuan);
            decimal tySuatLN = tongDoanhThu > 0 ? (tongLoiNhuan / tongDoanhThu * 100) : 0;

            // Tìm Max Doanh Thu để làm mốc vẽ biểu đồ 100%
            decimal maxRevenue = dataList.Any() ? dataList.Max(x => x.DoanhThu) : 1;
            if (maxRevenue == 0) maxRevenue = 1;

            // 3. Thiết lập khung báo cáo (Rộng hơn để chứa nhiều cột)
            int totalWidth = 125;
            string lineTop = "╔" + new string('═', totalWidth) + "╗";
            string lineMiddle = "╟" + new string('─', totalWidth) + "╢";
            string lineBottom = "╚" + new string('═', totalWidth) + "╝";
            string sep = "│";

            StringBuilder sb = new StringBuilder();

            // HEADER
            sb.AppendLine(lineTop);
            sb.AppendLine("║" + CenterString("BÁO CÁO HIỆU QUẢ KINH DOANH & CƠ CẤU LỢI NHUẬN", totalWidth) + "║");
            sb.AppendLine("║" + CenterString($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}", totalWidth) + "║");
            sb.AppendLine(lineMiddle);

            // TỔNG QUAN
            sb.AppendLine("║  1. KẾT QUẢ KINH DOANH TOÀN KỲ" + new string(' ', totalWidth - 32) + "║");
            sb.AppendLine(lineMiddle);
            // Format 3 cột cân đối cho tổng quan
            string rowSummary = string.Format("║ Tổng Doanh Thu: {0,15:N0}  {1}  Tổng Chi Phí: {2,15:N0}  {1}  Lợi Nhuận Ròng: {3,15:N0} ║",
                tongDoanhThu, sep, tongChiPhi, tongLoiNhuan);
            sb.AppendLine(rowSummary);
            sb.AppendLine(string.Format("║ Tỷ suất Lợi nhuận/Doanh thu: {0,6:N2} % {1}", tySuatLN, new string(' ', totalWidth - 43) + "║"));
            sb.AppendLine(lineMiddle);

            // BẢNG CHI TIẾT
            sb.AppendLine("║  2. CHI TIẾT & BIỂU ĐỒ TRỰC QUAN (CƠ CẤU DOANH THU)" + new string(' ', totalWidth - 53) + "║");
            sb.AppendLine(lineMiddle);

            // Header Bảng: THÁNG | DOANH THU | CHI PHÍ | LỢI NHUẬN | BIỂU ĐỒ (Chồng)
            string headerTable = string.Format("║ {0,-11} {1} {2,16} {3} {4,16} {5} {6,16} {7} {8,-38} ║",
                "THÁNG", sep, "DOANH THU", sep, "CHI PHÍ", sep, "LỢI NHUẬN", sep, "   CƠ CẤU (▒ CHI PHÍ | █ LÃI)");
            sb.AppendLine(headerTable);

            string lineTable = string.Format("║{0}┼{1}┼{2}┼{3}┼{4}║",
                new string('─', 12), new string('─', 18), new string('─', 18), new string('─', 18), new string('─', 42));
            sb.AppendLine(lineTable);

            // VÒNG LẶP VẼ DỮ LIỆU
            foreach (var item in dataList)
            {
                // Logic vẽ biểu đồ chồng (Stacked Bar)
                // Tổng chiều dài thanh = Tỷ lệ Doanh thu tháng đó so với Max Doanh thu
                // Trong thanh đó: Phần Chi phí (▒) + Phần Lợi nhuận (█)
                int maxBarLen = 38;
                double ratioTotal = (double)item.DoanhThu / (double)maxRevenue;
                int totalLen = (int)(ratioTotal * maxBarLen);
                if (totalLen <= 0 && item.DoanhThu > 0) totalLen = 1;

                // Tính độ dài phần chi phí
                double ratioCost = item.DoanhThu > 0 ? (double)item.ChiPhi / (double)item.DoanhThu : 0;
                int lenCost = (int)(totalLen * ratioCost);
                // Độ dài phần lợi nhuận = Tổng - Chi phí (để đảm bảo khớp)
                int lenProfit = totalLen - lenCost;

                // Xử lý trường hợp lỗ (Chi phí > Doanh thu)
                if (lenProfit < 0) { lenCost = totalLen; lenProfit = 0; }

                string bar = "   " + new string('▒', lenCost) + new string('█', lenProfit);

                // In dòng
                string row = string.Format("║ {0,-11} {1} {2,16:N0} {3} {4,16:N0} {5} {6,16:N0} {7} {8} ║",
                    item.ThoiGian.ToString("MM/yyyy"),
                    sep, item.DoanhThu,
                    sep, item.ChiPhi,
                    sep, item.LoiNhuan,
                    sep, bar.PadRight(39)); // Pad để giữ khung

                // Fix lỗi biên
                if (row.Length < lineTop.Length) row += " ";
                sb.AppendLine(row);
            }

            sb.AppendLine(lineBottom);
            sb.AppendLine(" Ghi chú: Biểu đồ thể hiện tỷ trọng trong Doanh thu (▒ Chi phí, █ Lợi nhuận).");

            return sb.ToString();
        }

        // Hàm hỗ trợ căn giữa
        private string CenterString(string text, int width)
        {
            if (text.Length >= width) return text;
            int padL = (width - text.Length) / 2;
            int padR = width - text.Length - padL;
            return new string(' ', padL) + text + new string(' ', padR);
        }

        // Hàm hỗ trợ dòng 2 cột
        private string FormatTwoColumnRow(string label, string value, int width)
        {
            int contentW = width - 4;
            int lblW = 50;
            int valW = contentW - lblW;
            return "║ " + label.PadRight(lblW) + value.PadLeft(valW) + " ║";
        }
    }
}