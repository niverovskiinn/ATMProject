using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfClient.Tools.Managers;
using WpfClient.Tools.Navigation;
using WpfClient.ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public ClientManager Client { get; set; }
        //new ClientManager();


        private void InitializeApplication()
        {
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            //NavigationManager.Instance.Navigate(ViewType.Login);
            NavigationManager.Instance.Navigate(ViewType.Actions);
            //NavigationManager.Instance.Navigate(ViewType.TransferMoney);
            ClientManager.Instance.Initialize();
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
  
    }
}
