using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using TraceIt.Utilities;
using Xamarin.Essentials;

namespace TraceIt.Services
{
    public class DataService : IDataService
    {
        public static SQLiteAsyncConnection Database;
        public bool Initialised = false;

        public static readonly Lazy<Task<SQLiteAsyncConnection>> CreateLazyConnection = new Lazy<Task<SQLiteAsyncConnection>>(async () =>
        {
            var databaseName = FileNames.DatabaseName;
            string documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(documentsDirectory, databaseName);

            File.Delete(databasePath); // Removed for debugging purposes.

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

        public async Task<ObservableCollection<AssessmentStandards>> GetAssessmentStandardsAsync()
        {
            var assessmentStandards = await Database.Table<AssessmentStandards>().ToListAsync();
            return new ObservableCollection<AssessmentStandards>(assessmentStandards);
        }

        public async Task<AssessmentStandards> GetStandardByIDAsync(int id)
        {
            var standard = await Database.Table<AssessmentStandards>()
                .Where(s => s.ID == id)
                .FirstOrDefaultAsync();

            return standard;
        }

        public async Task UpdateStandardAsync(AssessmentStandards standard)
        {
            await Database.UpdateAsync(standard);
        }

        public async Task<ObservableCollection<Subjects>> GetSubjectsAsync()
        {
            var subjects = await Database.Table<Subjects>().ToListAsync();
            return new ObservableCollection<Subjects>(subjects);
        }

        public async Task<ObservableCollection<Subjects>> GetSelectedSubjectsAsync()
        {
            var subjects = await Database.Table<Subjects>()
                .Where(s => s.Selected == "true")
                .ToListAsync();

            return new ObservableCollection<Subjects>(subjects);
        }

        public async Task UpdateSubjectAsync(Subjects subject)
        {
            await Database.UpdateAsync(subject);
        }

    }
}
