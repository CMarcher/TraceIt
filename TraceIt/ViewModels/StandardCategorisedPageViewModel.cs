using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using Xamarin.Forms;

namespace TraceIt.ViewModels
{
    public class StandardCategorisedPageViewModel : BaseViewModel
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public List<SubfieldModel> Subfields { get; set; }

        public StandardCategorisedPageViewModel()
        {
            SetSubjects().SafeFireAndForget(false);
            SetSubfields().SafeFireAndForget(false);
        }

        async Task SetSubjects() => Subjects = await App.DataService.GetSubjectsAsync();

        async Task SetSubfields() => Subfields = await App.DataService.GetSubfieldsAsync(Services.DataService.FilterOptions.Unit);

    }
}
