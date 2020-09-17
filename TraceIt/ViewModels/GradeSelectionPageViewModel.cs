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

            App.MessagingService.Subscribe(this, MessagingService.MessageType.PushStandard,
                (sender) =>
                {
                    Task.Run(() => Standard.PushChangesAsync(false));
                    App.MessagingService.Send(MessagingService.MessageType.RefreshEndorsements);
                });
        }

    }
}
