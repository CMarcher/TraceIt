using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace TraceIt.Models
{
    public class DataRepository : BaseModel
    {
        public ObservableCollection<Subject> SelectedSubjects = new ObservableCollection<Subject>();

        public DataRepository()
        {
            Task.Run(InitialiseSubjects).Wait();
        }

        async Task InitialiseSubjects()
        {
            SelectedSubjects = await App.DataService.GetSelectedSubjectsAsync();

            foreach (var subject in SelectedSubjects)
            {
                var standards = await App.DataService.GetStandardsForSubjectAsync(subject.Name);
                foreach (var standard in standards)
                {
                    SetSubjectCredits(subject, standard);
                }

                await subject.PushChanges();
            }
        }

        void SetSubjectCredits(Subject subject, Standard standard)
        {
            if (standard.FinalGrade == Standard.Grade.Excellence)
                subject.ExcellenceCredits += standard.Credits;
            else if (standard.FinalGrade == Standard.Grade.Merit)
                subject.MeritCredits += standard.Credits;

            subject.Credits += standard.Credits;
        }
    }
}
