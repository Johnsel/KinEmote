using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using xn;

namespace KinEmote
{
    public partial class StreamView : Form
    {
        private Context context;
        private DepthGenerator depth;
        private Thread readerThread;
        private bool shouldRun;
        private Bitmap bitmap;
        private int[] histogram;

        public StreamView()
        {
            InitializeComponent();
            Console.WriteLine("StreamView 0");

            this.context = new Context(@".\data\openniconfig.xml");
            this.depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            Console.WriteLine("StreamView 1");
            if (this.depth == null)
            {
                throw new Exception("Viewer must have a depth node!");
            }

            this.histogram = new int[this.depth.GetDeviceMaxDepth()];

            MapOutputMode mapMode = this.depth.GetMapOutputMode();

            this.bitmap = new Bitmap((int)mapMode.nXRes, (int)mapMode.nYRes, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Console.WriteLine("StreamView 2");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.Location = this.Location;
            lock (this)
            {
                e.Graphics.DrawImage(this.bitmap,
                    this.panelView.Location.X,
                    this.panelView.Location.Y,
                    this.panelView.Size.Width,
                    this.panelView.Size.Height);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Don't allow the background to paint
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.shouldRun = false;
            this.readerThread.Join();
            base.OnClosing(e);
        }

        private unsafe void CalcHist(DepthMetaData depthMD)
        {
            // reset
            for (int i = 0; i < this.histogram.Length; ++i)
                this.histogram[i] = 0;

            ushort* pDepth = (ushort*)depthMD.DepthMapPtr.ToPointer();

            int points = 0;
            for (int y = 0; y < depthMD.YRes; ++y)
            {
                for (int x = 0; x < depthMD.XRes; ++x, ++pDepth)
                {
                    ushort depthVal = *pDepth;
                    if (depthVal != 0)
                    {
                        this.histogram[depthVal]++;
                        points++;
                    }
                }
            }

            for (int i = 1; i < this.histogram.Length; i++)
            {
                this.histogram[i] += this.histogram[i - 1];
            }

            if (points > 0)
            {
                for (int i = 1; i < this.histogram.Length; i++)
                {
                    this.histogram[i] = (int)(256 * (1.0f - (this.histogram[i] / (float)points)));
                }
            }
        }

        private unsafe void ReaderThread()
        {
            DepthMetaData depthMD = new DepthMetaData();

            while (this.shouldRun)
            {
                try
                {
                    this.context.WaitOneUpdateAll(this.depth);
                }
                catch (Exception)
                {
                }

                this.depth.GetMetaData(depthMD);

                CalcHist(depthMD);

                lock (this)
                {
                    Rectangle rect = new Rectangle(0, 0, this.bitmap.Width, this.bitmap.Height);
                    BitmapData data = this.bitmap.LockBits(rect, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    ushort* pDepth = (ushort*)this.depth.GetDepthMapPtr().ToPointer();

                    // set pixels
                    for (int y = 0; y < depthMD.YRes; ++y)
                    {
                        byte* pDest = (byte*)data.Scan0.ToPointer() + y * data.Stride;
                        for (int x = 0; x < depthMD.XRes; ++x, ++pDepth, pDest += 3)
                        {
                            byte pixel = (byte)this.histogram[*pDepth];
                            pDest[0] = 0;
                            pDest[1] = pixel;
                            pDest[2] = pixel;
                        }
                    }

                    this.bitmap.UnlockBits(data);
                }

                this.Invalidate();
            }
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                Properties.Settings.Default.TopMost = false;
                this.TopMost = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                stayOnTopToolStripMenuItem.Checked = false;
            }
            else
            {
                Properties.Settings.Default.TopMost = true;
                this.TopMost = true;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                stayOnTopToolStripMenuItem.Checked = true;
            }
            Properties.Settings.Default.Save();
        }

        private void StreamView_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StreamViewerLocation = this.DesktopLocation;
            Properties.Settings.Default.Save();
        }

        private void StreamView_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (Properties.Settings.Default.TopMost)
                {
                    this.TopMost = true;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    stayOnTopToolStripMenuItem.Checked = true;
                }

                this.shouldRun = true;
                this.readerThread = new Thread(ReaderThread);
                this.readerThread.Start();
            }
            else
            {
                this.shouldRun = false;
                this.readerThread.Join();
            }
        }
    }
}
