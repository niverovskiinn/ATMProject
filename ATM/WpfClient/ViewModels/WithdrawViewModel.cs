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
        private ObservableCollection<Account> _accountsToWithdraw;
        private Account _selectedAccountToWithdraw;
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

        public ObservableCollection<Account> AccountsToWithdraw
        {
            get { return _accountsToWithdraw; }
            set
            {
                _accountsToWithdraw = value;
                OnPropertyChanged();
            }
        }

        public Account SelectedAccountToWithdraw
        {
            get { return _selectedAccountToWithdraw; }
            set
            {                   //TODO clear all digits of account id except few of them, using one more method
                                //TODO make cases by type of account, COMMISION
                _selectedAccountToWithdraw = value;
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
                WithdrawAmount = "0";
                return _backCommand ?? (_backCommand =
                           new RelayCommand<object>(BackImplementation));
            }
        }

        public ICommand EnterCommand
        {
            get
            {
                return _enterCommand ?? (_enterCommand =
                           new RelayCommand<object>(DepositCashImplementation, CanEnterExecute));
            }
        }

        #endregion

        #endregion

        private bool CanEnterExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_withdrawAmount) && !String.Equals("0", _withdrawAmount);
        }


        private void BackImplementation(object o)
        {
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private void DepositCashImplementation(object o)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Amount to deposit: " + WithdrawAmount);
        }

        //TEST
        public WithdrawViewModel()
        {
            this._accountsToWithdraw = new ObservableCollection<Account>()
            {
                new Account(1,0,3m,DateTime.Now,0,"ab", ""),
                new Account(2,0,3m,DateTime.Now,0,"bc", ""),
                new Account(3,0,3m,DateTime.Now,0,"cd","")
            };
            this.SelectedAccountToWithdraw = AccountsToWithdraw[0];
        }
    }
}
