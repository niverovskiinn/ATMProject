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
    class WithdrawViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;
        private string _withdrawAmount = "";

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands

        private ICommand _backCommand;
        private ICommand _enterCommand;

        #endregion
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>

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

        public string WithdrawAmount
        {
            get { return _withdrawAmount; }
            set
            {
                _withdrawAmount = value;
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

        #region Commands

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand =
                           new RelayCommand<object>(BackImplementation));
            }
        }

        public ICommand EnterCommand
        {
            get
            {
                return _enterCommand ?? (_enterCommand =
                           new RelayCommand<object>(WithdrawCashImplementation, CanEnterExecute));
            }
        }

        #endregion

        #endregion

        private bool CanEnterExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_withdrawAmount) && !String.Equals("0", _withdrawAmount)
                && (SelectedAccount != null) && (SelectedAccount.StatusId == 1);
        }


        private void BackImplementation(object o)
        {
            WithdrawAmount = "0";
            SelectedAccount = null;
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private async void WithdrawCashImplementation(object o)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                bool res = false;
                try
                {
                    res = ClientManager.Instance.WithdrawFromAccount(SelectedAccount.Id, Convert.ToDecimal(WithdrawAmount.Trim()));
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Operation failed.\nReason:{Environment.NewLine}{e.Message}");
                    return false;
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                MessageBox.Show("Transaction has been done successfully!");
            }

            WithdrawAmount = "0";
            NavigationManager.Instance.Navigate(ViewType.Actions);

        }


        private async void Initialize()
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(async () =>
            {
                try
                {
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

        public WithdrawViewModel()
        {
            WithdrawAmount = "0";
            Initialize();
        }
    }
}
