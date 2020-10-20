using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Utilities;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class SubjectSelectionPageViewModel
    {
        public ObservableCollection<SelectedSubject> Subjects { get; private set; } = new ObservableCollection<SelectedSubject>();
        public Command ChangeSelectionCommand { get; private set; }

        public SubjectSelectionPageViewModel()
        {
            Task.Run(SetSubjectsAsync).Wait();
            Debug.WriteLine("Subjects finished loading.");
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            ChangeSelectionCommand = new Command<SelectedSubject>(async (subject) => await ChangeSubjectSelection(subject));
        }

        async Task ChangeSubjectSelection(SelectedSubject subject)
        {
            Task task = subject.Selected == false ?
                subject.Select() :
                subject.Deselect();

            await task;
        }

        async Task SetSubjectsAsync() => Subjects = await App.DataService.GetSelectedSubjectsAsync();
    }
}
