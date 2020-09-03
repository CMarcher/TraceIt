using Syncfusion.XForms.Buttons;
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
        public SelectedStandardsPageViewModel ViewModel { get; private set; } = new SelectedStandardsPageViewModel();

        public StandardSelectionPage()
        {
            InitializeComponent();
            Title = StatusTracker.CurrentSubject.Name;
        }

        private async void buttonViewMore_Clicked(object sender, EventArgs e)
        {
            var standard = (sender as SfButton)?.BindingContext as Standard;
            await Navigation.PushAsync(new StandardDetailPage(standard));
        }

        private async void buttonAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new StandardCategorisedPage()));
        }

        private async void selectGradeButton_Clicked(object sender, EventArgs e)
        {
            var standard = (sender as Button)?.BindingContext as Standard;
            StatusTracker.CurrentStandard = standard;

            await Navigation.PushModalAsync(new NavigationPage(new GradeSelectionPage()));
        }

        /// <summary>
        /// Used only for debugging class variables at a point in time.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


    }
}