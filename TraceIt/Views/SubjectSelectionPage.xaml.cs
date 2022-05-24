using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectSelectionPage : ContentPage
    {
        public SubjectSelectionPage()
        {
            InitializeComponent();
            Initialise();
        }

        void Initialise()
        {
            Title = "Select Subjects for " + StatusTracker.CurrentYear;
            RefreshDatasource();
        }

        void RefreshDatasource()
        {
            subjectsListView.DataSource.Filter = FilterSubjects;
            subjectsListView.DataSource.Refresh();
        }

        private void buttonConfirm_Clicked(object sender, EventArgs e)
        {
            var viewmodel = BindingContext as SubjectSelectionPageViewModel;

            Application.Current.MainPage = new ShellHomePage();
        }

        private async void addButton_Clicked(object sender, EventArgs e)
        {
            string subjectName = await DisplayPromptAsync("Add New Subject", "Enter the name of your subject", "OK", "Cancel", "Subject name");
            var viewmodel = BindingContext as SubjectSelectionPageViewModel;

            // Add custom subject adding here...
        }

        private async void closeButton_Clicked(object sender, EventArgs e)
            => await Navigation.PopModalAsync();

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (subjectsListView.DataSource != null)
                RefreshDatasource();
        }

        private bool FilterSubjects(object item)
        {
            if (subjectsListView.DataSource is null)
                return true;

            var subject = item as SelectedSubject;
            int year = StatusTracker.CurrentYear;
            bool matchesSelectedYear = subject.Year == year;
            bool searchCriteriaMatched;

            if (searchBar.Text == null)
                searchCriteriaMatched = true;
            else
                searchCriteriaMatched = subject.BaseSubject.Name.ToLower().Contains(searchBar.Text.ToLower());

            return matchesSelectedYear is true && searchCriteriaMatched is true;
        }
    }
}