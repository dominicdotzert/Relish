using System;
using System.Collections.Generic;
using Relish.Models;
using Relish.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Relish.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientFilterPopup : PopupPage
    {
        public IngredientFilterPopup(List<Ingredient> specifiedIngredients, List<Ingredient> allIngredients, Action<Ingredient> addIngredient)
        {
            InitializeComponent();
            BindingContext = new IngredientFilterPopupViewModel(specifiedIngredients, allIngredients, addIngredient);
        }
    }
}