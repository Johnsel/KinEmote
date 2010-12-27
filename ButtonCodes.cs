using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinEmote
{
    static class ButtonCodes
    {
        private static ClientType _clientType;

        static public string Up
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "up";
                }
                else
                {
                    return null;
                }
            }
        }

        static public string Down
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "down";
                }
                else
                {
                    return null;
                }
            }
        }
        
        static public string Left
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "left";
                }
                else
                {
                    return null;
                }
            }
        }
        static public string Right
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "right";
                }
                else
                {
                    return null;
                }
            }
        }
        
        static public string Back
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "backspace";
                }
                else
                {
                    return null;
                }
            }
        }
        static public string Menu
        {
            get
            {
                if (_clientType == ClientType.XBMC)
                {
                    return "c";
                }
                else if (_clientType == ClientType.Boxee)
                {
                    return "i";
                }
                else
                {
                    return null;
                }
            }
        }

        static public string Enter
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "return";
                }
                else
                {
                    return null;
                }
            }
        }

        static public string PlayPause
        {
            get
            {
                if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
                {
                    return "space";
                }
                else
                {
                    return null;
                }
            }
        }

        static public void SetType(ClientType clientType)
        {
            _clientType = clientType;
        }
    }
}
