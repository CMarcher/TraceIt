﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TraceIt.Services;
using TraceIt.Models.Query_Models;

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

        public ObservableCollection<CreditBreakdown> CreditBreakdowns = new ObservableCollection<CreditBreakdown>();

        public bool Initialised { get; private set; }

        public DataRepository() { }

        public async Task InitialiseAsync()
        {
            await Task.Run(InitialiseSubjects);
            await Task.Run(InitialiseStandards);
            await Task.Run(InitialiseBreakdown);

            App.MessagingService.Send(MessagingService.MessageType.RepositoryInitialisationComplete);
            App.MessagingService.Send(MessagingService.MessageType.RefreshStandards);
        }

        async Task InitialiseSubjects()
        {
            SelectedSubjects = await App.DataService.GetSelectedSubjectsAsync();

            foreach (var subject in SelectedSubjects)
                await subject.InitialiseStandards();

            Initialised = true;
        }

        async Task InitialiseStandards()
            => SelectedStandards = await App.DataService.GetSelectedStandardsAsync();

        async Task InitialiseBreakdown()
            => CreditBreakdowns = await App.DataService.GetCreditBreakdownsAsync();
        
        public async Task RemoveSubject(SelectedSubject subject)
        {
            SelectedSubjects.Remove(subject);
            await subject.Delete();
        }
    }
}
