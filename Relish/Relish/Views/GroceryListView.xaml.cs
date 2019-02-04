using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroceryListView : CustomContentPage
	{
		public GroceryListView ()
		{
			InitializeComponent ();
		}
	}
}