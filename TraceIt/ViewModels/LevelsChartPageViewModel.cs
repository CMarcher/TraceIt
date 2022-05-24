using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Services;

namespace TraceIt.ViewModels
{
    public class LevelsChartPageViewModel : BaseViewModel
    {
        private ObservableCollection<Level> _levels;
        public ObservableCollection<Level> Levels
        {
            get => _levels;
            set => SetProperty(ref _levels, value, nameof(Levels));
        }

        private int _literacyCredits;
        public int LiteracyCredits
        {
            get => _literacyCredits;
            set => SetProperty(ref _literacyCredits, value, nameof(LiteracyCredits));
        }

        private int _numeracyCredits;
        public int NumeracyCredits
        {
            get => _numeracyCredits;
            set => SetProperty(ref _numeracyCredits, value, nameof(NumeracyCredits));
        }

        public LevelsChartPageViewModel()
        {
            Initialise();
        }

        private void Initialise()
        {
            SubscribeToMessages();

            if (App.UserManagerService.LoggedIn is false)
                SetDependencies();
        }

        private void SetDependencies()
        {
            SetLevels();
            SetLiteracyAndNumeracyCredits();
        }
        
        private void SetLevels()
            => Levels = App.DataRepository.SelectedSubjects.GetAndSetLevels();

        private void SetLiteracyAndNumeracyCredits()
        {
            var tuple = App.DataRepository.SelectedSubjects.GetLiteracyAndNumeracyCredits();
            LiteracyCredits = tuple.Item1;
            NumeracyCredits = tuple.Item2;
        }

        private void SubscribeToMessages()
        {
            App.MessagingService.Subscribe(this, MessagingService.MessageType.RepositoryInitialisationComplete,
                (sender) => SetDependencies());

            App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshStandards,
                (sender) => SetDependencies());
        }
    }
}
