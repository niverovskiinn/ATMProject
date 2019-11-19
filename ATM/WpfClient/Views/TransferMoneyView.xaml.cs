using System.Windows.Controls;
using WpfClient.Tools.Navigation;
using WpfClient.ViewModels;

namespace WpfClient.Views
{
    /// <summary>
    /// Interaction logic for TransferMoneyView.xaml
    /// </summary>
    public partial class TransferMoneyView : UserControl, INavigatable
    {
        public TransferMoneyView()
        {
            InitializeComponent();
            DataContext = new TransferMoneyViewModel();
        }
    }
}
