using System;
using System.Collections.Generic;
using System.Text;
using TraceIt.Models;
using TraceIt.Utilities;

namespace TraceIt.ViewModels
{
    public class GradeSelectionPageViewModel : BaseViewModel
    {
        public Standard Standard { get; private set; }

        public GradeSelectionPageViewModel()
        {
            Standard = StatusTracker.CurrentStandard;
        }
    }
}
