using BusinessAccessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCThongKeDoanhThu : UserControl
    {
        // Sự kiện chuyển trang
        public delegate void NavigateRequestEventHandler(UserControl uc);
        public event NavigateRequestEventHandler NavigateRequest;

        // Khai báo biến
        private BALThongKe dbtk;
        private bool timKiemTheoNgay = false;
        private bool timKiemTheoThang = false;

        public UCThongKeDoanhThu()
        {
            InitializeComponent();
            Dock = DockStyle.Fill; // Tự động lấp đầy form cha
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
            // Dùng Font Consolas để căn chỉnh cột thẳng hàng
            txt_thongKe.Font = new Font("Consolas", 11, FontStyle.Regular);

            txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
        }

        // Tải dữ liệu mặc định khi vừa mở UC (Mặc định: Theo Ngày)
        private void LoadDuLieuMacDinh()
        {
            // Giả lập thao tác bấm nút "Theo ngày" để load dữ liệu ngay lập tức
            btn_doanhThuTheoNgay_Click(null, null);
        }

        // Sự kiện nút Quay lại
        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            NavigateRequest?.Invoke(Program.ucThongKe);
        }

        // Sự kiện nút Doanh thu theo NGÀY (Kiêm chức năng Làm mới / Refresh)
        private void btn_doanhThuTheoNgay_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Tải lại dữ liệu từ DB
                DataTable dt = dbtk.DoanhThuTheoNgay();

                // 2. Reset DataSource để ép vẽ lại lưới
                dgv_doanhThuTheoNgay.DataSource = null;
                dgv_doanhThuTheoNgay.DataSource = dt;

                // 3. Cập nhật giao diện
                dgv_doanhThuTheoNgay.Visible = true;
                dgv_doanhThuTheoThang.Visible = false;
                txt_thongKe.Visible = false;

                // 4. Reset tìm kiếm & trạng thái
                txt_timKiem.Text = "Nhập ngày (dd/mm/yyyy)";
                txt_timKiem.ForeColor = Color.Gray;
                timKiemTheoNgay = true;
                timKiemTheoThang = false;

                // 5. Cấu hình cột Grid
                if (dgv_doanhThuTheoNgay.DataSource != null)
                {
                    dgv_doanhThuTheoNgay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    if (dgv_doanhThuTheoNgay.Columns.Contains("ngay"))
                    {
                        dgv_doanhThuTheoNgay.Columns["ngay"].Width = 200;
                        dgv_doanhThuTheoNgay.Columns["ngay"].HeaderText = "Ngày";
                    }
                    if (dgv_doanhThuTheoNgay.Columns.Contains("tongTien"))
                    {
                        dgv_doanhThuTheoNgay.Columns["tongTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgv_doanhThuTheoNgay.Columns["tongTien"].HeaderText = "Tổng Tiền";
                    }
                }

                // 6. Tính toán sẵn báo cáo text (chưa hiện ngay)
                txt_thongKe.Text = PhanTichDoanhThuNgay(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu ngày: " + ex.Message);
            }
        }

        // Sự kiện nút Doanh thu theo THÁNG (Kiêm chức năng Làm mới / Refresh)
        private void btn_doanhThuTheoThang_Click(object sender, EventArgs e)
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
                dgv_doanhThuTheoNgay.Visible = false;
                txt_thongKe.Visible = false;

                // 4. Reset tìm kiếm & trạng thái
                txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
                txt_timKiem.ForeColor = Color.Gray;
                timKiemTheoNgay = false;
                timKiemTheoThang = true;

                // 5. Tính toán sẵn báo cáo text
                txt_thongKe.Text = PhanTichDoanhThuThang(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tháng: " + ex.Message);
            }
        }

        // Sự kiện nút Thống Kê (Hiển thị báo cáo Text)
        private void btn_thongKe_Click(object sender, EventArgs e)
        {
            dgv_doanhThuTheoNgay.Visible = false;
            dgv_doanhThuTheoThang.Visible = false;

            txt_thongKe.Visible = true;
            txt_thongKe.BringToFront();
        }

        // Xử lý sự kiện tìm kiếm
        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            string timkiem = txt_timKiem.Text.Trim();

            if (timKiemTheoThang && dgv_doanhThuTheoThang.DataSource != null)
            {
                DataTable dt = (DataTable)dgv_doanhThuTheoThang.DataSource;
                if (string.IsNullOrEmpty(timkiem) || timkiem.StartsWith("Nhập"))
                    dt.DefaultView.RowFilter = "";
                else
                    dt.DefaultView.RowFilter = string.Format("CONVERT(ThoiGianBatDau, 'System.String') LIKE '%{0}%'", timkiem);
            }
            else if (timKiemTheoNgay && dgv_doanhThuTheoNgay.DataSource != null)
            {
                DataTable dt = (DataTable)dgv_doanhThuTheoNgay.DataSource;
                if (string.IsNullOrEmpty(timkiem) || timkiem.StartsWith("Nhập"))
                    dt.DefaultView.RowFilter = "";
                else
                    dt.DefaultView.RowFilter = string.Format("CONVERT(ngay, 'System.String') LIKE '%{0}%'", timkiem);
            }
        }

        // Xử lý click ô tìm kiếm
        private void txt_timKiem_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_timKiem.Text.StartsWith("Nhập"))
            {
                txt_timKiem.Text = "";
                txt_timKiem.ForeColor = Color.Black;
            }
        }

        // Hàm tạo báo cáo cho NGÀY
        private string PhanTichDoanhThuNgay(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "⚠️ Không có dữ liệu.";

            var data = dt.AsEnumerable().Select(row => new {
                ThoiGian = row.Field<DateTime>("ngay"),
                Tien = row.Field<decimal>("tongTien")
            }).OrderBy(x => x.ThoiGian).ToList();

            return TaoBaoCaoChung(data, "DOANH THU THEO NGÀY", "dd/MM/yyyy");
        }

        // Hàm tạo báo cáo cho THÁNG
        private string PhanTichDoanhThuThang(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "⚠️ Không có dữ liệu.";

            var data = dt.AsEnumerable().Select(row => new {
                ThoiGian = row.Field<DateTime>("ThoiGianBatDau"),
                Tien = row.Field<decimal>("DoanhThu")
            }).OrderBy(x => x.ThoiGian).ToList();

            return TaoBaoCaoChung(data, "DOANH THU THEO THÁNG", "MM/yyyy");
        }

        // Hàm chung để vẽ khung báo cáo và biểu đồ Text
        private string TaoBaoCaoChung(dynamic dataList, string title, string dateFormat)
        {
            decimal tongTien = 0;
            decimal maxTien = 0;
            decimal minTien = 0;
            DateTime maxDate = DateTime.MinValue;
            DateTime minDate = DateTime.MinValue;

            int count = 0;
            foreach (var item in dataList)
            {
                decimal val = item.Tien;
                tongTien += val;
                if (count == 0 || val > maxTien) { maxTien = val; maxDate = item.ThoiGian; }
                if (count == 0 || val < minTien) { minTien = val; minDate = item.ThoiGian; }
                count++;
            }
            decimal trungBinh = count > 0 ? tongTien / count : 0;

            // Cấu hình khung 100 ký tự
            int totalWidth = 100;
            string lineTop = "╔" + new string('═', totalWidth) + "╗";
            string lineMid = "╟" + new string('─', totalWidth) + "╢";
            string lineBot = "╚" + new string('═', totalWidth) + "╝";
            string sep = "│";

            StringBuilder sb = new StringBuilder();

            // 1. Header
            sb.AppendLine(lineTop);
            sb.AppendLine("║" + CenterString($"BÁO CÁO {title}", totalWidth) + "║");
            sb.AppendLine("║" + CenterString($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}", totalWidth) + "║");
            sb.AppendLine(lineMid);

            // 2. Tổng quan
            sb.AppendLine("║  1. TỔNG QUAN TÀI CHÍNH" + new string(' ', totalWidth - 24) + "║");
            sb.AppendLine(lineMid);
            sb.AppendLine(FormatTwoColumnRow("• TỔNG DOANH THU", $"{tongTien:N0} VNĐ", totalWidth));
            sb.AppendLine(FormatTwoColumnRow("• Trung bình", $"{trungBinh:N0} VNĐ", totalWidth));
            sb.AppendLine(FormatTwoColumnRow("• Cao nhất", $"{maxTien:N0} VNĐ ({maxDate.ToString(dateFormat)})", totalWidth));
            sb.AppendLine(FormatTwoColumnRow("• Thấp nhất", $"{minTien:N0} VNĐ ({minDate.ToString(dateFormat)})", totalWidth));
            sb.AppendLine(lineMid);

            // 3. Biểu đồ chi tiết
            sb.AppendLine("║  2. BIỂU ĐỒ CHI TIẾT" + new string(' ', totalWidth - 21) + "║");
            sb.AppendLine(lineMid);

            // Header bảng
            // Cột: THỜI GIAN(15) | DOANH THU(25) | BIỂU ĐỒ(còn lại)
            string headerTbl = string.Format("║ {0,-15} {1} {2,25} {3} {4,-52} ║",
                "THỜI GIAN", sep, "DOANH THU (VNĐ)", sep, "   MỨC ĐỘ TĂNG TRƯỞNG");
            sb.AppendLine(headerTbl);

            string lineTbl = string.Format("║{0}┼{1}┼{2}║",
               new string('─', 17), new string('─', 27), new string('─', 54));
            sb.AppendLine(lineTbl);

            // Vẽ dòng dữ liệu
            foreach (var item in dataList)
            {
                int maxBarLen = 48; // Giới hạn chiều dài thanh (để chừa chỗ cho padding)
                double ratio = maxTien > 0 ? (double)item.Tien / (double)maxTien : 0;
                int barLen = (int)(ratio * maxBarLen);
                if (barLen <= 0 && item.Tien > 0) barLen = 1;

                // Thêm 3 khoảng trắng trước biểu đồ để lệch về trái, cân đối hơn
                string bar = "   " + new string('█', barLen);

                string row = string.Format("║ {0,-15} {1} {2,25:N0} {3} {4} ║",
                     item.ThoiGian.ToString(dateFormat),
                     sep,
                     item.Tien,
                     sep,
                     bar.PadRight(52)
                     );

                // Fix lỗi hiển thị biên
                if (row.Length < lineTop.Length) row += " ";
                sb.AppendLine(row);
            }
            sb.AppendLine(lineBot);

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

        // Hàm hỗ trợ dòng 2 cột (Label - Value)
        private string FormatTwoColumnRow(string label, string value, int width)
        {
            int contentW = width - 4;
            int lblW = 40;
            int valW = contentW - lblW;
            return "║ " + label.PadRight(lblW) + value.PadLeft(valW) + " ║";
        }
    }
}