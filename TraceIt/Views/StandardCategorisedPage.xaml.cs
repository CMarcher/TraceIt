using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardCategorisedPage : ContentPage
    {
        StandardCategorisedPageViewModel ViewModel;

        public StandardCategorisedPage()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            ViewModel = new StandardCategorisedPageViewModel();
            BindingContext = ViewModel;
            SetItemsSource();
        }

        void SetItemsSource()
        {
            bool unitFilterSelected = assessmentSelector.SelectedIndex == 1;
            if (unitFilterSelected)
                listView.ItemsSource = ViewModel.Subfields;
            else
                listView.ItemsSource = ViewModel.Subjects;

            searchBar.Text = "";
        }

        private void assessmentSelector_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
            => SetItemsSource();
        
        private async void collectionViewCategories_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
            => await PushPage(e.ItemData);
        
        private async Task PushPage(object item)
        {
            if (item is SubfieldModel model)
                await Navigation.PushAsync(new StandardCategorisedDetailPage(model.Subfield, Services.DataService.FilterOption.Subfield));
            else if (item is Subject subject)
                await Navigation.PushAsync(new StandardCategorisedDetailPage(subject.Name, Services.DataService.FilterOption.Subject));
            else if (item is Standard standard)
                await Navigation.PushAsync(new StandardDetailPage(standard));
            else
                throw new Exception("Invalid item type: " + item.GetType());

        }

        private async void closeButton_Clicked(object sender, EventArgs e)
            => await Navigation.TryPopModalAsync();
        
        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text != "" && searchBar.Text != null)
            {
                searchBar.CheckForInvalidCharacters();
                ResetSearchBarIfEmpty();
                listView.ItemsSource = await GetFilteredStandards(searchBar.Text);
            }
                
            else
                SetItemsSource();
        }

        private void ResetSearchBarIfEmpty()
        {
            string text = searchBar.Text;
            bool searchBarEmpty = text is "" || text is null;

            if (searchBarEmpty)
                SetItemsSource();
        }

        async Task<List<Standard>> GetFilteredStandards(string search)
            => await App.DataService.GetMatchingStandards(search);

        private void searchBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            bool textChanged = e.PropertyName == "Text";

            if (textChanged)
                ResetSearchBarIfEmpty();
        }
    }
}