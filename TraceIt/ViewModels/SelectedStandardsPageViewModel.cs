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
                Code = 97321,
                Title = "Demonstrate understanding of something",
                Subject_Reference = "Smart English 1.1",
                Assessment_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                Code = 97321,
                Title = "Demonstrate understanding of something",
                Subject_Reference = "Smart English 1.1",
                Assessment_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                Code = 97321,
                Title = "Demonstrate understanding of something",
                Subject_Reference = "Smart English 1.1",
                Assessment_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                Code = 97321,
                Title = "Demonstrate understanding of something",
                Subject_Reference = "Smart English 1.1",
                Assessment_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            },
            new AssessmentStandard()
            {
                Code = 97321,
                Title = "Demonstrate understanding of something",
                Subject_Reference = "Smart English 1.1",
                Assessment_Type = "Achievement",
                Hyperlink = "Whatever",
                Credits = 4
            }

        };

    }
}
