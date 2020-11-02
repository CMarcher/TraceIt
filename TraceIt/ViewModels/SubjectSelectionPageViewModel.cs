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
    public class SubjectSelectionPageViewModel : BaseViewModel
    {
        private ObservableCollection<SelectedSubject> _subjects;
        public ObservableCollection<SelectedSubject> Subjects
        {
            get => _subjects;
            set => SetProperty(ref _subjects, value, nameof(Subjects));
        }

        private ObservableCollection<SelectedSubject> _customSubjects;
        public ObservableCollection<SelectedSubject> CustomSubjects
        {
            get => _customSubjects;
            set => SetProperty(ref _customSubjects, value, nameof(CustomSubjects));
        }

        public Command ChangeSelectionCommand { get; private set; }

        public SubjectSelectionPageViewModel()
        {
            SetSubjectsAsync();
            Debug.WriteLine("Subjects finished loading.");
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
