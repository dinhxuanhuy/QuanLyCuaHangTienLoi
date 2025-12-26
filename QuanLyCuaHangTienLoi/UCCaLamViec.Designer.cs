namespace QuanLyCuaHangTienLoi
{
    partial class UC_caLamViec
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
            this.txt_timKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_dieuChinh = new Guna.UI2.WinForms.Guna2Button();
            this.gbChiTiet = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dgv_caLamViec = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_caLamViec)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_timKiem
            // 
            this.txt_timKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_timKiem.DefaultText = "Nhập ngày (dd/mm/yyyy)";
            this.txt_timKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_timKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_timKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_timKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_timKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_timKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_timKiem.ForeColor = System.Drawing.Color.Gray;
            this.txt_timKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_timKiem.Location = new System.Drawing.Point(230, 98);
            this.txt_timKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_timKiem.Name = "txt_timKiem";
            this.txt_timKiem.PlaceholderText = "";
            this.txt_timKiem.SelectedText = "";
            this.txt_timKiem.Size = new System.Drawing.Size(353, 43);
            this.txt_timKiem.TabIndex = 51;
            this.txt_timKiem.TextChanged += new System.EventHandler(this.txt_timKiem_TextChanged);
            this.txt_timKiem.Click += new System.EventHandler(this.txt_timKiem_Click);
            this.txt_timKiem.Leave += new System.EventHandler(this.txt_timKiem_Leave);
            // 
            // btn_dieuChinh
            // 
            this.btn_dieuChinh.BorderRadius = 8;
            this.btn_dieuChinh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_dieuChinh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_dieuChinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_dieuChinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_dieuChinh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_dieuChinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_dieuChinh.ForeColor = System.Drawing.Color.White;
            this.btn_dieuChinh.Location = new System.Drawing.Point(879, 64);
            this.btn_dieuChinh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_dieuChinh.Name = "btn_dieuChinh";
            this.btn_dieuChinh.Size = new System.Drawing.Size(216, 63);
            this.btn_dieuChinh.TabIndex = 52;
            this.btn_dieuChinh.Text = "Chỉnh sửa";
            this.btn_dieuChinh.Click += new System.EventHandler(this.btn_dieuChinh_Click);
            // 
            // gbChiTiet
            // 
            this.gbChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbChiTiet.BorderRadius = 12;
            this.gbChiTiet.Controls.Add(this.btnLamMoi);
            this.gbChiTiet.Controls.Add(this.guna2HtmlLabel1);
            this.gbChiTiet.Controls.Add(this.btn_dieuChinh);
            this.gbChiTiet.Controls.Add(this.txt_timKiem);
            this.gbChiTiet.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gbChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbChiTiet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbChiTiet.ForeColor = System.Drawing.Color.Black;
            this.gbChiTiet.Location = new System.Drawing.Point(0, 0);
            this.gbChiTiet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbChiTiet.Name = "gbChiTiet";
            this.gbChiTiet.Size = new System.Drawing.Size(1150, 201);
            this.gbChiTiet.TabIndex = 57;
            this.gbChiTiet.Text = "Ca Làm Việc";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 8;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(879, 132);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(216, 47);
            this.btnLamMoi.TabIndex = 59;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(42, 98);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(151, 28);
            this.guna2HtmlLabel1.TabIndex = 53;
            this.guna2HtmlLabel1.Text = "Tìm kiếm";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(31, 164);
            this.guna2HtmlLabel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(167, 28);
            this.guna2HtmlLabel3.TabIndex = 50;
            this.guna2HtmlLabel3.Text = "Tìm kiếm";
            // 
            // dgv_caLamViec
            // 
            this.dgv_caLamViec.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(223)))), ((int)(((byte)(251)))));
            this.dgv_caLamViec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_caLamViec.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_caLamViec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_caLamViec.ColumnHeadersHeight = 66;
            this.dgv_caLamViec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_caLamViec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(233)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(185)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_caLamViec.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_caLamViec.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.dgv_caLamViec.Location = new System.Drawing.Point(0, 206);
            this.dgv_caLamViec.Name = "dgv_caLamViec";
            this.dgv_caLamViec.RowHeadersVisible = false;
            this.dgv_caLamViec.RowHeadersWidth = 72;
            this.dgv_caLamViec.RowTemplate.Height = 50;
            this.dgv_caLamViec.Size = new System.Drawing.Size(1150, 391);
            this.dgv_caLamViec.TabIndex = 58;
            this.dgv_caLamViec.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Blue;
            this.dgv_caLamViec.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(223)))), ((int)(((byte)(251)))));
            this.dgv_caLamViec.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv_caLamViec.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv_caLamViec.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgv_caLamViec.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgv_caLamViec.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv_caLamViec.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.dgv_caLamViec.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(242)))));
            this.dgv_caLamViec.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_caLamViec.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_caLamViec.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgv_caLamViec.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_caLamViec.ThemeStyle.HeaderStyle.Height = 66;
            this.dgv_caLamViec.ThemeStyle.ReadOnly = false;
            this.dgv_caLamViec.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(233)))), ((int)(((byte)(252)))));
            this.dgv_caLamViec.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_caLamViec.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_caLamViec.ThemeStyle.RowsStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv_caLamViec.ThemeStyle.RowsStyle.Height = 50;
            this.dgv_caLamViec.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(185)))), ((int)(((byte)(246)))));
            this.dgv_caLamViec.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaCa";
            this.Column1.HeaderText = "Mã ca";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "NgayThangNam";
            this.Column2.HeaderText = "Ngày";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Buoi";
            this.Column3.HeaderText = "Buổi";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MaNV";
            this.Column4.HeaderText = "Mã NV";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TenNhanVien";
            this.Column5.HeaderText = "Tên NV";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // UC_caLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgv_caLamViec);
            this.Controls.Add(this.gbChiTiet);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UC_caLamViec";
            this.Size = new System.Drawing.Size(1150, 600);
            this.gbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_caLamViec)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox txt_timKiem;
        private Guna.UI2.WinForms.Guna2Button btn_dieuChinh;
        private Guna.UI2.WinForms.Guna2GroupBox gbChiTiet;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_caLamViec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}
