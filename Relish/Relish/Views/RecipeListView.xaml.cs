using Relish.Database;
using Relish.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeListView : ContentPage
	{
		public RecipeListView(SearchQuery query)
		{
			InitializeComponent();
            BindingContext = new RecipeListViewModel(query, Navigation);
        }
	}
}