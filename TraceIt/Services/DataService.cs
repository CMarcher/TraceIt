using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using TraceIt.Utilities;
using Xamarin.Essentials;
using System.Linq.Expressions;

namespace TraceIt.Services
{
    public class DataService : IDataService
    {
        public static SQLiteAsyncConnection Database;
        public bool Initialised = false;

        public enum StandardType
        {
            All,
            Achievement,
            Unit
        }

        public enum Level
        {
            All,
            One,
            Two,
            Three
        }

        public enum FilterByOption
        {
            Subfield,
            Subject
        }

        public static readonly Func<Task<SQLiteAsyncConnection>> CreateLazyConnection = new Func<Task<SQLiteAsyncConnection>>(async () =>
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
                Database = await CreateLazyConnection();
                Initialised = true;
            }
        }

        public async Task<ObservableCollection<AssessmentStandard>> GetStandardsAsync()
        {
            var assessmentStandards = await Database.Table<AssessmentStandard>().ToListAsync();
            return new ObservableCollection<AssessmentStandard>(assessmentStandards);
        }

        public async Task<ObservableCollection<AssessmentStandard>> GetFilteredStandardsAsync(string parameter, FilterByOption filterByOption)
        {
            Expression<Func<AssessmentStandard, bool>> subjectQuery = standard => standard.Subject == parameter;
            Expression<Func<AssessmentStandard, bool>> subfieldQuery = standard => standard.Subfield == parameter;
            Expression<Func<AssessmentStandard, bool>> finalQuery;

            bool isSubjectQuery = filterByOption == FilterByOption.Subject;
            if (isSubjectQuery)
                finalQuery = subjectQuery;
            else
                finalQuery = subfieldQuery;

            var standards = await Database.Table<AssessmentStandard>().Where(finalQuery).ToListAsync();
            return new ObservableCollection<AssessmentStandard>(standards);
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

        public async Task<List<SubfieldModel>> GetSubfieldsAsync(StandardType filterOptions)
        {
            switch (filterOptions)
            {
                case StandardType.All:
                    return await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards");

                case StandardType.Achievement:
                    return await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards" +
                        "WHERE Standard_Type = 'A'");

                case StandardType.Unit:
                    return await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards " + 
                        "WHERE Standard_Type = 'U'");
                    
                default:
                    return null;
            }
        }

    }
}
