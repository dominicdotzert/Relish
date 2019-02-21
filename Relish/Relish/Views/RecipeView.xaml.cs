using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView : CustomContentPage
    {
        public RecipeView()
        {
            InitializeComponent();
        }
    }
}