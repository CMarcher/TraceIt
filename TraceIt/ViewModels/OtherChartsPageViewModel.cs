using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TraceIt.Extensions;
using TraceIt.Models;
using Syncfusion.XForms.ProgressBar;

namespace TraceIt.ViewModels
{
    public class OtherChartsPageViewModel : BaseViewModel
    {
        private int _rankScore;
        public int RankScore
        {
            get => _rankScore;
            set => SetProperty(ref _rankScore, value, nameof(RankScore));
        }

        private ObservableCollection<RankScoreProgressIndicator> _progressIndicator;
        public ObservableCollection<RankScoreProgressIndicator> ProgressIndicators
        {
            get => _progressIndicator;
            set => SetProperty(ref _progressIndicator, value, nameof(ProgressIndicators));
        }

        public OtherChartsPageViewModel()
        {
            Initialise();
        }

        private void Initialise()
        {
            SubscribeToMessages();
            InitialiseRankScoreStepViewProgress();

            if (App.UserManagerService.LoggedIn is false)
                SetDependentProperties();
        }

        private void SetDependentProperties()
        {
            SetRankScore();
        }

        private void InitialiseRankScoreStepViewProgress()
        {
            ProgressIndicators = new ObservableCollection<RankScoreProgressIndicator>()
            {
                new RankScoreProgressIndicator() { Progress = 150, Title = "First stage" },
                new RankScoreProgressIndicator() { Progress = 165, Title = "Second stage" },
                new RankScoreProgressIndicator() { Progress = 180, Title = "Third stage" },
                new RankScoreProgressIndicator() { Progress = 200, Title = "Fourth stage" },
                new RankScoreProgressIndicator() { Progress = 210, Title = "Fifth stage" },
                new RankScoreProgressIndicator() { Progress = 230, Title = "Fifth stage" },
                new RankScoreProgressIndicator() { Progress = 250, Title = "Sixth stage" },
                new RankScoreProgressIndicator() { Progress = 260, Title = "Seventh stage" },
                new RankScoreProgressIndicator() { Progress = 275, Title = "Eighth stage" },
                new RankScoreProgressIndicator() { Progress = 280, Status = StepStatus.Completed, Title = "Ninth stage" }
            };
        }

        private void SetRankScore()
        {
            RankScore = App.DataRepository.SelectedSubjects.GetRankScore();
            //CalculateRankScoreStepViewProgress();
        }

        private void CalculateRankScoreStepViewProgress()
        {
            for (int index = 0; index < ProgressIndicators.Count; index++)
            {
                if (index is 0)
                    ProgressIndicators[index].SetStatus(RankScore);
                else
                    ProgressIndicators[index].SetStatus(RankScore, ProgressIndicators[index - 1].Progress);
            }
        }

        private void SubscribeToMessages()
        {
            App.MessagingService.Subscribe(this, Services.MessagingService.MessageType.RepositoryInitialisationComplete,
                (sender) => SetDependentProperties());

            App.MessagingService.Subscribe(this, Services.MessagingService.MessageType.RefreshStandards,
                (sender) => SetDependentProperties());
        }
    }
}
