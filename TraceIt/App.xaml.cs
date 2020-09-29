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
        public static NavigationService NavigationService { get; private set; }
        public static DataService DataService { get; private set; }
        public static MessagingService MessagingService { get; private set; }
        public static DataRepository DataRepository { get; private set; }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg0ODA1QDMxMzgyZTMyMmUzMFlYWEFCOEZOODM4dXB2QmpHMmlVM3VuZi9kdzhXUFB4SlNDVHhwVmtMWkE9");
            InitializeComponent();

            MainPage = new LoginPage();

            DataService = new DataService();
            DataRepository = new DataRepository();
            MessagingService = new MessagingService();
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
