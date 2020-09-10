using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Views;
using Xamarin.Forms;
using TraceIt.Services;

namespace TraceIt.ViewModels
{
    public class CreditsChartPageViewModel : BaseViewModel
    {
        private int _totalCredits;
        public int TotalCredits
        {
            get { return _totalCredits; }
            set
            {
                _totalCredits = value;
                OnPropertyChanged(nameof(TotalCredits));
            }
        }

        private int _achievedCredits;
        public int AchievedCredits
        {
            get { return _achievedCredits; }
            set
            {
                _achievedCredits = value;
                OnPropertyChanged(nameof(AchievedCredits));
            }
        }

        public CreditsChartPageViewModel()
        {
            Task.Run(SetCredits).Wait();
            SubscribeToMessage();
        }

        async Task SetCredits()
        {
            var credits = await App.DataService.GetAchievedAndTotalCreditsAsync();
            TotalCredits = credits.Item1;
            AchievedCredits = credits.Item2;
        }

        void SubscribeToMessage()
        {
            App.MessagingService.Subscribe(this, MessagingService.MessageType.PushStandard,
                async (sender) => await SetCredits());
        }
    }
}
