using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using ManagedNite;

using XBMC;

namespace KinEmote
{
    public partial class Main : Form
    {
        #region Declarations
        SensorHandler hand = new SensorHandler(7, 7);
        ButtonSender buttonSender;
        
        System.Timers.Timer intervalTimer = new System.Timers.Timer();

        Point currentPoint;
        Point lastPoint;
        bool backPlaneEnabled = false;

        StreamView streamViewer;
        #endregion

        #region Ctor
        public Main()
        {
            InitializeComponent();
        }
        #endregion

        #region FormEvents
        private void Main_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Main_load 0");
            tbIp.Text = Properties.Settings.Default.IpAddress;

            if (Properties.Settings.Default.Port == 9777)
            {
                checkBoxXBMC.Checked = true;
            }
            else if (Properties.Settings.Default.Port == 9770)
            {
                checkBoxBoxee.Checked = true;
            }

            Console.WriteLine("Main_load 1");

            if (!hand.IsOK)
            {
                MessageBox.Show("Error initializing Kinect Sensor. Application will shut down");
                if (hand != null)
                {
                    hand.Dispose();
                }
                this.Close();
            }
            
            Console.WriteLine("Main_load 2");

            if (Properties.Settings.Default.AutoStartStreamViewer == true)
            {
                streamViewer = new StreamView();
                autostartVisualFeedToolStripMenuItem.Checked = true;
            }


            hand.PushDetected += new EventHandler<SelectableSlider2DSelectEventArgs>(hand_PushDetected);
            hand.HoverDetected += new EventHandler<HoverEventArgs>(hand_HoverDetected);

            hand.HandDetectedEvent += new EventHandler<EventArgs>(hand_HandDetectedEvent);
            hand.SessionEnded += new EventHandler<EventArgs>(hand_SessionEnded);

            intervalTimer.Elapsed += new ElapsedEventHandler(intervalTimer_Tick);
        }

        private void butConnect_Click(object sender, EventArgs e)
        {
            if (buttonSender == null || !buttonSender.Connected)
            {
                Properties.Settings.Default.IpAddress = tbIp.Text;
                Properties.Settings.Default.Save();
                if (checkBoxXBMC.Checked)
                {
                    buttonSender = new ButtonSender(ClientType.XBMC, Properties.Settings.Default.IpAddress, Properties.Settings.Default.Port);
                }
                else if (checkBoxBoxee.Checked)
                {
                    buttonSender = new ButtonSender(ClientType.Boxee, Properties.Settings.Default.IpAddress, Properties.Settings.Default.Port);
                }

                butConnect.Text = "Disconnect";
            }
            else
            {
                buttonSender.Disconnect();
                butConnect.Text = "Connect";
            }
        }

        private void openVisualFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (streamViewer == null)
                streamViewer = new StreamView();
            
            if (!streamViewer.Visible)
            {
                if (streamViewer.InvokeRequired)
                {
                    streamViewer.Invoke((Action)(() => { streamViewer.Show(); }));
                    streamViewer.Invoke((Action)(() => { streamViewer.DesktopLocation = Properties.Settings.Default.StreamViewerLocation; }));
                }
                else
                {
                    streamViewer.Show();
                    streamViewer.DesktopLocation = Properties.Settings.Default.StreamViewerLocation;
                }
                openVisualFeedToolStripMenuItem.Checked = true;
            }
            else
            {
                streamViewer.Invoke((Action)(() => { streamViewer.Hide(); }));
                openVisualFeedToolStripMenuItem.Checked = false;
            }
        }

        private void autostartVisualFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AutoStartStreamViewer == false)
            {
                Properties.Settings.Default.AutoStartStreamViewer = true;
                autostartVisualFeedToolStripMenuItem.Checked = true;
            }
            else
            {
                Properties.Settings.Default.AutoStartStreamViewer = false;
                autostartVisualFeedToolStripMenuItem.Checked = false;
            }

            Properties.Settings.Default.Save();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            hand.Dispose();
            if (streamViewer != null)
            {
                if (!streamViewer.IsDisposed)
                {
                    streamViewer.Close();
                    streamViewer.Dispose();
                }
            }
        }

        private void checkBoxXBMC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxXBMC.Checked == true)
            {
                checkBoxBoxee.Checked = false;
            }
            Properties.Settings.Default.Port = 9777;
            Properties.Settings.Default.Save();
        }

        private void checkBoxBoxee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBoxee.Checked == true)
            {
                checkBoxXBMC.Checked = false;
            }

            Properties.Settings.Default.Port = 9770;
            Properties.Settings.Default.Save();
        }

        private void linkLabelVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forum.xbmc.org/showthread.php?t=87663");
        }
        #endregion

        #region Timer
        private void intervalTimer_Tick(object sender, ElapsedEventArgs e)
        {
            if (buttonSender == null || !buttonSender.Connected)
                return;

            if (!hand.HandDetected)
                intervalTimer.Stop();

            if (currentPoint.X <3)
            {
                buttonSender.SendKey(ButtonCodes.Left);
                //eventClient.SendButton("left", "R1", ButtonFlagsType.BTN_NO_REPEAT);
            }
            else if (currentPoint.X == 3)
            {
                if (currentPoint.Y < 3)
                {
                    buttonSender.SendKey(ButtonCodes.Down);
                    //eventClient.SendButton("down", "R1", ButtonFlagsType.BTN_NO_REPEAT);
                }
                else if (currentPoint.Y > 3)
                {
                    buttonSender.SendKey(ButtonCodes.Up);
                    //eventClient.SendButton("up", "R1", ButtonFlagsType.BTN_NO_REPEAT);
                }
            }
            else if (currentPoint.X > 3)
            {
                buttonSender.SendKey(ButtonCodes.Right);
                //eventClient.SendButton("right", "R1", ButtonFlagsType.BTN_NO_REPEAT);
            }
        }
        #endregion

        #region NiteEvents
        void hand_HandDetectedEvent(object sender, EventArgs e)
        {
            if (buttonSender == null || !buttonSender.Connected)
                return;

            backPlaneEnabled = false;
            intervalTimer.Start();
            intervalTimer.Interval = 20;

            if (streamViewer == null)
                streamViewer = new StreamView();

            if (Properties.Settings.Default.AutoStartStreamViewer == true)
            {
                if (streamViewer.Visible == false)
                {
                    if (streamViewer.InvokeRequired)
                    {
                        streamViewer.Invoke((Action)(() => { streamViewer.Show(); }));
                    }
                    else
                    {
                        this.Invoke((Action)(() => { streamViewer.Show(); }));
                    }
                    streamViewer.DesktopLocation = Properties.Settings.Default.StreamViewerLocation;
                }
            }

            buttonSender.SendNotification("Kinect Remote", "Hand Detected");
        }

        void hand_HoverDetected(object sender, HoverEventArgs e)
        {
            if (buttonSender == null || !buttonSender.Connected)
                return;

            currentPoint.X = e.currentX;
            currentPoint.Y = e.currentY;

            textBox1.Invoke((Action)(() => { this.textBox1.Text = currentPoint.X + " - " + currentPoint.Y; }));

            if (this.backPlaneEnabled)
            {
                handlebackPlane();
            }
            else
            {
                handleNormalMode();
            }

            lastPoint.X = e.currentX;
            lastPoint.Y = e.currentY;
        }

        void hand_PushDetected(object sender, SelectableSlider2DSelectEventArgs e)
        {
            if (buttonSender == null || !buttonSender.Connected)
                return;

            if (e.SelectDirection == Direction.Forward)
            {
                if (backPlaneEnabled)
                {

                    backPlaneEnabled = false;
                    buttonSender.SendNotification("Kinect Remote", "Push detected, normal mode activated.");
                }
                else
                {
                    buttonSender.SendKey(ButtonCodes.Enter);
                    //eventClient.SendButton("select", "R1", ButtonFlagsType.BTN_NO_REPEAT);
                }
            }
            else
            {
                intervalTimer.Stop();
                buttonSender.SendNotification("Kinect Remote", "Hand moved backward, mode B activated.");
                backPlaneEnabled = true;
            }
        }

        void hand_SessionEnded(object sender, EventArgs e)
        {
            if (buttonSender == null || !buttonSender.Connected)
                return;

            buttonSender.SendNotification("Kinect Remote", "Session ended.");
            intervalTimer.Stop();

            if (Properties.Settings.Default.AutoStartStreamViewer == true)
            {
                if (streamViewer.Visible)
                {
                    streamViewer.Invoke((Action)(() => { streamViewer.Hide(); }));
                }
            }
        }
        #endregion
    }
}
