﻿using System;
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

        private async void collectionViewCategories_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            await PushPage(e.ItemData);
        }

        private async Task PushPage(object item)
        {
            if (item is SubfieldModel)
                await Navigation.PushAsync(new StandardCategorisedDetailPage(((SubfieldModel)item).Subfield, Services.DataService.FilterOption.Subfield));
            else if (item is Subject)
                await Navigation.PushAsync(new StandardCategorisedDetailPage(((Subject)item).Name, Services.DataService.FilterOption.Subject));
            else if (item is Standard)
                await Navigation.PushAsync(new StandardDetailPage((Standard)item));
            else
                throw new Exception("Invalid item type: " + item.GetType());

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

            if (searchBar.Text != "")
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

        async Task<List<Standard>> GetFilteredStandards(string search)
        {
            return await App.DataService.GetMatchingStandards(search);
        }
    }
}