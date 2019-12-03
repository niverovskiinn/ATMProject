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
    internal class DepositViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;
        private string _depositAmount;
        private string _accountInfo;

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

        public string AccountInfo
        {
            get { return _accountInfo; }
            set
            {
                _accountInfo = value; 
                OnPropertyChanged();
            }
        }

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {                  
                _selectedAccount = value;
                if (_selectedAccount != null)
                {
                    AccountTypes t = (AccountTypes) SelectedAccount.TypeId;
                    StatusType ts = (StatusType)SelectedAccount.StatusId;

                    AccountInfo = $"Type: {t}\nRemaining: {SelectedAccount.AmountMoney} UAH\nStatus: {ts}"; //якщо що - видалити аккаунтінфо з цього класу,
                                                                              //також лейбл з в'юхи, і поміняти енам в акаунті на інт назад
                }
                else
                {
                    AccountInfo = "";
                }
                OnPropertyChanged();
            }
        }

        public string DepositAmount
        {
            get { return _depositAmount; }
            set
            {
                _depositAmount = value;
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
                           new RelayCommand<object>(DepositCashImplementation,CanDepositExecute));
            }
        }

        #endregion

        #endregion

        private bool CanDepositExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_depositAmount) && !String.Equals("0",_depositAmount)
                && (SelectedAccount != null) && (SelectedAccount.StatusId == StatusType.Active);
        }


        private void BackImplementation(object o)
        {
            DepositAmount = "0";
            SelectedAccount = null;
            AccountInfo = null;
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private async void DepositCashImplementation(object o)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                bool res = false;
                try
                {
                    res = ClientManager.Instance.DepositToAccount(SelectedAccount.Id, Convert.ToDecimal(DepositAmount.Trim()));
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Operation failed.\nReason:{Environment.NewLine}{e.Message}");
                    return false;
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (!result)
            {
                MessageBox.Show($"Transfer is unsuccessful.",
                    "Denied",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            else
            {
                MessageBox.Show("Transaction has been done successfully!");
            }

            DepositAmount = "0";
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

        //TEST
        public DepositViewModel()
        {
            DepositAmount = "0";
            Initialize();
        }
    }
}
