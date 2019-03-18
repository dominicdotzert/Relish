using System;
using System.Threading.Tasks;
using Relish.Data;
using Relish.Models;
using Relish.ViewModels;
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

        private void RecipeButton_OnClicked(object sender, EventArgs e)
        {
        }

        private async void MealPlanButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new MealPlanView());
        }

        private async void GroceryListButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new GroceryListView(_localDataManager));
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
    }
}
