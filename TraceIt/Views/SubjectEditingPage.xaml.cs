using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectEditingPage : ContentPage
    {
        public SubjectEditingPage()
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

        private async void closeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            App.MessagingService.Send(Services.MessagingService.MessageType.RefreshDataSource);
        }

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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var subject = (sender as Frame).BindingContext as SelectedSubject;
            var viewmodel = BindingContext as SubjectEditingPageViewModel;
            bool isBeingDeselected = subject.Selected is true;
            bool deselectionConfirmed;

            string title = "Subject being removed";
            string message = "You are about to remove this subject, which will clear all standards you have added. Continue?";
            string confirm = "Yes", cancel = "Never mind";

            if (isBeingDeselected)
            {
                deselectionConfirmed = await DisplayAlert(title, message, confirm, cancel);

                if (deselectionConfirmed)
                    viewmodel.ChangeSelectionCommand.Execute(subject);
            }
            else
                viewmodel.ChangeSelectionCommand.Execute(subject);
        }
    }
}