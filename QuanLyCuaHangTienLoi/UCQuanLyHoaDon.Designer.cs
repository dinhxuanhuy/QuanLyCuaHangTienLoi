namespace QuanLyCuaHangTienLoi
{
    partial class UCQuanLyHoaDon
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
            this.btn_hoaDonNhap = new Guna.UI2.WinForms.Guna2Button();
            this.btn_hoaDonBan = new Guna.UI2.WinForms.Guna2Button();
            this.gbChiTiet = new Guna.UI2.WinForms.Guna2GroupBox();
            this.SuspendLayout();
            // 
            // btn_hoaDonNhap
            // 
            this.btn_hoaDonNhap.BorderRadius = 8;
            this.btn_hoaDonNhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_hoaDonNhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_hoaDonNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_hoaDonNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_hoaDonNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(83)))), ((int)(((byte)(251)))));
            this.btn_hoaDonNhap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_hoaDonNhap.ForeColor = System.Drawing.Color.White;
            this.btn_hoaDonNhap.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_hoaDonNhap.Location = new System.Drawing.Point(630, 250);
            this.btn_hoaDonNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_hoaDonNhap.Name = "btn_hoaDonNhap";
            this.btn_hoaDonNhap.Size = new System.Drawing.Size(220, 160);
            this.btn_hoaDonNhap.TabIndex = 37;
            this.btn_hoaDonNhap.Text = "Lịch sử nhập hàng";
            this.btn_hoaDonNhap.Click += new System.EventHandler(this.btn_hoaDonNhap_Click);
            // 
            // btn_hoaDonBan
            // 
            this.btn_hoaDonBan.BorderRadius = 8;
            this.btn_hoaDonBan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_hoaDonBan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_hoaDonBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_hoaDonBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_hoaDonBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(83)))), ((int)(((byte)(251)))));
            this.btn_hoaDonBan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_hoaDonBan.ForeColor = System.Drawing.Color.White;
            this.btn_hoaDonBan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_hoaDonBan.Location = new System.Drawing.Point(300, 250);
            this.btn_hoaDonBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_hoaDonBan.Name = "btn_hoaDonBan";
            this.btn_hoaDonBan.Size = new System.Drawing.Size(220, 160);
            this.btn_hoaDonBan.TabIndex = 36;
            this.btn_hoaDonBan.Text = "Hóa đơn bán";
            this.btn_hoaDonBan.Click += new System.EventHandler(this.btn_hoaDonBan_Click);
            // 
            // gbChiTiet
            // 
            this.gbChiTiet.BackColor = System.Drawing.Color.White;
            this.gbChiTiet.BorderRadius = 12;
            this.gbChiTiet.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.gbChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbChiTiet.FillColor = System.Drawing.Color.White;
            this.gbChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbChiTiet.ForeColor = System.Drawing.Color.Black;
            this.gbChiTiet.Location = new System.Drawing.Point(0, 0);
            this.gbChiTiet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbChiTiet.Name = "gbChiTiet";
            this.gbChiTiet.Size = new System.Drawing.Size(1150, 102);
            this.gbChiTiet.TabIndex = 35;
            this.gbChiTiet.Text = "Quản Lý Hóa Đơn";
            // 
            // UCQuanLyHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btn_hoaDonNhap);
            this.Controls.Add(this.btn_hoaDonBan);
            this.Controls.Add(this.gbChiTiet);
            this.Name = "UCQuanLyHoaDon";
            this.Size = new System.Drawing.Size(1150, 600);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btn_hoaDonNhap;
        private Guna.UI2.WinForms.Guna2Button btn_hoaDonBan;
        private Guna.UI2.WinForms.Guna2GroupBox gbChiTiet;
    }
}
