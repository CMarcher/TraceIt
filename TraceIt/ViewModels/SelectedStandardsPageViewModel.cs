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
        public ObservableCollection<AssessmentStandard> Standards = new ObservableCollection<AssessmentStandard>();

        public SelectedStandardsPageViewModel()
        {
            Task.Run(SetStandards).Wait();

            MessagingCenter.Subscribe<StandardCategorisedDetailPageViewModel>(this, "Update standards", (sender) =>
            {
                Task.Run(SetStandards);
            });
        }

        async Task SetStandards() => Standards = await App.DataService.GetSelectedStandards(StatusTracker.CurrentSubject.Name);

    }
}
