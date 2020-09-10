using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.Services;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class SelectedStandardsPageViewModel : BaseViewModel
    {
        private ObservableCollection<Standard> _standards;
        public ObservableCollection<Standard> Standards
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
            Standards = new ObservableCollection<Standard>();
            Task.Run(SetStandards).Wait();

            App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshStandards, 
                (sender) => Task.Run(SetStandards).Wait());
        }

        async Task SetStandards() => Standards = await App.DataService.GetSelectedStandards(StatusTracker.CurrentSubject.Name);

    }
}
