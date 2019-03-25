using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Relish.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpgradeToPremiumPopup : PopupPage
    {
        public UpgradeToPremiumPopup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the popup and prevents double clicks.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">EventArgs.</param>
        private async void CancelButtonClicked(object sender, System.EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Count != 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}