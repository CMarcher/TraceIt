using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Services;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class EndorsementsChartPageViewModel : BaseViewModel
    {
        List<Standard> Standards = new List<Standard>();

        public Endorsement LevelOneEndorsement { get; private set; } = new Endorsement();
        public Endorsement LevelTwoEndorsement { get; private set; } = new Endorsement();
        public Endorsement LevelThreeEndorsement { get; private set; } = new Endorsement();

        bool initialised = false;

        public EndorsementsChartPageViewModel()
        {
            Initialise();
            initialised = true;

            App.MessagingService.Subscribe(this, MessagingService.MessageType.RefreshEndorsements,
                (sender) => Initialise());
        }

        void Initialise()
        {
            Task.Run(SetStandards).Wait();
            SetEndorsements();
        }

        async Task SetStandards()
            => Standards = await App.DataService.GetSelectedStandardsAsync();

        void SetEndorsements()
        {
            if (initialised)
                ClearEndorsements();

            foreach (var standard in Standards)
            {
                AddToLevelEndorsement(standard);
            }
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

        void ClearEndorsements()
        {
            LevelOneEndorsement.ClearCredits();
            LevelTwoEndorsement.ClearCredits();
            LevelThreeEndorsement.ClearCredits();
        }


    }
}
