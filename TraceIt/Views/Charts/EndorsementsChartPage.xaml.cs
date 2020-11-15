using Syncfusion.DataSource;
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
            InitialiseData();
        }

        private void InitialiseData()
        {
            InitialiseDataSource();
            RefreshDataSource();
        }

        private void InitialiseDataSource()
        {
            listViewEndorsements.DataSource.Filter = FilterEndorsements;
        }

        private void RefreshDataSource()
        {
            if (listViewEndorsements.DataSource is null)
                return;

            listViewEndorsements.DataSource.Filter = FilterEndorsements;
            listViewEndorsements.DataSource.RefreshFilter();
            listViewEndorsements.DataSource.Refresh();
        }

        private bool FilterEndorsements(object item)
        {
            if (item is SubjectEndorsement)
            {
                var viewmodel = BindingContext as EndorsementsChartPageViewModel;
                var endorsement = item as SubjectEndorsement;

                bool matchesSelectedYear = endorsement.Year == viewmodel.Year;

                return matchesSelectedYear;
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