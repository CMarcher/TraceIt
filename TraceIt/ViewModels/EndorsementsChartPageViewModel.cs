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
        private LevelEndorsement _levelOneEndorsement;
        public LevelEndorsement LevelOneEndorsement
        {
            get => _levelOneEndorsement;
            private set => SetProperty(ref _levelOneEndorsement, value, nameof(LevelOneEndorsement));
        }

        private LevelEndorsement _levelTwoEndorsement;
        public LevelEndorsement LevelTwoEndorsement
        {
            get => _levelTwoEndorsement;
            set => SetProperty(ref _levelTwoEndorsement, value, nameof(LevelTwoEndorsement));
        }

        private LevelEndorsement _levelThreeEndorsement;
        public LevelEndorsement LevelThreeEndorsement
        {
            get => _levelThreeEndorsement;
            set => SetProperty(ref _levelThreeEndorsement, value, nameof(LevelThreeEndorsement));
        }

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

        bool Initialised = false;

        public EndorsementsChartPageViewModel()
        {
            Initialise();
        }

        private void Initialise()
        {
            var usermanager = App.UserManagerService;
            SubscribeToMessages();
            InitialiseEndorsements();

            if (!usermanager.LoggedIn)
                SetEndorsements();

            Year = StatusTracker.CurrentYear;
        }

        private void InitialiseEndorsements()
        {
            LevelOneEndorsement = new LevelEndorsement();
            LevelTwoEndorsement = new LevelEndorsement();
            LevelThreeEndorsement = new LevelEndorsement();
        }

        private void SubscribeToMessages()
        {
            App.MessagingService.Subscribe(this, MessagingService.MessageType.RepositoryInitialisationComplete,
                (sender) => SetEndorsements());

            App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshStandards,
                (sender) => SetEndorsements());
        }

        private void SetEndorsements()
        {
            SetSubjectEndorsements();
            SetLevelEndorsements();

            if (Initialised is false)
            {
                RaiseViewModelInitialised();
                Initialised = true;
            }
        }

        private void SetSubjectEndorsements()
            => SubjectEndorsements = App.DataRepository.SelectedSubjects.GetSubjectEndorsements();

        private void SetLevelEndorsements()
        {
            var repo = App.DataRepository;
            var subjects = repo.SelectedSubjects;
            var levelEndorsements = subjects.GetLevelEndorsements();

            LevelOneEndorsement = levelEndorsements[0];
            LevelTwoEndorsement = levelEndorsements[1];
            LevelThreeEndorsement = levelEndorsements[2];
        }
    }
}
