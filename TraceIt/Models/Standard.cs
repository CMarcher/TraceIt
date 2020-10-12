using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TraceIt.Services;

namespace TraceIt.Models
{
    [Table("AssessmentStandards")]
    public class Standard : BaseModel
    {
        #region Properties
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

        public enum NCEALevel
        {
            One = 1,
            Two,
            Three
        }

        [NotNull]
        public NCEALevel Level { get; set; }

        [NotNull]
        public int Credits { get; set; }

        public enum GradeOptions
        {
            AchievedOnly,
            UpToMerit,
            UpToExcellence
        }

        [NotNull]
        public GradeOptions GradingScheme { get; set; }

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
            get => _finalGrade;
            set
            {
                SetProperty(ref _finalGrade, value, nameof(FinalGrade));
                RaiseFinalGradeChanged();
            }
        }
        #endregion

        #region Methods
        public async Task PushChangesAsync(bool refreshRequired)
        {
            await App.DataService?.UpdateStandardAsync(this);

            if (refreshRequired)
                App.MessagingService.Send(MessagingService.MessageType.RefreshStandards);
        }

        public async Task SelectAsync(Subject subject)
        {
            AddedTo = subject.Name;
            Selected = true;
            await PushChangesAsync(true);
        }

        public async Task DeselectAsync()
        {
            AddedTo = null;
            Selected = false;
            GoalGrade = Grade.NotAchieved;
            PracticeGrade = Grade.NotAchieved;
            FinalGrade = Grade.NotAchieved;
            await PushChangesAsync(true);
        }

        #endregion

        public event EventHandler FinalGradeChanged;

        private void RaiseFinalGradeChanged()
            => FinalGradeChanged?.Invoke(this, new EventArgs());
    }
}
