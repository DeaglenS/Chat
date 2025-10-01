namespace Chat.DesktopClient.Views
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void add(string Msg)
        {
            List.Items.Add(Msg);
        }

    }
    public class an
    {
        public void add(string Msg)
        {

        }
    }

}
