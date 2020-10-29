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
            if (item is SubjectEndorsement)
            {
                var viewmodel = BindingContext as EndorsementsChartPageViewModel;
                var endorsementType = GetEndorsementType();
                var endorsement = item as SubjectEndorsement;

                bool matchesSelectedYear = endorsement.Year == viewmodel.Year;
                bool matchesEndorsementType = endorsement.EndorsementType == endorsementType;

                return matchesSelectedYear && matchesEndorsementType;
            }
            else
                throw new Exception("Invalid item type: " + item.GetType());
        }

        private SubjectEndorsement.EndorsementTypes GetEndorsementType()
        {
            var index = endorsementTypeSelector.SelectedIndex;

            if (index is 0)
                return SubjectEndorsement.EndorsementTypes.Merit;
            else if (index is 1)
                return SubjectEndorsement.EndorsementTypes.Excellence;
            else
                return SubjectEndorsement.EndorsementTypes.Merit;
        }

        private void SfSegmentedControl_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            RefreshDataSource();
        }

        private void endorsementTypeSelector_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            RefreshDataSource();
        }
    }
}