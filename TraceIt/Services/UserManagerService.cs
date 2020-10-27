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

        string usernameKey = "Username";
        string loggedInKey = "LoggedIn";
        string subjectsSelectedKey = "SubjectsSelected";
        string selectedYearKey = "SelectedYear";

        public UserManagerService()
        {
            CreatePreferencesIfNotSet();
            Username = GetUsername();
            LoggedIn = GetLoginStatus();
        }

        private void CreatePreferencesIfNotSet()
        {
            bool preferencesIntialised =
                Preferences.ContainsKey(loggedInKey) &&
                Preferences.ContainsKey(usernameKey) &&
                Preferences.ContainsKey(subjectsSelectedKey) &&
                Preferences.ContainsKey(selectedYearKey);

            if(preferencesIntialised is false)
            {
                SetUsername();
                SetLoginStatus();
                SetSubjectsSelected();
                SetSelectedYear();
            }
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

        public void SetSelectedYear(int year = 2020)
        {
            Preferences.Set(selectedYearKey, year);
            SelectedYear = GetSelectedYear();
        }

        private int GetSelectedYear()
            => Preferences.Get(selectedYearKey, 2020);
    }
}
