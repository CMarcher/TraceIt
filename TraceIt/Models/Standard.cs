using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace TraceIt.Models
{
    [Table("AssessmentStandards")]
    public class Standard : BaseModel
    {
        [PrimaryKey, NotNull, Unique]
        public int ID { get; set; }

        [NotNull]
        public string Subfield { get; }

        [NotNull]
        public string Domain { get; }

        [NotNull, Unique]
        public int Code { get; }

        [NotNull]
        public string StandardType { get; }

        [NotNull]
        public string AssessmentType { get; }

        public string Subject { get; }

        public string SubjectReference { get; }

        [NotNull]
        public string Title { get; }

        [NotNull]
        public int Level { get; }

        [NotNull]
        public int Credits { get; }

        [NotNull]
        public string GradingScheme { get; }

        [NotNull]
        public string Status { get; }

        public string ExpiryDate { get; }

        [NotNull]
        public string Publication_Date { get; }

        [NotNull]
        public int CurrentVersion { get; }

        public string Hyperlink { get; }

        private bool _selected;
        [NotNull]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        private string _addedTo;
        public string AddedTo
        {
            get { return _addedTo; }
            set
            {
                _addedTo = value;
                OnPropertyChanged(nameof(AddedTo));
            }
        }

        [NotNull]
        public bool IsLiteracy { get; }

        [NotNull]
        public bool IsNumeracy { get; }

        [NotNull]
        public bool IsReading { get; }

        [NotNull]
        public bool IsWriting { get; }

        public enum Grade
        {
            NotAchieved,
            Achieved,
            Merit,
            Excellence
        }

        public Grade GoalGrade { get; set; }

        public Grade PracticeGrade { get; set; }

        public Grade FinalGrade { get; set; }


    }
}
