using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views.Charts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EndorsementsChartPage : ContentView
    {
        public EndorsementsChartPage()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            RefreshDataSource();
        }

        private void RefreshDataSource()
        {
            listViewEndorsements.DataSource.Filter = FilterEndorsements;
            listViewEndorsements.DataSource.RefreshFilter();
            listViewEndorsements.DataSource.Refresh();
        }
        
        private bool FilterEndorsements(object item)
        {
            if (item is SelectedSubject)
            {
                var viewmodel = BindingContext as EndorsementsChartPageViewModel;
                var subject = item as SelectedSubject;
                bool isSelected = subject.Selected;
                bool matchesSelectedYear = subject.Year == viewmodel.Year;

                return isSelected && matchesSelectedYear;
            }
            else
                throw new Exception("Invalid item type: " + item.GetType());
        }

        private void SfSegmentedControl_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            RefreshDataSource();
        }
    }
}