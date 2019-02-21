using Relish.Models;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientsView : CustomContentPage
	{
        private readonly IngredientManager _ingredientManager;

        public IngredientsView(IngredientManager ingredientManager)
		{
            _ingredientManager = ingredientManager;
            InitializeComponent();
            BindingContext = new IngredientsViewModel(ingredientManager);
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Prevent double clicks by only allowing one popup to display at a time.
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count != 0)
            {
                return;
            }

            var ingredient = (Ingredient)e.Item;
            var ingredientList = ((IngredientsViewModel) BindingContext).IngredientMasterList.ToList();
            
            PopupNavigation.Instance.PushAsync(new IngredientPopup(ingredient, _ingredientManager, ingredientList, false));
        }
    }
}