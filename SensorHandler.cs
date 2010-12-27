using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using ManagedNite;
using System.Threading;

namespace KinEmote
{
    class HoverEventArgs : EventArgs
    {
        public int currentY;
        public int currentX;
        public HoverEventArgs(int currentX, int currentY)
        {
            this.currentX = currentX;
            this.currentY = currentY;
        }
    }
    class HoverDeltaXEventArgs : EventArgs
    {
        public int deltaX;
        public int deltaConst;
        public HoverDeltaXEventArgs(int deltaX, int deltaConst)
        {
            this.deltaX = deltaX;
            this.deltaConst = deltaConst;
        }
    }
    class HoverDeltaYEventArgs : EventArgs
    {
        public int deltaY;
        public int deltaConst;

        public HoverDeltaYEventArgs(int deltaY, int deltaConst)
        {
            this.deltaY = deltaY;
            this.deltaConst = deltaConst;
        }
    }
    class IsCircleEventArgs : EventArgs
    {
        public float radius;

        public IsCircleEventArgs(float radius)
        {
            this.radius = radius;
        }
    }
    class PushEventArgs : EventArgs
    {
        public bool isPushed;
        public PushEventArgs(bool isPushed)
        {
            this.isPushed = isPushed;
        }
    }

    class SensorHandler
    {
        public event EventHandler<HoverEventArgs> HoverDetected;
        public event EventHandler<SelectableSlider2DSelectEventArgs> PushDetected;

        public event EventHandler<EventArgs> HandDetectedEvent;
        public event EventHandler<EventArgs> SessionEnded;

        public XnMSessionManager sessionManager;
        XnMOpenNIContext context;
        bool terminate = false;

        public XnMSelectableSlider2D slider2D;

        XnMFlowRouter flowRouter;

        XnMPointDenoiser pointDenoiser;

        bool isOK;
        public bool IsOK
        {
            get
            {
                return isOK;
            }
            set
            {
                isOK = value;
            }
        }

        bool handDetected;//In session
        public bool HandDetected
        {
            get
            {
                return handDetected;
            }
            set
            {
                handDetected = value;
            }
        }

        private XnMPoint lastRealWorldPoint;

        private int lastXPosition;
        private int lastYPosition;
        private int xItems;
        private int yItems;

        private Thread readerThread;

        public SensorHandler(int xItems, int yItems)
        {
            isOK = true;
            handDetected = false;
            lastXPosition = -1;
            lastYPosition = -1;
            this.xItems = xItems;
            this.yItems = yItems;

            try
            {
                context = new XnMOpenNIContext();
                context.Init();

                sessionManager = new XnMSessionManager(context, "Click", "RaiseHand");
                sessionManager.QuickRefocusTimeout = 0;
                
                sessionManager.SessionStarted += new EventHandler<PointEventArgs>(SessionStarted);
                sessionManager.SessionEnded += new EventHandler(sessionManager_SessionEnded);
                
                slider2D = new XnMSelectableSlider2D(xItems, yItems, 700, 600);
                slider2D.HysteresisRatio = 0.01F;

                slider2D.ItemHovered += new EventHandler<SelectableSlider2DHoverEventArgs>(slider2D_ItemHovered);
                slider2D.ItemSelected += new EventHandler<SelectableSlider2DSelectEventArgs>(slider2D_ItemSelected);

                pointDenoiser = new XnMPointDenoiser(10);
                pointDenoiser.PrimaryPointUpdate += new EventHandler<HandPointContextEventArgs>(pointDenoiser_PrimaryPointUpdate);
                pointDenoiser.AddListener(slider2D);

                flowRouter = new XnMFlowRouter();
                flowRouter.SetActiveControl(pointDenoiser);

                sessionManager.AddListener(flowRouter);

                readerThread = new Thread(new ThreadStart(SpinInfinite));
                readerThread.Priority = ThreadPriority.BelowNormal;
                readerThread.Start();
            }

            catch (XnMException ex)
            {
                Debug.WriteLine(ex.Message);
                isOK = false;
            }
        }

        void pointDenoiser_PrimaryPointUpdate(object sender, HandPointContextEventArgs e)
        {
            lastRealWorldPoint = e.HPC.Position;
        }

        public void sessionManager_SessionEnded(object sender, EventArgs e)
        {
            if (SessionEnded != null)
                SessionEnded(sender, e);
        }


        private void SessionStarted(object sender, PointEventArgs e)
        {
            HandDetectedEvent(this, null);
            handDetected = true;

            slider2D.Reposition(e.Point);
        }

        private void SpinInfinite()
        {
            while (!terminate)
            {
                uint rc = context.Update();
                if (rc == 0)
                    sessionManager.Update(context);
            }
        }

        private void slider2D_ItemHovered(object sender, SelectableSlider2DHoverEventArgs e)
        {
            if (lastXPosition == -1) lastXPosition = e.X;//For the first time
            if (lastYPosition == -1) lastYPosition = e.Y;//For the first time

            HoverDetected(this, new HoverEventArgs(e.X, e.Y));

            lastXPosition = e.X;
            lastYPosition = e.Y;
        }

        private void slider2D_ItemSelected(object sender, SelectableSlider2DSelectEventArgs e)
        {
            if (e.SelectDirection == Direction.Backward)
            {
                slider2D.Reposition(lastRealWorldPoint);
            }

            PushDetected(sender, e);
        }
        
        public void Dispose()
        {
            terminate = true;
            this.readerThread.Join();
        }
    }
}