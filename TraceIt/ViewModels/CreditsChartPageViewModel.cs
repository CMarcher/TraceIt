using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Views;
using Xamarin.Forms;
using TraceIt.Services;
using System.Collections.ObjectModel;
using TraceIt.Models;
using TraceIt.Models.Query_Models;

namespace TraceIt.ViewModels
{
    public class CreditsChartPageViewModel : BaseViewModel
    {
        private int _totalCredits;
        public int TotalCredits
        {
            get => _totalCredits;
            set => SetProperty(ref _totalCredits, value, nameof(TotalCredits));
        }

        private int _achievedCredits;
        public int AchievedCredits
        {
            get => _achievedCredits;
            set => SetProperty(ref _achievedCredits, value, nameof(AchievedCredits));
        }

        private ObservableCollection<Standard> _selectedStandards;
        public ObservableCollection<Standard> SelectedStandards
        {
            get => _selectedStandards;
            set => SetProperty(ref _selectedStandards, value, nameof(SelectedStandards));
        }

        private ObservableCollection<CreditBreakdown> _creditBreakdowns;
        public ObservableCollection<CreditBreakdown> CreditBreakdowns
        {
            get => _creditBreakdowns;
            set => SetProperty(ref _creditBreakdowns, value, nameof(CreditBreakdowns));
        }

        public CreditsChartPageViewModel()
        {
            Initialise();
        }

        void Initialise()
        {
            Task.Run(SetCredits).Wait();
            SetStandards();
            SetCreditBreakdowns();
            SubscribeToMessages();
        }

        void SetStandards()
            => SelectedStandards = App.DataRepository.SelectedStandards;

        async Task SetCredits()
        {
            var credits = await App.DataService.GetAchievedAndTotalCreditsAsync();
            TotalCredits = credits.Item1;
            AchievedCredits = credits.Item2;
        }

        void SetCreditBreakdowns()
            => CreditBreakdowns = App.DataRepository.CreditBreakdowns;

        void SubscribeToMessages()
        {
            App.MessagingService.Subscribe(this, MessagingService.MessageType.PushStandard,
                async (sender) => await SetCredits());

            //App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshStandards,
            //    (sender) => SetStandards());
        }
    }
}
