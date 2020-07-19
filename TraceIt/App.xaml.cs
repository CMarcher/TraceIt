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
        public static DataService DataService { get; } = new DataService();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg0ODA1QDMxMzgyZTMyMmUzMFlYWEFCOEZOODM4dXB2QmpHMmlVM3VuZi9kdzhXUFB4SlNDVHhwVmtMWkE9");
            InitializeComponent();

            MainPage = new Login();
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
