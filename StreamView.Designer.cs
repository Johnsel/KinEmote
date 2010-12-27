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
            this.panelView = new System.Windows.Forms.Panel();
            this.contextMenuStripStreamView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripStreamView.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelView
            // 
            this.panelView.ContextMenuStrip = this.contextMenuStripStreamView;
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(0, 0);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(251, 159);
            this.panelView.TabIndex = 0;
            this.panelView.Visible = false;
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
            // StreamView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 159);
            this.ContextMenuStrip = this.contextMenuStripStreamView;
            this.ControlBox = false;
            this.Controls.Add(this.panelView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StreamView";
            this.Text = "KinEmote Streamviewer";
            this.LocationChanged += new System.EventHandler(this.StreamView_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.StreamView_VisibleChanged);
            this.contextMenuStripStreamView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStreamView;
        private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;

    }
}