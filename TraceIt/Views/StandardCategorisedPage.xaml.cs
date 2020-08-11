using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ViewModel = new StandardCategorisedPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetItemsSource();
        }

        void SetItemsSource()
        {
            bool unitFilterSelected = assessmentSelector.SelectedIndex == 1;
            if (unitFilterSelected)
                listView.ItemsSource = ViewModel.Subfields;
            else
                listView.ItemsSource = ViewModel.Subjects;
        }

        private void assessmentSelector_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            SetItemsSource();
        }

        private void collectionViewCategories_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try
            {
                var item = (SubfieldModel)e.ItemData;
                Navigation.PushAsync(new StandardCategorisedDetailPage(item.Subfield, Services.DataService.FilterByOption.Subfield));
            }
            catch { DisplayAlert("Error!", "Don't select subject items!", "I'm sorry!"); }
        }

        private void closeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(listView.DataSource != null)
            //{
            //    listView.DataSource.Filter = FilterItems;
            //    listView.DataSource.Refresh();
            //}

            if (searchBar.Text != null)
                listView.ItemsSource = await GetFilteredStandards(searchBar.Text);
            else
                SetItemsSource();
        }

        //private bool FilterItems(object obj)
        //{
        //    if (searchBar.Text is null)
        //        return true;

        //    if(obj is Subject)
        //    {

        //    }
        //}

        async Task<List<AssessmentStandard>> GetFilteredStandards(string search)
        {
            return await App.DataService.GetMatchingStandards(search);
        }
    }
}