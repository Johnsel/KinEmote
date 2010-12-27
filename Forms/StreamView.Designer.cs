namespace KinEmote
{
    partial class StreamView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamView));
            this.contextMenuStripStreamView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxOverlay = new System.Windows.Forms.PictureBox();
            this.contextMenuStripStreamView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOverlay)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripStreamView
            // 
            this.contextMenuStripStreamView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stayOnTopToolStripMenuItem});
            this.contextMenuStripStreamView.Name = "contextMenuStripStreamView";
            this.contextMenuStripStreamView.Size = new System.Drawing.Size(153, 48);
            // 
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
            this.stayOnTopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stayOnTopToolStripMenuItem.Text = "Stay on Top";
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
            // 
            // pictureBoxOverlay
            // 
            this.pictureBoxOverlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxOverlay.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxOverlay.Image")));
            this.pictureBoxOverlay.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxOverlay.Name = "pictureBoxOverlay";
            this.pictureBoxOverlay.Size = new System.Drawing.Size(251, 159);
            this.pictureBoxOverlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOverlay.TabIndex = 1;
            this.pictureBoxOverlay.TabStop = false;
            // 
            // StreamView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 159);
            this.ContextMenuStrip = this.contextMenuStripStreamView;
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxOverlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StreamView";
            this.Text = "KinEmote Streamviewer";
            this.LocationChanged += new System.EventHandler(this.StreamView_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.StreamView_VisibleChanged);
            this.contextMenuStripStreamView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOverlay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripStreamView;
        private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxOverlay;

    }
}