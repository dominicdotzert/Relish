﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Relish.Data;
using Relish.Models;
using Relish.ViewModels;
using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListView : CustomContentPage
    {
        public RecipeListView(Task<List<Recipe>> loadTask, LocalDataManager localDataManager, string noResultsString)
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel(loadTask, localDataManager, noResultsString, Navigation);
        }
    }
}