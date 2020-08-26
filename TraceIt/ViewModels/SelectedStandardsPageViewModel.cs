using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class SelectedStandardsPageViewModel : BaseViewModel
    {
        private ObservableCollection<AssessmentStandard> _standards;
        public ObservableCollection<AssessmentStandard> Standards
        {
            get { return _standards; }
            set
            {
                _standards = value;
                OnPropertyChanged(nameof(Standards));
            }
        }

        public SelectedStandardsPageViewModel()
        {
            Standards = new ObservableCollection<AssessmentStandard>();

            Task.Run(SetStandards).Wait();

            MessagingCenter.Subscribe<StandardCategorisedDetailPageViewModel>(this, "Update standards", (sender) =>
            {
                Task.Run(SetStandards);
                MessagingCenter.Send(this, "Update items source");
            });
        }

        async Task SetStandards() => Standards = await App.DataService.GetSelectedStandards(StatusTracker.CurrentSubject.Name);

    }
}
