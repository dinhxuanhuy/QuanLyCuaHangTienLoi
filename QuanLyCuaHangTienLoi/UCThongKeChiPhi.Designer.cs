namespace QuanLyCuaHangTienLoi
{
    partial class UCThongKeChiPhi
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.gbChiTiet = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btn_thongKe = new Guna.UI2.WinForms.Guna2Button();
            this.txt_timKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btn_chiPhiTheoThang = new Guna.UI2.WinForms.Guna2Button();
            this.btn_quayLai = new Guna.UI2.WinForms.Guna2Button();
            this.txt_thongKe = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgv_doanhThuTheoThang = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoanhThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_doanhThuTheoThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // gbChiTiet
            // 
            this.gbChiTiet.BackColor = System.Drawing.Color.White;
            this.gbChiTiet.BorderColor = System.Drawing.Color.White;
            this.gbChiTiet.BorderRadius = 12;
            this.gbChiTiet.Controls.Add(this.btn_thongKe);
            this.gbChiTiet.Controls.Add(this.txt_timKiem);
            this.gbChiTiet.Controls.Add(this.guna2HtmlLabel3);
            this.gbChiTiet.Controls.Add(this.btn_chiPhiTheoThang);
            this.gbChiTiet.Controls.Add(this.btn_quayLai);
            this.gbChiTiet.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.gbChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbChiTiet.FillColor = System.Drawing.Color.White;
            this.gbChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbChiTiet.ForeColor = System.Drawing.Color.Black;
            this.gbChiTiet.Location = new System.Drawing.Point(0, 0);
            this.gbChiTiet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbChiTiet.Name = "gbChiTiet";
            this.gbChiTiet.Size = new System.Drawing.Size(1150, 243);
            this.gbChiTiet.TabIndex = 30;
            this.gbChiTiet.Text = "Thống Kê Chi Phí";
            // 
            // btn_thongKe
            // 
            this.btn_thongKe.BackColor = System.Drawing.Color.White;
            this.btn_thongKe.BorderRadius = 8;
            this.btn_thongKe.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_thongKe.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_thongKe.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_thongKe.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_thongKe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(83)))), ((int)(((byte)(251)))));
            this.btn_thongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_thongKe.ForeColor = System.Drawing.Color.White;
            this.btn_thongKe.Location = new System.Drawing.Point(847, 190);
            this.btn_thongKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_thongKe.Name = "btn_thongKe";
            this.btn_thongKe.Size = new System.Drawing.Size(220, 42);
            this.btn_thongKe.TabIndex = 52;
            this.btn_thongKe.Text = "Thống Kê";
            this.btn_thongKe.Click += new System.EventHandler(this.btn_thongKe_Click);
            // 
            // txt_timKiem
            // 
            this.txt_timKiem.BackColor = System.Drawing.Color.White;
            this.txt_timKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_timKiem.DefaultText = "Nhập thời gian bắt đầu (dd/mm/yyyy)";
            this.txt_timKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_timKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_timKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_timKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_timKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txt_timKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_timKiem.ForeColor = System.Drawing.Color.Gray;
            this.txt_timKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txt_timKiem.Location = new System.Drawing.Point(204, 164);
            this.txt_timKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_timKiem.Name = "txt_timKiem";
            this.txt_timKiem.PlaceholderText = "";
            this.txt_timKiem.SelectedText = "";
            this.txt_timKiem.Size = new System.Drawing.Size(353, 43);
            this.txt_timKiem.TabIndex = 51;
            this.txt_timKiem.TextChanged += new System.EventHandler(this.txt_timKiem_TextChanged);
            this.txt_timKiem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_timKiem_MouseClick);
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.White;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(31, 164);
            this.guna2HtmlLabel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(167, 28);
            this.guna2HtmlLabel3.TabIndex = 50;
            this.guna2HtmlLabel3.Text = "Tìm kiếm";
            // 
            // btn_chiPhiTheoThang
            // 
            this.btn_chiPhiTheoThang.BackColor = System.Drawing.Color.White;
            this.btn_chiPhiTheoThang.BorderRadius = 8;
            this.btn_chiPhiTheoThang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_chiPhiTheoThang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_chiPhiTheoThang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_chiPhiTheoThang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_chiPhiTheoThang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_chiPhiTheoThang.ForeColor = System.Drawing.Color.White;
            this.btn_chiPhiTheoThang.Location = new System.Drawing.Point(31, 64);
            this.btn_chiPhiTheoThang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_chiPhiTheoThang.Name = "btn_chiPhiTheoThang";
            this.btn_chiPhiTheoThang.Size = new System.Drawing.Size(260, 68);
            this.btn_chiPhiTheoThang.TabIndex = 49;
            this.btn_chiPhiTheoThang.Text = "Cập Nhật Thống Kê";
            this.btn_chiPhiTheoThang.Click += new System.EventHandler(this.btn_chiPhiTheoThang_Click);
            // 
            // btn_quayLai
            // 
            this.btn_quayLai.BackColor = System.Drawing.Color.White;
            this.btn_quayLai.BorderRadius = 8;
            this.btn_quayLai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_quayLai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_quayLai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_quayLai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_quayLai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(83)))), ((int)(((byte)(251)))));
            this.btn_quayLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_quayLai.ForeColor = System.Drawing.Color.White;
            this.btn_quayLai.Location = new System.Drawing.Point(847, 73);
            this.btn_quayLai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_quayLai.Name = "btn_quayLai";
            this.btn_quayLai.Size = new System.Drawing.Size(220, 68);
            this.btn_quayLai.TabIndex = 38;
            this.btn_quayLai.Text = "Quay lại";
            this.btn_quayLai.Click += new System.EventHandler(this.btn_quayLai_Click);
            // 
            // txt_thongKe
            // 
            this.txt_thongKe.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_thongKe.DefaultText = "";
            this.txt_thongKe.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_thongKe.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_thongKe.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_thongKe.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_thongKe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txt_thongKe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_thongKe.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txt_thongKe.Location = new System.Drawing.Point(603, 249);
            this.txt_thongKe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_thongKe.Multiline = true;
            this.txt_thongKe.Name = "txt_thongKe";
            this.txt_thongKe.PlaceholderText = "";
            this.txt_thongKe.SelectedText = "";
            this.txt_thongKe.Size = new System.Drawing.Size(547, 350);
            this.txt_thongKe.TabIndex = 53;
            this.txt_thongKe.Visible = false;
            // 
            // dgv_doanhThuTheoThang
            // 
            this.dgv_doanhThuTheoThang.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_doanhThuTheoThang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_doanhThuTheoThang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgv_doanhThuTheoThang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_doanhThuTheoThang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_doanhThuTheoThang.ColumnHeadersHeight = 66;
            this.dgv_doanhThuTheoThang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_doanhThuTheoThang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column1,
            this.DoanhThu,
            this.Column3});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(233)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(185)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_doanhThuTheoThang.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_doanhThuTheoThang.GridColor = System.Drawing.Color.LightGray;
            this.dgv_doanhThuTheoThang.Location = new System.Drawing.Point(0, 249);
            this.dgv_doanhThuTheoThang.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_doanhThuTheoThang.Name = "dgv_doanhThuTheoThang";
            this.dgv_doanhThuTheoThang.RowHeadersVisible = false;
            this.dgv_doanhThuTheoThang.RowHeadersWidth = 72;
            this.dgv_doanhThuTheoThang.RowTemplate.Height = 50;
            this.dgv_doanhThuTheoThang.Size = new System.Drawing.Size(1148, 351);
            this.dgv_doanhThuTheoThang.TabIndex = 55;
            this.dgv_doanhThuTheoThang.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Blue;
            this.dgv_doanhThuTheoThang.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_doanhThuTheoThang.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv_doanhThuTheoThang.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv_doanhThuTheoThang.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgv_doanhThuTheoThang.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgv_doanhThuTheoThang.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv_doanhThuTheoThang.ThemeStyle.GridColor = System.Drawing.Color.LightGray;
            this.dgv_doanhThuTheoThang.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(242)))));
            this.dgv_doanhThuTheoThang.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_doanhThuTheoThang.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_doanhThuTheoThang.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgv_doanhThuTheoThang.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_doanhThuTheoThang.ThemeStyle.HeaderStyle.Height = 66;
            this.dgv_doanhThuTheoThang.ThemeStyle.ReadOnly = false;
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(233)))), ((int)(((byte)(252)))));
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.Height = 50;
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(185)))), ((int)(((byte)(246)))));
            this.dgv_doanhThuTheoThang.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "MaDT";
            this.dataGridViewTextBoxColumn1.FillWeight = 39.62784F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã DT";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ThoiGianBatDau";
            this.dataGridViewTextBoxColumn2.FillWeight = 245.9336F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Thời Gian Bắt Đầu";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 9;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ThoiGianKetThuc";
            this.dataGridViewTextBoxColumn3.FillWeight = 14.4385F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Thời Gian Kết Thúc";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "ChiPhiNhapHang";
            this.Column1.HeaderText = "Chi Phí Nhập Hàng";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // DoanhThu
            // 
            this.DoanhThu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DoanhThu.DataPropertyName = "DoanhThu";
            this.DoanhThu.HeaderText = "Doanh Thu";
            this.DoanhThu.MinimumWidth = 6;
            this.DoanhThu.Name = "DoanhThu";
            this.DoanhThu.Width = 150;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.DataPropertyName = "LoiNhuan";
            this.Column3.HeaderText = "Lợi Nhuận";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // chart2
            // 
            chartArea2.AxisX.ScaleView.Position = 1D;
            chartArea2.AxisX.ScaleView.Size = 5D;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(3, 248);
            this.chart2.Name = "chart2";
            series2.BackImageTransparentColor = System.Drawing.Color.White;
            series2.BackSecondaryColor = System.Drawing.Color.Black;
            series2.BorderColor = System.Drawing.Color.Red;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.ShadowColor = System.Drawing.Color.Black;
            series2.YValuesPerPoint = 4;
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(595, 348);
            this.chart2.TabIndex = 56;
            this.chart2.Text = "chart2";
            this.chart2.Visible = false;
            // 
            // UCThongKeChiPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.dgv_doanhThuTheoThang);
            this.Controls.Add(this.txt_thongKe);
            this.Controls.Add(this.gbChiTiet);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UCThongKeChiPhi";
            this.Size = new System.Drawing.Size(1150, 600);
            this.gbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_doanhThuTheoThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GroupBox gbChiTiet;
        private Guna.UI2.WinForms.Guna2Button btn_quayLai;
        private Guna.UI2.WinForms.Guna2TextBox txt_timKiem;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button btn_chiPhiTheoThang;
        private Guna.UI2.WinForms.Guna2TextBox txt_thongKe;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_doanhThuTheoThang;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoanhThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Guna.UI2.WinForms.Guna2Button btn_thongKe;
    }
}
