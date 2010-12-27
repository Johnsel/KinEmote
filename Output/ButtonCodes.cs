using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KinEmote
{
    static class ButtonCodes
    {
        #region Declarations
        private static ClientType _clientType;

        static public Keys MidUp
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Up;
                }
                else
                {
                    return Properties.CustomKeys.Default.MidUp;
                }
            }
        }

        static public Keys MidDown
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Down;
                }
                else
                {
                    return Properties.CustomKeys.Default.MidDown;
                }
            }
        }
        
        static public Keys MidLeft
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Left;
                }
                else
                {
                    return Properties.CustomKeys.Default.MidLeft;
                }
            }
        }

        static public Keys MidRight
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Right;
                }
                else
                {
                    return Properties.CustomKeys.Default.MidRight;
                }
            }
        }
        
        static public Keys BackLeft
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Back;
                }
                else
                {
                    return Properties.CustomKeys.Default.BackLeft;
                }
            }
        }
        static public Keys BackUp
        {
            get
            {
                if (_clientType == ClientType.XBMC)
                {
                    return Keys.C;
                }
                else if (_clientType == ClientType.Boxee)
                {
                    return Keys.I;
                }
                else
                {
                    return Properties.CustomKeys.Default.BackUp;
                }
            }
        }

        static public Keys BackRight
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Space;
                }
                else
                {
                    return Properties.CustomKeys.Default.BackRight;
                }
            }
        }

        static public Keys MidPush
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return Keys.Return;
                }
                else
                {
                    return Properties.CustomKeys.Default.Push;
                }
            }
        }
        #endregion

        #region Methods
        static public void SetType(ClientType clientType)
        {
            _clientType = clientType;
        }
        #endregion
    }
}
