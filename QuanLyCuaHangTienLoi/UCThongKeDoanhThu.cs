using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCThongKeDoanhThu : UserControl
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
        bool timKiemTheoNgay = false;
        bool timKiemTheoThang = false;
        string time = "";
        public UCThongKeDoanhThu()
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

        private void btn_doanhThuTheoNgay_Click(object sender, EventArgs e)
        {
            DataTable dt = dbtk.DoanhThuTheoNgay();
            dgv_doanhThuTheoNgay.DataSource = dt;
            dgv_doanhThuTheoNgay.Visible = true;
            dgv_doanhThuTheoThang.Visible = false;
            time = "ngay";
            chart2.Visible = false;
            chart1.Visible = false;
            txt_timKiem.Text = "Nhập ngày (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
            timKiemTheoNgay = true;
            timKiemTheoThang = false;
            txt_thongKe.Visible = false;

            // ----- BẮT ĐẦU CỐ ĐỊNH CHIỀU RỘNG CỘT (PHẦN 2) -----
            if (dgv_doanhThuTheoNgay.DataSource != null)
            {
                // Tắt chế độ tự động thay đổi kích thước
                dgv_doanhThuTheoNgay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                if (dgv_doanhThuTheoNgay.Columns.Contains("ngay"))
                {
                    dgv_doanhThuTheoNgay.Columns["ngay"].Width = 200; // Cố định chiều rộng cho cột 'ngay'
                    dgv_doanhThuTheoNgay.Columns["ngay"].HeaderText = "Ngày";
                }
                if (dgv_doanhThuTheoNgay.Columns.Contains("tongTien"))
                {
                    // Cho cột 'tongTien' tự động giãn để lấp đầy phần còn lại
                    dgv_doanhThuTheoNgay.Columns["tongTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgv_doanhThuTheoNgay.Columns["tongTien"].HeaderText = "Tổng Tiền";
                }
            }
            // ----- KẾT THÚC CỐ ĐỊNH CHIỀU RỘNG CỘT (PHẦN 2) -----

            // ----- BẮT ĐẦU CẤU HÌNH BIỂU ĐỒ (PHẦN 3) -----
            chart1.Series.Clear();
            // 2. Thiết lập Series và loại biểu đồ (Dạng CỘT - Column)
            if (chart1.Series.Count == 0)
            {
                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
            }
            var series = chart1.Series[0];
            series.Name = "Doanh thu theo ngày";

            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

            // Cố định chiều rộng cột (thường dùng 0.8 cho Column)
            series.CustomProperties = "PointWidth=0.8";

            // GÁN DỮ LIỆU
            series.Points.DataBindXY(dt.DefaultView, "ngay", dt.DefaultView, "tongTien");

            // 3. Cấu hình ChartArea
            if (chart1.ChartAreas.Count > 0)
            {
                var chartArea = chart1.ChartAreas[0];

                // Tắt TOÀN BỘ CẤU HÌNH LIÊN QUAN ĐẾN SCROLL/ZOOM (trên chuột)
                chartArea.CursorX.IsUserEnabled = false;
                chartArea.CursorX.IsUserSelectionEnabled = false;

                // Cấu hình SÁT MÉP TRÁI (Margin)
                chartArea.AxisX.IsMarginVisible = false;

                // Yêu cầu 1: Chỉ giữ lưới ngang, bỏ lưới dọc
                chartArea.AxisX.MajorGrid.Enabled = false; // Bỏ lưới dọc (Trục X)
                chartArea.AxisY.MajorGrid.Enabled = true;  // Giữ lưới ngang (Trục Y)

                // Yêu cầu 2: Các ngày để dọc (xoay nhãn 90 độ)
                chartArea.AxisX.LabelStyle.Angle = -90;

                // Yêu cầu: Định dạng nhãn trục X thành Ngày-Tháng-Năm (dd-MM-yyyy)
                chartArea.AxisX.LabelStyle.Format = "dd-MM-yyyy";

                // Yêu cầu 3: Tạo thanh trượt ngang cho trường hợp nhiều ngày
                // Bật Scroll và Zoom cho trục X
                chartArea.AxisX.ScaleView.Zoomable = true;
                chartArea.AxisX.ScrollBar.Enabled = true;

                // Thiết lập kích thước ban đầu (hiển thị 10 ngày đầu tiên)
                chartArea.AxisX.ScaleView.Size = 10;

                // Thiết lập tiêu đề trục
                chartArea.AxisX.Title = "Ngày";
                chartArea.AxisY.Title = "Tổng Tiền";
            }

            // Kích hoạt Legend
            chart1.Legends[0].Enabled = true;
            // ----- KẾT THÚC CẤU HÌNH BIỂU ĐỒ (PHẦN 3) -----
            txt_thongKe.Text = PhanTichDoanhThu(dt);
        }

        // Phương thức phân tích DataTable và trả về chuỗi thống kê
        private string PhanTichDoanhThu(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "Không có dữ liệu doanh thu để thống kê.";
            }

            // 1. Tính Tổng Doanh Thu
            decimal tongDoanhThu = dt.AsEnumerable()
                                     .Sum(row => row.Field<decimal>("tongTien"));

            // 2. Tìm Ngày có Doanh Thu Cao nhất
            DataRow rowMax = dt.AsEnumerable()
                               .OrderByDescending(row => row.Field<decimal>("tongTien"))
                               .FirstOrDefault();

            // 3. Tìm Ngày có Doanh Thu Thấp nhất
            DataRow rowMin = dt.AsEnumerable()
                               .OrderBy(row => row.Field<decimal>("tongTien"))
                               .FirstOrDefault();

            // 4. Định dạng Chuỗi Kết Quả
            StringBuilder sb = new StringBuilder();

            // Loại bỏ '\n' thừa và chỉ dùng AppendLine để đảm bảo xuống dòng
            sb.AppendLine("📈 BÁO CÁO THỐNG KÊ DOANH THU THEO NGÀY");
            sb.AppendLine("---------------------------------------");
            sb.AppendLine($"Tổng số ngày có giao dịch: {dt.Rows.Count}");
            sb.AppendLine($"Tổng Doanh Thu trong kỳ: {tongDoanhThu:N0} VNĐ");
            sb.AppendLine(); // Thêm một dòng trống cho đẹp

            if (rowMax != null)
            {
                DateTime ngayMax = rowMax.Field<DateTime>("ngay");
                decimal tienMax = rowMax.Field<decimal>("tongTien");
                sb.AppendLine($"Ngày có Doanh Thu CAO NHẤT: {ngayMax:dd/MM/yyyy} ({tienMax:N0} VNĐ)");
            }

            if (rowMin != null && rowMin != rowMax)
            {
                DateTime ngayMin = rowMin.Field<DateTime>("ngay");
                decimal tienMin = rowMin.Field<decimal>("tongTien");
                sb.AppendLine($"Ngày có Doanh Thu THẤP NHẤT: {ngayMin:dd/MM/yyyy} ({tienMin:N0} VNĐ)");
            }
            sb.AppendLine("---------------------------------------");
            sb.AppendLine("Tiếp tục làm việc...");

            return sb.ToString();
        }
        private void btn_doanhThuTheoThang_Click(object sender, EventArgs e)
        {
            DataTable dt = dbtk.DoanhThuTheoThang();
            dgv_doanhThuTheoThang.DataSource = dt;
            dgv_doanhThuTheoThang.Visible = true;
            time = "thang";
            dgv_doanhThuTheoNgay.Visible = false;
            txt_timKiem.Text = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            txt_timKiem.ForeColor = Color.Gray;
            timKiemTheoNgay = false;
            timKiemTheoThang = true;
            // Đã sửa: chart1 -> chart2
            chart2.Visible = false;
            chart1.Visible = false;
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
            series.Points.DataBindY(dt.DefaultView, "DoanhThu");

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
                decimal maxDoanhThuDecimal = dt.AsEnumerable().Max(row => row.Field<decimal>("DoanhThu"));
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
                chartArea.AxisY.Title = "Tổng Tiền";
            }

            // Kích hoạt Legend
            // Đã sửa: chart1 -> chart2
            chart2.Legends[0].Enabled = true;
            // ----- KẾT THÚC CẤU HÌNH BIỂU ĐỒ (PHẦN 3) -----
            txt_thongKe.Text = PhanTichDoanhThuTheoThang(dt);
        }


        private string PhanTichDoanhThuTheoThang(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "Không có dữ liệu doanh thu để thống kê.";
            }

            // 1. Tính Tổng Doanh Thu
            decimal tongDoanhThu = dt.AsEnumerable()
                                     .Sum(row => row.Field<decimal>("DoanhThu"));

            // 2. Tìm Ngày có Doanh Thu Cao nhất
            DataRow rowMax = dt.AsEnumerable()
                               .OrderByDescending(row => row.Field<decimal>("DoanhThu"))
                               .FirstOrDefault();

            // 3. Tìm Ngày có Doanh Thu Thấp nhất
            DataRow rowMin = dt.AsEnumerable()
                               .OrderBy(row => row.Field<decimal>("DoanhThu"))
                               .FirstOrDefault();

            // 4. Định dạng Chuỗi Kết Quả
            StringBuilder sb = new StringBuilder();

            // Loại bỏ '\n' thừa và chỉ dùng AppendLine để đảm bảo xuống dòng
            sb.AppendLine("📈 BÁO CÁO THỐNG KÊ DOANH THU THEO THÁNG");
            sb.AppendLine("---------------------------------------");
            sb.AppendLine($"Tổng số tháng có giao dịch: {dt.Rows.Count}");
            sb.AppendLine($"Tổng Doanh Thu trong kỳ: {tongDoanhThu:N0} VNĐ");
            sb.AppendLine(); // Thêm một dòng trống cho đẹp

            if (rowMax != null)
            {
                DateTime ngayMax = rowMax.Field<DateTime>("ThoiGianBatDau");
                decimal tienMax = rowMax.Field<decimal>("DoanhThu");
                sb.AppendLine($"Tháng có Doanh Thu CAO NHẤT: {ngayMax:MM/yyyy} ({tienMax:N0} VNĐ)");
            }

            if (rowMin != null && rowMin != rowMax)
            {
                DateTime ngayMin = rowMin.Field<DateTime>("ThoiGianBatDau");
                decimal tienMin = rowMin.Field<decimal>("DoanhThu");
                sb.AppendLine($"Tháng có Doanh Thu THẤP NHẤT: {ngayMin:MM/yyyy} ({tienMin:N0} VNĐ)");
            }
            sb.AppendLine("---------------------------------------");
            sb.AppendLine("Tiếp tục làm việc...");

            return sb.ToString();
        }
        private void txt_timKiem_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_timKiem.Text == "Nhập thời gian bắt đầu (dd/mm/yyyy)" || txt_timKiem.Text == "Nhập ngày (dd/mm/yyyy)")
            {
                txt_timKiem.Text = "";
                txt_timKiem.ForeColor = Color.Black;
            }
        }

        private void txt_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (timKiemTheoThang)
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
            if (timKiemTheoNgay)
            {
                if (dgv_doanhThuTheoNgay.DataSource != null)
                {
                    DataTable dt = (DataTable)dgv_doanhThuTheoNgay.DataSource;
                    string timkiem = txt_timKiem.Text.Trim();
                    if (timkiem == "")
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        string filterExpression = string.Format("CONVERT(ngay, 'System.String') LIKE '%{0}%'", timkiem);
                        dt.DefaultView.RowFilter = filterExpression;
                    }
                }
            }
        }

        private void btn_thongKe_Click(object sender, EventArgs e)
        {
            dgv_doanhThuTheoNgay.Visible = false;
            dgv_doanhThuTheoThang.Visible = false;
            if (time == "ngay")
            {
                chart2.Visible = false;
                chart1.Visible = true;
            }
            else if(time == "thang")
            {
                chart2.Visible = true;
                chart1.Visible = false;
            }
            txt_thongKe.Visible = true;
        }
    }
}
