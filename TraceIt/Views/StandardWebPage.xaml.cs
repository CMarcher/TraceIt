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
        string CurrentSource { get; set; }

        public StandardWebPage(AssessmentStandard standard)
        {
            InitializeComponent();
            webView.BindingContext = standard;
            if (Device.RuntimePlatform == Device.iOS)
                webView.Source = standard.Hyperlink;
            else if (Device.RuntimePlatform == Device.Android)
                webView.Source = "https://docs.google.com/viewer?url=" + standard.Hyperlink;

            SetToolbarEnabling();
        }

        void SetToolbarEnabling()
        {
            backToolbarItem.IsEnabled = webView.CanGoBack;
            forwardToolbarItem.IsEnabled = webView.CanGoForward;
        }

        private void loadNZQAToolbarItem_Clicked(object sender, EventArgs e)
        {
            webView.Source = "https://www.nzqa.govt.nz/ncea/subjects/";
        }

        private async void launchSafariToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool launchWeb = await DisplayAlert("Leaving TraceIt", "This will launch the current page outside the app. Continue?",
                "Sure!", "Never mind");

            if (launchWeb)
                await Launcher.OpenAsync(CurrentSource);
        }

        private void backToolbarItem_Clicked(object sender, EventArgs e)
        {
            webView.GoBack();
        }

        private void forwardToolbarItem_Clicked(object sender, EventArgs e)
        {
            webView.GoForward();
        }

        private void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            CurrentSource = e.Url;
            SetToolbarEnabling();
        }

        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            SetToolbarEnabling();
        }
    }
}