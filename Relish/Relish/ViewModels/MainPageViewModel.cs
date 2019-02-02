using System;
using System.Collections.Generic;
using System.Text;

namespace Relish.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            // construct stuff
            //SearchButtonClicked = GoToSearchPage;
        }

        // TODO Find relay command and MVVM stuff later
        //RelayCommand SearchButtonClicked { get; }

        private void GoToSearchPage(object sender, EventArgs e)
        {
            // do stuff
        }
    }
}
