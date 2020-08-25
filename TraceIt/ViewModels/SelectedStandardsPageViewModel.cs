using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;

namespace TraceIt.ViewModels
{
    public class SelectedStandardsPageViewModel : BaseViewModel
    {
        public List<AssessmentStandard> Standards = new List<AssessmentStandard>();

        public SelectedStandardsPageViewModel()
        {
            Task.Run(SetStandards).Wait();
        }

        async Task SetStandards() => Standards = await App.DataService.GetSelectedStandards(StatusTracker.CurrentSubject.Name);

    }
}
