﻿using System;
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
        public ObservableCollection<Subject> subjects { get; private set; } = new ObservableCollection<Subject>();

        public Command UpdateSubjectCommand { get; private set; }

        public SubjectSelectionPageViewModel()
        {
            UpdateSubjectCommand = new Command<Subject>(async (subject) => await UpdateSubjectAsync(subject));

            Task.Run(SetSubjectsAsync).Wait();
        }

        async Task SetSubjectsAsync() => subjects = await App.DataService.GetSubjectsAsync();

        async Task UpdateSubjectAsync(Subject subject)
        {
            if (subject.Selected == false)
                subject.Selected = true;
            else
                subject.Selected = false;

            await App.DataService.UpdateSubjectAsync(subject);
        }
    }
}
