using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TraceIt.ViewModels
{
    public class SubjectsPageViewModel
    {
        public ObservableCollection<string> Subjects = new ObservableCollection<string>()
        {
            "English"
        };
    }
}
