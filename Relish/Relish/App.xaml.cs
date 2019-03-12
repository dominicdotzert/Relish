using Relish.Models;
using Relish.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Relish
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var localDataManager = new LocalDataManager();
            MainPage = new NavigationPage(new MainPage(localDataManager));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
