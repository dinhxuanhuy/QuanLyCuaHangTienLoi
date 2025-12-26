using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTienLoi
{
    /// <summary>
    /// Modern Flat UI Style Guide - Centralized styling constants and helpers
    /// Primary color scheme derived from frmTrangChu
    /// </summary>
    public static class UIStyles
    {
        #region Color Constants

        // Primary Colors (from frmTrangChu)
        public static readonly Color PrimaryColor = Color.FromArgb(52, 152, 219);      // #3498DB - Active/Primary Blue
        public static readonly Color SecondaryColor = Color.FromArgb(44, 62, 80);      // #2C3E50 - Dark Blue/Gray

        // Background Colors
        public static readonly Color BackgroundLight = Color.FromArgb(245, 247, 250);  // #F5F7FA - Light gray background
        public static readonly Color BackgroundWhite = Color.White;                     // #FFFFFF
        public static readonly Color ContentBackground = Color.FromArgb(236, 240, 241); // #ECF0F1 - Content panel background

        // Button Colors
        public static readonly Color ButtonDanger = Color.FromArgb(220, 53, 69);       // #DC3545 - Delete/Danger Red
        public static readonly Color ButtonSecondary = Color.FromArgb(108, 117, 125);  // #6C757D - Secondary/Cancel Gray
        public static readonly Color ButtonSuccess = Color.FromArgb(40, 167, 69);      // #28A745 - Success/Save Green
        public static readonly Color ButtonWarning = Color.FromArgb(255, 128, 0);      // #FF8000 - Warning/Edit Orange

        // Text Colors
        public static readonly Color TextDark = Color.FromArgb(17, 24, 39);            // #111827 - Dark text
        public static readonly Color TextLight = Color.White;                           // #FFFFFF - Light text (on dark bg)
        public static readonly Color TextMuted = Color.FromArgb(127, 140, 141);        // #7F8C8D - Muted/placeholder

        // DataGridView Colors
        public static readonly Color GridHeaderBackground = Color.FromArgb(52, 152, 219);  // #3498DB - Primary Blue
        public static readonly Color GridRowBackground = Color.White;
        public static readonly Color GridAlternateRowBackground = Color.FromArgb(248, 249, 250); // #F8F9FA - Very light gray
        public static readonly Color GridSelectionBackground = Color.FromArgb(107, 185, 246);    // #6BB9F6 - Light blue selection
        public static readonly Color GridLineColor = Color.FromArgb(229, 231, 235);              // #E5E7EB - Light gray grid lines

        // GroupBox/Panel Colors
        public static readonly Color GroupBoxBackground = Color.White;
        public static readonly Color GroupBoxBorder = Color.FromArgb(52, 152, 219);    // #3498DB - Primary Blue border

        #endregion

        #region Font Constants

        public static readonly Font DefaultFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font DefaultFontBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font HeaderFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font LabelFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        public static readonly Font InputFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        #endregion

        #region Size Constants

        public const int GridHeaderHeight = 40;
        public const int GridRowHeight = 35;
        public const int ButtonBorderRadius = 8;
        public const int InputBorderRadius = 6;
        public const int GroupBoxBorderRadius = 12;

        #endregion

        #region Apply Methods

        /// <summary>
        /// Applies modern flat UI style to a UserControl and all its child controls
        /// Call this method in the UserControl constructor after InitializeComponent()
        /// </summary>
        public static void ApplyUIStyle(UserControl userControl)
        {
            // Set UserControl properties - NO border, white/light background
            userControl.BackColor = BackgroundLight;
            userControl.Padding = new Padding(15);

            // Apply styles to all child controls recursively
            ApplyStylesToControls(userControl.Controls);
        }

        /// <summary>
        /// Recursively applies styles to all controls in a ControlCollection
        /// </summary>
        private static void ApplyStylesToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case Guna2DataGridView dgv:
                        ApplyDataGridViewStyle(dgv);
                        break;

                    case Guna2GroupBox groupBox:
                        ApplyGroupBoxStyle(groupBox);
                        // Recurse into GroupBox children
                        ApplyStylesToControls(groupBox.Controls);
                        break;

                    case Guna2Panel panel:
                        ApplyPanelStyle(panel);
                        // Recurse into Panel children
                        ApplyStylesToControls(panel.Controls);
                        break;

                    case Guna2Button button:
                        ApplyButtonStyle(button);
                        break;

                    case Guna2TextBox textBox:
                        ApplyTextBoxStyle(textBox);
                        break;

                    case Guna2ComboBox comboBox:
                        ApplyComboBoxStyle(comboBox);
                        break;

                    case Label label:
                        ApplyLabelStyle(label);
                        break;

                    default:
                        // Recurse into any container control
                        if (control.HasChildren)
                        {
                            ApplyStylesToControls(control.Controls);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Applies Modern Flat UI style to DataGridView
        /// </summary>
        public static void ApplyDataGridViewStyle(Guna2DataGridView dgv)
        {
            // General settings
            dgv.BackgroundColor = BackgroundWhite;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = GridLineColor;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;

            // Header style
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = GridHeaderHeight;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = GridHeaderBackground,
                ForeColor = TextLight,
                Font = HeaderFont,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = GridHeaderBackground,
                SelectionForeColor = TextLight,
                WrapMode = DataGridViewTriState.False
            };

            // Row style
            dgv.RowTemplate.Height = GridRowHeight;

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = GridRowBackground,
                ForeColor = TextDark,
                Font = DefaultFont,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                SelectionBackColor = GridSelectionBackground,
                SelectionForeColor = TextDark,
                WrapMode = DataGridViewTriState.False
            };

            // Alternate row style
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = GridAlternateRowBackground,
                ForeColor = TextDark,
                Font = DefaultFont,
                SelectionBackColor = GridSelectionBackground,
                SelectionForeColor = TextDark
            };

            // Cell border style - horizontal lines only
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Theme style (Guna2 specific)
            dgv.ThemeStyle.BackColor = BackgroundWhite;
            dgv.ThemeStyle.GridColor = GridLineColor;
            dgv.ThemeStyle.HeaderStyle.BackColor = GridHeaderBackground;
            dgv.ThemeStyle.HeaderStyle.ForeColor = TextLight;
            dgv.ThemeStyle.HeaderStyle.Font = HeaderFont;
            dgv.ThemeStyle.HeaderStyle.Height = GridHeaderHeight;
            dgv.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ThemeStyle.RowsStyle.BackColor = GridRowBackground;
            dgv.ThemeStyle.RowsStyle.ForeColor = TextDark;
            dgv.ThemeStyle.RowsStyle.Font = DefaultFont;
            dgv.ThemeStyle.RowsStyle.Height = GridRowHeight;
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = GridSelectionBackground;
            dgv.ThemeStyle.RowsStyle.SelectionForeColor = TextDark;
            dgv.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ThemeStyle.AlternatingRowsStyle.BackColor = GridAlternateRowBackground;
        }

        /// <summary>
        /// Applies Modern Flat UI style to GroupBox
        /// </summary>
        public static void ApplyGroupBoxStyle(Guna2GroupBox groupBox)
        {
            groupBox.FillColor = GroupBoxBackground;
            groupBox.BackColor = GroupBoxBackground;
            groupBox.CustomBorderColor = GroupBoxBorder;
            groupBox.BorderRadius = GroupBoxBorderRadius;
            groupBox.Font = DefaultFontBold;
            groupBox.ForeColor = TextDark;
        }

        /// <summary>
        /// Applies Modern Flat UI style to Panel
        /// </summary>
        public static void ApplyPanelStyle(Guna2Panel panel)
        {
            panel.FillColor = BackgroundWhite;
            panel.BackColor = BackgroundWhite;
        }

        /// <summary>
        /// Applies Modern Flat UI style to Button based on its text content or name
        /// </summary>
        public static void ApplyButtonStyle(Guna2Button button)
        {
            button.BorderRadius = ButtonBorderRadius;
            button.Font = DefaultFontBold;
            button.ForeColor = TextLight;

            string buttonText = button.Text.ToLowerInvariant();
            string buttonName = button.Name.ToLowerInvariant();

            // Determine button type based on text or name
            if (buttonText.Contains("xóa") || buttonText.Contains("xoa") || buttonText.Contains("delete") ||
                buttonName.Contains("xoa") || buttonName.Contains("delete"))
            {
                // Danger button (Delete)
                button.FillColor = ButtonDanger;
            }
            else if (buttonText.Contains("hủy") || buttonText.Contains("huy") || buttonText.Contains("cancel") ||
                     buttonName.Contains("huy") || buttonName.Contains("cancel"))
            {
                // Secondary button (Cancel)
                button.FillColor = ButtonSecondary;
            }
            else if (buttonText.Contains("lưu") || buttonText.Contains("luu") || buttonText.Contains("save") ||
                     buttonText.Contains("quay") || buttonText.Contains("back") ||
                     buttonName.Contains("luu") || buttonName.Contains("save") ||
                     buttonName.Contains("quaylai") || buttonName.Contains("back"))
            {
                // Success button (Save/Back)
                button.FillColor = ButtonSuccess;
            }
            else if (buttonText.Contains("sửa") || buttonText.Contains("sua") || buttonText.Contains("edit") ||
                     buttonText.Contains("nhập") || buttonText.Contains("nhap") ||
                     buttonName.Contains("sua") || buttonName.Contains("edit") ||
                     buttonName.Contains("nhap"))
            {
                // Warning button (Edit/Import)
                button.FillColor = ButtonWarning;
            }
            else
            {
                // Primary button (Add/Default)
                button.FillColor = PrimaryColor;
            }

            // Hover states
            button.HoverState.FillColor = DarkenColor(button.FillColor, 0.1f);
            button.HoverState.ForeColor = TextLight;
        }

        /// <summary>
        /// Applies Modern Flat UI style to TextBox
        /// </summary>
        public static void ApplyTextBoxStyle(Guna2TextBox textBox)
        {
            textBox.BorderRadius = InputBorderRadius;
            textBox.BorderColor = Color.FromArgb(203, 213, 225);
            textBox.FocusedState.BorderColor = PrimaryColor;
            textBox.HoverState.BorderColor = PrimaryColor;
            textBox.FillColor = BackgroundWhite;
            textBox.Font = InputFont;
            textBox.ForeColor = TextDark;
            textBox.PlaceholderForeColor = TextMuted;
        }

        /// <summary>
        /// Applies Modern Flat UI style to ComboBox
        /// </summary>
        public static void ApplyComboBoxStyle(Guna2ComboBox comboBox)
        {
            comboBox.BorderRadius = InputBorderRadius;
            comboBox.BorderColor = Color.FromArgb(203, 213, 225);
            comboBox.FocusedColor = PrimaryColor;
            comboBox.FocusedState.BorderColor = PrimaryColor;
            comboBox.HoverState.BorderColor = PrimaryColor;
            comboBox.HoverState.FillColor = BackgroundWhite;
            comboBox.FillColor = BackgroundWhite;
            comboBox.Font = InputFont;
            comboBox.ForeColor = TextDark;
            comboBox.ItemsAppearance.BackColor = BackgroundWhite;
            comboBox.ItemsAppearance.ForeColor = TextDark;
        }

        /// <summary>
        /// Applies Modern Flat UI style to Label
        /// </summary>
        public static void ApplyLabelStyle(Label label)
        {
            label.Font = LabelFont;
            label.ForeColor = TextDark;
            label.BackColor = Color.Transparent;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Darkens a color by a given factor (0.0 - 1.0)
        /// </summary>
        private static Color DarkenColor(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));

            return Color.FromArgb(color.A, 
                r < 0 ? 0 : r, 
                g < 0 ? 0 : g, 
                b < 0 ? 0 : b);
        }

        #endregion
    }
}
