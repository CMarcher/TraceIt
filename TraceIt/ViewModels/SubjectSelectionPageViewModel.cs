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
        public ObservableCollection<Subjects> subjects = new ObservableCollection<Subjects>();

        public SubjectSelectionPageViewModel()
        {
            SetSubjectsAsync().SafeFireAndForget(false);
        }

        async Task SetSubjectsAsync()
        {
            subjects = await App.DataService.GetSubjectsAsync();
        }
    }
}
