using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
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
    public partial class SubjectsPage : BasePage
    {
        public SubjectsPage()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            yearPicker.SelectedIndex = GetYear();
            SetTitle();
        }

        private void SetTitle()
        {
            var usermanager = App.UserManagerService;
            var username = usermanager.Username;

            Title = username + "'s Subjects";
        }

        private int GetYear()
        {
            var year = StatusTracker.CurrentYear;

            if (year is 2017)
                return 0;
            if (year is 2018)
                return 1;
            if (year is 2019)
                return 2;
            if (year is 2020)
                return 3;
            else
                return 3;
        }

        private async void buttonViewInfo_Clicked(object sender, EventArgs e)
        {
            var subject = (sender as Button)?.BindingContext as SelectedSubject;
            StatusTracker.CurrentSubject = subject;

            await Navigation.PushAsync(new SelectedStandardsPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            loadingView.IsVisible = false;
            loadingView.IsRunning = false;
        }

        private void yearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewSubjects.DataSource.Filter = FilterSubject;
            listViewSubjects.DataSource.Refresh();
            listViewSubjects.DataSource.RefreshFilter();
        }

        bool FilterSubject(object item)
        {
            if (listViewSubjects.DataSource is null || yearPicker.SelectedIndex == -1)
                return true;

            var subject = item as SelectedSubject;
            int year = int.Parse((string)yearPicker.SelectedItem);
            bool matchesSelectedYear = subject.Year == year;
            bool isSelected = subject.Selected;

            return matchesSelectedYear && isSelected;
        }

        private async void addSubjectsToolbarItem_Clicked(object sender, EventArgs e)
            => await Navigation.PushModalAsync(new NavigationPage(new SubjectEditingPage()));
    }
}