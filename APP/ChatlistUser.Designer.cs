namespace APP
{
    partial class ChatlistUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatlistUser));
            this.Image = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.usrn = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.name = new Bunifu.UI.WinForms.BunifuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.AllowFocused = false;
            this.Image.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Image.AutoSizeHeight = true;
            this.Image.BorderRadius = 35;
            this.Image.Cursor = System.Windows.Forms.Cursors.Default;
            this.Image.Image = ((System.Drawing.Image)(resources.GetObject("Image.Image")));
            this.Image.IsCircle = true;
            this.Image.Location = new System.Drawing.Point(11, 13);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(70, 70);
            this.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image.TabIndex = 0;
            this.Image.TabStop = false;
            this.Image.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            // 
            // usrn
            // 
            this.usrn.AllowParentOverrides = false;
            this.usrn.AutoEllipsis = false;
            this.usrn.Cursor = System.Windows.Forms.Cursors.Default;
            this.usrn.CursorType = System.Windows.Forms.Cursors.Default;
            this.usrn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.usrn.Location = new System.Drawing.Point(90, 40);
            this.usrn.Name = "usrn";
            this.usrn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.usrn.Size = new System.Drawing.Size(80, 21);
            this.usrn.TabIndex = 2;
            this.usrn.Text = "User Name";
            this.usrn.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.usrn.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.bunifuLabel1.Location = new System.Drawing.Point(231, 41);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(57, 20);
            this.bunifuLabel1.TabIndex = 3;
            this.bunifuLabel1.Text = "Đã chọn";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.bunifuLabel1.Visible = false;
            this.bunifuLabel1.Click += new System.EventHandler(this.bunifuLabel1_Click);
            // 
            // name
            // 
            this.name.AllowParentOverrides = false;
            this.name.AutoEllipsis = false;
            this.name.Cursor = System.Windows.Forms.Cursors.Default;
            this.name.CursorType = System.Windows.Forms.Cursors.Default;
            this.name.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(189, 10);
            this.name.Name = "name";
            this.name.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.name.Size = new System.Drawing.Size(97, 25);
            this.name.TabIndex = 1;
            this.name.Text = "User Name";
            this.name.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.name.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.name.Visible = false;
            this.name.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
            // 
            // ChatlistUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.bunifuLabel1);
            this.Controls.Add(this.usrn);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Image);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ChatlistUser";
            this.Size = new System.Drawing.Size(320, 100);
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Bunifu.UI.WinForms.BunifuPictureBox Image;
        private Bunifu.UI.WinForms.BunifuLabel usrn;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuLabel name;
    }
}
