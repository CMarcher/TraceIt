using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Services;
using TraceIt.Utilities;
using TraceIt.Views;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class StandardCategorisedDetailPageViewModel : BaseViewModel
    {
        public ObservableCollection<Standard> Standards { get; private set; }
        public Command ChangeStandardSelectionCommand { get; private set; }

        public StandardCategorisedDetailPageViewModel(string parameter, DataService.FilterOption filterByOption)
        {
            Task.Run(() => SetStandards(parameter, filterByOption)).Wait();

            ChangeStandardSelectionCommand = new Command<Standard>(async (standard) => await ChangeStandardSelection(standard));
        }

        async Task SetStandards(string parameter, DataService.FilterOption filterByOption) =>
            Standards = await App.DataService.GetCategorisedStandardsAsync(parameter, filterByOption);

        async Task ChangeStandardSelection(Standard standard)
        {
            var subject = StatusTracker.CurrentSubject;

            Task task = standard.Selected == false ?
                subject.AddStandardAsync(standard) :
                subject.RemoveStandardAsync(standard);

            await task;
        }
    }
}
