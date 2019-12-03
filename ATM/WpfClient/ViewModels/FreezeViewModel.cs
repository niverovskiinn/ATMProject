using System;
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
    internal class FreezeViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;
        
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands

        private ICommand _backCommand;
        private ICommand _submitCommand;
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

        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand =
                           new RelayCommand<object>(SubmitImplementation, CanFreezeExecute));
            }
        }

        #endregion

        #endregion


        private void BackImplementation(object o)
        {
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private bool CanFreezeExecute(object o)
        {
            return (SelectedAccount != null) && (SelectedAccount.StatusId == StatusType.Active);
        }

        private async void SubmitImplementation(object o)
        {
            //MessageBox.Show($"SenderId: {SelectedAccount.Id}\nRecipientCard: {RecipientCard}\nAmount: {Amount}\nNotes: {Notes}");

            var mesres = MessageBox.Show($"Do you really want to freeze account:" +
                                         $"\n {SelectedAccount.Id}, TypeId: {SelectedAccount.TypeId}?","Freeze?",
                                        MessageBoxButton.YesNo,MessageBoxImage.Question, MessageBoxResult.No);
            if (mesres == MessageBoxResult.Yes)
            { 
                LoaderManager.Instance.ShowLoader();

                var result = await Task.Run(() =>
                {
                    bool res = false;
                    try
                    {
                        res = ClientManager.Instance.FreezeAccountById(SelectedAccount.Id);
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
        }

        private async void Initialize()
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(async () =>
            {
                try
                {
                    //StationManager.ReinitializeAccounts();
                    //Accounts = StationManager.Accounts;
                   // await AccountsManager.Instance.ReInitialize();
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

        internal FreezeViewModel()
        {
            Initialize();
        }
    }
}
