using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class SubjectSelectionPageViewModel
    {
        public ObservableCollection<Subject> Subjects { get; private set; } = new ObservableCollection<Subject>();

        public Command UpdateSubjectCommand { get; private set; }

        public SubjectSelectionPageViewModel()
        {
            UpdateSubjectCommand = new Command<Subject>(async (subject) => await UpdateSubjectAsync(subject));

            Task.Run(SetSubjectsAsync).Wait();
        }

        async Task SetSubjectsAsync() => Subjects = await App.DataService.GetSubjectsAsync();

        async Task UpdateSubjectAsync(Subject subject)
        {
            subject.Selected = !subject.Selected;

            await App.DataService.UpdateOrInsertSubjectAsync(subject);
        }
    }
}
