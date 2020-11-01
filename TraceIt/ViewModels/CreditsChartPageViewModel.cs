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
        private TotalBreakdown _levelOneBreakdown;
        public TotalBreakdown LevelOneBreakdown
        {
            get => _levelOneBreakdown;
            set => SetProperty(ref _levelOneBreakdown, value, nameof(LevelOneBreakdown));        
        }

        private TotalBreakdown _levelTwoBreakdown;
        public TotalBreakdown LevelTwoBreakdown
        {
            get => _levelTwoBreakdown;
            set => SetProperty(ref _levelTwoBreakdown, value, nameof(LevelTwoBreakdown));
        }

        private TotalBreakdown _levelThreeBreakdown;
        public TotalBreakdown LevelThreeBreakdown
        {
            get => _levelThreeBreakdown;
            set => SetProperty(ref _levelThreeBreakdown, value, nameof(LevelThreeBreakdown));
        }

        private TotalBreakdown _overallBreakdown;
        public TotalBreakdown OverallBreakdown
        {
            get => _overallBreakdown;
            set => SetProperty(ref _overallBreakdown, value, nameof(OverallBreakdown));
        }

        private ObservableCollection<Standard> _selectedStandards;
        public ObservableCollection<Standard> SelectedStandards
        {
            get => _selectedStandards;
            set => SetProperty(ref _selectedStandards, value, nameof(SelectedStandards));
        }

        private ObservableCollection<GradeBreakdown> _gradeBreakdowns;
        public ObservableCollection<GradeBreakdown> GradeBreakdowns
        {
            get => _gradeBreakdowns;
            set => SetProperty(ref _gradeBreakdowns, value, nameof(GradeBreakdowns));
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
            SetTotalBreakdowns();
        }

        private void SetTotalBreakdowns()
        {
            var breakdowns = App.DataRepository.SelectedSubjects.GetTotalBreakdowns();
            LevelOneBreakdown = breakdowns[0];
            LevelTwoBreakdown = breakdowns[1];
            LevelThreeBreakdown = breakdowns[2];
            OverallBreakdown = breakdowns[3];
        }

        private void SetCreditBreakdowns()
            => GradeBreakdowns = App.DataRepository.SelectedSubjects.GetGradeBreakdowns();

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
