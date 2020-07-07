using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TraceIt.Models;

namespace TraceIt.ViewModels
{
    public class SelectedStandardsPageViewModel
    {
        public ObservableCollection<AssessmentStandard> Standards = new ObservableCollection<AssessmentStandard>()
        {
            new AssessmentStandard()
            {
                AS_Code = 97321,
                Title = "Demonstrate understanding of something",
                Sub_Reference = "Smart English 1.1",
                AS_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                AS_Code = 97321,
                Title = "Demonstrate understanding of something",
                Sub_Reference = "Smart English 1.1",
                AS_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                AS_Code = 97321,
                Title = "Demonstrate understanding of something",
                Sub_Reference = "Smart English 1.1",
                AS_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                AS_Code = 97321,
                Title = "Demonstrate understanding of something",
                Sub_Reference = "Smart English 1.1",
                AS_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                AS_Code = 97321,
                Title = "Demonstrate understanding of something",
                Sub_Reference = "Smart English 1.1",
                AS_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            }

        };

    }
}
