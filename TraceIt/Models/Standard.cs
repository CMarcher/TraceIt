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
        public enum StandardTypes
        {
            Unit,
            Achievement
        }

        public enum AssessmentTypes
        {
            Internal,
            External
        }

        public enum Levels
        {
            One = 1,
            Two,
            Three
        }

        public enum GradeOptions
        {
            AchievedOnly,
            UpToMerit,
            UpToExcellence
        }

        public enum Grade
        {
            NoGrade = 0,
            NotAchieved,
            Achieved,
            Merit,
            Excellence
        }

        #region Properties
        [PrimaryKey, NotNull, Unique]
        public int ID { get; set; }

        public int SubjectID { get; set; }

        [NotNull]
        public string Subfield { get; set; }

        [NotNull]
        public string Domain { get; set; }

        [NotNull, Unique]
        public int Code { get; set; }

        [NotNull]
        public StandardTypes StandardType { get; set; }

        [NotNull]
        public AssessmentTypes AssessmentType { get; set; }

        public string Subject { get; set; }

        public string SubjectReference { get; set; }

        [NotNull]
        public string Title { get; set; }

        [NotNull]
        public Levels Level { get; set; }

        [NotNull]
        public int Credits { get; set; }

        [NotNull]
        public GradeOptions GradingScheme { get; set; }

        [NotNull]
        public string Status { get; set; }

        public string ExpiryDate { get; set; }

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
            bool updatedCompleted = await App.DataService?.UpdateStandardAsync(this);

            if (refreshRequired && updatedCompleted)
                App.MessagingService.Send(MessagingService.MessageType.RefreshStandards);
        }

        public async Task SelectAsync(SelectedSubject subject)
        {
            AddedTo = subject.BaseSubject.Name;
            SubjectID = subject.ID;
            Selected = true;
            await PushChangesAsync(true);
        }

        public async Task DeselectAsync()
        {
            AddedTo = null;
            SubjectID = 0;
            Selected = false;
            GoalGrade = Grade.NoGrade;
            PracticeGrade = Grade.NoGrade;
            FinalGrade = Grade.NoGrade;
            await PushChangesAsync(true);
        }

        #endregion

        public event EventHandler FinalGradeChanged;

        private void RaiseFinalGradeChanged()
            => FinalGradeChanged?.Invoke(this, new EventArgs());
    }
}
