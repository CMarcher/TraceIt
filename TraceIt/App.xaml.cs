using System;
using TraceIt.Services;
using TraceIt.Utilities;
using TraceIt.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; } = new NavigationService();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjY0ODgzQDMxMzgyZTMxMmUzMERoZVZvNG82NEcrOWphQnVaYmJ4c3pSMWU5NDhRaGdyM00rZHFOSWwxbDg9");
            InitializeComponent();

            NavigationService.Configure(ViewNames.SubjectSelectionPage, typeof(SubjectSelectionPage));
            NavigationService.Configure(ViewNames.SubjectsPage, typeof(SubjectsPage));

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
