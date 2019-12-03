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
using MessageBox = System.Windows.MessageBox;

namespace WpfClient.ViewModels
{
    internal class TransferMoneyViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;
        private string _recipientCard;
        private string _amount = "0";
        private string _notes;

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands

        private ICommand _backCommand;
        private ICommand _sendCommand;
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

        public string RecipientCard
        {
            get { return _recipientCard; }
            set
            {
                _recipientCard = value; 
                OnPropertyChanged();
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
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

        public ICommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand =
                           new RelayCommand<object>(SendImplementation, CanSendExecute));
            }
        }

        #endregion

        #endregion


        private void BackImplementation(object o)
        {
            Amount = "0";
            RecipientCard = "";
            SelectedAccount = Accounts?[0];
            Notes = "";
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private bool CanSendExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_amount) && !String.Equals("0", _amount) &&
                   !String.IsNullOrWhiteSpace(_recipientCard);
        }

        private async void SendImplementation(object o) //TODO DO YOU REALLY WANT TO SEND????MB INSERT PASSWORD OF CARD INSERTED TO ATM
        {
            MessageBox.Show($"SenderId: {SelectedAccount.Id}\nRecipientCard: {RecipientCard}\nAmount: {Amount}\nNotes: {Notes}");
            LoaderManager.Instance.ShowLoader();
            
            var result = await Task.Run(() =>
            {
                bool res = false;
                try
                {
                    res = ClientManager.Instance.SendMoneyToCard(SelectedAccount.Id, RecipientCard, Convert.ToDecimal(Amount.Trim()),Notes.Trim());
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
                MessageBox.Show($"Transfer is unsuccessful.\nNot enough money on balance!",
                    "Denied",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        public async void Initialize()
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(async () =>
            {
               try
                {
                    //StationManager.ReinitializeAccounts();
                    //Accounts = StationManager.Accounts;
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
        public TransferMoneyViewModel()
        {
            Initialize();
        }
    }
}
