using System;
using TraceIt.Models;
using TraceIt.Services;
using TraceIt.Utilities;
using TraceIt.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("primary-icons.ttf", Alias = "PrimaryIcons")]
namespace TraceIt
{

    public partial class App : Application
    {
        public static NavigationService NavigationService { get; } = new NavigationService();
        public static DataService DataService { get; } = new DataService();
        public static MessagingService MessagingService { get; } = new MessagingService();
        public static DataRepository DataRepository { get; } = new DataRepository();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg0ODA1QDMxMzgyZTMyMmUzMFlYWEFCOEZOODM4dXB2QmpHMmlVM3VuZi9kdzhXUFB4SlNDVHhwVmtMWkE9");
            InitializeComponent();

            MainPage = new LoginPage();
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
