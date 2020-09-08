using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.Views;
using TraceIt.Services;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class GradeSelectionPageViewModel : BaseViewModel
    {
        public Standard Standard { get; private set; }

        public GradeSelectionPageViewModel()
        {
            Standard = StatusTracker.CurrentStandard;

            App.MessagingService.Subscribe(this, MessagingService.MessageType.UpdateStandard,
                async (sender) => await UpdateStandard(Standard));
        }

        async Task UpdateStandard(Standard standard) => await App.DataService.UpdateStandardAsync(standard);

    }
}
