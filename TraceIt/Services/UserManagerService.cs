using System;
using System.Collections.Generic;
using System.Text;
using TraceIt.Models;
using Xamarin.Essentials;

namespace TraceIt.Services
{
    public class UserManagerService : BaseModel
    {
        private string _username;
        public string Username
        {
            get => _username;
            private set => SetProperty(ref _username, value, nameof(Username));
        }

        private bool _loggedIn;
        public bool LoggedIn
        {
            get => _loggedIn;
            private set => SetProperty(ref _loggedIn, value, nameof(LoggedIn));
        }

        public bool SubjectsSelected { get; private set; }
        public int SelectedYear { get; set; }
        public string ReviewedVersion { get; private set; }
        public DateTime ReviewedDate { get; private set; }

        string usernameKey = "Username";
        string loggedInKey = "LoggedIn";
        string subjectsSelectedKey = "SubjectsSelected";
        string selectedYearKey = "SelectedYear";
        string reviewedVersionKey = "ReviewedVersion";
        string reviewedDateKey = "ReviewedDate";

        public UserManagerService()
        {
            CreatePreferencesIfNotSet();
            Username = GetUsername();
            LoggedIn = GetLoginStatus();
            ReviewedVersion = GetReviewedVersion();
            ReviewedDate = GetReviewedDate();
        }

        private void CreatePreferencesIfNotSet()
        {
            bool loggedInKeyInitialised = Preferences.ContainsKey(loggedInKey);
            bool usernameKeyInitialised = Preferences.ContainsKey(usernameKey);
            bool subjectsSelectedKeyInitialised = Preferences.ContainsKey(subjectsSelectedKey);
            bool selectedYearKeyInitialised = Preferences.ContainsKey(selectedYearKey);
            bool reviewedVersionKeyInitialised = Preferences.ContainsKey(reviewedVersionKey);
            bool reviewedDateKeyInitialised = Preferences.ContainsKey(reviewedDateKey);

            if (loggedInKeyInitialised is false)
                SetLoginStatus();
            if (usernameKeyInitialised is false)
                SetUsername();
            if (subjectsSelectedKeyInitialised is false)
                SetSubjectsSelected();
            if (selectedYearKeyInitialised is false)
                SetSelectedYear();
            if (reviewedVersionKeyInitialised is false)
                SetReviewedVersion();
            if (reviewedDateKeyInitialised is false)
                SetReviewedDate();
        }

        public void SetUsername(string name = null)
        {
            Preferences.Set(usernameKey, name);
            Username = GetUsername();
        }

        private string GetUsername()
            => Preferences.Get(usernameKey, null);

        public void SetLoginStatus(bool loggedIn = false)
        {
            Preferences.Set(loggedInKey, loggedIn);
            LoggedIn = GetLoginStatus();
        }

        public bool GetLoginStatus()
            => Preferences.Get(loggedInKey, false);

        public void SetSubjectsSelected(bool subjectsSelected = false)
        {
            Preferences.Set(subjectsSelectedKey, subjectsSelected);
            SubjectsSelected = GetSubjectsSelected();
        }

        private bool GetSubjectsSelected()
            => Preferences.Get(subjectsSelectedKey, false);

        public void SetSelectedYear(int year = 2021)
        {
            Preferences.Set(selectedYearKey, year);
            SelectedYear = GetSelectedYear();
        }

        private int GetSelectedYear()
            => Preferences.Get(selectedYearKey, 2021);

        public void SetReviewedVersion(string version = null)
        {
            Preferences.Set(reviewedVersionKey, version);
            ReviewedVersion = version;
        }

        private string GetReviewedVersion()
            => Preferences.Get(reviewedVersionKey, null);

        public void SetReviewedDate(DateTime reviewDate = new DateTime())
        {
            Preferences.Set(reviewedDateKey, reviewDate);
            ReviewedDate = reviewDate;
        }

        private DateTime GetReviewedDate()
            => Preferences.Get(reviewedDateKey, new DateTime());
    }
}
