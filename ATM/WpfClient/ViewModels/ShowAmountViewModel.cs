using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                AmountInfo = $"Account number: {_selectedAccount.Id}\nAccount type: {_selectedAccount.TypeId}\n Amount of money: ";
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

        #endregion

        #endregion


        private void BackImplementation(object o)
        {
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        //TEST
        public ShowAmountViewModel()
        {
            this._accounts = new ObservableCollection<Account>()
            {
                new Account(1,0,3m,DateTime.Now,0,"ab",""),
                new Account(2,0,3m,DateTime.Now,0,"bc",""),
                new Account(3,0,3m,DateTime.Now,0,"cd","")
            };
            this.SelectedAccount = Accounts[0];
        }
    }
}
