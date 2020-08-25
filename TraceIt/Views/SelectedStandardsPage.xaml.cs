﻿using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardSelectionPage : ContentPage
    {
        public SelectedStandardsPageViewModel ViewModel = new SelectedStandardsPageViewModel();

        public StandardSelectionPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
            Title = StatusTracker.CurrentSubject.Name;
        }

        private void buttonViewMore_Clicked(object sender, EventArgs e)
        {
            var standard = (sender as SfButton)?.BindingContext as AssessmentStandard;
            Navigation.PushAsync(new StandardDetailPage(standard));
        }

        private void buttonAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new StandardCategorisedPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}