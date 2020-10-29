using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    public class SubjectEndorsement : BaseModel
    {
        public enum EndorsementTypes
        {
            Merit,
            Excellence
        }

        public string Name { get; private set; }
        public int Year { get; private set; }
        public int InternalCredits { get; private set; }
        public int ExternalCredits { get; private set; }
        public int TotalCredits { get; private set; }
        public EndorsementTypes EndorsementType { get; private set; }

        public SubjectEndorsement(SelectedSubject subject, EndorsementTypes endorsementType)
        {
            Name = subject.BaseSubject.Name;
            EndorsementType = endorsementType;
            Year = subject.Year;

            SetCredits(subject);
        }

        public void SetCredits(SelectedSubject subject)
        {
            var standardsFilter = GetFilter();
            var filteredStandards = subject.Standards.Where(new Func<Standard, bool>(standardsFilter));

            InternalCredits = filteredStandards.CountCredits(x => x.AssessmentType is Standard.AssessmentTypes.Internal);
            ExternalCredits = filteredStandards.CountCredits(x => x.AssessmentType is Standard.AssessmentTypes.External);
            TotalCredits = InternalCredits + ExternalCredits;

            InternalCredits = Math.Min(InternalCredits, 3);
            ExternalCredits = Math.Min(ExternalCredits, 3);
            TotalCredits = TotalCredits - InternalCredits - ExternalCredits;

        }

        private Predicate<Standard> GetFilter()
        {
            switch (EndorsementType)
            {
                case EndorsementTypes.Merit:
                    return x => x.FinalGrade is Standard.Grade.Merit || x.FinalGrade is Standard.Grade.Excellence;
                case EndorsementTypes.Excellence:
                    return x => x.FinalGrade is Standard.Grade.Excellence;
                default:
                    throw new Exception("EndorsementType not set");
            }
        }
    }
}
