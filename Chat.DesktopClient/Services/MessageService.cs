using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using Core;
using Chat.DesktopClient.Managers;
using Newtonsoft.Json;
using Chat.DesktopClient.ViewModels;
namespace Chat.DesktopClient.Services
{
    class MessageService
    {
        private const string API = "message";

        private readonly ConnectionManager _connectionManager;

        public MessageService()
        {
            _connectionManager = new ConnectionManager(API);
            _ = _connectionManager.StartConnection();
        }

        public void SendMessage(string message)
        {
            Message messageObject = new Message
            {
                Text = message
            };

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }


    }
    class ab
    {
        //   ab(string a)
        //    {
        //
        //  }
        public ab(string n)
        {

        }
        public void GetMsg(string message)
        {
            ad a = null;
            a.getMsg2(message);
            //  a.
        }
    }
}
