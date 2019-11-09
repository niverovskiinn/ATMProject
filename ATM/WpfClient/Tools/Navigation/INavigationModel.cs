namespace WpfClient.Tools.Navigation
{
    enum ViewType
    {
        Login,
        Actions,
        Deposit,
        History,
        Withdraw,
        TransferMoney,
        ShowAmount,
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
