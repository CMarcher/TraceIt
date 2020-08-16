using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;

namespace TraceIt.ViewModels
{
    public class SubjectsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Subject> Subjects { get; private set; }

        public SubjectsPageViewModel()
        {
            Task.Run(SetSubjects).Wait();
        }

        private async Task SetSubjects()
        {
            Subjects = await App.DataService.GetSelectedSubjectsAsync();
        }

        
    }
}
