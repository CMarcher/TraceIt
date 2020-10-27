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
using TraceIt.Extensions;

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

        public bool Initialised { get; set; }

        public CreditsChartPageViewModel()
        {
            Initialise();
        }

        private void Initialise()
        {
            SubscribeToMessages();
            Initialised = true;
        }

        private void Refresh()
        {
            SetCreditBreakdowns();
            SetCredits();
        }

        private void SetCredits()
        {
            var credits = App.DataRepository.SelectedSubjects.GetAchievedAndTotalCredits();
            TotalCredits = credits.Item1;
            AchievedCredits = credits.Item2;
        }

        private void SetCreditBreakdowns()
            => CreditBreakdowns = App.DataRepository.SelectedSubjects.GetCreditBreakdowns();

        private void SubscribeToMessages()
        {
            if (Initialised is false)
            {
                App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshStandards,
                    (sender) => Refresh());

                App.MessagingService.Subscribe(this, MessagingService.MessageType.RepositoryInitialisationComplete,
                    (sender) => Refresh());
            }
        }
    }
}
