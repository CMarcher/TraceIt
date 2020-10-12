using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Models.Query_Models
{
    public class CreditBreakdown
    {
        public enum Grades
        {
            NotAchieved,
            Achieved,
            Merit,
            Excellence
        }

        public int Credits { get; set; }
        public Grades Grade { get; set; }
        
    }
}
