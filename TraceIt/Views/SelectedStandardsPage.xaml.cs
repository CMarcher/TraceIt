using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedStandardsPage : BasePage
    {
        public SelectedStandardsPageViewModel ViewModel { get; private set; } = new SelectedStandardsPageViewModel();

        public SelectedStandardsPage()
        {
            InitializeComponent();
            Title = StatusTracker.CurrentSubject.BaseSubject.Name;
        }

        private async void buttonViewMore_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var standard = button?.BindingContext as Standard;

            button.SetEnabledForAndroid(false);
            await Navigation.PushAsync(new StandardDetailPage(standard));
            button.SetEnabledForAndroid(true);
        }

        private async void buttonAdd_Clicked(object sender, EventArgs e)
        {
            var button = sender as VisualElement;

            button.SetEnabledForAndroid(false);
            await Navigation.PushModalAsync(new NavigationPage(new StandardCategorisedPage()));
            button.SetEnabledForAndroid(true);
        }

        private async void selectGradeButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var standard = button?.BindingContext as Standard;
            StatusTracker.CurrentStandard = standard;

            button.SetEnabledForAndroid(false);
            await Navigation.PushModalAsync(new NavigationPage(new GradeSelectionPage()));
            button.SetEnabledForAndroid(true);
        }
    }
}