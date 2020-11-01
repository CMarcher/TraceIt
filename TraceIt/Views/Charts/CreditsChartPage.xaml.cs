using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views.Charts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditsChartPage : ContentView
    {
        public CreditsChartPage()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            levelFilterSelector.SelectedIndex = 0;
            chartSelector.SelectedIndex = 0;
        }
    }
}