using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Services;
using TraceIt.Views;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class StandardCategorisedDetailPageViewModel : BaseViewModel
    {
        public ObservableCollection<AssessmentStandard> Standards { get; private set; }
        public Command NavigateToDetailCommand { get; private set; }

        public StandardCategorisedDetailPageViewModel(string parameter, DataService.FilterByOption filterByOption)
        {
            Task.Run(() => SetStandards(parameter, filterByOption)).Wait();

            NavigateToDetailCommand = new Command<AssessmentStandard>(async (standard) => await NavigateToDetail(standard));
        }

        async Task SetStandards(string parameter, DataService.FilterByOption filterByOption) =>
            Standards = await App.DataService.GetCategorisedStandardsAsync(
                              parameter, filterByOption);
        
        async Task NavigateToDetail(AssessmentStandard standard) => 
            await App.Current.MainPage.Navigation.PushAsync(new StandardDetailPage(standard));
    }
}
