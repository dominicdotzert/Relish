using Relish.Data;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientsView : CustomContentPage
    {
        public IngredientsView(LocalDataManager localDataManager)
        {
            InitializeComponent();
            BindingContext = new IngredientsViewModel(localDataManager);
        }
    }
}