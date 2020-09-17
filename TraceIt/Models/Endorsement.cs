using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Models
{
    public class Endorsement : BaseModel
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

        public void AddCredits(Standard standard)
        {
            if (standard.FinalGrade == Standard.Grade.Merit)
                MeritCredits += standard.Credits;

            else if (standard.FinalGrade == Standard.Grade.Excellence)
                ExcellenceCredits += standard.Credits;
        }

        public void ClearCredits()
        {
            MeritCredits = 0;
            ExcellenceCredits = 0;
        }
    }
}
