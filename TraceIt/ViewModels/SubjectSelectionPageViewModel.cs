using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;

namespace TraceIt.ViewModels
{
    public class SubjectSelectionPageViewModel
    {
        public ObservableCollection<Subject> subjects { get; set; } = new ObservableCollection<Subject>();

        public SubjectSelectionPageViewModel()
        {
            //SetSubjectsAsync().SafeFireAndForget(false); 
            Task.Run(SetSubjectsAsync).Wait();
        }

        async Task SetSubjectsAsync() => subjects = await App.DataService.GetSubjectsAsync();
    }
}
