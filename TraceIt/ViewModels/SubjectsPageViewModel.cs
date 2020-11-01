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
        private ObservableCollection<SelectedSubject> _subjects;
        public ObservableCollection<SelectedSubject> Subjects
        {
            get => _subjects;
            set => SetProperty(ref _subjects, value, nameof(Subjects));
        }

        public Command RemoveSubjectCommand { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, nameof(IsBusy));
        }

        public SubjectsPageViewModel()
        {
            SetSubjects();
            SetCommands();
            IsBusy = true;

            App.MessagingService.Subscribe(this, Services.MessagingService.MessageType.RepositoryInitialisationComplete,
                (sender) => { SetSubjects(); IsBusy = false; });

            if (Subjects != null)
                IsBusy = false;
        }

        private void SetSubjects()
            => Subjects = App.DataRepository.SelectedSubjects;

        private void SetCommands()
            => RemoveSubjectCommand = new Command<SelectedSubject>(async (subject) => await RemoveSubject(subject));
        
        private async Task RemoveSubject(SelectedSubject subject)
            => await subject.Deselect();
    }
}
