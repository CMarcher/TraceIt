using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Models.Query_Models;

namespace TraceIt.Services
{
    public interface IDataService
    {
        Task DeleteSubjectAsync(Subject subject);
        Task<Tuple<int, int>> GetAchievedAndTotalCreditsAsync();
        Task<ObservableCollection<Standard>> GetCategorisedStandardsAsync(string parameter, DataService.FilterOption filterByOption);
        Task<List<Standard>> GetMatchingStandards(string searchQuery);
        Task<ObservableCollection<Standard>> GetSelectedStandardsAsync();
        Task<ObservableCollection<SelectedSubject>> GetSelectedSubjectsAsync();
        Task<Standard> GetStandardByIDAsync(int id);
        Task<ObservableCollection<Standard>> GetStandardsAsync();
        Task<ObservableCollection<Standard>> GetStandardsForSubjectAsync(string subjectName);
        Task<List<SubfieldModel>> GetSubfieldsAsync(DataService.StandardType filterOptions);
        Task<ObservableCollection<Subject>> GetSubjectsAsync();
        Task InsertSelectedSubjectAsync(SelectedSubject subject);
        Task UpdateOrInsertSelectedSubjectAsync(SelectedSubject subject);
        Task UpdateOrInsertSubjectAsync(Subject subject);
        Task<bool> UpdateStandardAsync(Standard standard);
        Task UpdateSubjectsAsync(List<Subject> subjects);
    }
}