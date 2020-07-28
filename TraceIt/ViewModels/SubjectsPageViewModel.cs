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
        public ObservableCollection<Subjects> Subjects = new ObservableCollection<Subjects>()
        {
            new Subjects() { Name = "Chemistry" },
            new Subjects() { Name = "Sound Production" },
            new Subjects() { Name = "English" },
            new Subjects() { Name = "Computer Studies" },
            new Subjects() { Name = "Calculus" },

        };

        
    }
}
