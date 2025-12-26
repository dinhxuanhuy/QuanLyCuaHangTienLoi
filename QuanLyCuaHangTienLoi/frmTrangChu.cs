using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessAccessLayer;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTienLoi
{
    public partial class frmTrangChu : Form
    {
        public static string MaNV = "";
        
        // Color constants for modern UI
        private readonly Color SidebarColor = Color.FromArgb(44, 62, 80);
        private readonly Color ActiveButtonColor = Color.FromArgb(52, 152, 219);
        private readonly Color NormalButtonColor = Color.FromArgb(44, 62, 80);
        private readonly Color TextColorNormal = Color.FromArgb(236, 240, 241);
        private readonly Color TextColorActive = Color.White;
        
        // Current active button reference
        private Guna2Button currentActiveButton;
        
        public frmTrangChu()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Helper method to show a UserControl in the content panel
        /// and update the active button state
        /// </summary>
        /// <param name="uc">UserControl to display</param>
        /// <param name="activeButton">The button that was clicked (can be null)</param>
        /// <param name="pageTitle">Title to display in the header</param>
        private void ShowUserControl(UserControl uc, Guna2Button activeButton, string pageTitle)
        {
            if (panelContent.Controls.Count > 0 && panelContent.Controls[0] == uc)
                return; // Already showing this UserControl
            
            // Clear previous content
            panelContent.Controls.Clear();
            
            // Add and configure the new UserControl
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);
            uc.BringToFront();
            
            // Update page title
            if (!string.IsNullOrEmpty(pageTitle))
            {
                lblPageTitle.Text = pageTitle;
            }
            
            // Update active button state
            if (activeButton != null)
            {
                SetActiveButton(activeButton);
            }
        }
        
        /// <summary>
        /// Sets the visual active state for the specified button
        /// and resets the previous active button
        /// </summary>
        private void SetActiveButton(Guna2Button button)
        {
            // Reset previous active button
            if (currentActiveButton != null && currentActiveButton != button)
            {
                currentActiveButton.FillColor = NormalButtonColor;
                currentActiveButton.ForeColor = TextColorNormal;
            }
            
            // Set new active button
            button.FillColor = ActiveButtonColor;
            button.ForeColor = TextColorActive;
            currentActiveButton = button;
        }
        
        /// <summary>
        /// Original method kept for backward compatibility
        /// </summary>
        public void LoadUserControl(UserControl uc, bool xoa)
        {
            if (panelContent.Controls.Count > 0 && panelContent.Controls[0] == uc)
                return; // Already showing this UserControl
                
            if (xoa && panelContent.Controls.Count > 0)
            {
                var oldUC = panelContent.Controls[0];
                oldUC.Dispose();
            }

            panelContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            // Set encoding for form
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.SizeGripStyle = SizeGripStyle.Hide;

            Program.ucCaLamViec = new UC_caLamViec();
            Program.ucCaLamViecDieuChinh = new UCCaLamViecDieuChinh();
            Program.ucKhuyenMai = new UCKhuyenMai();
            Program.ucNhaCungCap = new UCNhaCungCap();
            Program.ucNhanVien = new UCNhanVien();
            Program.ucSanPham = new UCSanPham();
            Program.ucThongKe = new UCThongKe();
            Program.ucThongKeDoanhThu = new UCThongKeDoanhThu();
            Program.ucThongKeChiPhi = new UCThongKeChiPhi();
            Program.ucThongKeLoiNhuan = new UCThongKeLoiNhuan();
            Program.ucQuanLyDuLieu = new UCQuanLyDuLieu();
            Program.ucHoaDonBan = new UCHoaDonBan();
            Program.ucHoaDonNhap = new UCHoaDonNhap();
            Program.ucQuanLyHoaDon = new UCQuanLyHoaDon();
            Program.ucQuanLyTaiKhoan = new UCQuanLyTaiKhoan();
            Program.ucThemHDBanHang = new UCThemHDBanHang();
            Program.ucChatbox = new UCChatbox();

            // Event subscriptions for module navigation
            Program.ucThongKe.NavigateRequest += Uc_NavigateRequest;
            Program.ucThongKeChiPhi.NavigateRequest += Uc_NavigateRequest;
            Program.ucThongKeDoanhThu.NavigateRequest += Uc_NavigateRequest;
            Program.ucThongKeLoiNhuan.NavigateRequest += Uc_NavigateRequest;
            
            Program.ucQuanLyDuLieu.NavigateRequest += Uc_NavigateRequest;
            Program.ucNhanVien.NavigateRequest += Uc_NavigateRequest;
            Program.ucKhuyenMai.NavigateRequest += Uc_NavigateRequest;
            Program.ucNhaCungCap.NavigateRequest += Uc_NavigateRequest;
            Program.ucSanPham.NavigateRequest += Uc_NavigateRequest;
            
            Program.ucQuanLyHoaDon.NavigateRequest += Uc_NavigateRequest;
            Program.ucHoaDonBan.NavigateRequest += Uc_NavigateRequest;
            Program.ucThemHDBanHang.NavigateRequest += Uc_NavigateRequest;
            Program.ucHoaDonNhap.NavigateRequest += Uc_NavigateRequest;
            
            Program.ucCaLamViec.NavigateRequest += Uc_NavigateRequest;
            Program.ucCaLamViecDieuChinh.NavigateRequest += Uc_NavigateRequest;
            
            Program.ucQuanLyTaiKhoan.NavigateRequest += Uc_NavigateRequest;

            // Initial button states
            btn_quanLyCa.Enabled = false;
            btn_quanLyDuLieu.Enabled = false;
            btn_quanLyHoaDon.Enabled = false;
            btn_quanLyTaiKhoan.Enabled = false;
            btn_thongKe.Enabled = false;

            // Load default view
            ShowUserControl(Program.ucQuanLyTaiKhoan, btn_quanLyTaiKhoan, "Đăng Nhập");
        }

        private void Uc_NavigateRequest(UserControl uc)
        {
            LoadUserControl(uc, false);
        }
        
        public void CapNhatTenNhanVien(string a, string b)
        {
            if (labTenNV != null)
            {
                labTenNV.Text = a;
                labMaNV.Text = "Mã: " + b;
            }
        }

        private void guna2ControlBox4_Click(object sender, EventArgs e)
        {
            // Minimize button click handler
        }

        private void btn_chatBox_Click_1(object sender, EventArgs e)
        {
            ShowUserControl(Program.ucChatbox, btn_chatBox, "Hỗ Trợ");
        }

        private void btn_quanLyTaiKhoan_Click_1(object sender, EventArgs e)
        {
            ShowUserControl(Program.ucQuanLyTaiKhoan, btn_quanLyTaiKhoan, "Quản Lý Tài Khoản");
        }

        private void btn_thongKe_Click_1(object sender, EventArgs e)
        {
            ShowUserControl(Program.ucThongKe, btn_thongKe, "Thống Kê");
        }

        private void btn_quanLyDuLieu_Click_1(object sender, EventArgs e)
        {
            ShowUserControl(Program.ucQuanLyDuLieu, btn_quanLyDuLieu, "Quản Lý Dữ Liệu");
        }

        private void btn_quanLyCa_Click_1(object sender, EventArgs e)
        {
            ShowUserControl(Program.ucCaLamViec, btn_quanLyCa, "Quản Lý Ca");
        }

        private void btn_quanLyHoaDon_Click_1(object sender, EventArgs e)
        {
            ShowUserControl(Program.ucQuanLyHoaDon, btn_quanLyHoaDon, "Quản Lý Hóa Đơn");
        }
    }
}
