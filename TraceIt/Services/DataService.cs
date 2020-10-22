using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using TraceIt.Utilities;
using Xamarin.Essentials;
using System.Linq.Expressions;
using SQLiteNetExtensionsAsync.Extensions;

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

        public enum FilterOption
        {
            Subfield,
            Subject
        }

        public static readonly Func<Task<SQLiteAsyncConnection>> CreateConnection = new Func<Task<SQLiteAsyncConnection>>(async () =>
        {
            var databaseName = FileNames.DatabaseName;
            string documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(documentsDirectory, databaseName);

            bool fileExists = File.Exists(databasePath);
            if (!fileExists)
            {
                using (var assetStream = await FileSystem.OpenAppPackageFileAsync(databaseName))
                using (var fileStream = new FileStream(databasePath, FileMode.Create, FileAccess.Write))
                    await assetStream.CopyToAsync(fileStream);
            }

            var connection = new SQLiteAsyncConnection(databasePath, Constants.Flags);
            return connection;
        });

        public DataService()
        {
            Task.Run(InitialiseAsync);
        }

        async Task InitialiseAsync()
        {
            if (!Initialised)
            {
                Database = await CreateConnection();
                Initialised = true;
            }
        }

        #region Standard methods
        public async Task<ObservableCollection<Standard>> GetStandardsAsync()
        {
            var assessmentStandards = await Database.Table<Standard>().ToListAsync();
            return new ObservableCollection<Standard>(assessmentStandards);
        }

        public async Task<ObservableCollection<Standard>> GetCategorisedStandardsAsync(string parameter, FilterOption filterByOption)
        {
            Expression<Func<Standard, bool>> subjectQuery = standard => standard.Subject == parameter;
            Expression<Func<Standard, bool>> subfieldQuery = standard => standard.Subfield == parameter
                                                                      && standard.StandardType == Standard.StandardTypes.Unit;
            Expression<Func<Standard, bool>> finalQuery;

            bool isSubjectQuery = filterByOption == FilterOption.Subject;
            if (isSubjectQuery)
                finalQuery = subjectQuery;
            else
                finalQuery = subfieldQuery;

            var standards = await Database.Table<Standard>().Where(finalQuery).ToListAsync();
            return new ObservableCollection<Standard>(standards);
        }

        public async Task<Standard> GetStandardByIDAsync(int id)
        {
            var standard = await Database.Table<Standard>()
                .Where(s => s.ID == id)
                .FirstOrDefaultAsync();

            return standard;
        }

        public async Task<ObservableCollection<Standard>> GetStandardsForSubjectAsync(string subjectName)
        {
            var list = await Database.QueryAsync<Standard>(
                "SELECT * FROM AssessmentStandards " +
                "WHERE AddedTo = '" + subjectName + "' " +
                "ORDER BY Title;"
                );

            return list.ToObservableCollection<Standard>();
        }

        public async Task<ObservableCollection<Standard>> GetSelectedStandardsAsync()
        {
            var result = await Database.Table<Standard>()
                .Where(standard => standard.Selected == true)
                .ToListAsync();

            return result.ToObservableCollection<Standard>();
        }

        public async Task<List<Standard>> GetMatchingStandards(string searchQuery)
        {
            return await Database.QueryAsync<Standard>(
                "SELECT * FROM AssessmentStandards " +
                "WHERE Title LIKE '%" + searchQuery + "%' OR Code LIKE '%" + searchQuery + "%' " +
                "ORDER BY Title " +
                "LIMIT 100;");
        }

        public async Task UpdateStandardAsync(Standard standard)
            => await Database.UpdateAsync(standard);
        #endregion

        #region Subject methods
        public async Task<ObservableCollection<Subject>> GetSubjectsAsync()
        {
            var subjects = await Database.Table<Subject>()
                .OrderBy(subject => subject.Name)
                .ToListAsync();

            return new ObservableCollection<Subject>(subjects);
        }

        public async Task UpdateOrInsertSubjectAsync(Subject subject)
            => await UpdateOrInsertObjectAsync(subject);

        public async Task UpdateSubjectsAsync(List<Subject> subjects)
        {
            foreach (var subject in subjects)
                await UpdateOrInsertSubjectAsync(subject);
        }

        public async Task DeleteSubjectAsync(Subject subject)
        {
            if (subject.Custom is true)
                await Database.DeleteAsync(subject);
            else
                throw new Exception("Can't delete base subjects.");
        }
        #endregion

        #region SelectedSubject methods
        public async Task<ObservableCollection<SelectedSubject>> GetSelectedSubjectsAsync()
        {
            var subjects = await Database.Table<SelectedSubject>().ToListAsync();
            foreach (var subject in subjects)
                await Database.GetChildrenAsync(subject);

            return new ObservableCollection<SelectedSubject>(subjects);
        }

        public async Task InsertSelectedSubjectAsync(SelectedSubject subject)
            => await Database.InsertAsync(subject);

        public async Task UpdateOrInsertSelectedSubjectAsync(SelectedSubject subject)
            => await UpdateOrInsertObjectAsync(subject);

        #endregion

        #region Other methods
        public async Task<List<SubfieldModel>> GetSubfieldsAsync(StandardType filterOptions)
        {
            switch (filterOptions)
            {
                case StandardType.All:
                    return await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards " +
                        " ORDER BY Subfield;");

                case StandardType.Achievement:
                    return await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards" +
                        "WHERE StandardType = 0 ORDER BY Subfield;");

                case StandardType.Unit:
                    return await Database.QueryAsync<SubfieldModel>(
                        "SELECT DISTINCT Subfield FROM AssessmentStandards " +
                        "WHERE StandardType = 0 ORDER BY Subfield;");

                default:
                    return null;
            }
        }

        public async Task<Tuple<int, int>> GetAchievedAndTotalCreditsAsync()
        {
            var total = await Database.ExecuteScalarAsync<int>("SELECT SUM(Credits) FROM AssessmentStandards " +
                "WHERE Selected = 1");

            var achieved = await Database.ExecuteScalarAsync<int>("SELECT SUM(Credits) FROM AssessmentStandards " +
                "WHERE Selected = 1 AND FinalGrade > 1");

            return new Tuple<int, int>(total, achieved);
        }

        public async Task<ObservableCollection<CreditBreakdown>> GetCreditBreakdownsAsync()
        {
            var notAchievedCredits = await Database.ExecuteScalarAsync<int>("SELECT SUM(Credits) FROM AssessmentStandards " +
                "WHERE Selected = 1 AND FinalGrade = 1");

            var achievedCredits = await Database.ExecuteScalarAsync<int>("SELECT SUM(Credits) FROM AssessmentStandards " +
                "WHERE Selected = 1 AND FinalGrade = 2");

            var meritCredits = await Database.ExecuteScalarAsync<int>("SELECT SUM(Credits) FROM AssessmentStandards " +
                "WHERE Selected = 1 AND FinalGrade = 3");

            var excellenceCredits = await Database.ExecuteScalarAsync<int>("SELECT SUM(Credits) FROM AssessmentStandards " +
                "WHERE Selected = 1 AND FinalGrade = 4");

            var notAchieved = new CreditBreakdown()
            {
                Credits = notAchievedCredits,
                Grade = CreditBreakdown.Grades.NotAchieved
            };

            var achieved = new CreditBreakdown()
            {
                Credits = achievedCredits,
                Grade = CreditBreakdown.Grades.Achieved
            };

            var merit = new CreditBreakdown()
            {
                Credits = meritCredits,
                Grade = CreditBreakdown.Grades.Merit
            };

            var excellence = new CreditBreakdown()
            {
                Credits = excellenceCredits,
                Grade = CreditBreakdown.Grades.Excellence
            };

            return new ObservableCollection<CreditBreakdown>()
                { notAchieved, achieved, merit, excellence };
        }
        #endregion

        #region Generic methods

        async Task UpdateOrInsertObjectAsync(object item)
        {
            var rowsUpdated = await Database.UpdateAsync(item);
            bool notUpdated = rowsUpdated == 0;

            if (notUpdated)
                await Database.InsertAsync(item);
        }

        #endregion
    }
}
