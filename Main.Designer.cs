namespace KinEmote
{
    partial class Main
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.labIp = new System.Windows.Forms.Label();
            this.butConnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxXBMC = new System.Windows.Forms.CheckBox();
            this.checkBoxBoxee = new System.Windows.Forms.CheckBox();
            this.linkLabelVersion = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.openVisualFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autostartVisualFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(88, 260);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 20);
            this.textBox1.TabIndex = 0;
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(88, 234);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(184, 20);
            this.tbIp.TabIndex = 1;
            // 
            // labIp
            // 
            this.labIp.AutoSize = true;
            this.labIp.Location = new System.Drawing.Point(12, 237);
            this.labIp.Name = "labIp";
            this.labIp.Size = new System.Drawing.Size(59, 13);
            this.labIp.TabIndex = 2;
            this.labIp.Text = "Ip address:";
            // 
            // butConnect
            // 
            this.butConnect.Location = new System.Drawing.Point(361, 258);
            this.butConnect.Name = "butConnect";
            this.butConnect.Size = new System.Drawing.Size(124, 23);
            this.butConnect.TabIndex = 3;
            this.butConnect.Text = " Connect";
            this.butConnect.UseVisualStyleBackColor = true;
            this.butConnect.Click += new System.EventHandler(this.butConnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::KinEmote.Properties.Resources.KinEmote_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(555, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // checkBoxXBMC
            // 
            this.checkBoxXBMC.AutoSize = true;
            this.checkBoxXBMC.Location = new System.Drawing.Point(278, 236);
            this.checkBoxXBMC.Name = "checkBoxXBMC";
            this.checkBoxXBMC.Size = new System.Drawing.Size(56, 17);
            this.checkBoxXBMC.TabIndex = 5;
            this.checkBoxXBMC.Text = "XBMC";
            this.checkBoxXBMC.UseVisualStyleBackColor = true;
            this.checkBoxXBMC.CheckedChanged += new System.EventHandler(this.checkBoxXBMC_CheckedChanged);
            // 
            // checkBoxBoxee
            // 
            this.checkBoxBoxee.AutoSize = true;
            this.checkBoxBoxee.Location = new System.Drawing.Point(278, 262);
            this.checkBoxBoxee.Name = "checkBoxBoxee";
            this.checkBoxBoxee.Size = new System.Drawing.Size(56, 17);
            this.checkBoxBoxee.TabIndex = 6;
            this.checkBoxBoxee.Text = "Boxee";
            this.checkBoxBoxee.UseVisualStyleBackColor = true;
            this.checkBoxBoxee.CheckedChanged += new System.EventHandler(this.checkBoxBoxee_CheckedChanged);
            // 
            // linkLabelVersion
            // 
            this.linkLabelVersion.AutoSize = true;
            this.linkLabelVersion.Location = new System.Drawing.Point(444, 288);
            this.linkLabelVersion.Name = "linkLabelVersion";
            this.linkLabelVersion.Size = new System.Drawing.Size(114, 13);
            this.linkLabelVersion.TabIndex = 7;
            this.linkLabelVersion.TabStop = true;
            this.linkLabelVersion.Text = "version 0.2 public beta";
            this.linkLabelVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelVersion_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Current X - Y:";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuVideo});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(555, 24);
            this.mainMenuStrip.TabIndex = 9;
            this.mainMenuStrip.Text = "menuStripMain";
            // 
            // toolStripMenuVideo
            // 
            this.toolStripMenuVideo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openVisualFeedToolStripMenuItem,
            this.autostartVisualFeedToolStripMenuItem});
            this.toolStripMenuVideo.Name = "toolStripMenuVideo";
            this.toolStripMenuVideo.Size = new System.Drawing.Size(49, 20);
            this.toolStripMenuVideo.Text = "Video";
            // 
            // openVisualFeedToolStripMenuItem
            // 
            this.openVisualFeedToolStripMenuItem.Name = "openVisualFeedToolStripMenuItem";
            this.openVisualFeedToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openVisualFeedToolStripMenuItem.Text = "Show visual feed";
            this.openVisualFeedToolStripMenuItem.Click += new System.EventHandler(this.openVisualFeedToolStripMenuItem_Click);
            // 
            // autostartVisualFeedToolStripMenuItem
            // 
            this.autostartVisualFeedToolStripMenuItem.Name = "autostartVisualFeedToolStripMenuItem";
            this.autostartVisualFeedToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.autostartVisualFeedToolStripMenuItem.Text = "Autostart visual feed";
            this.autostartVisualFeedToolStripMenuItem.Click += new System.EventHandler(this.autostartVisualFeedToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(183)))), ((int)(((byte)(115)))));
            this.ClientSize = new System.Drawing.Size(555, 302);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabelVersion);
            this.Controls.Add(this.checkBoxBoxee);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxXBMC);
            this.Controls.Add(this.labIp);
            this.Controls.Add(this.butConnect);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.Text = "KinEmote v0.2 Beta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Label labIp;
        private System.Windows.Forms.Button butConnect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxXBMC;
        private System.Windows.Forms.CheckBox checkBoxBoxee;
        private System.Windows.Forms.LinkLabel linkLabelVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuVideo;
        private System.Windows.Forms.ToolStripMenuItem openVisualFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autostartVisualFeedToolStripMenuItem;
    }
}