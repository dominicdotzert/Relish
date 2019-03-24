using System.Collections.Generic;
using System.Threading.Tasks;
using Relish.Data;
using Relish.Models;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListView : CustomContentPage
    {
        public RecipeListView(
            Task<List<Recipe>> loadTask,
            LocalDataManager localDataManager,
            string titleString,
            string noResultsString,
            bool containsSavedItems = false)
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel(loadTask, localDataManager, Navigation, titleString, noResultsString, containsSavedItems);
        }
    }
}