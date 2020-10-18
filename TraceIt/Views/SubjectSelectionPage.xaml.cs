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
            Title = "Select Subjects for " + StatusTracker.CurrentYear;
        }

        private async void buttonConfirm_Clicked(object sender, EventArgs e)
        {
            await App.DataService.UpdateSubjectsAsync(GetSelectedItems());
            Application.Current.MainPage = new ShellHomePage();
        }

        private async void addButton_Clicked(object sender, EventArgs e)
        {
            string subjectName = await DisplayPromptAsync("Add New Subject", "Enter the name of your subject", "OK", "Cancel", "Subject name");
            var viewmodel = BindingContext as SubjectSelectionPageViewModel;

            viewmodel.Subjects.Add(new Subject()
            {
                Name = subjectName,
                Custom = true
            });
        }

        private async void closeButton_Clicked(object sender, EventArgs e)
            => await Navigation.PopModalAsync();

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (subjectsListView.DataSource != null)
            {
                subjectsListView.DataSource.Filter = FilterSubjects;
                subjectsListView.DataSource.Refresh();
            }
        }

        private bool FilterSubjects(object obj)
        {
            if (searchBar.Text == null)
                return true;

            var subject = obj as Subject;
            if (subject.Name.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }

        private List<Subject> GetSelectedItems()
        {
            return subjectsListView.SelectedItems.ToListWithAction<Subject>(
                (subject) =>
                {
                    subject.Selected = true;
                    subject.Year = StatusTracker.CurrentYear; });
        }
    }
}