using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
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
            listViewEndorsements.DataSource.Filter = FilterEndorsements;
        }

        private bool FilterEndorsements(object item)
        {
            if (item is SelectedSubject)
            {
                var subject = item as SelectedSubject;
                bool isSelected = subject.Selected;
                bool matchesSelectedYear = subject.Year == 1;
            }
            else
                throw new Exception("Invalid item type: " + item.GetType());
        }
    }
}