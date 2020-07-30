﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using TraceIt.Utilities;
using Xamarin.Essentials;

namespace TraceIt.Services
{
    public class DataService : IDataService
    {
        public static SQLiteAsyncConnection Database;
        public bool Initialised = false;

        public enum FilterOptions
        {
            All,
            Achievement,
            Unit
        }

        public static readonly Lazy<Task<SQLiteAsyncConnection>> CreateLazyConnection = new Lazy<Task<SQLiteAsyncConnection>>(async () =>
        {
            var databaseName = FileNames.DatabaseName;
            string documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(documentsDirectory, databaseName);

            //File.Delete(databasePath); // Removed for debugging purposes.

            bool fileExists = File.Exists(databasePath);
            if (!fileExists)
            {
                using (var assetStream = await FileSystem.OpenAppPackageFileAsync(databaseName))
                using (var fileStream = new FileStream(databasePath, FileMode.Create, FileAccess.Write))
                {
                    await assetStream.CopyToAsync(fileStream);
                }
            }

            var connection = new SQLiteAsyncConnection(databasePath, Constants.Flags);
            return connection;
        });

        public DataService()
        {
            InitialiseAsync().SafeFireAndForget(false);
        }

        async Task InitialiseAsync()
        {
            if (!Initialised)
            {
                Database = await CreateLazyConnection.Value;
                Initialised = true;
            }
        }

        public async Task<ObservableCollection<AssessmentStandard>> GetAssessmentStandardsAsync()
        {
            var assessmentStandards = await Database.Table<AssessmentStandard>().ToListAsync();
            return new ObservableCollection<AssessmentStandard>(assessmentStandards);
        }

        public async Task<AssessmentStandard> GetStandardByIDAsync(int id)
        {
            var standard = await Database.Table<AssessmentStandard>()
                .Where(s => s.ID == id)
                .FirstOrDefaultAsync();

            return standard;
        }

        public async Task UpdateStandardAsync(AssessmentStandard standard)
        {
            await Database.UpdateAsync(standard);
        }

        public async Task<ObservableCollection<Subject>> GetSubjectsAsync()
        {
            var subjects = await Database.Table<Subject>().ToListAsync();
            return new ObservableCollection<Subject>(subjects);
        }

        public async Task<ObservableCollection<Subject>> GetSelectedSubjectsAsync()
        {
            var subjects = await Database.Table<Subject>()
                .Where(s => s.Selected == "true")
                .ToListAsync();

            return new ObservableCollection<Subject>(subjects);
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            await Database.UpdateAsync(subject);
        }

        public async Task GetSubfieldsAsync(FilterOptions filterOptions)
        {
            List<SubfieldModel> subfields;

            switch (filterOptions)
            {
                case FilterOptions.All:
                    subfields = await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards");
                    break;
                case FilterOptions.Achievement:
                    subfields = await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards" +
                        "WHERE Standard_Type = 'A'");
                    break;
                case FilterOptions.Unit:
                    subfields = await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards" +
                        "WHERE Standard_Type = 'U'");
                    break;
            }
        }

    }
}
