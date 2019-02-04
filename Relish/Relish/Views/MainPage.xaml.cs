using System;
using Xamarin.Forms;

namespace Relish.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void SearchButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartSearchView());
        }

        async void IngredientsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IngredientsView());
        }

        async void RecipeButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipeView());
        }

        async void MealPlanButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MealPlanView());
        }

        async void GroceryListButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroceryListView());
        }
    }
}
