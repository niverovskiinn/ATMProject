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
        Freeze
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
