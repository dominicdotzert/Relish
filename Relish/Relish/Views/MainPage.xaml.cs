using Relish.Data;
using Relish.ViewModels;
using Xamarin.Forms;

namespace Relish.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(LocalDataManager localDataManager)
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(localDataManager, Navigation);
        }
    }
}
