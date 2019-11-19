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
        private ObservableCollection<Account> _accountsToDeposit;
        private Account _selectedAccountToDeposit;
        private string _depositAmount;

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

        public ObservableCollection<Account> AccountsToDeposit
        {
            get { return _accountsToDeposit; }
            set
            {
                _accountsToDeposit = value;
                OnPropertyChanged();
            }
        }

        public Account SelectedAccountToDeposit
        {
            get { return _selectedAccountToDeposit; }
            set
            {                   //TODO clear all digits of account id except few of them, using one more method
                                //TODO make cases by type of account, COMMISION
                _selectedAccountToDeposit = value;
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
                           new RelayCommand<object>(DepositCashImplementation));
            }
        }

        #endregion

        #endregion


        private void BackImplementation(object o)
        {
            NavigationManager.Instance.Navigate(ViewType.Actions);
        }

        private void DepositCashImplementation(object o)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Amount to deposit: "+ DepositAmount);
        }

        //TEST
        public DepositViewModel()
        {
            this._accountsToDeposit = new ObservableCollection<Account>()
            {
                new Account(1,0,3m,DateTime.Now,0,"ab", ""),
                new Account(2,0,3m,DateTime.Now,0,"bc", ""),
                new Account(3,0,3m,DateTime.Now,0,"cd","")
            };
            this.SelectedAccountToDeposit = AccountsToDeposit[0];
        }
    }
}
