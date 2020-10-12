using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class SubjectsPageViewModel : BaseViewModel
    {
        private ObservableCollection<Subject> _subjects;
        public ObservableCollection<Subject> Subjects
        {
            get => _subjects;
            set => SetProperty(ref _subjects, value, nameof(Subjects));
        }

        public Command RemoveSubjectCommand { get; private set; }

        public SubjectsPageViewModel()
        {
            SetSubjects();
            SetCommands();
        }

        public static async Task<SubjectsPageViewModel> InitialiseAsync()
        {
            await Task.Run(App.DataRepository.InitialiseAsync);
            var viewmodel = new SubjectsPageViewModel();
            return viewmodel;
        }

        private void SetSubjects()
            => Subjects = App.DataRepository.SelectedSubjects;

        private void SetCommands()
            => RemoveSubjectCommand = new Command<Subject>(async (subject) => await RemoveSubject(subject));
        
        private async Task RemoveSubject(Subject subject)
            => await App.DataRepository.RemoveSubject(subject);

    }
}
