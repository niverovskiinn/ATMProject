using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClient.Models;
using WpfClient.Tools;
using WpfClient.Tools.Managers;
using WpfClient.Tools.Navigation;

namespace WpfClient.ViewModels
{
    internal class ShowAmountViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;
        private string _amountInfo;

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands

        private ICommand _backCommand;
        private ICommand _showCommand;

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
                                //TODO make cases by type of account, which info to show
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        public string AmountInfo
        {
            get { return _amountInfo; }
            set
            {
                _amountInfo = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility;}
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get { return _isControlEnabled;}
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

        public ICommand ShowCommand
        {
            get
            {
                return _showCommand ?? (_backCommand =
                           new RelayCommand<object>(o =>
                           {
                               AmountInfo = $"Account number: {SelectedAccount.Id}\n" +
                                            $"Account type: {SelectedAccount.TypeId}\n" +
                                            $" Amount of money: {SelectedAccount.AmountMoney} UAH";
                           }));
            }
        }
        #endregion

        #endregion


        private void BackImplementation(object o)
        {
            AmountInfo = "";
            SelectedAccount = null;
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
        public ShowAmountViewModel()
        {
            Initialize();
        }
    }
}
