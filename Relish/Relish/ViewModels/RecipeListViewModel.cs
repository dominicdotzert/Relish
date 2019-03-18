using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Database;
using Relish.Models;
using Relish.Views;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    public class RecipeListViewModel : NotifyPropertyChanged
    {
        private readonly INavigation _navigation;

        private bool _searchComplete;

        public RecipeListViewModel(SearchQuery query, INavigation navigation)
        {
            _navigation = navigation;

            OpenRecipeCommand = new Command(OpenRecipe);

            Task.Run(async () =>
            {
                var result = await query.StartSearch();

                SearchComplete = true;
                RecipeResults = new ObservableCollection<Recipe>(result);
                OnPropertyChanged(nameof(RecipeResults));
            });
        }

        public ICommand OpenRecipeCommand { get; }

        public bool SearchComplete
        {
            get => _searchComplete;

            set
            {
                if (_searchComplete != value)
                {
                    _searchComplete = value;
                    OnPropertyChanged(nameof(SearchComplete));
                } 
            }
        }

        public ObservableCollection<Recipe> RecipeResults { get; private set; }

        private void OpenRecipe(object recipeObject)
        {
            if (_navigation.NavigationStack[_navigation.NavigationStack.Count - 1].GetType() == typeof(RecipeView))
            {
                return;
            }

            var recipe = (Recipe)recipeObject;
            _navigation.PushAsync(new RecipeView(recipe));
        }
    }
}
