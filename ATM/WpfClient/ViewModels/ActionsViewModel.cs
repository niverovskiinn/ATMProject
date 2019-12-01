using System.Windows;
using System.Windows.Input;
using WpfClient.Tools;
using WpfClient.Tools.Managers;
using WpfClient.Tools.Navigation;
using MessageBox = System.Windows.MessageBox;

namespace WpfClient.ViewModels
{
    internal class ActionsViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands
        private ICommand _showAmountCommand;
        private ICommand _transferMoneyCommand;
        private ICommand _depositCommand;
        private ICommand _withdrawCommand;
        private ICommand _historyCommand;
        private ICommand _freezeCommand;
        private ICommand _logOutCommand;

        #endregion
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand ShowAmountCommand
        {
            get
            {
                return _showAmountCommand ?? (_showAmountCommand =
                           new RelayCommand<object>(o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.ShowAmount);
                           }));
            }
        }

        public ICommand DepositCommand
        {
            get
            {
                return _depositCommand ?? (_depositCommand =
                           new RelayCommand<object>(o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.Deposit);
                           }));
            }
        }

        public ICommand TransferMoneyCommand
        {
            get
            {
                return _transferMoneyCommand ?? (_transferMoneyCommand =
                           new RelayCommand<object>(o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.TransferMoney);
                           }));
            }
        }

        public ICommand WithdrawCommand
        {
            get
            {
                return _withdrawCommand ?? (_withdrawCommand =
                           new RelayCommand<object>(o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.Withdraw);
                           }));
            }
        }

        public ICommand FreezeCommand
        {
            get
            {
                return _freezeCommand ?? (_freezeCommand =
                           new RelayCommand<object>(o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.Freeze);
                           }));
            }
        }


        public ICommand HistoryCommand
        {
            get
            {
                return _historyCommand ?? (_historyCommand =
                           new RelayCommand<object>(o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.History);
                           }));
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ?? (_logOutCommand =
                           new RelayCommand<object>(LogOutImplementation));
            }
        }

        #endregion

        #endregion


        private void LogOutImplementation(object o)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to log out?", "Log out?",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            { 
                StationManager.CurrentUser = null;
                StationManager.Accounts = null;
                NavigationManager.Instance.Navigate(ViewType.Login);
            }
        }

        public ActionsViewModel()
        {
        }
    }
}
