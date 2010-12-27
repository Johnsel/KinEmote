using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBMC;

namespace KinEmote
{
    partial class Main
    {
        #region Declarations
        bool stopRepeat = false;
        #endregion

        #region EventHandling
        void handlebackPlane()
        {
            if (currentPoint.X < 3)
            {
                if (!stopRepeat)
                {
                    buttonSender.SendKey(ButtonCodes.Back);
                    //eventClient.SendButton("backspace", "KB", ButtonFlagsType.BTN_NO_REPEAT);
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
                    buttonSender.SendKey(ButtonCodes.PlayPause);
                    //eventClient.SendButton("pause", "R1", ButtonFlagsType.BTN_NO_REPEAT);
                    stopRepeat = true;
                }
            }

            if (currentPoint.Y < 1)
            {
                hand.sessionManager.EndSession();
                intervalTimer.Stop();
                hand.sessionManager_SessionEnded(this, null);
            }

            if (currentPoint.Y > 4)
            {
                if (!stopRepeat)
                {
                    buttonSender.SendKey(ButtonCodes.Menu);
                    //eventClient.SendButton("c", "KB", ButtonFlagsType.BTN_NO_REPEAT);
                    stopRepeat = true;
                }
            }
        }
        #endregion
    }
}
