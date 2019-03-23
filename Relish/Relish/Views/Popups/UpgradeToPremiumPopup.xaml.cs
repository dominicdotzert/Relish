using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Relish.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdgradeToPremiumPopup : PopupPage
	{
		public UpdgradeToPremiumPopup ()
		{
			InitializeComponent ();
		}

        private async void CancelButtonClicked(object sender, System.EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Count != 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}