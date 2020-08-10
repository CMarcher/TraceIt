using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TraceIt.Services;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardCategorisedDetailPage : ContentPage
    {
        public StandardCategorisedDetailPage(string parameter, DataService.FilterByOption filterByOption)
        {
            InitializeComponent();
            Task.Run(() => SetStandards(parameter, filterByOption)).Wait();
        }

        async Task SetStandards(string parameter, DataService.FilterByOption filterByOption)
        {
            categorisedStandardsList.ItemsSource = await App.DataService.GetFilteredStandardsAsync(
                parameter, filterByOption);
        }
    }
}