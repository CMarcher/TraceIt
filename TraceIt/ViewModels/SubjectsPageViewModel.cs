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
        public ObservableCollection<SelectedSubject> Subjects = new ObservableCollection<SelectedSubject>()
        {
            new SelectedSubject("Chemistry", 24, 5),
            new SelectedSubject("Calculus", 24, 5),
            new SelectedSubject("Computer Studies", 24, 5),
            new SelectedSubject("Sound Production", 24, 5),
            new SelectedSubject("Drama", 24, 5)
        };

        bool isVisible = false;   // The button is invisible by default

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }

            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }
    }
}
