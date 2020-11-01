using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views.Charts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainChartsPage : ContentPage
    {
        public MainChartsPage()
        {
            InitializeComponent();
        }

        private void tabSelector_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            tabsCarouselView.SelectedIndex = tabSelector.SelectedIndex;
        }

        private void tabsCarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            tabSelector.SelectedIndex = tabsCarouselView.SelectedIndex;
        }
    }
}