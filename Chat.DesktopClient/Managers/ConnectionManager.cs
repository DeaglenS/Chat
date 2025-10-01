using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;

namespace Chat.DesktopClient.Managers
{
    using Chat.DesktopClient.Services;
    using Chat.DesktopClient.Views;
    using System.Text;

    class ConnectionManager
    {
        private readonly string _api;

        public ClientWebSocket Client { get; private set; }

        public ConnectionManager(string api)
        {
            _api = api;
        }

        public async Task StartConnection()
        {
            Client = new ClientWebSocket();
            await Client.ConnectAsync(new Uri($"ws://localhost:5000/{_api}"), CancellationToken.None);
            while (1 == 1)
            {
                /*
                var send = Task.Run(async () =>
                {
                    string message;

                    while ((message = Console.ReadLine()) != null && message != string.Empty)
                    {
                        Message messageObject = new Message
                        {
                            Text = message
                        };

                        var jsonMessage = JsonConvert.SerializeObject(messageObject);
                        var bytes = Encoding.UTF8.GetBytes(jsonMessage);
                        //client = new ClientWebSocket();
                        //await client.ConnectAsync(new Uri($"ws://localhost:5000/{_api}"), CancellationToken.None);

                        client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                });
                */
                var receive = ReciveAsync(Client);
                await Task.WhenAll(receive);
            }


        }


        private async Task ReciveAsync(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];

            while (true)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                //Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
                // MsgList.Items.Add(Encoding.UTF8.GetString(buffer, 0, result.Count));
                //string n = "2";
                // ab a(n);
                //  a.GetMsg(Encoding.UTF8.GetString(buffer, 0, result.Count));
                ((MainWindow)System.Windows.Application.Current.MainWindow).List.Items.Add(Encoding.UTF8.GetString(buffer, 0, result.Count));

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }

            }
        }
    }
}
