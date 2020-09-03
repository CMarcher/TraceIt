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
        public string Subfield { get; set; }

        [NotNull]
        public string Domain { get; set; }

        [NotNull, Unique]
        public int Code { get; set; }

        [NotNull]
        public string StandardType { get; set; }

        [NotNull]
        public string AssessmentType { get; set; }

        public string Subject { get; set; }

        public string SubjectReference { get; set; }

        [NotNull]
        public string Title { get; set; }

        [NotNull]
        public int Level { get; set; }

        [NotNull]
        public int Credits { get; set; }

        [NotNull]
        public string GradingScheme { get; set; }

        [NotNull]
        public string Status { get; set; }

        public string ExpiryDate { get; set; }

        [NotNull]
        public string PublicationDate { get; set; }

        [NotNull]
        public int CurrentVersion { get; set; }

        public string Hyperlink { get; set; }

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
        public bool IsLiteracy { get; set; }

        [NotNull]
        public bool IsNumeracy { get; set; }

        [NotNull]
        public bool IsReading { get; set; }

        [NotNull]
        public bool IsWriting { get; set; }

        public enum Grade
        {
            NotAchieved,
            Achieved,
            Merit,
            Excellence
        }

        private Grade _goalGrade;
        public Grade GoalGrade
        {
            get { return _goalGrade; }
            set
            {
                _goalGrade = value;
                OnPropertyChanged(nameof(GoalGrade));
            }
        }

        private Grade _practiceGrade;
        public Grade PracticeGrade
        {
            get { return _practiceGrade; }
            set
            {
                _practiceGrade = value;
                OnPropertyChanged(nameof(PracticeGrade));
            }
        }

        private Grade _finalGrade;
        public Grade FinalGrade
        {
            get { return _finalGrade; }
            set
            {
                _finalGrade = value;
                OnPropertyChanged(nameof(FinalGrade));
            }
        }
    }
}
