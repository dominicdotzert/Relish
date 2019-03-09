using Xamarin.Forms;

namespace Relish.Views.CustomComponents
{
    /// <summary>
    /// Custom ContentPage class for overriding the OnAppearing and OnDisappearing methods
    /// to show and hide the navigation bar. This is needed to smooth the transition from
    /// the main page (without a navigation bar) to its child pages (which *have* navigation bars).
    /// Else transition is jarring.
    /// </summary>
    public class CustomContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
