using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class SubjectEditingPageViewModel : BaseViewModel
    {
        private ObservableCollection<SelectedSubject> _subjects;
        public ObservableCollection<SelectedSubject> Subjects
        {
            get => _subjects;
            set => SetProperty(ref _subjects, value, nameof(Subjects));
        }
        public Command ChangeSelectionCommand { get; private set; }

        public SubjectEditingPageViewModel()
        {
            SetSubjectsAsync();
            InitialiseCommands();
        }

        private void InitialiseCommands()
            => ChangeSelectionCommand = new Command<SelectedSubject>(async (subject) => await ChangeSubjectSelection(subject));

        private async Task ChangeSubjectSelection(SelectedSubject subject)
        {
            Task task = subject.Selected == false ?
                subject.Select() :
                subject.Deselect();

            await task;
        }

        private void SetSubjectsAsync() => Subjects = App.DataRepository.SelectedSubjects;
    }
}
