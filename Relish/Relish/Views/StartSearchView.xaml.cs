﻿using Relish.Data;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartSearchView : CustomContentPage
    {
        public StartSearchView(LocalDataManager localDataManager)
        {
            InitializeComponent();
            BindingContext = new StartSearchViewModel(localDataManager, Navigation);
        }
    }
}