using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TraceIt.Models;

namespace TraceIt.ViewModels
{
    public class SubjectsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Subject> Subjects = new ObservableCollection<Subject>()
        {
            new Subject() { Name = "Chemistry" },
            new Subject() { Name = "Sound Production" },
            new Subject() { Name = "English" },
            new Subject() { Name = "Computer Studies" },
            new Subject() { Name = "Calculus" }
        };

        
    }
}
