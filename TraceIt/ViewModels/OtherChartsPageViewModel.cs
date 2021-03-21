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

        private bool _hasUniEntrance;
        public bool HasUniEntrance
        {
            get => _hasUniEntrance;
            set => SetProperty(ref _hasUniEntrance, value, nameof(HasUniEntrance));
        }

        private int _readingCredits;
        public int ReadingCredits
        {
            get => _readingCredits;
            set => SetProperty(ref _readingCredits, value, nameof(ReadingCredits));
        }

        private int _writingCredits;
        public int WritingCredits
        {
            get => _writingCredits;
            set => SetProperty(ref _writingCredits, value, nameof(WritingCredits));
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
            SetHasUniEntrance();
            SetReadingAndWritingCredits();
        }

        private void SetHasUniEntrance()
        {
            HasUniEntrance = App.DataRepository.SelectedSubjects.HasAchievedUniversityEntrance();
        }

        private void SetReadingAndWritingCredits()
        {
            var credits = App.DataRepository.SelectedSubjects.GetReadingAndWritingCredits();
            ReadingCredits = credits.Item1;
            WritingCredits = credits.Item2;
        }

        private void InitialiseRankScoreStepViewProgress()
        {
            ProgressIndicators = new ObservableCollection<RankScoreProgressIndicator>()
            {
                new RankScoreProgressIndicator() { Progress = 150 },
                new RankScoreProgressIndicator() { Progress = 165 },
                new RankScoreProgressIndicator() { Progress = 180 },
                new RankScoreProgressIndicator() { Progress = 200 },
                new RankScoreProgressIndicator() { Progress = 210 },
                new RankScoreProgressIndicator() { Progress = 230 },
                new RankScoreProgressIndicator() { Progress = 250 },
                new RankScoreProgressIndicator() { Progress = 260 },
                new RankScoreProgressIndicator() { Progress = 275 },
                new RankScoreProgressIndicator() { Progress = 280 }
            };
        }

        private void SetRankScore()
        {
            RankScore = App.DataRepository.SelectedSubjects.GetRankScore();
            CalculateRankScoreStepViewProgress();
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
