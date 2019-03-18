using System.Collections.Generic;
using Relish.Data;
using Relish.Models;
using Relish.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Relish.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientPopup : PopupPage
    {
        public IngredientPopup(Ingredient ingredient, LocalDataManager localDataManager, List<IngredientList> ingredientList, bool newIngredient)
        {
            InitializeComponent();
            BindingContext = new IngredientPopupViewModel(ingredient, localDataManager, ingredientList, newIngredient);
        }
    }
}