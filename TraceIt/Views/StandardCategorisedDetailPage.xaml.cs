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
            Initialise(parameter, filterByOption);
        }

        private void Initialise(string parameter, DataService.FilterOption filterByOption)
        {
            ViewModel = new StandardCategorisedDetailPageViewModel(parameter, filterByOption);
            BindingContext = ViewModel;

            Title = parameter;
            categorisedStandardsList.ItemsSource = ViewModel.Standards;
            InitialiseOrRefreshDataSource();
        }

        private void InitialiseOrRefreshDataSource()
        {
            categorisedStandardsList.DataSource.Filter = FilterStandards;
            categorisedStandardsList.DataSource.Refresh();
            categorisedStandardsList.DataSource.RefreshFilter();
        }

        private async void categorisedStandardsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new StandardDetailPage(e.ItemData as Standard));
        }

        private bool FilterStandards(object item)
        {
            if (categorisedStandardsList.DataSource is null)
                return true;

            var standard = item as Standard;
            bool matchesSelectedLevel = standard.Level == GetLevelFromIndex();

            return matchesSelectedLevel;
        }

        private Standard.Levels GetLevelFromIndex()
        {
            int index = levelSelector.SelectedIndex;

            if (index is 0)
                return Standard.Levels.One;
            else if (index is 1)
                return Standard.Levels.Two;
            else if (index is 2)
                return Standard.Levels.Three;
            else
                return Standard.Levels.One;
        }

        private void levelSelector_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
            => InitialiseOrRefreshDataSource();
    }
}