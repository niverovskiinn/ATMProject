using System;
using WpfClient.Views;

namespace WpfClient.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    ViewsDictionary.Add(viewType, new LoginView());
                    break;
                case ViewType.Actions:
                    ViewsDictionary.Add(viewType, new ActionsView());
                    break;
                case ViewType.History:
                    ViewsDictionary.Add(viewType, new HistoryView());
                    break;
                case ViewType.Deposit:
                    ViewsDictionary.Add(viewType, new DepositView());
                    break;
                case ViewType.ShowAmount:
                    ViewsDictionary.Add(viewType, new ShowAmountView());
                    break;
                case ViewType.TransferMoney:
                    ViewsDictionary.Add(viewType, new TransferMoneyView());
                    break;
                case ViewType.Withdraw:
                    ViewsDictionary.Add(viewType, new WithdrawView());
                    break;
                case ViewType.Freeze:
                    ViewsDictionary.Add(viewType, new FreezeView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }

        //protected override void ReInitializeView(ViewType viewType)
        //{
        //    ViewsDictionary.Values.GetEnumerator().
        //}
    }
}
