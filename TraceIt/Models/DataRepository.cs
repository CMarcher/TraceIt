using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TraceIt.Services;
using TraceIt.Models.Query_Models;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    public class DataRepository : BaseModel
    {
        private ObservableCollection<SelectedSubject> _selectedSubjects;
        public ObservableCollection<SelectedSubject> SelectedSubjects
        {
            get => _selectedSubjects;
            private set => SetProperty(ref _selectedSubjects, value, nameof(SelectedSubjects));
        }

        private ObservableCollection<Standard> _selectedStandards;
        public ObservableCollection<Standard> SelectedStandards
        {
            get => _selectedStandards;
            set => SetProperty(ref _selectedStandards, value, nameof(SelectedStandards));
        }


        public bool Initialised { get; private set; }

        public DataRepository() { }

        void SubscribeToMessages()
        {
        }

        public async Task InitialiseAsync()
        {
            await Task.Run(InitialiseSubjects);
            await Task.Run(InitialiseStandards);

            App.MessagingService.Send(MessagingService.MessageType.RepositoryInitialisationComplete);
            App.MessagingService.Send(MessagingService.MessageType.RefreshStandards);

            SubscribeToMessages();
        }

        async Task InitialiseSubjects()
        {
            var selectedSubjects = await App.DataService.GetSelectedSubjectsAsync();
            SelectedSubjects = selectedSubjects.OrderBy(x => x.BaseSubject.Name).ToObservableCollection();

            foreach (var subject in SelectedSubjects)
                await subject.InitialiseStandards();

            Initialised = true;
        }

        async Task InitialiseStandards()
            => SelectedStandards = await App.DataService.GetSelectedStandardsAsync();
    }
}
