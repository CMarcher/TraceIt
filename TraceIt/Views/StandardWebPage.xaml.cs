using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardWebPage : ContentPage
    {
        public StandardWebPage(AssessmentStandard standard)
        {
            InitializeComponent();
            webView.BindingContext = standard;
            if (Device.RuntimePlatform == Device.iOS)
                webView.Source = standard.Hyperlink;
            else if (Device.RuntimePlatform == Device.Android)
                webView.Source = "https://docs.google.com/viewer?url=" + standard.Hyperlink;
        }

        private async void launchWebToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool launchWeb = await DisplayAlert("Leaving TraceIt", "This will take you to NZQA's website, outside the app. Continue?",
                "Sure!", "Never mind");

            if (launchWeb)
                await Launcher.OpenAsync("https://www.nzqa.govt.nz/ncea/subjects/");
        }
    }
}