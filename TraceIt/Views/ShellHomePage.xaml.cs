using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.Views.Charts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellHomePage : GradientShellPage
    {
        public ShellHomePage()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("subjectsPage", typeof(SubjectsPage));
            Routing.RegisterRoute("creditsChartPage", typeof(CreditsChartPage));
            Routing.RegisterRoute("endorsementsChartPage", typeof(CreditsChartPage));
        }
    }
}