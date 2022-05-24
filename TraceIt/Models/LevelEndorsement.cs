using System;
using System.Collections.Generic;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    public class LevelEndorsement : BaseModel
    {
        private int _meritCredits;
        public int MeritCredits
        {
            get { return _meritCredits; }
            set { SetProperty(ref _meritCredits, value, nameof(MeritCredits)); }
        }

        private int _excellenceCredits;
        public int ExcellenceCredits
        {
            get { return _excellenceCredits; }
            set { SetProperty(ref _excellenceCredits, value, nameof(ExcellenceCredits)); }
        }

        public LevelEndorsement(IEnumerable<Standard> standards)
        {
            AddCredits(standards);
        }

        public LevelEndorsement()
        {

        }

        public void AddCredits(IEnumerable<Standard> standards)
        {
            MeritCredits = standards.CountCredits(x => x.FinalGrade >= Standard.Grade.Merit);
            ExcellenceCredits = standards.CountCredits(x => x.FinalGrade is Standard.Grade.Excellence);
        }
    }
}
