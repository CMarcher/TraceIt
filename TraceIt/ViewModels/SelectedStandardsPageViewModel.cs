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

        public Command RemoveStandardCommand { get; set; }

        public SelectedStandardsPageViewModel()
        {
            Standards = new ObservableCollection<Standard>();
            SetStandards();
            SetCommands();
        }

        void SetStandards()
            => Standards = StatusTracker.CurrentSubject.Standards;

        void SetCommands()
            => RemoveStandardCommand = new Command<Standard>(async (standard) => await RemoveStandard(standard));
        
        async Task RemoveStandard(Standard standard)
            => await StatusTracker.CurrentSubject.RemoveStandardAsync(standard);
    }
}
