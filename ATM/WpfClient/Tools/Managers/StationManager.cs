using System;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        internal static User CurrentUser { get; set; }


        //private static IDataStorage _dataStorage;

        //internal static IDataStorage DataStorage
        //{
        //    get { return _dataStorage; }
        //}

        //internal static void Initialize(IDataStorage dataStorage)
        // {
        //   _dataStorage = dataStorage;
        //}

        internal static void CloseApp()
        {
            MessageBox.Show("Shutting Down");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
