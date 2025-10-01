using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Core;
namespace Chat.DesktopClient2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        ClientWebSocket CLIENT;
        public async Task StartConnection()
        {
            var client = new ClientWebSocket();
            CLIENT = client;
            await client.ConnectAsync(new Uri($"ws://localhost:5000/message"), CancellationToken.None);
            // Console.WriteLine("Connected to server");
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
                var receive = ReciveAsync(client);
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
                MsgList.Items.Add(Encoding.UTF8.GetString(buffer, 0, result.Count));


                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            StartConnection();

        }

        private void SendMsg_Click(object sender, RoutedEventArgs e)
        {
            /*
            var client = CLIENT;
            var send = Task.Run(async () =>
            {
                string message;

                message = MsgBox.Text;

                Message messageObject = new Message
                {
                    Text = message
                };

                var jsonMessage = JsonConvert.SerializeObject(messageObject);
                var bytes = Encoding.UTF8.GetBytes(jsonMessage);
                //client = new ClientWebSocket();
                //await client.ConnectAsync(new Uri($"ws://localhost:5000/{_api}"), CancellationToken.None);

                client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            });
            */

            string message;
            var client = CLIENT;
            message = MsgBox.Text;

            Message messageObject = new Message
            {
                Text = message
            };

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}

