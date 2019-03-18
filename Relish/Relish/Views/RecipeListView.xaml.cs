using Relish.Data;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListView : CustomContentPage
    {
        public RecipeListView(SearchQuery query)
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel(query, Navigation);
        }
    }
}