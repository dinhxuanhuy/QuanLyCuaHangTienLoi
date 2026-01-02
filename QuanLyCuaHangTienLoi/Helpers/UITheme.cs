using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTienLoi.Helpers
{
    /// <summary>
    /// Modern UI Theme Configuration for Convenience Store Management System
    /// Design System: Professional Dashboard + Minimalism + Modern UI
    /// Color Palette: Trust Blue + Neutral Grey + Accent Orange
    /// </summary>
    public static class UITheme
    {
        #region Color Palette - Professional Modern Dashboard

        // Primary Colors - Trust Blue
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);        // #2563EB - Main brand blue
        public static readonly Color PrimaryDark = Color.FromArgb(29, 78, 216);    // #1D4ED8 - Hover state
        public static readonly Color PrimaryLight = Color.FromArgb(59, 130, 246);  // #3B82F6 - Secondary blue

        // Secondary Colors - Professional Grey
        public static readonly Color Secondary = Color.FromArgb(15, 23, 42);       // #0F172A - Dark text/sidebar
        public static readonly Color SecondaryLight = Color.FromArgb(51, 65, 85);  // #334155 - Muted text

        // CTA / Accent Colors
        public static readonly Color Accent = Color.FromArgb(249, 115, 22);        // #F97316 - CTA Orange
        public static readonly Color AccentHover = Color.FromArgb(234, 88, 12);    // #EA580C - CTA Hover

        // Success/Warning/Error - Status Colors
        public static readonly Color Success = Color.FromArgb(34, 197, 94);        // #22C55E - Green
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);       // #F59E0B - Amber
        public static readonly Color Error = Color.FromArgb(239, 68, 68);          // #EF4444 - Red
        public static readonly Color Info = Color.FromArgb(59, 130, 246);          // #3B82F6 - Blue

        // Background Colors
        public static readonly Color Background = Color.FromArgb(248, 250, 252);   // #F8FAFC - Main background
        public static readonly Color Surface = Color.FromArgb(255, 255, 255);      // #FFFFFF - Cards/Panels
        public static readonly Color SurfaceHover = Color.FromArgb(241, 245, 249); // #F1F5F9 - Hover state

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);     // #0F172A - Primary text
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);  // #475569 - Muted text
        public static readonly Color TextMuted = Color.FromArgb(148, 163, 184);    // #94A3B8 - Placeholder

        // Border Colors
        public static readonly Color Border = Color.FromArgb(226, 232, 240);       // #E2E8F0 - Light border
        public static readonly Color BorderDark = Color.FromArgb(203, 213, 225);   // #CBD5E1 - Darker border
        public static readonly Color BorderFocus = Color.FromArgb(59, 130, 246);   // #3B82F6 - Focus state

        // Sidebar Colors
        public static readonly Color SidebarBg = Color.FromArgb(15, 23, 42);       // #0F172A - Dark sidebar
        public static readonly Color SidebarText = Color.FromArgb(226, 232, 240);  // #E2E8F0 - Sidebar text
        public static readonly Color SidebarHover = Color.FromArgb(30, 41, 59);    // #1E293B - Hover bg
        public static readonly Color SidebarActive = Color.FromArgb(37, 99, 235);  // #2563EB - Active item

        #endregion

        #region Typography
        
        public static readonly Font FontHeading = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
        public static readonly Font FontSubheading = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        public static readonly Font FontBody = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBodyBold = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        public static readonly Font FontSmall = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font FontButton = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

        #endregion

        #region Component Styles

        /// <summary>
        /// Apply modern style to Guna2Button - Primary variant
        /// </summary>
        public static void ApplyPrimaryButtonStyle(Guna2Button btn)
        {
            btn.FillColor = Primary;
            btn.ForeColor = Color.White;
            btn.Font = FontButton;
            btn.BorderRadius = 8;
            btn.BorderThickness = 0;
            btn.Cursor = Cursors.Hand;
            
            // Hover state
            btn.HoverState.FillColor = PrimaryDark;
            btn.HoverState.ForeColor = Color.White;
            btn.HoverState.BorderColor = PrimaryDark;
            
            // Pressed state
            btn.PressedColor = Color.FromArgb(30, 64, 175); // #1E40AF
            
            // Disabled state
            btn.DisabledState.FillColor = Color.FromArgb(148, 163, 184);
            btn.DisabledState.ForeColor = Color.White;
            btn.DisabledState.BorderColor = Color.FromArgb(148, 163, 184);
        }

        /// <summary>
        /// Apply modern style to Guna2Button - Secondary/Ghost variant
        /// </summary>
        public static void ApplySecondaryButtonStyle(Guna2Button btn)
        {
            btn.FillColor = Color.Transparent;
            btn.ForeColor = Primary;
            btn.Font = FontButton;
            btn.BorderRadius = 8;
            btn.BorderThickness = 2;
            btn.BorderColor = Primary;
            btn.Cursor = Cursors.Hand;
            
            // Hover state
            btn.HoverState.FillColor = Color.FromArgb(239, 246, 255); // #EFF6FF
            btn.HoverState.ForeColor = PrimaryDark;
            btn.HoverState.BorderColor = PrimaryDark;
        }

        /// <summary>
        /// Apply modern style to Guna2Button - Success variant
        /// </summary>
        public static void ApplySuccessButtonStyle(Guna2Button btn)
        {
            btn.FillColor = Success;
            btn.ForeColor = Color.White;
            btn.Font = FontButton;
            btn.BorderRadius = 8;
            btn.BorderThickness = 0;
            btn.Cursor = Cursors.Hand;
            
            btn.HoverState.FillColor = Color.FromArgb(22, 163, 74); // Darker green
            btn.HoverState.ForeColor = Color.White;
        }

        /// <summary>
        /// Apply modern style to Guna2Button - Warning variant
        /// </summary>
        public static void ApplyWarningButtonStyle(Guna2Button btn)
        {
            btn.FillColor = Warning;
            btn.ForeColor = Color.White;
            btn.Font = FontButton;
            btn.BorderRadius = 8;
            btn.BorderThickness = 0;
            btn.Cursor = Cursors.Hand;
            
            btn.HoverState.FillColor = Color.FromArgb(217, 119, 6); // Darker amber
            btn.HoverState.ForeColor = Color.White;
        }

        /// <summary>
        /// Apply modern style to Guna2Button - Danger variant
        /// </summary>
        public static void ApplyDangerButtonStyle(Guna2Button btn)
        {
            btn.FillColor = Error;
            btn.ForeColor = Color.White;
            btn.Font = FontButton;
            btn.BorderRadius = 8;
            btn.BorderThickness = 0;
            btn.Cursor = Cursors.Hand;
            
            btn.HoverState.FillColor = Color.FromArgb(220, 38, 38); // Darker red
            btn.HoverState.ForeColor = Color.White;
        }

        /// <summary>
        /// Apply modern style to Guna2TextBox
        /// </summary>
        public static void ApplyTextBoxStyle(Guna2TextBox txt)
        {
            txt.FillColor = Surface;
            txt.ForeColor = TextPrimary;
            txt.Font = FontBody;
            txt.BorderRadius = 8;
            txt.BorderColor = Border;
            txt.BorderThickness = 1;
            txt.PlaceholderForeColor = TextMuted;
            
            // Focus state
            txt.FocusedState.BorderColor = BorderFocus;
            txt.FocusedState.FillColor = Surface;
            
            // Hover state
            txt.HoverState.BorderColor = BorderDark;
            
            // Disabled state
            txt.DisabledState.BorderColor = Border;
            txt.DisabledState.FillColor = Background;
            txt.DisabledState.ForeColor = TextMuted;
        }

        /// <summary>
        /// Apply modern style to Guna2ComboBox
        /// </summary>
        public static void ApplyComboBoxStyle(Guna2ComboBox cbo)
        {
            cbo.FillColor = Surface;
            cbo.ForeColor = TextPrimary;
            cbo.Font = FontBody;
            cbo.BorderRadius = 8;
            cbo.BorderColor = Border;
            cbo.BorderThickness = 1;
            
            // Focus state
            cbo.FocusedState.BorderColor = BorderFocus;
            cbo.FocusedColor = BorderFocus;
            
            // Hover state  
            cbo.HoverState.BorderColor = BorderDark;
            cbo.HoverState.FillColor = Surface;
            
            // Items appearance
            cbo.ItemsAppearance.BackColor = Surface;
            cbo.ItemsAppearance.ForeColor = TextPrimary;
            cbo.ItemsAppearance.SelectedBackColor = Color.FromArgb(239, 246, 255);
        }

        /// <summary>
        /// Apply modern style to Guna2Panel - Card variant
        /// </summary>
        public static void ApplyCardStyle(Guna2Panel panel)
        {
            panel.FillColor = Surface;
            panel.BorderRadius = 12;
            panel.BorderColor = Border;
            panel.BorderThickness = 1;
            
            // Shadow
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.FromArgb(30, 0, 0, 0);
            panel.ShadowDecoration.Depth = 10;
            panel.ShadowDecoration.Shadow = new Padding(0, 2, 0, 0);
        }

        /// <summary>
        /// Apply modern style to Guna2Panel - Sidebar variant
        /// </summary>
        public static void ApplySidebarStyle(Guna2Panel panel)
        {
            panel.FillColor = SidebarBg;
            panel.BorderRadius = 0;
            panel.BorderThickness = 0;
        }

        /// <summary>
        /// Apply modern style to Guna2GroupBox
        /// </summary>
        public static void ApplyGroupBoxStyle(Guna2GroupBox groupBox)
        {
            groupBox.FillColor = Surface;
            groupBox.ForeColor = TextPrimary;
            groupBox.Font = FontSubheading;
            groupBox.BorderRadius = 12;
            groupBox.BorderColor = Border;
            groupBox.BorderThickness = 1;
            groupBox.CustomBorderColor = Border;
        }

        /// <summary>
        /// Apply modern style to Guna2DataGridView
        /// </summary>
        public static void ApplyDataGridViewStyle(Guna2DataGridView dgv)
        {
            dgv.BackgroundColor = Surface;
            dgv.BorderStyle = BorderStyle.None;
            
            // Default cell style
            dgv.DefaultCellStyle.BackColor = Surface;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = FontBody;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Primary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            
            // Alternate row style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Background;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Primary;
            
            // Column header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBodyBold;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 8, 8, 8);
            
            // Grid lines
            dgv.GridColor = Border;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            
            // Row template
            dgv.RowTemplate.Height = 48;
            dgv.ColumnHeadersHeight = 52;
            
            // Theme colors
            dgv.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgv.ThemeStyle.HeaderStyle.ForeColor = TextPrimary;
            dgv.ThemeStyle.HeaderStyle.Font = FontBodyBold;
            
            dgv.ThemeStyle.RowsStyle.BackColor = Surface;
            dgv.ThemeStyle.RowsStyle.ForeColor = TextPrimary;
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgv.ThemeStyle.RowsStyle.SelectionForeColor = Primary;
            
            dgv.ThemeStyle.AlternatingRowsStyle.BackColor = Background;
            dgv.ThemeStyle.AlternatingRowsStyle.ForeColor = TextPrimary;
            dgv.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgv.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Primary;
        }

        /// <summary>
        /// Apply modern style to sidebar navigation button
        /// </summary>
        public static void ApplySidebarButtonStyle(Guna2Button btn, bool isActive = false)
        {
            btn.FillColor = isActive ? SidebarActive : Color.Transparent;
            btn.ForeColor = SidebarText;
            btn.Font = FontBody;
            btn.BorderRadius = 8;
            btn.BorderThickness = 0;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = HorizontalAlignment.Left;
            btn.ImageAlign = HorizontalAlignment.Left;
            btn.TextOffset = new Point(10, 0);
            btn.ImageOffset = new Point(10, 0);
            
            // Hover state
            btn.HoverState.FillColor = isActive ? SidebarActive : SidebarHover;
            btn.HoverState.ForeColor = Color.White;
        }

        /// <summary>
        /// Apply modern style to Label - Title variant
        /// </summary>
        public static void ApplyTitleLabelStyle(Label lbl)
        {
            lbl.ForeColor = TextPrimary;
            lbl.Font = FontHeading;
            lbl.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Apply modern style to Label - Subtitle variant
        /// </summary>
        public static void ApplySubtitleLabelStyle(Label lbl)
        {
            lbl.ForeColor = TextSecondary;
            lbl.Font = FontBody;
            lbl.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Apply modern style to Label - Body variant
        /// </summary>
        public static void ApplyBodyLabelStyle(Label lbl)
        {
            lbl.ForeColor = TextPrimary;
            lbl.Font = FontBody;
            lbl.BackColor = Color.Transparent;
        }

        #endregion

        #region Gradient Helpers

        /// <summary>
        /// Create a gradient brush for modern UI elements
        /// </summary>
        public static LinearGradientBrush CreateGradientBrush(Rectangle rect, Color startColor, Color endColor)
        {
            return new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
        }

        /// <summary>
        /// Get gradient colors for statistics cards
        /// </summary>
        public static (Color Start, Color End) GetStatCardGradient(string type)
        {
            return type.ToLower() switch
            {
                "revenue" or "doanhthu" => (Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246)),
                "expense" or "chiphi" => (Color.FromArgb(239, 68, 68), Color.FromArgb(248, 113, 113)),
                "profit" or "loinhuan" => (Color.FromArgb(34, 197, 94), Color.FromArgb(74, 222, 128)),
                "product" or "sanpham" => (Color.FromArgb(168, 85, 247), Color.FromArgb(192, 132, 252)),
                "order" or "hoadon" => (Color.FromArgb(249, 115, 22), Color.FromArgb(251, 146, 60)),
                _ => (Primary, PrimaryLight)
            };
        }

        #endregion

        #region Animation Helpers

        /// <summary>
        /// Apply smooth hover animation effect (use with MouseEnter/Leave events)
        /// </summary>
        public static void ApplyHoverEffect(Control control, bool isHovering)
        {
            if (control is Guna2Button btn)
            {
                // Animate opacity change
                btn.Animated = true;
                btn.AnimatedGIF = false;
            }
            else if (control is Guna2Panel panel)
            {
                panel.ShadowDecoration.Depth = isHovering ? 15 : 10;
            }
        }

        #endregion

        #region Form Setup

        /// <summary>
        /// Apply modern theme to entire form
        /// </summary>
        public static void ApplyFormTheme(Form form)
        {
            form.BackColor = Background;
            form.Font = FontBody;
            form.ForeColor = TextPrimary;
        }

        /// <summary>
        /// Apply modern theme to UserControl
        /// </summary>
        public static void ApplyUserControlTheme(UserControl uc)
        {
            uc.BackColor = Background;
            uc.Font = FontBody;
            uc.ForeColor = TextPrimary;
        }

        /// <summary>
        /// Apply theme to all child controls recursively
        /// </summary>
        public static void ApplyThemeToAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case Guna2Button btn:
                        ApplyPrimaryButtonStyle(btn);
                        break;
                    case Guna2TextBox txt:
                        ApplyTextBoxStyle(txt);
                        break;
                    case Guna2ComboBox cbo:
                        ApplyComboBoxStyle(cbo);
                        break;
                    case Guna2GroupBox gb:
                        ApplyGroupBoxStyle(gb);
                        break;
                    case Guna2DataGridView dgv:
                        ApplyDataGridViewStyle(dgv);
                        break;
                    case Guna2Panel panel:
                        if (panel.Dock == DockStyle.Left)
                            ApplySidebarStyle(panel);
                        else
                            ApplyCardStyle(panel);
                        break;
                    case Label lbl:
                        ApplyBodyLabelStyle(lbl);
                        break;
                }

                // Recursively apply to child controls
                if (control.HasChildren)
                {
                    ApplyThemeToAllControls(control);
                }
            }
        }

        #endregion
    }
}
