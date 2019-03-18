using Relish.Data;
using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryListView : CustomContentPage
    {
        public GroceryListView(LocalDataManager localDataManager)
        {
            InitializeComponent();
        }
    }
}