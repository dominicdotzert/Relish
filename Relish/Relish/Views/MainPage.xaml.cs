using Relish.Models;
using Relish.ViewModels;
using System;
using System.Threading.Tasks;
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

        async void SearchButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new StartSearchView());
        }

        async void IngredientsButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new IngredientsView(_ingredientManager));
        }

        async void RecipeButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new RecipeView());
        }

        async void MealPlanButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new MealPlanView());
        }

        async void GroceryListButton_OnClicked(object sender, EventArgs e)
        {
            await NewPage(new GroceryListView());
        }

        // Prevent double clicks of button
        async Task NewPage(Page page)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != page.GetType())
            {
                await Navigation.PushAsync(page);
            }
        }
    }
}
