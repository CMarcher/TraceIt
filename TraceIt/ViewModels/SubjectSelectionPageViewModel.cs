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
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" },
                new Subject() { Name = "Subject" }

        };

        public SubjectSelectionPageViewModel()
        {
            
        }
    }
}
