using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        internal static User CurrentUser { get; set; }

        internal static List<Account> Accounts { get; set; }

        internal static void ReinitializeAccounts()
        {
            try
            {
                Accounts = ClientManager.Instance.GetAccountsByPassport(CurrentUser.Passport);
            }
            catch (Exception e)
            {
                MessageBox.Show("Couldn't get accounts info properly." +
                                $"\nReason: {e.Message}");
                throw;
            }
        }

        internal static void CloseApp()
        {
            //MessageBox.Show("Shutting Down");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
