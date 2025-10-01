namespace Chat.DesktopClient.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Services;
    using Chat.DesktopClient.Views;
    using System.Media;

    public class MainWindowViewModel : BindableBase
    {
        private readonly MessageService _messageService;

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand SendMessageCommand { get; private set; }

        public MainWindowViewModel()
        {
            _messageService = new MessageService();
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _messageService.SendMessage(_message);
        }
    }

    class ad
    {
        public void getMsg2(string a)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).List.Items.Add(a);
            //MainWindow avv = null;
            //  avv.List.Items.Add(a);
        }
    }
}
