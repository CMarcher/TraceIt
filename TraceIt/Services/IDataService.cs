﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Models.Query_Models;

namespace TraceIt.Services
{
    public interface IDataService
    {
        Task<ObservableCollection<Standard>> GetCategorisedStandardsAsync(string parameter, DataService.FilterByOption filterByOption);
        Task<List<Standard>> GetMatchingStandards(string searchQuery);
        Task<ObservableCollection<Subject>> GetSelectedSubjectsAsync();
        Task<Standard> GetStandardByIDAsync(int id);
        Task<ObservableCollection<Standard>> GetStandardsAsync();
        Task<List<SubfieldModel>> GetSubfieldsAsync(DataService.StandardType filterOptions);
        Task<ObservableCollection<Subject>> GetSubjectsAsync();
        Task UpdateStandardAsync(Standard standard);
        Task UpdateSubjectAsync(Subject subject);
        Task UpdateSubjectsAsync(List<Subject> subjects);
    }
}