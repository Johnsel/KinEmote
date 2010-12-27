using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBMC;
using WindowsInput;

namespace KinEmote
{
    enum ClientType
    {
        XBMC,
        Boxee,
        Custom
    }

    class ButtonSender
    {
        #region Declarations
        private ClientType _clientType;
        private EventClient eventClient;

        public bool Connected = false;
        #endregion

        #region Ctor
        public ButtonSender(ClientType clientType)
        {
            _clientType = clientType;
            ButtonCodes.SetType(_clientType);

            Connected = true;
        }

        public ButtonSender(ClientType clientType, string ipAddress, int port)
        {
            _clientType = clientType;

            ButtonCodes.SetType(_clientType);

            if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
            {
                eventClient = new EventClient();

                eventClient.Connect(ipAddress, port);
            }

            Connected = true;
        }
        #endregion

        #region Methods
        public void SendKey(System.Windows.Forms.Keys key)
        {
            if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
            {
                if (key == System.Windows.Forms.Keys.Back)
                {
                    eventClient.SendButton("backspace", "KB", ButtonFlagsType.BTN_NO_REPEAT);
                }
                else
                {
                    eventClient.SendButton(key.ToString(), "KB", ButtonFlagsType.BTN_NO_REPEAT);
                }
            }
            else
            {
                WindowsInput.InputSimulator.SimulateKeyPress((VirtualKeyCode)key);
            }
        }

        public void SendNotification(string title, string content)
        {
            if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
            {
                eventClient.SendNotification(title, content);
            }
        }

        public void Disconnect()
        {
            if (eventClient.Connected)
            {
                eventClient.SendBye();
                eventClient.Disconnect();
            }

            Connected = false;
        }
        #endregion
    }
}
