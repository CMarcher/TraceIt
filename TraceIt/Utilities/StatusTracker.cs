using System;
using System.Collections.Generic;
using System.Text;
using TraceIt.Models;

namespace TraceIt.Utilities
{
    public static class StatusTracker
    {
        public static Subject CurrentSubject { get; set; } = null;
        public static Standard CurrentStandard { get; set; } = null;
    }
}
