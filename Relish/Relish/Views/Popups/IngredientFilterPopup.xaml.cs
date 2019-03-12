using System;
using System.Collections.Generic;
using Relish.Models;
using Relish.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relish.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientFilterPopup : PopupPage
    {
        private readonly Action<Ingredient> _addIngredient;

		public IngredientFilterPopup(List<Ingredient> specifiedIngredients, List<Ingredient> allIngredients, Action<Ingredient> addIngredient)
        {
            _addIngredient = addIngredient;

			InitializeComponent();
            BindingContext = new IngredientFilterPopupViewModel(specifiedIngredients, allIngredients);
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ingredient = (Ingredient)e.Item;
            _addIngredient(ingredient);
            PopupNavigation.Instance.PopAsync();
        }
    }
}