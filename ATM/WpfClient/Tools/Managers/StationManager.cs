using System;
using System.Net.Http;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        internal static User CurrentUser { get; set; }

        internal static void CloseApp()
        {
            //MessageBox.Show("Shutting Down");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
