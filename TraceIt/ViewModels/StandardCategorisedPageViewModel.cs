using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using TraceIt.Utilities;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class StandardCategorisedPageViewModel : BaseViewModel
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public List<SubfieldModel> Subfields { get; set; }
        public Command ChangeStandardSelectionCommand { get; private set; }

        public StandardCategorisedPageViewModel()
        {
            Task.Run(async () =>
            {
                await SetSubjects();
                await SetSubfields();
            }).Wait();

            ChangeStandardSelectionCommand = new Command<Standard>(async (standard) => await ChangeStandardSelection(standard));
        }

        async Task SetSubjects() => Subjects = await App.DataService.GetSubjectsAsync();

        async Task SetSubfields() => Subfields = await App.DataService.GetSubfieldsAsync(Services.DataService.StandardType.Unit);

        async Task ChangeStandardSelection(Standard standard)
        {
            var subject = StatusTracker.CurrentSubject;

            Task task = standard.Selected == false ?
                subject.AddStandardAsync(standard) :
                subject.RemoveStandardAsync(standard);

            await task;
        }

    }
}
