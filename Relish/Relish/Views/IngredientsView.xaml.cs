using System.Linq;
using Relish.Models;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
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