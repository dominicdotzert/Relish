﻿using Relish.Views.CustomComponents;
using Xamarin.Forms.Xaml;

namespace Relish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealPlanView : CustomContentPage
    {
        public MealPlanView()
        {
            InitializeComponent();
        }
    }
}