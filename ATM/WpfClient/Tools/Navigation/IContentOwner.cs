using System.Windows.Controls;

namespace WpfClient.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
