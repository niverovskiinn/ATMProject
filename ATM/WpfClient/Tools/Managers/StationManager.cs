using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        internal static User CurrentUser { get; set; }

        internal static ObservableCollection<Account> Accounts { get; set; }

        internal static async Task ReinitializeAccounts()
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                List<Account> tmp;
                try
                {
                    tmp = ClientManager.Instance.GetAccountsByPassport(CurrentUser.Passport);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to get info about accounts." +
                                    $"\nReason:{Environment.NewLine}{e.Message}");
                    return false;
                }

                if (tmp != null)
                {
                    Accounts.Clear();
                    foreach (var acc in tmp)
                    {
                        Accounts.Add(acc);
                    }
                    //Accounts = new ObservableCollection<Account>(tmp);
                }

                return true;
            });
            LoaderManager.Instance.HideLoader();
        }

        internal static void CloseApp()
        {
            //MessageBox.Show("Shutting Down");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
