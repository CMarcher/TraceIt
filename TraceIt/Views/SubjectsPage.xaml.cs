using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectsPage : BasePage
    {
        public SubjectsPage()
        {
            InitializeComponent();
        }

        private async void buttonViewInfo_Clicked(object sender, EventArgs e)
        {
            var subject = (sender as Button)?.BindingContext as Subject;
            StatusTracker.CurrentSubject = subject;

            await Navigation.PushAsync(new SelectedStandardsPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Initialised is false)
                await SetContext();

            loadingView.IsVisible = false;
            loadingView.IsRunning = false;
        }

        async Task SetContext()
        {
            BindingContext = await SubjectsPageViewModel.InitialiseAsync();
            Initialised = true;
        }
    }
}