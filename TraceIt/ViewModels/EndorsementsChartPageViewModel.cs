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

        public Endorsement LevelOneEndorsement { get; private set; } = new Endorsement();
        public Endorsement LevelTwoEndorsement { get; private set; } = new Endorsement();
        public Endorsement LevelThreeEndorsement { get; private set; } = new Endorsement();

        private ObservableCollection<SelectedSubject> _subjectEndorsements;
        public ObservableCollection<SelectedSubject> SubjectEndorsements
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
            => Standards = SubjectEndorsements.GetSelectedStandards();

        private void SetEndorsements()
        {
            if (initialised)
            {
                SetSubjectEndorsements();
                ClearEndorsements();
            }

            SetStandards();

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
        {
            SubjectEndorsements = App.DataRepository.SelectedSubjects;
        }

        private void ClearEndorsements()
        {
            LevelOneEndorsement.ClearCredits();
            LevelTwoEndorsement.ClearCredits();
            LevelThreeEndorsement.ClearCredits();
        }
    }
}
