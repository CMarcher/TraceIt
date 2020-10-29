using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    [Table("SelectedSubjects")]
    public class SelectedSubject : BaseModel
    {
        public SelectedSubject() 
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Subject))]
        public int SubjectID { get; set; }

        [ManyToOne]
        public Subject BaseSubject { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value, nameof(Selected));
        }

        private int _credits;
        [NotNull]
        public int Credits
        {
            get => _credits;
            set => SetProperty(ref _credits, value, nameof(Credits));
        }

        private int _passedCredits;
        [NotNull]
        public int PassedCredits
        {
            get => _passedCredits;
            set => SetProperty(ref _passedCredits, value, nameof(PassedCredits));
        }

        private int _meritCredits;
        [NotNull]
        public int MeritCredits
        {
            get => _meritCredits;
            set => SetProperty(ref _meritCredits, value, nameof(MeritCredits));
        }

        private int _excellenceCredits;
        [NotNull]
        public int ExcellenceCredits
        {
            get => _excellenceCredits;
            set => SetProperty(ref _excellenceCredits, value, nameof(ExcellenceCredits));
        }

        private bool _endorsementEligible;
        [Ignore]
        public bool EndorsementEligible
        {
            get => _endorsementEligible;
            set => SetProperty(ref _endorsementEligible, value, nameof(EndorsementEligible));
        }

        private int _standardsCount;
        [Column("Standards")]
        public int StandardsCount
        {
            get => _standardsCount;
            set => SetProperty(ref _standardsCount, value, nameof(StandardsCount));
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value, nameof(Year));
        }

        void CountStandards()
            => StandardsCount = Standards.Count;

        public async Task PushChangesAsync()
            => await App.DataService.UpdateOrInsertSelectedSubjectAsync(this);

        public async Task Select()
        {
            Selected = true;
            Standards = new ObservableCollection<Standard>();
            await PushChangesAsync();
        }

        public async Task Deselect()
        {
            Selected = false;
            await Delete();
            await PushChangesAsync();
        }

        private async Task RefreshAsync()
        {
            SetCredits();
            CountStandards();
            SetEndorsementEligibilty();
            await PushChangesAsync();
        }

        public async Task Delete()
        {
            await ClearStandards();
        }

        private void SetCredits()
        {
            Credits = 0;
            MeritCredits = 0;
            ExcellenceCredits = 0;

            foreach (var standard in Standards)
                AddStandardCredits(standard);
        }

        private void AddStandardCredits(Standard standard)
        {
            if (standard.FinalGrade is Standard.Grade.Excellence)
                ExcellenceCredits += standard.Credits;
            else if (standard.FinalGrade is Standard.Grade.Merit)
                MeritCredits += standard.Credits;

            Credits += standard.Credits;
        }

        [Ignore]
        public ObservableCollection<Standard> Standards { get; set; } = new ObservableCollection<Standard>();

        public async Task InitialiseStandards()
        {
            Standards = await App.DataService.GetStandardsForSubjectAsync(this);
            await RefreshAsync();

            foreach (var standard in Standards)
                SubscribeToFinalGradeChanged(standard);

            await SetPassedCredits();
        }

        public async Task AddStandardAsync(Standard standard)
        {
            Standards.Add(standard);
            await standard.SelectAsync(this);
            await RefreshAsync();
            SubscribeToFinalGradeChanged(standard);
        }

        public async Task RemoveStandardAsync(Standard standard)
        {
            try
            {
                Standards.RemoveByID(standard);
                UnsubscribeFromFinalGradeChanged(standard);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            await standard.DeselectAsync();
            await RefreshAsync();
        }

        private async Task ClearStandards()
        {
            foreach (var standard in Standards)
            {
                UnsubscribeFromFinalGradeChanged(standard);
                await standard.DeselectAsync();
            }
        }

        private void SetEndorsementEligibilty()
            => EndorsementEligible = Standards.IsEligibleForEndorsement();

        private void SubscribeToFinalGradeChanged(Standard standard)
            => standard.FinalGradeChanged += new EventHandler(OnFinalGradeChanged);

        private void UnsubscribeFromFinalGradeChanged(Standard standard)
            => standard.FinalGradeChanged -= OnFinalGradeChanged;

        private async void OnFinalGradeChanged(object sender, EventArgs e)
        {
            await SetPassedCredits();
            SetCredits();
        }

        private async Task SetPassedCredits()
        {
            PassedCredits = Standards.CountCredits(standard => (int)standard.FinalGrade >= 2);
            await PushChangesAsync();
        }
    }
}
