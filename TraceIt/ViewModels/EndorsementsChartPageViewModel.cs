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
        public ObservableCollection<SubjectEndorsement> SubjectEndorsements { get; private set; } = new ObservableCollection<SubjectEndorsement>();

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
                case Standard.NCEALevel.One:
                    LevelOneEndorsement.AddCredits(standard);
                    break;

                case Standard.NCEALevel.Two:
                    LevelTwoEndorsement.AddCredits(standard);
                    break;

                case Standard.NCEALevel.Three:
                    LevelThreeEndorsement.AddCredits(standard);
                    break;
            }
        }

        void SetSubjectEndorsements()
        {
            var subjects = App.DataRepository.SelectedSubjects;

            foreach(var subject in subjects)
            {
                var meritCredits = subject.MeritCredits; 
                var excellenceCredits = subject.ExcellenceCredits;
                var name = subject.Name;

                SubjectEndorsements.Add(new SubjectEndorsement()
                {
                    Name = name,
                    MeritCredits = meritCredits,
                    ExcellenceCredits = excellenceCredits
                });
            }
        }

        void ClearEndorsements()
        {
            LevelOneEndorsement.ClearCredits();
            LevelTwoEndorsement.ClearCredits();
            LevelThreeEndorsement.ClearCredits();
            SubjectEndorsements.ClearCredits();
        }


    }
}
