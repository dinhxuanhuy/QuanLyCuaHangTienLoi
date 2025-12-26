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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCThongKeChiPhi : UserControl
    {
        // ----- BẮT ĐẦU THÊM MỚI -----
        // 1. Định nghĩa khuôn mẫu (delegate) cho sự kiện
        //    Sự kiện này sẽ gửi đi một UserControl
        public delegate void NavigateRequestEventHandler(UserControl uc);

        // 2. Định nghĩa sự kiện (event) dựa trên khuôn mẫu đó
        //    Form cha (frmTrangChu) sẽ "lắng nghe" sự kiện này
        public event NavigateRequestEventHandler NavigateRequest;
        // ----- KẾT THÚC THÊM MỚI -----
        BALThongKe dbtk;
        public UCThongKeChiPhi()
        {
            InitializeComponent();
            UIStyles.ApplyUIStyle(this);
            dbtk = new BALThongKe();
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            // Lấy UserControl Doanh Thu mà bạn đã tạo sẵn trong Program.cs
            UserControl ucCanChuyenToi = Program.ucThongKe;

            // 4. Kích hoạt sự kiện và gửi UserControl đi
            //    Dấu ? (null-conditional operator) để kiểm tra xem có ai (frmTrangChu)
            //    đang lắng nghe sự kiện này không. Nếu có, nó sẽ gọi Invoke.
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void btn_chiPhiTheoThang_Click(object sender, EventArgs e)
        {
            DataTable dt = dbtk.DoanhThuTheoThang();
            dgv_doanhThuTheoThang.DataSource = dt;
            dgv_doanhThuTheoThang.Visible = true;
            chart2.Visible = false;
            txt_thongKe.Visible = false;
            txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
            // Đã sửa: chart1 -> chart2
            chart2.Visible = false;
            txt_thongKe.Visible = false;

            // ----- BẮT ĐẦU CẤU HÌNH BIỂU ĐỒ (PHẦN 3) -----
            // Đã sửa: chart1 -> chart2
            chart2.Series.Clear();

            // Đã sửa: chart1 -> chart2
            if (chart2.Series.Count == 0)
            {
                // Đã sửa: chart1 -> chart2
                chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
            }
            var series = chart2.Series[0];
            series.Name = "Doanh thu theo tháng";

            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // 1. CỐ ĐỊNH CHIỀU RỘNG: Chuyển sang kiểu Auto (Rời rạc/Categorical)
            series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Auto;

            // Cố định chiều rộng cột (tương đối)
            series.CustomProperties = "PointWidth=0.8";

            // 2. GÁN DỮ LIỆU: Chỉ gán giá trị Y, giá trị X là Index
            series.Points.DataBindY(dt.DefaultView, "ChiPhiNhapHang");

            // Lặp qua các điểm dữ liệu để đặt nhãn X thủ công
            for (int i = 0; i < series.Points.Count; i++)
            {
                DateTime date = dt.Rows[i].Field<DateTime>("ThoiGianBatDau");
                series.Points[i].AxisLabel = date.ToString("MM-yyyy");
            }

            // 3. Cấu hình ChartArea
            // Đã sửa: chart1 -> chart2
            if (chart2.ChartAreas.Count > 0)
            {
                // Đã sửa: chart1 -> chart2
                var chartArea = chart2.ChartAreas[0];

                // 🌟 KHẮC PHỤC LỖI NHÃN ĐIỂM ĐẦU (Ngăn Chart tự động cắt/thay đổi nhãn)
                chartArea.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;

                // CỐ ĐỊNH TỶ LỆ TRỤC X: Đặt Minimum/Maximum dựa trên Index
                // 🌟 ĐIỀU CHỈNH: Đặt Minimum về 0 (thay vì 0.5) và thêm cột đệm +1 ở cuối.
                chartArea.AxisX.Minimum = 0;
                chartArea.AxisX.Maximum = dt.Rows.Count + 1; // Thêm 1 đơn vị đệm ảo ở cuối

                // CỐ ĐỊNH TRỤC Y (Chống giật khi trượt)
                decimal maxDoanhThuDecimal = dt.AsEnumerable().Max(row => row.Field<decimal>("ChiPhiNhapHang"));
                double maxDoanhThu = (double)maxDoanhThuDecimal * 1.1;
                chartArea.AxisY.Minimum = 0;
                chartArea.AxisY.Maximum = maxDoanhThu;

                // Tắt TOÀN BỘ CẤU HÌNH LIÊN QUAN ĐẾN SCROLL/ZOOM (trên chuột)
                chartArea.CursorX.IsUserEnabled = false;
                chartArea.CursorX.IsUserSelectionEnabled = false;

                // Cấu hình SÁT MÉP TRÁI (Margin)
                // 🌟 TẮT Margin để cột đầu sát mép trái
                chartArea.AxisX.IsMarginVisible = false;

                // Yêu cầu 1: Chỉ giữ lưới ngang, bỏ lưới dọc
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.Enabled = true;

                // Yêu cầu 2: Các nhãn để dọc (xoay nhãn 90 độ)
                chartArea.AxisX.LabelStyle.Angle = -90;

                // Buộc nhãn ở điểm đầu/cuối của phạm vi cuộn phải hiển thị
                chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;

                // Cố định Interval là 1 (cho mỗi điểm dữ liệu/cột)
                chartArea.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
                chartArea.AxisX.Interval = 1;

                // CẤU HÌNH THANH TRƯỢT
                int maxMonthsDisplay = 8; // Số cột tối đa hiển thị cùng lúc

                chartArea.AxisX.ScaleView.Zoomable = true;
                chartArea.AxisX.ScrollBar.Enabled = true;

                chartArea.AxisX.ScaleView.Size = maxMonthsDisplay;

                // Đảm bảo thanh cuộn bắt đầu từ đầu
                chartArea.AxisX.ScaleView.Position = 0;

                // Thiết lập tiêu đề trục
                chartArea.AxisX.Title = "Tháng";
                chartArea.AxisY.Title = "Chi Phí";
            }

            // Kích hoạt Legend
            // Đã sửa: chart1 -> chart2
            chart2.Legends[0].Enabled = true;
            // ----- KẾT THÚC CẤU HÌNH BIỂU ĐỒ (PHẦN 3) -----
            txt_thongKe.Text = PhanTichChiPhiTheoThang(dt);
        }
        private string PhanTichChiPhiTheoThang(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "Không có dữ liệu doanh thu để thống kê.";
            }

            // 1. Tính Tổng Chi Phí
            decimal tongChiPhi = dt.AsEnumerable()
                                     .Sum(row => row.Field<decimal>("ChiPhiNhapHang"));

            // 2. Tìm Ngày có Chi Phí Cao nhất
            DataRow rowMax = dt.AsEnumerable()
                               .OrderByDescending(row => row.Field<decimal>("ChiPhiNhapHang"))
                               .FirstOrDefault();

            // 3. Tìm Ngày có Chí Phí Thấp nhất
            DataRow rowMin = dt.AsEnumerable()
                               .OrderBy(row => row.Field<decimal>("ChiPhiNhapHang"))
                               .FirstOrDefault();

            // 4. Định dạng Chuỗi Kết Quả
            StringBuilder sb = new StringBuilder();

            // Loại bỏ '\n' thừa và chỉ dùng AppendLine để đảm bảo xuống dòng
            sb.AppendLine("📈 BÁO CÁO THỐNG KÊ CHI PHÍ THEO THÁNG");
            sb.AppendLine("---------------------------------------");
            sb.AppendLine($"Tổng số tháng có giao dịch: {dt.Rows.Count}");
            sb.AppendLine($"Tổng Chi Phí trong kỳ: {tongChiPhi:N0} VNĐ");
            sb.AppendLine(); // Thêm một dòng trống cho đẹp

            if (rowMax != null)
            {
                DateTime ngayMax = rowMax.Field<DateTime>("ThoiGianBatDau");
                decimal tienMax = rowMax.Field<decimal>("ChiPhiNhapHang");
                sb.AppendLine($"Tháng có Chi Phí CAO NHẤT: {ngayMax:MM/yyyy} ({tienMax:N0} VNĐ)");
            }

            if (rowMin != null && rowMin != rowMax)
            {
                DateTime ngayMin = rowMin.Field<DateTime>("ThoiGianBatDau");
                decimal tienMin = rowMin.Field<decimal>("ChiPhiNhapHang");
                sb.AppendLine($"Tháng có Chi Phí THẤP NHẤT: {ngayMin:MM/yyyy} ({tienMin:N0} VNĐ)");
            }
            sb.AppendLine("---------------------------------------");
            sb.AppendLine("Tiếp tục làm việc...");

            return sb.ToString();
        }

        private void txt_timKiem_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_timKiem.Text == "Nhập thời gian bắt đầu (dd/mm/yyyy)")
            {
                txt_timKiem.Text = "";
                txt_timKiem.ForeColor = Color.Black;
            }
        }

        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgv_doanhThuTheoThang.DataSource != null)
            {
                DataTable dt = (DataTable)dgv_doanhThuTheoThang.DataSource;
                string timkiem = txt_timKiem.Text.Trim();
                if (timkiem == "")
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    string filterExpression = string.Format("CONVERT(ThoiGianBatDau, 'System.String') LIKE '%{0}%'", timkiem);
                    dt.DefaultView.RowFilter = filterExpression;
                }
            }
        }

        private void btn_thongKe_Click(object sender, EventArgs e)
        {
            dgv_doanhThuTheoThang.Visible = false;
            chart2.Visible = true;
            txt_thongKe.Visible = true;
        }
    }
}
