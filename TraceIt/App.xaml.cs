using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
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
        public static DataService DataService { get; private set; } = new DataService();
        public static MessagingService MessagingService { get; private set; } = new MessagingService();
        public static DataRepository DataRepository { get; private set; } = new DataRepository();
        public static UserManagerService UserManagerService { get; private set; } = new UserManagerService();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzMxOTk3QDMxMzgyZTMzMmUzME1PM1U5eks0N1FEMDZiMFdaRlZjdnl0NUVESVk5a2lmdkFNa0I1WXlEbWs9");
            InitializeComponent();

            LaunchPage();
        }

        private void StartAppCenter()
        {
            string iOSKey = "ios=63e9764e-1878-44f1-9d77-8e672067b549;";
            string androidKey = "android=e8acd4eb-f157-430e-a9c1-c7fb04dc78d4;";
            AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start(iOSKey + androidKey, typeof(Analytics), typeof(Crashes));
        }

        private void LaunchPage()
        {
            if (UserManagerService.LoggedIn)
                MainPage = new ShellHomePage();
            else
                MainPage = new LoginPage();
        }

        protected async override void OnStart()
        {
            StartAppCenter();
            await Task.Run(DataRepository.InitialiseAsync);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
