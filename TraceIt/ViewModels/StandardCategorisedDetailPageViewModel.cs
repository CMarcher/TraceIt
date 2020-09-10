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
        public Command NavigateToDetailCommand { get; private set; }
        public Command ChangeStandardSelectionCommand { get; private set; }

        public StandardCategorisedDetailPageViewModel(string parameter, DataService.FilterOption filterByOption)
        {
            Task.Run(() => SetStandards(parameter, filterByOption)).Wait();

            NavigateToDetailCommand = new Command<Standard>(async (standard) => await NavigateToDetail(standard));
            ChangeStandardSelectionCommand = new Command<Standard>(async (standard) => await ChangeStandardSelection(standard));
        }

        async Task SetStandards(string parameter, DataService.FilterOption filterByOption) =>
            Standards = await App.DataService.GetCategorisedStandardsAsync(
                              parameter, filterByOption);
        
        async Task NavigateToDetail(Standard standard) => 
            await App.Current.MainPage.Navigation.PushAsync(new StandardDetailPage(standard));

        async Task ChangeStandardSelection(Standard standard)
        {
            standard.Selected = HandleSelection(standard);
            await standard.PushChangesAsync();

            App.MessagingService.Send(MessagingService.MessageType.PushStandard);
        }

        bool HandleSelection(Standard standard)
        {
            standard.AddedTo = standard.Selected == false ? StatusTracker.CurrentSubject.Name : null;
            return standard.Selected == false ? true : false;
        }
    }
}
