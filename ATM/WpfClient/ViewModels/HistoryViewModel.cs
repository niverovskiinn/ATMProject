using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClient.Models;
using WpfClient.Tools;
using WpfClient.Tools.Managers;
using WpfClient.Tools.Navigation;

namespace WpfClient.ViewModels
{
    internal class HistoryViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;

        private ObservableCollection<Transaction> _transactions;
        private DateTime _fromDate;
        private DateTime _toDate;

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands
        private ICommand _showCommand;
        private ICommand _backCommand;
        #endregion

        #endregion

        #region Properties

        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {                   //TODO clear all digits of account id except few of them, using one more method
                //TODO make cases by type of account, COMMISION
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

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



        public DateTime FromDate
        {
            get { return _fromDate;}
            set
            {
                _fromDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            private set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }


        public ICommand ShowCommand
        {
            get
            {
                return _showCommand ?? (_showCommand = new RelayCommand<object>(
                           ShowImplementation,CanShowExecute));
            }

        }

        private async void ShowImplementation(object o)
        {
            LoaderManager.Instance.ShowLoader();
            List<Transaction> tmp = null;
            var result = await Task.Run(async () =>
            {
                try
                {
                    tmp = ClientManager.Instance.GetTransactionsPeriod(SelectedAccount.Id,FromDate,ToDate);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to get transactions." +
                                    $"\nReason:{Environment.NewLine}{e.Message}");
                    return false;
                }
                return true;
            });

            if (result)
            {
                if (tmp != null)
                {
                    foreach (var t in tmp)
                    {
                        Transactions.Add(t);
                    }
                }
            }
            LoaderManager.Instance.HideLoader();
        }

        private bool CanShowExecute(object o)
        {
            return (ToDate.CompareTo(FromDate) > 0) && (SelectedAccount != null) && (SelectedAccount.StatusId == 1);
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand =
                           new RelayCommand<object>(BackImplementation));
            }
        }
        #endregion

        private void BackImplementation(object o)
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private async void Initialize()
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(async () =>
            {
                try
                {
                    //await AccountsManager.Instance.ReInitialize();
                    Accounts = AccountsManager.Instance.Accs;
                    if (Accounts != null)
                    {
                        SelectedAccount = Accounts[0];
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to get info about accounts." +
                                    $"\nReason:{Environment.NewLine}{e.Message}");
                    return false;
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();

            if (!result)
            {
                MessageBox.Show($"Failed to get info about accounts");
                NavigationManager.Instance.Navigate(ViewType.Actions);

            }
        }

        internal HistoryViewModel()
        {
            Initialize();
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            Transactions = new ObservableCollection<Transaction>();
        }

    }
}
