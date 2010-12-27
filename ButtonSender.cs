using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBMC;

namespace KinEmote
{
    public enum ClientType
    {
        XBMC,
        Boxee,
        Custom
    }

    class ButtonSender
    {
        ClientType _clientType;
        EventClient eventClient;

        public bool Connected = false;

        public ButtonSender(ClientType clientType, string ipAddress, int port)
        {
            _clientType = clientType;

            ButtonCodes.SetType(_clientType);

            if (_clientType == ClientType.XBMC)
            {
                eventClient = new EventClient();
                Connected = true;

                eventClient.Connect(ipAddress, port);
                eventClient.SendHelo("KinEmote v0.2 beta");
            }
            else if (_clientType == ClientType.Boxee)
            {
                eventClient = new EventClient();
                Connected = true;

                eventClient.Connect(ipAddress, port);
                eventClient.SendHelo("Kinect Remote v0.01");
            }
            if (_clientType == ClientType.Custom)
            {
            }
        }

        public void SendKey(string key)
        {
            if (_clientType == ClientType.XBMC || _clientType == ClientType.Boxee)
            {
                eventClient.SendButton(key, "KB", ButtonFlagsType.BTN_NO_REPEAT);
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
    }
}
