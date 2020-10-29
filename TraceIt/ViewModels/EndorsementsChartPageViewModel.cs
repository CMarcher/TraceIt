using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Services;
using TraceIt.Utilities;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class EndorsementsChartPageViewModel : BaseViewModel
    {
        ObservableCollection<Standard> Standards = new ObservableCollection<Standard>();

        public LevelEndorsement LevelOneEndorsement { get; private set; } = new LevelEndorsement();
        public LevelEndorsement LevelTwoEndorsement { get; private set; } = new LevelEndorsement();
        public LevelEndorsement LevelThreeEndorsement { get; private set; } = new LevelEndorsement();

        private ObservableCollection<SubjectEndorsement> _subjectEndorsements;
        public ObservableCollection<SubjectEndorsement> SubjectEndorsements
        {
            get => _subjectEndorsements;
            set => SetProperty(ref _subjectEndorsements, value, nameof(SubjectEndorsements));
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value, nameof(Year));
        }

        bool initialised = false;

        public EndorsementsChartPageViewModel()
        {
            var usermanager = App.UserManagerService;
            SubscribeToMessages();
            Initialise();

            if (!usermanager.LoggedIn)
                SetEndorsements();
        }

        private void Initialise()
        {
            initialised = true;
            Year = StatusTracker.CurrentYear;
        }

        private void SubscribeToMessages()
        {
            App.MessagingService.Subscribe(this, MessagingService.MessageType.RepositoryInitialisationComplete,
                (sender) => SetEndorsements());

            App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshStandards,
                (sender) => SetEndorsements());
        }

        private void SetStandards()
            => Standards = App.DataRepository.SelectedSubjects.GetSelectedStandards();

        private void SetEndorsements()
        {
            if (initialised is true)
                ClearLevelEndorsements();

            SetStandards();
            SetSubjectEndorsements();

            foreach (var standard in Standards)
                AddToLevelEndorsement(standard);
        }

        private void AddToLevelEndorsement(Standard standard)
        {
            switch (standard.Level)
            {
                case Standard.Levels.One:
                    LevelOneEndorsement.AddCredits(standard);
                    break;

                case Standard.Levels.Two:
                    LevelTwoEndorsement.AddCredits(standard);
                    break;

                case Standard.Levels.Three:
                    LevelThreeEndorsement.AddCredits(standard);
                    break;
            }
        }

        private void SetSubjectEndorsements()
            => SubjectEndorsements = App.DataRepository.SelectedSubjects.GetSubjectEndorsements();
        
        private void ClearLevelEndorsements()
        {
            LevelOneEndorsement.ClearCredits();
            LevelTwoEndorsement.ClearCredits();
            LevelThreeEndorsement.ClearCredits();
        }
    }
}
