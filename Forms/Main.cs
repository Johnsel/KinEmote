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
        private SensorHandler sensorHandler;
        private StreamView streamViewer;
        private CustomKeySetup customKeySetup;
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
            tbIp.Text = Properties.Settings.Default.IpAddress;

            switch (Properties.Settings.Default.ClientType)
            {
                case ClientType.XBMC:
                    {
                        checkBoxXBMC.Checked = true;
                    }
                    break;

                case ClientType.Boxee:
                    {
                        checkBoxBoxee.Checked = true;
                    }
                    break;

                case ClientType.Custom:
                    {
                        checkBoxCustom.Checked = true;
                    }
                    break;
            }

            if (Properties.Settings.Default.AutoStartStreamViewer == true)
            {
                streamViewer = new StreamView();
                autostartVisualFeedToolStripMenuItem.Checked = true;
            }
        }

        private void butConnect_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (sensorHandler == null)
            {
                sensorHandler = new SensorHandler();
                sensorHandler.HandDetected += new EventHandler<EventArgs>(sensorHandler_HandDetected);
                sensorHandler.HoverDetected += new EventHandler<SelectableSlider2DHoverEventArgs>(sensorHandler_HoverDetected);
                sensorHandler.SessionEnded += new EventHandler<EventArgs>(sensorHandler_SessionEnded);

                if (!sensorHandler.isOK)
                {
                    sensorHandler.Dispose();
                    sensorHandler = null;
                    Cursor = Cursors.Default;
                    return;
                }

                Properties.Settings.Default.IpAddress = tbIp.Text;
                Properties.Settings.Default.Save();

                if (checkBoxXBMC.Checked)
                {
                    ButtonCodes.SetType(ClientType.XBMC);

                    ButtonSender buttonSender = new ButtonSender(ClientType.XBMC, Properties.Settings.Default.IpAddress, 9777);
                    buttonSender.SendNotification("KinEmote v0.3", "Connected");
                }
                else if (checkBoxBoxee.Checked)
                {
                    ButtonCodes.SetType(ClientType.Boxee);

                    ButtonSender buttonSender = new ButtonSender(ClientType.Boxee, Properties.Settings.Default.IpAddress, 9770);
                    buttonSender.SendNotification("KinEmote v0.3", "Connected");
                }
                else if (checkBoxCustom.Checked)
                {
                    ButtonCodes.SetType(ClientType.Custom);
                }

                butConnect.Text = "Disconnect";
            }
            else
            {
                sensorHandler.Dispose();
                sensorHandler = null;
                butConnect.Text = "Connect";
            }

            Cursor = Cursors.Default;
        }

        private void openVisualFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (streamViewer == null)
                streamViewer = new StreamView();
            
            if (!streamViewer.Visible)
            {
                if (streamViewer.InvokeRequired)
                {
                    streamViewer.Invoke((Action)(() => { streamViewer.Show(); }));
                    streamViewer.Invoke((Action)(() => { streamViewer.DesktopLocation = Properties.Settings.Default.StreamViewerLocation; }));
                    openVisualFeedToolStripMenuItem.Checked = true;
                }
                else
                {
                    if (!streamViewer.IsDisposed)
                    {
                        streamViewer.Show();
                        streamViewer.DesktopLocation = Properties.Settings.Default.StreamViewerLocation;
                        openVisualFeedToolStripMenuItem.Checked = true;
                    }
                    else
                    {
                        streamViewer = null;
                    }
                }

                Cursor = Cursors.Default;
            }
            else
            {
                streamViewer.Invoke((Action)(() => { streamViewer.Hide(); }));
                openVisualFeedToolStripMenuItem.Checked = false;

                Cursor = Cursors.Default;
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
            if (sensorHandler != null)
            {
                sensorHandler.Dispose();
                sensorHandler = null;
            }
            if (streamViewer != null)
            {
                if (!streamViewer.IsDisposed)
                {
                    streamViewer.Close();
                }
            }
        }

        private void checkBoxXBMC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxXBMC.Checked == true)
            {
                checkBoxBoxee.Checked = false;
                checkBoxCustom.Checked = false;

                Properties.Settings.Default.ClientType = ClientType.XBMC;
                Properties.Settings.Default.Save();
            }
        }

        private void checkBoxBoxee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBoxee.Checked == true)
            {
                checkBoxXBMC.Checked = false;
                checkBoxCustom.Checked = false;

                Properties.Settings.Default.ClientType = ClientType.Boxee;
                Properties.Settings.Default.Save();
            }
        }

        private void checkBoxCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCustom.Checked)
            {
                checkBoxXBMC.Checked = false;
                checkBoxBoxee.Checked = false;

                Properties.Settings.Default.ClientType = ClientType.Custom;
            }
        }

        private void editCustomKeymapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (customKeySetup != null && !customKeySetup.IsDisposed)
            {
                customKeySetup.Activate();
            }
            else
            {
                customKeySetup = new CustomKeySetup();
                customKeySetup.Show();
            }
        }

        private void linkLabelVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forum.xbmc.org/showthread.php?t=87663");
        }
        #endregion

        #region SensorEvents
        private void sensorHandler_HandDetected(object sender, EventArgs e)
        {
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
        }

        private void sensorHandler_HoverDetected(object sender, SelectableSlider2DHoverEventArgs e)
        {
            textBox1.Invoke((Action)(() => { this.textBox1.Text = e.X + " - " + e.Y; }));
        }

        void sensorHandler_SessionEnded(object sender, EventArgs e)
        {
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
