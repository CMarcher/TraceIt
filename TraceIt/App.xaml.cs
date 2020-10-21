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
        public static NavigationService NavigationService { get; private set; } = new NavigationService();
        public static DataService DataService { get; private set; } = new DataService();
        public static MessagingService MessagingService { get; private set; } = new MessagingService();
        public static DataRepository DataRepository { get; private set; } = new DataRepository();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzMxOTk3QDMxMzgyZTMzMmUzME1PM1U5eks0N1FEMDZiMFdaRlZjdnl0NUVESVk5a2lmdkFNa0I1WXlEbWs9");
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected async override void OnStart()
        {
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
