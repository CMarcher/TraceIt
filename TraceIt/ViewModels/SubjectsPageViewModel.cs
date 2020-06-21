using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TraceIt.Models;

namespace TraceIt.ViewModels
{
    public class SubjectsPageViewModel
    {
        public ObservableCollection<SelectedSubject> Subjects = new ObservableCollection<SelectedSubject>()
        {
            new SelectedSubject("Chemistry", 24, 5),
            new SelectedSubject("Calculus", 24, 5),
            new SelectedSubject("Computer Studies", 24, 5),
            new SelectedSubject("Sound Production", 24, 5),
            new SelectedSubject("Drama", 24, 5)
        };
    }
}
