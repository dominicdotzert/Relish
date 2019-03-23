using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Data;
using Relish.Models;
using Relish.Views;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    public class RecipeListViewModel : NotifyPropertyChanged
    {
        private readonly LocalDataManager _localDataManager;
        private readonly INavigation _navigation;

        private bool _loadError;
        private bool _searchComplete;
        private bool _searchHasResults;

        public RecipeListViewModel(Task<List<Recipe>> loadTask, LocalDataManager localDataManager, string noResultsString, INavigation navigation)
        {
            _localDataManager = localDataManager;
            _navigation = navigation;

            NoResultsString = noResultsString;
            OpenRecipeCommand = new Command(OpenRecipe);
            
            Task.Run(async () =>
            {
                try
                {
                    var result = await loadTask;
                    RecipeResults = new ObservableCollection<Recipe>(result);
                    SearchHasResults = RecipeResults.Count != 0;
                }
                catch
                {
                    LoadError = true;
                    RecipeResults = new ObservableCollection<Recipe>();
                    return;
                }

                SearchComplete = true;
                OnPropertyChanged(nameof(RecipeResults));
            });
        }

        public ICommand OpenRecipeCommand { get; }

        public bool LoadError
        {
            get => _loadError;

            set
            {
                if (_loadError != value)
                {
                    _loadError = value;
                    OnPropertyChanged(nameof(LoadError));
                }
            }
        }

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

        public bool SearchHasResults
        {
            get => _searchHasResults;

            set
            {
                if (_searchHasResults != value)
                {
                    _searchHasResults = value;
                    OnPropertyChanged(nameof(SearchHasResults));
                }
            }
        }

        public string NoResultsString { get; }

        public ObservableCollection<Recipe> RecipeResults { get; private set; }

        private void OpenRecipe(object recipeObject)
        {
            var stack = _navigation.NavigationStack;
            if (stack.Count != 0 && stack[stack.Count - 1].GetType() == typeof(RecipeView))
            {
                return;
            }

            var recipe = (Recipe)recipeObject;
            _navigation.PushAsync(new RecipeView(recipe, _localDataManager));
        }
    }
}
