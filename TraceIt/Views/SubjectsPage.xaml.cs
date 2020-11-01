using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
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
    public partial class SubjectsPage : BasePage
    {
        public SubjectsPage()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            yearPicker.SelectedIndex = GetIndexFromYear();
            SetTitle();
            SubscribeToMessages();
            SetUserHasLoggedIn();
        }

        private void SetTitle()
        {
            var usermanager = App.UserManagerService;
            var username = usermanager.Username;

            Title = username + "'s Subjects";
        }

        private void SubscribeToMessages()
        {
            App.MessagingService.Subscribe(this, Services.MessagingService.MessageType.RefreshDataSource,
                (sender) => RefreshDataSource());
        }

        private void SetUserHasLoggedIn()
        {
            var usermanager = App.UserManagerService;
            usermanager.SetLoginStatus(true);
        }

        private int GetIndexFromYear()
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

        private int GetYearFromIndex()
        {
            var index = yearPicker.SelectedIndex;

            if (index is 0)
                return 2017;
            else if (index is 1)
                return 2018;
            else if (index is 2)
                return 2019;
            else if (index is 3)
                return 2020;
            else
                return 2020;
        }

        private async void buttonViewInfo_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var subject = button?.BindingContext as SelectedSubject;
            StatusTracker.CurrentSubject = subject;

            button.SetEnabledForAndroid(false);
            await Navigation.PushAsync(new SelectedStandardsPage());
            button.SetEnabledForAndroid(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetTitle();
        }

        private void yearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataSource();
            StatusTracker.CurrentYear = GetYearFromIndex();
        }

        private void RefreshDataSource()
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
        {
            var button = sender as ToolbarItem;

            button.SetEnabledForAndroid(false);
            await Navigation.PushModalAsync(new NavigationPage(new SubjectEditingPage()));
            button.SetEnabledForAndroid(true);
        }
    }
}