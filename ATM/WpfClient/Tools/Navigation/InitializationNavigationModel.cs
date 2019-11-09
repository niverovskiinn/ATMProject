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
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
