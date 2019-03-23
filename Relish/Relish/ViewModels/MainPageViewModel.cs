using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Data;
using Relish.Resources;
using Relish.Views;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    /// <summary>
    /// ViewModel class to represent the main Relish screen.
    /// </summary>
    public class MainPageViewModel
    {
        private readonly LocalDataManager _localDataManager;
        private readonly INavigation _navigation;

        public MainPageViewModel(LocalDataManager localDataManager, INavigation navigation)
        {
            _localDataManager = localDataManager;
            _navigation = navigation;

            FindRecipesCommand = new Command(OpenSearchPage);
            IngredientsCommand = new Command(OpenIngredientsPage);
            RecipeBookCommand = new Command(OpenRecipeBookPage);
            PremiumPopupCommand = new Command(OpenUpgradeToPremiumPopup);
        }

        /// <summary>
        /// Command for opening Search page.
        /// </summary>
        public ICommand FindRecipesCommand { get; }

        /// <summary>
        /// Command for opening Ingredients page.
        /// </summary>
        public ICommand IngredientsCommand { get; }

        /// <summary>
        /// Command for opening Recipe Book page.
        /// </summary>
        public ICommand RecipeBookCommand { get; }

        /// <summary>
        /// Command for displaying the Upgrade to Premium popup.
        /// </summary>
        public ICommand PremiumPopupCommand { get; }

        /// <summary>
        /// Opens StartSearchView page.
        /// </summary>
        private async void OpenSearchPage()
        {
            await NewPage(new StartSearchView(_localDataManager));
        }

        /// <summary>
        /// Opens Ingredients page.
        /// </summary>
        private async void OpenIngredientsPage()
        {
            await NewPage(new IngredientsView(_localDataManager));
        }

        /// <summary>
        /// Opens Recipe Book page.
        /// </summary>
        private async void OpenRecipeBookPage()
        {
            await NewPage(
                new RecipeListView(
                    _localDataManager.GetRecipes(),
                    _localDataManager,
                    Strings.Title_RecipeBook,
                    Strings.RecipeBook_NoSavedRecipes));
        }

        /// <summary>
        /// Displays Upgrade to Premium popup.
        /// </summary>
        private async void OpenUpgradeToPremiumPopup()
        {
            await NewPopup(new UpgradeToPremiumPopup());
        }

        /// <summary>
        /// Adds new page to the Navigation stack and prevents multiple button presses.
        /// </summary>
        /// <param name="page">The page to added to the Navigation stack.</param>
        /// <returns>The NewPage task.</returns>
        private async Task NewPage(Page page)
        {
            var stack = _navigation.NavigationStack;
            if (stack.Count == 1)
            {
                await _navigation.PushAsync(page);
            }
        }

        /// <summary>
        /// Adds a new popup page to the PopupNavigation stack and prevents multiple button presses.
        /// </summary>
        /// <param name="popup"></param>
        /// <returns>The NewPopup task.</returns>
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
