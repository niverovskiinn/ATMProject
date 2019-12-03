using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Tools.Managers
{
    public class AccountsManager
    {
        private static readonly object Locker = new object();
        private static AccountsManager _instance;

        public ObservableCollection<Account> Accs { get; set; }

        internal static AccountsManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    return _instance ?? (_instance = new AccountsManager());
                }
            }
        }

        private AccountsManager()
        {
        }

        internal void Initialize()
        {
            Accs = new ObservableCollection<Account>();
        }

        internal async Task ReInitialize()
        {
            LoaderManager.Instance.ShowLoader();
            List<Account> tmp = null;
            var result = await Task.Run(() =>
            {
                try
                {
                    tmp = ClientManager.Instance.GetAccountsByPassport(StationManager.CurrentUser.Passport);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to get info about accounts." +
                                    $"\nReason:{Environment.NewLine}{e.Message}");
                    return false;
                }
                return true;
            });

            if (result)
            {
                if (tmp != null)
                {
                    Accs.Clear();
                    foreach (var acc in tmp)
                    {
                        Accs.Add(acc);
                    }
                    //Accounts = new ObservableCollection<Account>(tmp);
                }
            }
            LoaderManager.Instance.HideLoader();
        }
    }

}
