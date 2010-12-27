using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using ManagedNite;

namespace KinEmote
{
    enum HandMode
    {
        Normal,
        BackPlane,
        Scroll
    }

    class HandHandler
    {
        #region Events
        public event EventHandler<EventArgs> DropSessionTrigger;
        #endregion

        #region Declarations
        private ButtonSender buttonSender;
        private Point lastPoint, currentPoint, lastRealWorldPoint;
        private Direction lastDirection;
        private HandMode handMode;
        private System.Timers.Timer intervalTimer;
        private bool stopRepeat = false;
        #endregion

        #region Ctor
        public HandHandler()
        {
            ClientType clientType = Properties.Settings.Default.ClientType;
            ButtonCodes.SetType(clientType);

            if (clientType == ClientType.Custom)
            {
                buttonSender = new ButtonSender(Properties.Settings.Default.ClientType);
            }
            else
            {
                if (clientType == ClientType.XBMC)
                {
                    buttonSender = new ButtonSender(Properties.Settings.Default.ClientType, Properties.Settings.Default.IpAddress, 9777);
                }
                else if (clientType == ClientType.Boxee)
                {
                    buttonSender = new ButtonSender(Properties.Settings.Default.ClientType, Properties.Settings.Default.IpAddress, 9770);
                }
                buttonSender.SendNotification("KinEmote v0.3", "Hand Detected, tracking started.");
            }
            
            lastDirection = Direction.Illegal;
            handMode = HandMode.Normal;
            lastPoint = new Point(1, 2);

            intervalTimer = new Timer();
            intervalTimer.Elapsed += new ElapsedEventHandler(intervalTimer_Elapsed);
        }
        #endregion
        
        #region Methods
        public void HandleMove(Point? currentPoint, Point? currentRealWorldPoint)
        {
            if (currentRealWorldPoint == null)
            {
                this.currentPoint = (Point)currentPoint;

                if (handMode == HandMode.Normal)
                {
                    HandleNormalMode((Point)currentPoint);
                    lastPoint = (Point)currentPoint;
                }
                else if (handMode == HandMode.BackPlane)
                {
                    bool dropSession = HandleBackPlaneMode((Point)currentPoint);
                    if (dropSession)
                    {
                        DropSessionTrigger(this, EventArgs.Empty);
                    }

                    lastPoint = (Point)currentPoint;
                }
            }
            else
            {
                lastRealWorldPoint = (Point)currentRealWorldPoint;
            }
        }

        public void HandlePush(Direction pushDirection)
        {
            if (handMode == HandMode.Normal)
            {
                if (pushDirection == Direction.Forward)
                {
                    buttonSender.SendKey(ButtonCodes.MidPush);
                }
                else
                {
                    intervalTimer.Stop();
                    buttonSender.SendNotification("KinEmote v0.3", "Hand moved to back plane.");
                    handMode = HandMode.BackPlane;
                }
            }
            else
            {
                handMode = HandMode.Normal;
                buttonSender.SendNotification("KinEmote v0.3", "Hand moved to mid plane.");
            }
        }

        private void ResetTimer()
        {
            ResetTimer(null);
        }

        private void ResetTimer(int? newInterval)
        {
            intervalTimer.Stop();
            if (newInterval != null)
            {
                intervalTimer.Interval = (int)newInterval;
            }
            intervalTimer.Start();
        }
        #endregion

        #region EventHandlers
        void intervalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (currentPoint.X < 3)
            {
                buttonSender.SendKey(ButtonCodes.MidLeft);
            }
            else if (currentPoint.X == 3)
            {
                if (currentPoint.Y < 3)
                {
                    buttonSender.SendKey(ButtonCodes.MidDown);
                }
                else if (currentPoint.Y > 3)
                {
                    buttonSender.SendKey(ButtonCodes.MidUp);
                }
            }
            else if (currentPoint.X > 3)
            {
                buttonSender.SendKey(ButtonCodes.MidRight);
            }
        }
        #endregion

        #region Normal Mode
        private void HandleNormalMode(Point currentPoint)
        {
            ///
            /// Horizontal
            /// 
            if (currentPoint == new Point(0, 2))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(100);
            }
            else if (currentPoint == new Point(0, 3))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(100);
            }
            else if (currentPoint == new Point(1, 3))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(300);
            }
            else if (currentPoint == new Point(2, 3))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(1000);
            }
            else if (currentPoint == new Point(3, 3))
            {
                intervalTimer.Stop();
                return;
            }
            else if (currentPoint == new Point(4, 3))
            {
                if (lastPoint.X < currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidRight);
                }
                ResetTimer(1000);
            }
            else if (currentPoint == new Point(5, 3))
            {
                if (lastPoint.X < currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidRight);
                }
                ResetTimer(300);
            }
            else if (currentPoint == new Point(6, 3))
            {
                if (lastPoint.X < currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidRight);
                }
                ResetTimer(100);
            }
            else if (currentPoint == new Point(0, 2))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(100);
            }
            else if (currentPoint == new Point(1, 2))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(300);
            }
            else if (currentPoint == new Point(2, 2))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidLeft);
                }
                ResetTimer(1000);
            }
            else if (currentPoint == new Point(3, 2))
            {
                intervalTimer.Stop();
                return;
            }
            else if (currentPoint == new Point(4, 2))
            {
                if (lastPoint.X < currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidRight);
                }
                ResetTimer(1000);
            }
            else if (currentPoint == new Point(5, 2))
            {
                if (lastPoint.X < currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidRight);
                }
                ResetTimer(300);
            }
            else if (currentPoint == new Point(6, 2))
            {
                if (lastPoint.X > currentPoint.X)
                {
                    buttonSender.SendKey(ButtonCodes.MidRight);
                }
                ResetTimer(100);
            }
            else
                ///
                /// Vertical
                /// 
                if (currentPoint == new Point(3, 0))
                {
                    if (lastPoint.Y < currentPoint.Y)
                    {
                        buttonSender.SendKey(ButtonCodes.MidDown);
                    }
                    ResetTimer(200);
                }
                else if (currentPoint == new Point(3, 1))
                {
                    if (lastPoint.Y > currentPoint.Y)
                    {
                        buttonSender.SendKey(ButtonCodes.MidDown);
                    }
                    ResetTimer(1000);
                }
                else if (currentPoint == new Point(3, 4))
                {
                    if (lastPoint.Y < currentPoint.Y)
                    {
                        buttonSender.SendKey(ButtonCodes.MidUp);
                    }
                    ResetTimer(1000);
                }
                else if (currentPoint == new Point(3, 5) || currentPoint == new Point(3, 6))
                {
                    if (lastPoint.Y < currentPoint.Y)
                    {
                        buttonSender.SendKey(ButtonCodes.MidUp);
                    }
                    ResetTimer(1000);
                }
                else
                {
                    intervalTimer.Stop();
                }
        }
        #endregion

        #region BackPlane Mode
        /// <returns>Bool indicating whether session should be dropped</returns>
        private bool HandleBackPlaneMode(Point currentPoint)
        {
            if (currentPoint.X < 3)
            {
                if (!stopRepeat)
                {
                    buttonSender.SendKey(ButtonCodes.BackLeft);
                    stopRepeat = true;
                }
            }

            if (currentPoint.X == 3 || currentPoint.X == 4)
            {
                stopRepeat = false;
            }

            if (currentPoint.X > 4)
            {
                if (!stopRepeat)
                {
                    buttonSender.SendKey(ButtonCodes.BackRight);
                    stopRepeat = true;
                }
            }

            if (currentPoint.Y < 1)
            {
                intervalTimer.Stop();
                return true;
            }

            if (currentPoint.Y > 4)
            {
                if (!stopRepeat)
                {
                    buttonSender.SendKey(ButtonCodes.BackUp);
                    stopRepeat = true;
                }
            }

            return false;
        }
        #endregion

        #region Dtor
        public void Dispose()
        {
            intervalTimer.Stop();
            intervalTimer.Dispose();

            buttonSender.SendNotification("KinEmote v0.3", "Session Ended");
            buttonSender = null;
        }
        #endregion
    }
}
