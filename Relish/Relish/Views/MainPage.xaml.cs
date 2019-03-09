using System;
using System.Threading.Tasks;
using Relish.Models;
using Relish.ViewModels;
using Xamarin.Forms;

namespace Relish.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly IngredientManager _ingredientManager;

        public MainPage(IngredientManager ingredientManager)
        {
            _ingredientManager = ingredientManager;

            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void SearchButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new StartSearchView());
        }

        private async void IngredientsButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new IngredientsView(_ingredientManager));
        }

        private async void RecipeButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new RecipeView());
        }

        private async void MealPlanButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new MealPlanView());
        }

        private async void GroceryListButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new GroceryListView());
        }

        // Prevent double clicks of button
        private async Task NewPage(Page page)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != page.GetType())
            {
                await Navigation.PushAsync(page);
            }
        }
    }
}
