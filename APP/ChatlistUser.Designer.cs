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
            this.Name = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel3 = new Bunifu.UI.WinForms.BunifuLabel();
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
            // Name
            // 
            this.Name.AllowParentOverrides = false;
            this.Name.AutoEllipsis = false;
            this.Name.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name.CursorType = System.Windows.Forms.Cursors.Default;
            this.Name.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name.Location = new System.Drawing.Point(87, 38);
            this.Name.Name = "Name";
            this.Name.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Name.Size = new System.Drawing.Size(73, 20);
            this.Name.TabIndex = 1;
            this.Name.Text = "User Name";
            this.Name.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.Name.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.Name.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
            // 
            // bunifuLabel3
            // 
            this.bunifuLabel3.AllowParentOverrides = false;
            this.bunifuLabel3.AutoEllipsis = false;
            this.bunifuLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel3.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.bunifuLabel3.Location = new System.Drawing.Point(229, 38);
            this.bunifuLabel3.Name = "bunifuLabel3";
            this.bunifuLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel3.Size = new System.Drawing.Size(61, 21);
            this.bunifuLabel3.TabIndex = 3;
            this.bunifuLabel3.Text = "Đã Chọn";
            this.bunifuLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel3.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.bunifuLabel3.Visible = false;
            // 
            // ChatlistUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.bunifuLabel3);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.Image);
            this.Margin = new System.Windows.Forms.Padding(0);
           
            this.Size = new System.Drawing.Size(320, 100);
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Bunifu.UI.WinForms.BunifuLabel Name;
        private Bunifu.UI.WinForms.BunifuPictureBox Image;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel3;
    }
}
