using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTienLoi.Helpers
{
    /// <summary>
    /// Modern Card Components for Dashboard UI
    /// </summary>
    public static class ModernCards
    {
        /// <summary>
        /// Create a modern dashboard menu card
        /// </summary>
        public static Guna2Panel CreateMenuCard(string title, string description, Color accentColor, EventHandler onClick)
        {
            var card = new Guna2Panel
            {
                Size = new Size(280, 180),
                BorderRadius = 16,
                FillColor = Color.White,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderThickness = 1,
                Cursor = Cursors.Hand,
                Padding = new Padding(24)
            };

            // Shadow effect
            card.ShadowDecoration.Enabled = true;
            card.ShadowDecoration.Color = Color.FromArgb(20, 0, 0, 0);
            card.ShadowDecoration.Depth = 15;
            card.ShadowDecoration.Shadow = new Padding(0, 4, 0, 0);

            // Accent bar at top
            var accentBar = new Panel
            {
                Size = new Size(60, 4),
                BackColor = accentColor,
                Location = new Point(24, 20)
            };
            card.Controls.Add(accentBar);

            // Title label
            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Location = new Point(24, 35)
            };
            card.Controls.Add(titleLabel);

            // Description label
            var descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = false,
                Size = new Size(232, 50),
                Location = new Point(24, 70)
            };
            card.Controls.Add(descLabel);

            // Arrow icon
            var arrowLabel = new Label
            {
                Text = "→",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true,
                Location = new Point(235, 140)
            };
            card.Controls.Add(arrowLabel);

            // Hover effects
            card.MouseEnter += (s, e) =>
            {
                card.FillColor = Color.FromArgb(248, 250, 252);
                card.ShadowDecoration.Depth = 25;
                arrowLabel.Location = new Point(240, 140);
            };

            card.MouseLeave += (s, e) =>
            {
                card.FillColor = Color.White;
                card.ShadowDecoration.Depth = 15;
                arrowLabel.Location = new Point(235, 140);
            };

            // Click event
            if (onClick != null)
            {
                card.Click += onClick;
                foreach (Control control in card.Controls)
                {
                    control.Click += onClick;
                    control.Cursor = Cursors.Hand;
                }
            }

            return card;
        }

        /// <summary>
        /// Create a statistic card with icon and value
        /// </summary>
        public static Guna2Panel CreateStatCard(string title, string value, string subtext, Color gradientStart, Color gradientEnd)
        {
            var card = new Guna2Panel
            {
                Size = new Size(260, 140),
                BorderRadius = 16,
                FillColor = gradientStart,
                Cursor = Cursors.Hand,
                Padding = new Padding(20)
            };

            // Shadow
            card.ShadowDecoration.Enabled = true;
            card.ShadowDecoration.Color = Color.FromArgb(40, gradientStart);
            card.ShadowDecoration.Depth = 20;

            // Value label (big number)
            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI Semibold", 28F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            card.Controls.Add(valueLabel);

            // Title label
            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 255, 255, 255),
                AutoSize = true,
                Location = new Point(20, 75)
            };
            card.Controls.Add(titleLabel);

            // Subtext label
            var subtextLabel = new Label
            {
                Text = subtext,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(180, 255, 255, 255),
                AutoSize = true,
                Location = new Point(20, 100)
            };
            card.Controls.Add(subtextLabel);

            return card;
        }

        /// <summary>
        /// Apply modern button style for dashboard cards
        /// </summary>
        public static void ApplyModernCardButtonStyle(Guna2Button btn, Color accentColor, string iconText = "")
        {
            btn.FillColor = Color.White;
            btn.ForeColor = Color.FromArgb(15, 23, 42);
            btn.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn.BorderRadius = 16;
            btn.BorderThickness = 1;
            btn.BorderColor = Color.FromArgb(226, 232, 240);
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(220, 140);
            btn.TextAlign = HorizontalAlignment.Center;

            // Hover state
            btn.HoverState.FillColor = accentColor;
            btn.HoverState.ForeColor = Color.White;
            btn.HoverState.BorderColor = accentColor;

            // Pressed state
            btn.PressedColor = Color.FromArgb(
                Math.Max(0, accentColor.R - 20),
                Math.Max(0, accentColor.G - 20),
                Math.Max(0, accentColor.B - 20)
            );

            // Shadow
            btn.ShadowDecoration.Enabled = true;
            btn.ShadowDecoration.Color = Color.FromArgb(20, 0, 0, 0);
            btn.ShadowDecoration.Depth = 10;
            btn.ShadowDecoration.Shadow = new Padding(0, 4, 0, 0);
        }

        /// <summary>
        /// Create a modern section header
        /// </summary>
        public static Panel CreateSectionHeader(string title, string subtitle = "")
        {
            var panel = new Panel
            {
                Size = new Size(800, 80),
                BackColor = Color.Transparent
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            panel.Controls.Add(titleLabel);

            if (!string.IsNullOrEmpty(subtitle))
            {
                var subtitleLabel = new Label
                {
                    Text = subtitle,
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    AutoSize = true,
                    Location = new Point(0, 45)
                };
                panel.Controls.Add(subtitleLabel);
            }

            return panel;
        }

        /// <summary>
        /// Apply modern style to Guna2GroupBox as a card header
        /// </summary>
        public static void ApplyModernHeaderStyle(Guna2GroupBox groupBox)
        {
            groupBox.FillColor = Color.White;
            groupBox.ForeColor = Color.FromArgb(15, 23, 42);
            groupBox.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            groupBox.BorderRadius = 0;
            groupBox.BorderColor = Color.FromArgb(226, 232, 240);
            groupBox.BorderThickness = 0;
            groupBox.CustomBorderColor = Color.FromArgb(226, 232, 240);
            groupBox.CustomBorderThickness = new Padding(0, 0, 0, 1);
        }

        /// <summary>
        /// Predefined accent colors for different card types
        /// </summary>
        public static class AccentColors
        {
            public static readonly Color Blue = Color.FromArgb(37, 99, 235);
            public static readonly Color Green = Color.FromArgb(34, 197, 94);
            public static readonly Color Orange = Color.FromArgb(249, 115, 22);
            public static readonly Color Purple = Color.FromArgb(139, 92, 246);
            public static readonly Color Red = Color.FromArgb(239, 68, 68);
            public static readonly Color Teal = Color.FromArgb(20, 184, 166);
            public static readonly Color Pink = Color.FromArgb(236, 72, 153);
            public static readonly Color Amber = Color.FromArgb(245, 158, 11);
        }
    }
}
