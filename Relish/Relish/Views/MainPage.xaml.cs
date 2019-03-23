using System;
using System.Threading.Tasks;
using Relish.Data;
using Relish.ViewModels;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Relish.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDataManager _localDataManager;

        public MainPage(LocalDataManager localDataManager)
        {
            _localDataManager = localDataManager;

            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void SearchButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new StartSearchView(_localDataManager));
        }

        private async void IngredientsButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new IngredientsView(_localDataManager));
        }

        private async void RecipeButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new RecipeListView(_localDataManager.GetRecipes(), _localDataManager, "No saved recipes"));
        }

        private async void MealPlanButton_OnClicked(object sender, EventArgs e)
        {
            await NewPopup(new UpdgradeToPremiumPopup());
        }

        private async void GroceryListButton_OnClicked(object sender, EventArgs e)
        {
            await NewPopup(new UpdgradeToPremiumPopup());
        }

        // Prevent double clicks of button
        private async Task NewPage(Page page)
        {
            var stack = Navigation.NavigationStack;
            if (stack.Count == 1)
            {
                await Navigation.PushAsync(page);
            }
        }

        private async Task NewPopup(PopupPage popup)
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count == 0 || stack[stack.Count - 1].GetType() != popup.GetType())
            {
                await PopupNavigation.Instance.PushAsync(popup);
            }
        }
    }
}
