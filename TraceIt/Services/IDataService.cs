using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Models.Query_Models;

namespace TraceIt.Services
{
    public interface IDataService
    {
        Task<ObservableCollection<AssessmentStandard>> GetCategorisedStandardsAsync(string parameter, DataService.FilterByOption filterByOption);
        Task<List<AssessmentStandard>> GetMatchingStandards(string searchQuery);
        Task<ObservableCollection<Subject>> GetSelectedSubjectsAsync();
        Task<AssessmentStandard> GetStandardByIDAsync(int id);
        Task<ObservableCollection<AssessmentStandard>> GetStandardsAsync();
        Task<List<SubfieldModel>> GetSubfieldsAsync(DataService.StandardType filterOptions);
        Task<ObservableCollection<Subject>> GetSubjectsAsync();
        Task UpdateStandardAsync(AssessmentStandard standard);
        Task UpdateSubjectAsync(Subject subject);
        Task UpdateSubjectsAsync(List<Subject> subjects);
    }
}