namespace quanlychungcu
{
    partial class Login
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chkGhinhopass = new Guna.UI.WinForms.GunaCheckBox();
            this.btnLogin = new Guna.UI.WinForms.GunaGradientButton();
            this.txtTK = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtMK = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.btnExit1 = new Guna.UI.WinForms.GunaButton();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.cbMangLamViec = new Guna.UI.WinForms.GunaComboBox();
            this.gunaLabel4 = new Guna.UI.WinForms.GunaLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(522, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "HỆ THỐNG QUẢN LÝ CHUNG CƯ ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(855, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "SMALL TOWER";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(861, 636);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 40);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(110, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 34);
            this.label5.TabIndex = 9;
            this.label5.Text = "ĐĂNG NHẬP";
            // 
            // chkGhinhopass
            // 
            this.chkGhinhopass.BackColor = System.Drawing.Color.Transparent;
            this.chkGhinhopass.BaseColor = System.Drawing.Color.White;
            this.chkGhinhopass.CheckedOffColor = System.Drawing.Color.Gray;
            this.chkGhinhopass.CheckedOnColor = System.Drawing.Color.Red;
            this.chkGhinhopass.FillColor = System.Drawing.Color.White;
            this.chkGhinhopass.Location = new System.Drawing.Point(116, 295);
            this.chkGhinhopass.Name = "chkGhinhopass";
            this.chkGhinhopass.Size = new System.Drawing.Size(119, 20);
            this.chkGhinhopass.TabIndex = 3;
            this.chkGhinhopass.Text = "Ghi nhớ mật khẩu";
            // 
            // btnLogin
            // 
            this.btnLogin.AnimationHoverSpeed = 0.07F;
            this.btnLogin.AnimationSpeed = 0.03F;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BaseColor1 = System.Drawing.Color.SlateBlue;
            this.btnLogin.BaseColor2 = System.Drawing.Color.Fuchsia;
            this.btnLogin.BorderColor = System.Drawing.Color.Black;
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLogin.FocusedColor = System.Drawing.Color.Empty;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Image = null;
            this.btnLogin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnLogin.ImageSize = new System.Drawing.Size(20, 20);
            this.btnLogin.Location = new System.Drawing.Point(140, 333);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.OnHoverBaseColor1 = System.Drawing.Color.Red;
            this.btnLogin.OnHoverBaseColor2 = System.Drawing.Color.OrangeRed;
            this.btnLogin.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnLogin.OnHoverForeColor = System.Drawing.Color.White;
            this.btnLogin.OnHoverImage = null;
            this.btnLogin.OnPressedColor = System.Drawing.Color.Black;
            this.btnLogin.Radius = 10;
            this.btnLogin.Size = new System.Drawing.Size(147, 40);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            // 
            // txtTK
            // 
            this.txtTK.BackColor = System.Drawing.Color.White;
            this.txtTK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTK.FocusedLineColor = System.Drawing.Color.Red;
            this.txtTK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTK.LineColor = System.Drawing.Color.Gainsboro;
            this.txtTK.Location = new System.Drawing.Point(154, 149);
            this.txtTK.Name = "txtTK";
            this.txtTK.PasswordChar = '\0';
            this.txtTK.SelectedText = "";
            this.txtTK.Size = new System.Drawing.Size(186, 26);
            this.txtTK.TabIndex = 1;
            this.txtTK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMK
            // 
            this.txtMK.AccessibleDescription = "Mật Khẩu";
            this.txtMK.AccessibleName = "Mật Khẩu";
            this.txtMK.BackColor = System.Drawing.Color.White;
            this.txtMK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMK.FocusedLineColor = System.Drawing.Color.Red;
            this.txtMK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMK.LineColor = System.Drawing.Color.Gainsboro;
            this.txtMK.Location = new System.Drawing.Point(154, 204);
            this.txtMK.Name = "txtMK";
            this.txtMK.PasswordChar = '●';
            this.txtMK.SelectedText = "";
            this.txtMK.Size = new System.Drawing.Size(186, 26);
            this.txtMK.TabIndex = 2;
            this.txtMK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel1.Location = new System.Drawing.Point(56, 149);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(79, 21);
            this.gunaLabel1.TabIndex = 12;
            this.gunaLabel1.Text = "Tài Khoản";
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.Location = new System.Drawing.Point(56, 204);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(79, 21);
            this.gunaLabel2.TabIndex = 13;
            this.gunaLabel2.Text = "Mật Khẩu";
            // 
            // btnExit1
            // 
            this.btnExit1.AnimationHoverSpeed = 0.07F;
            this.btnExit1.AnimationSpeed = 0.03F;
            this.btnExit1.BackColor = System.Drawing.Color.Transparent;
            this.btnExit1.BaseColor = System.Drawing.Color.Red;
            this.btnExit1.BorderColor = System.Drawing.Color.Black;
            this.btnExit1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExit1.FocusedColor = System.Drawing.Color.Empty;
            this.btnExit1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit1.ForeColor = System.Drawing.Color.White;
            this.btnExit1.Image = null;
            this.btnExit1.ImageSize = new System.Drawing.Size(20, 20);
            this.btnExit1.Location = new System.Drawing.Point(655, 399);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.OnHoverBaseColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnExit1.OnHoverBorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnExit1.OnHoverForeColor = System.Drawing.Color.White;
            this.btnExit1.OnHoverImage = null;
            this.btnExit1.OnPressedColor = System.Drawing.Color.Black;
            this.btnExit1.Radius = 10;
            this.btnExit1.Size = new System.Drawing.Size(100, 50);
            this.btnExit1.TabIndex = 14;
            this.btnExit1.Text = "Thoát";
            this.btnExit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnExit1.Click += new System.EventHandler(this.btnExit1_Click);
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.AutoSize = true;
            this.gunaLabel3.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gunaLabel3.Font = new System.Drawing.Font("MV Boli", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel3.Location = new System.Drawing.Point(534, 46);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(247, 49);
            this.gunaLabel3.TabIndex = 15;
            this.gunaLabel3.Text = "Small Tower";
            this.gunaLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gunaLabel3.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SingleBitPerPixel;
            // 
            // cbMangLamViec
            // 
            this.cbMangLamViec.BackColor = System.Drawing.Color.Transparent;
            this.cbMangLamViec.BaseColor = System.Drawing.Color.White;
            this.cbMangLamViec.BorderColor = System.Drawing.Color.Silver;
            this.cbMangLamViec.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMangLamViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMangLamViec.FocusedColor = System.Drawing.Color.Empty;
            this.cbMangLamViec.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbMangLamViec.ForeColor = System.Drawing.Color.Black;
            this.cbMangLamViec.FormattingEnabled = true;
            this.cbMangLamViec.Location = new System.Drawing.Point(199, 250);
            this.cbMangLamViec.Name = "cbMangLamViec";
            this.cbMangLamViec.OnHoverItemBaseColor = System.Drawing.Color.Red;
            this.cbMangLamViec.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cbMangLamViec.Size = new System.Drawing.Size(141, 26);
            this.cbMangLamViec.TabIndex = 16;
            // 
            // gunaLabel4
            // 
            this.gunaLabel4.AutoSize = true;
            this.gunaLabel4.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel4.Location = new System.Drawing.Point(56, 255);
            this.gunaLabel4.Name = "gunaLabel4";
            this.gunaLabel4.Size = new System.Drawing.Size(121, 21);
            this.gunaLabel4.TabIndex = 17;
            this.gunaLabel4.Text = "Mảng Làm Việc";
            // 
            // Login
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::quanlychungcu.Properties.Resources.LoginBG;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.ControlBox = false;
            this.Controls.Add(this.gunaLabel4);
            this.Controls.Add(this.cbMangLamViec);
            this.Controls.Add(this.gunaLabel3);
            this.Controls.Add(this.btnExit1);
            this.Controls.Add(this.gunaLabel2);
            this.Controls.Add(this.gunaLabel1);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkGhinhopass);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Small Tower";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label5;
        private Guna.UI.WinForms.GunaCheckBox chkGhinhopass;
        private Guna.UI.WinForms.GunaGradientButton btnLogin;
        private Guna.UI.WinForms.GunaLineTextBox txtTK;
        private Guna.UI.WinForms.GunaLineTextBox txtMK;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaButton btnExit1;
        private Guna.UI.WinForms.GunaLabel gunaLabel3;
        private Guna.UI.WinForms.GunaComboBox cbMangLamViec;
        private Guna.UI.WinForms.GunaLabel gunaLabel4;
    }
}

