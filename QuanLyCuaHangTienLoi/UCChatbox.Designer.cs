namespace QuanLyCuaHangTienLoi
{
    partial class UCChatbox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            chatContainer = new System.Windows.Forms.FlowLayoutPanel();
            panelInput = new Guna.UI2.WinForms.Guna2Panel();
            btnSend = new Guna.UI2.WinForms.Guna2Button();
            txtMessage = new Guna.UI2.WinForms.Guna2TextBox();
            panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnClear = new Guna.UI2.WinForms.Guna2Button();
            lblTitle = new System.Windows.Forms.Label();
            panelMain.SuspendLayout();
            panelInput.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain - Modern Container with subtle shadow
            // 
            panelMain.BackColor = System.Drawing.Color.White;
            panelMain.BorderRadius = 16;
            panelMain.Controls.Add(chatContainer);
            panelMain.Controls.Add(panelInput);
            panelMain.Controls.Add(panelHeader);
            panelMain.CustomizableEdges = customizableEdges11;
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.FillColor = System.Drawing.Color.White;
            panelMain.Location = new System.Drawing.Point(0, 0);
            panelMain.Margin = new System.Windows.Forms.Padding(20);
            panelMain.Name = "panelMain";
            panelMain.Padding = new System.Windows.Forms.Padding(0);
            panelMain.ShadowDecoration.BorderRadius = 16;
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelMain.ShadowDecoration.Depth = 20;
            panelMain.ShadowDecoration.Enabled = true;
            panelMain.ShadowDecoration.Color = System.Drawing.Color.FromArgb(20, 0, 0, 0);
            panelMain.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(8);
            panelMain.Size = new System.Drawing.Size(1869, 1200);
            panelMain.TabIndex = 0;
            // 
            // chatContainer - Modern Chat Area
            // 
            chatContainer.AutoScroll = true;
            chatContainer.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            chatContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            chatContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            chatContainer.Location = new System.Drawing.Point(0, 100);
            chatContainer.Margin = new System.Windows.Forms.Padding(0);
            chatContainer.Name = "chatContainer";
            chatContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            chatContainer.Size = new System.Drawing.Size(1869, 920);
            chatContainer.TabIndex = 2;
            chatContainer.WrapContents = false;
            // 
            // panelInput - Modern Floating Input Bar
            // 
            panelInput.BackColor = System.Drawing.Color.White;
            panelInput.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            panelInput.BorderRadius = 16;
            panelInput.BorderThickness = 1;
            panelInput.Controls.Add(btnSend);
            panelInput.Controls.Add(txtMessage);
            panelInput.CustomizableEdges = customizableEdges5;
            panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelInput.FillColor = System.Drawing.Color.White;
            panelInput.Location = new System.Drawing.Point(0, 1020);
            panelInput.Margin = new System.Windows.Forms.Padding(24);
            panelInput.Name = "panelInput";
            panelInput.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            panelInput.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelInput.ShadowDecoration.Enabled = true;
            panelInput.ShadowDecoration.Color = System.Drawing.Color.FromArgb(15, 0, 0, 0);
            panelInput.ShadowDecoration.Depth = 12;
            panelInput.Size = new System.Drawing.Size(1869, 180);
            panelInput.TabIndex = 1;
            // 
            // btnSend - Modern Gradient Send Button
            // 
            btnSend.Animated = true;
            btnSend.BorderRadius = 12;
            btnSend.CustomizableEdges = customizableEdges1;
            btnSend.DisabledState.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnSend.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnSend.DisabledState.FillColor = System.Drawing.Color.FromArgb(230, 230, 230);
            btnSend.DisabledState.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            btnSend.FillColor = System.Drawing.Color.FromArgb(99, 102, 241);
            btnSend.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            btnSend.ForeColor = System.Drawing.Color.White;
            btnSend.HoverState.FillColor = System.Drawing.Color.FromArgb(79, 70, 229);
            btnSend.HoverState.ForeColor = System.Drawing.Color.White;
            btnSend.Location = new System.Drawing.Point(1649, 16);
            btnSend.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            btnSend.Name = "btnSend";
            btnSend.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            btnSend.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSend.ShadowDecoration.Enabled = true;
            btnSend.ShadowDecoration.Color = System.Drawing.Color.FromArgb(40, 99, 102, 241);
            btnSend.ShadowDecoration.Depth = 10;
            btnSend.Size = new System.Drawing.Size(200, 148);
            btnSend.TabIndex = 1;
            btnSend.Text = "Gửi  ➤";
            btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSend.Click += btnSend_Click;
            // 
            // txtMessage - Modern Input Field
            // 
            txtMessage.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtMessage.BorderRadius = 12;
            txtMessage.BorderThickness = 2;
            txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtMessage.CustomizableEdges = customizableEdges3;
            txtMessage.DefaultText = "";
            txtMessage.DisabledState.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            txtMessage.DisabledState.FillColor = System.Drawing.Color.FromArgb(243, 244, 246);
            txtMessage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            txtMessage.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            txtMessage.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtMessage.FocusedState.BorderColor = System.Drawing.Color.FromArgb(99, 102, 241);
            txtMessage.FocusedState.FillColor = System.Drawing.Color.White;
            txtMessage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            txtMessage.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtMessage.HoverState.BorderColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtMessage.Location = new System.Drawing.Point(20, 16);
            txtMessage.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            txtMessage.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtMessage.PlaceholderText = "💬 Nhập tin nhắn của bạn tại đây...";
            txtMessage.SelectedText = "";
            txtMessage.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtMessage.Size = new System.Drawing.Size(1617, 148);
            txtMessage.TabIndex = 0;
            txtMessage.TextChanged += txtMessage_TextChanged;
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // panelHeader - Modern Gradient Header
            // 
            panelHeader.BackColor = System.Drawing.Color.FromArgb(99, 102, 241);
            panelHeader.BorderRadius = 0;
            panelHeader.Controls.Add(btnClear);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.CustomizableEdges = customizableEdges9;
            panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            panelHeader.FillColor = System.Drawing.Color.FromArgb(99, 102, 241);
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Margin = new System.Windows.Forms.Padding(0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            panelHeader.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelHeader.ShadowDecoration.Enabled = true;
            panelHeader.ShadowDecoration.Color = System.Drawing.Color.FromArgb(30, 99, 102, 241);
            panelHeader.ShadowDecoration.Depth = 8;
            panelHeader.Size = new System.Drawing.Size(1869, 100);
            panelHeader.TabIndex = 0;
            // 
            // btnClear - Modern Pill Ghost Button
            // 
            btnClear.Animated = true;
            btnClear.BorderColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            btnClear.BorderRadius = 20;
            btnClear.BorderThickness = 2;
            btnClear.CustomizableEdges = customizableEdges7;
            btnClear.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            btnClear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            btnClear.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            btnClear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            btnClear.FillColor = System.Drawing.Color.FromArgb(30, 255, 255, 255);
            btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnClear.ForeColor = System.Drawing.Color.White;
            btnClear.HoverState.BorderColor = System.Drawing.Color.White;
            btnClear.HoverState.FillColor = System.Drawing.Color.FromArgb(80, 255, 255, 255);
            btnClear.HoverState.ForeColor = System.Drawing.Color.White;
            btnClear.Location = new System.Drawing.Point(1645, 0);
            btnClear.Margin = new System.Windows.Forms.Padding(0);
            btnClear.Name = "btnClear";
            btnClear.PressedColor = System.Drawing.Color.FromArgb(60, 255, 255, 255);
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnClear.Size = new System.Drawing.Size(200, 100);
            btnClear.TabIndex = 1;
            btnClear.Text = "✕  Xóa";
            btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClear.Click += btnClear_Click;
            // 
            // lblTitle - Modern AI Assistant Title
            // 
            lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(24, 0);
            lblTitle.Margin = new System.Windows.Forms.Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(500, 100);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🤖  Trợ lý AI Cửa Hàng";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCChatbox - Modern AI Chat Interface
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            Controls.Add(panelMain);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "UCChatbox";
            Size = new System.Drawing.Size(1869, 1200);
            Load += UCChatbox_Load;
            panelMain.ResumeLayout(false);
            panelInput.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel panelInput;
        private Guna.UI2.WinForms.Guna2TextBox txtMessage;
        private Guna.UI2.WinForms.Guna2Button btnSend;
        private System.Windows.Forms.FlowLayoutPanel chatContainer;
        private Guna.UI2.WinForms.Guna2Button btnClear;
    }
}
