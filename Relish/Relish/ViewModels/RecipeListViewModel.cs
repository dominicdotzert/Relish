using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Relish.Database;
using Relish.Models;

namespace Relish.ViewModels
{
    public class RecipeListViewModel : NotifyPropertyChanged
    {
        private bool _searchComplete;

        public RecipeListViewModel(SearchQuery query)
        {
            Task.Run(async () =>
            {
                var result = await query.StartSearch();

                SearchComplete = true;
                RecipeResults = new ObservableCollection<Recipe>(result);
                OnPropertyChanged(nameof(RecipeResults));
            });
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

        public ObservableCollection<Recipe> RecipeResults { get; private set; }
    }
}
