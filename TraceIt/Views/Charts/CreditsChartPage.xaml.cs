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
            SetProgressBarBinding();
        }

        private void levelFilterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProgressBarBinding();
        }

        private void SetProgressBarBinding()
        {
            var index = levelFilterSelector.SelectedIndex;
            var viewmodel = BindingContext as CreditsChartPageViewModel;

            if (index is 0)
                creditsProgressBar.SetBinding(BindingContextProperty, new Binding(nameof(viewmodel.LevelOneBreakdown)));
            else if (index is 1)
                creditsProgressBar.SetBinding(BindingContextProperty, new Binding(nameof(viewmodel.LevelTwoBreakdown)));
            else if (index is 2)
                creditsProgressBar.SetBinding(BindingContextProperty, new Binding(nameof(viewmodel.LevelThreeBreakdown)));
            else
                creditsProgressBar.SetBinding(BindingContextProperty, new Binding(nameof(viewmodel.OverallBreakdown)));
        }
    }
}