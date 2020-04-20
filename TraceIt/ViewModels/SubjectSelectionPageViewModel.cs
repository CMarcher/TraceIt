using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using TraceIt.Models;

namespace TraceIt.ViewModels
{
    public class SubjectSelectionPageViewModel
    {
        public ObservableCollection<Subject> subjects = new ObservableCollection<Subject>()
        {
                new Subject("Subject 1"),
                new Subject("Subject 2"),
                new Subject("Subject 3"),
                new Subject("Subject 4"),
                new Subject("Subject 1"),
                new Subject("Subject 2"),
                new Subject("Subject 3"),
                new Subject("Subject 4"),
                new Subject("Subject 4"),
                new Subject("Subject 1"),
                new Subject("Subject 2"),
                new Subject("Subject 3"),
                new Subject("Subject 4"),
                new Subject("Subject 1"),
                new Subject("Subject 2"),
                new Subject("Subject 3"),
                new Subject("Subject 4"),
                new Subject("Subject 4"),
                new Subject("Subject 1"),
                new Subject("Subject 2"),
                new Subject("Subject 3"),
                new Subject("Subject 4"),
                new Subject("Subject 1"),
                new Subject("Subject 2"),
                new Subject("Subject 3"),
                new Subject("Subject 4"),
                new Subject("Subject 4")

        };
        public string testString { get; set; }
        public SubjectSelectionPageViewModel()
        {
            testString = "test";
        }
    }
}
