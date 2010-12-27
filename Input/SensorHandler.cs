using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using ManagedNite;
using System.Windows.Forms;
using System.Threading;

namespace KinEmote
{
    class SensorHandler
    {
        #region Events
        public event EventHandler<SelectableSlider2DHoverEventArgs> HoverDetected;
        public event EventHandler<EventArgs> HandDetected;
        public event EventHandler<EventArgs> SessionEnded;
        #endregion

        #region Declarations
        public bool isOK = true, handDetected = false;

        XnMOpenNIContext context;
        XnMSessionManager sessionManager;
        XnMFlowRouter flowRouter;
        XnMPointDenoiser pointDenoiser;
        XnMSelectableSlider2D slider2D;
        XnMPoint lastRealHandPoint;

        bool terminate = false;
        Thread readerThread;

        HandHandler handHandler;
        #endregion

        #region Ctor
        public SensorHandler()
        {
            try
            {
                context = new XnMOpenNIContext();
                context.Init();

                slider2D = new XnMSelectableSlider2D(7, 7, 700, 600);
                slider2D.HysteresisRatio = 0;
                slider2D.BorderWidth = 0;
                slider2D.ItemHovered += new EventHandler<SelectableSlider2DHoverEventArgs>(slider2D_ItemHovered);
                slider2D.ItemSelected += new EventHandler<SelectableSlider2DSelectEventArgs>(slider2D_ItemSelected);

                pointDenoiser = new XnMPointDenoiser(15);
                pointDenoiser.PrimaryPointUpdate += new EventHandler<HandPointContextEventArgs>(pointDenoiser_PrimaryPointUpdate);
                pointDenoiser.AddListener(slider2D);

                flowRouter = new XnMFlowRouter();
                flowRouter.SetActiveControl(pointDenoiser);

                sessionManager = new XnMSessionManager(context, "Click", "");
                sessionManager.SessionStarted += new EventHandler<PointEventArgs>(SessionStarted);
                sessionManager.SessionEnded += new EventHandler(sessionManager_SessionEnded);
                sessionManager.AddListener(flowRouter);

                readerThread = new Thread(new ThreadStart(SpinInfinite));
                readerThread.Priority = ThreadPriority.Highest;
                readerThread.Start();
            }
            catch (XnMException ex)
            {
                ///
                /// - todo: proper error logging here
                /// 

                MessageBox.Show("Error initializing NITE.");
                MessageBox.Show(ex.ExceptionDescription);

                isOK = false;
            }
        }
        #endregion

        #region EventHandlers
        private void SessionStarted(object sender, PointEventArgs e)
        {
            handDetected = true;
            slider2D.Reposition(new XnMPoint(e.Point.X, e.Point.Y, e.Point.Z));

            handHandler = new HandHandler();
            handHandler.DropSessionTrigger += new EventHandler<EventArgs>(handHandler_DropSessionTrigger);

            if (HandDetected != null)
            {
                HandDetected(sender, e);
            }
        }

        private void handHandler_DropSessionTrigger(object sender, EventArgs e)
        {
            sessionManager.EndSession();

            sessionManager_SessionEnded(sender, e);
        }

        private void pointDenoiser_PrimaryPointUpdate(object sender, HandPointContextEventArgs e)
        {
            if (e.HPC.nUserID == pointDenoiser.PrimaryID)
            {
                lastRealHandPoint = e.HPC.Position;

                handHandler.HandleMove(null, new Point?(new Point((int)e.HPC.Position.X, (int)e.HPC.Position.Y)));
            }
        }

        private void slider2D_ItemHovered(object sender, SelectableSlider2DHoverEventArgs e)
        {
            if (handHandler != null)
            {
                handHandler.HandleMove(new Point(e.X, e.Y), null);
            }

            if (HoverDetected != null)
            {
                HoverDetected(sender, e);
            }
        }

        private void slider2D_ItemSelected(object sender, SelectableSlider2DSelectEventArgs e)
        {
            if (handHandler != null)
            {
                handHandler.HandlePush(e.SelectDirection);
            }
            
            if (e.SelectDirection == Direction.Backward)
            {
                slider2D.Reposition(new XnMPoint(lastRealHandPoint.X, lastRealHandPoint.Y, lastRealHandPoint.Z));
            }
        }

        private void sessionManager_SessionEnded(object sender, EventArgs e)
        {
            if (handHandler != null)
            {
                handHandler.Dispose();
                handHandler = null;
            }

            if (SessionEnded != null)
            {
                SessionEnded(sender, e);
            }
        }
        #endregion

        #region Methods
        private void SpinInfinite()
        {
            while (!terminate)
            {
                uint rc = context.Update();
                if (rc == 0)
                    sessionManager.Update(context);
            }
        }
        #endregion

        #region Dtor
        public void Dispose()
        {
            terminate = true;
            if (this.readerThread != null)
            {
                this.readerThread.Join();
            }
        }
        #endregion
    }
}