using System;
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
    internal class LoginViewModel:BaseViewModel
    {
        #region Fields

        private string _cardNumber;
        private string _pin;

        #region Commands
        private ICommand _signInCommand;
        private ICommand _clearCommand;
        #endregion
        #endregion


        #region Properties

        public string CardNumber        //DO WE NEED SETTER?
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                OnPropertyChanged();
            }
        }

        public string Pin               
        {
            get { return _pin; }
            set
            {
                _pin = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand =
                           new RelayCommand<object>(SignInImplementation, CanSignInExecute));
            }
        }

        public ICommand ClearCommand
        {
            get { return _clearCommand ?? (_clearCommand = new RelayCommand<object>(o => { Pin = ""; })); }
        }

        #endregion
        #endregion

        private bool CanSignInExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_cardNumber) && !String.IsNullOrWhiteSpace(_pin);
            //@TODO add numeric checking
        }

        private async void SignInImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            User currentUser;
            var result = await Task.Run(() =>
            {
                try
                {
                    currentUser = ClientManager.Instance.GetUserByCredentials(CardNumber, Pin);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Sign in failed. Reason:{Environment.NewLine}{e.Message}");
                    Pin = "";
                    return false;
                }

                if (currentUser != null)
                {
                    StationManager.CurrentUser = currentUser;
                    //StationManager.Accounts = new ObservableCollection<Account>();
                    AccountsManager.Instance.Initialize();
                    ///////////TODO remove
                    MessageBox.Show($"Sign in successful.\nCurrent user: {StationManager.CurrentUser}",
                    "Login successful",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                        );
                    ///////////
                }
                return true;
            });
            Pin = "";
            LoaderManager.Instance.HideLoader();
            
            if (result)
            {
                CardNumber = "";
                NavigationManager.Instance.Navigate(ViewType.Actions);
            }
        }

    }
}
