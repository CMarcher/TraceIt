using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    public class RankScoreSubject : BaseModel
    {
        private int _achievedCredits;
        public int AchievedCredits
        {
            get => _achievedCredits;
            set => SetProperty(ref _achievedCredits, value, nameof(AchievedCredits));
        }

        private int _meritCredits;
        public int MeritCredits
        {
            get => _meritCredits;
            set => SetProperty(ref _meritCredits, value, nameof(MeritCredits));
        }

        private int _excellenceCredits;
        public int ExcellenceCredits
        {
            get => _excellenceCredits;
            set => SetProperty(ref _excellenceCredits, value, nameof(ExcellenceCredits));
        }

        private int _rankScore;
        public int RankScore
        {
            get => _rankScore;
            set => SetProperty(ref _rankScore, value, nameof(RankScore));
        }

        public List<Standard> Standards { get; private set; }
        private const int CREDIT_LIMIT = 24;
        private const int ACHIEVED_MULTIPLIER = 2;
        private const int MERIT_MULTIPLIER = 3;
        private const int EXCELLENCE_MULTIPLIER = 4;

        public RankScoreSubject()
        {
            Initialise();
        }

        private void Initialise()
        {
            Standards = new List<Standard>();
        }

        public void AddStandard(Standard standard)
            => Standards.Add(standard);
        
        public void IntialiseRankScore()
        {
            AchievedCredits = Standards.CountCredits(x => x.FinalGrade == Standard.Grade.Achieved);
            MeritCredits = Standards.CountCredits(x => x.FinalGrade == Standard.Grade.Merit);
            ExcellenceCredits = Standards.CountCredits(x => x.FinalGrade == Standard.Grade.Excellence);

            var total = AchievedCredits + MeritCredits + ExcellenceCredits;
            bool breachedCreditLimit = total > CREDIT_LIMIT;

            if (breachedCreditLimit)
                ReduceCredits();

            CalculateRankScore();
        }

        private void CalculateRankScore()
        {
            var achievedScore = AchievedCredits * ACHIEVED_MULTIPLIER;
            var meritScore = MeritCredits * MERIT_MULTIPLIER;
            var excellenceScore = ExcellenceCredits * EXCELLENCE_MULTIPLIER;

            RankScore = achievedScore + meritScore + excellenceScore;
        }

        private void ReduceCredits()
        {
            Func<int, bool> creditsZeroed = credits => credits == 0;

            while (GetTotal() > CREDIT_LIMIT)
            {
                if (creditsZeroed(AchievedCredits) is false)
                    AchievedCredits--;
                else if (creditsZeroed(MeritCredits) is false)
                    MeritCredits--;
                else
                    ExcellenceCredits--;
            }
        }

        private int GetTotal()
            => AchievedCredits + MeritCredits + ExcellenceCredits;
    }
}
