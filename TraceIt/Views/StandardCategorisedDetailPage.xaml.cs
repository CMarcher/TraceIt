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
using TraceIt.ViewModels;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardCategorisedDetailPage : ContentPage
    {
        StandardCategorisedDetailPageViewModel ViewModel;

        public StandardCategorisedDetailPage(string parameter, DataService.FilterOption filterByOption)
        {
            InitializeComponent();
            ViewModel = new StandardCategorisedDetailPageViewModel(parameter, filterByOption);
            BindingContext = ViewModel;

            Title = parameter;
            categorisedStandardsList.ItemsSource = ViewModel.Standards;
        }

        private void categorisedStandardsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new StandardDetailPage(e.ItemData as Standard));
        }
    }
}