﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.Models;
using WpfClient.Tools;
using WpfClient.Tools.Managers;

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

        public string Pin               // DO WE NEED SETTER?
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
            get { return _clearCommand ?? (_clearCommand = new RelayCommand<object>(ClearImplementation)); }
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
            var result = await Task.Run((() =>
            {
                User currentUser;
                try
                {
                    currentUser = ClientManager.
                }
                
            }));

        }

        private async void ClearImplementation(object obj)
        {
            throw new NotImplementedException();
            //TODO clear pin and maybe card number
        }

    }
}
