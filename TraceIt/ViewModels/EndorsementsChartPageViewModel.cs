using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Services;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class EndorsementsChartPageViewModel : BaseViewModel
    {
        ObservableCollection<Standard> Standards = new ObservableCollection<Standard>();

        public Endorsement LevelOneEndorsement { get; private set; } = new Endorsement();
        public Endorsement LevelTwoEndorsement { get; private set; } = new Endorsement();
        public Endorsement LevelThreeEndorsement { get; private set; } = new Endorsement();
        public ObservableCollection<SelectedSubject> SubjectEndorsements { get; private set; }

        bool initialised = false;

        public EndorsementsChartPageViewModel()
        {
            App.MessagingService.Subscribe(this, MessagingService.MessageType.RepositoryInitialisationComplete,
                (sender) => Initialise());
        }

        void Initialise()
        {
            Task.Run(SetStandards).Wait();
            SetEndorsements();
            initialised = true;
        }

        async Task SetStandards()
            => Standards = await App.DataService.GetSelectedStandardsAsync();

        void SetEndorsements()
        {
            if (initialised)
                ClearEndorsements();

            foreach (var standard in Standards)
                AddToLevelEndorsement(standard);

            SetSubjectEndorsements();
        }

        void AddToLevelEndorsement(Standard standard)
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

        void SetSubjectEndorsements()
        {
            var subjects = App.DataRepository.SelectedSubjects;
            SubjectEndorsements = subjects;
        }

        void ClearEndorsements()
        {
            LevelOneEndorsement.ClearCredits();
            LevelTwoEndorsement.ClearCredits();
            LevelThreeEndorsement.ClearCredits();
        }
    }
}
