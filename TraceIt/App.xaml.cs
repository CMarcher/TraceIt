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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@31382e312e30JjoK2h5lbWLizVsj9T5ilsulMKsoSD4VYPGFcva8oiM=");
            InitializeComponent();

            NavigationService.Configure(ViewNames.SubjectSelectionPage, typeof(SubjectSelectionPage));
            NavigationService.Configure(ViewNames.StandardSelectionPage, typeof(StandardSelectionPage));
            MainPage = new NavigationPage(new MainPage());
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
