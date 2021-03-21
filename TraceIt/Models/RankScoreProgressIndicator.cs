using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Models
{
    public class RankScoreProgressIndicator : BaseModel
    {
        private StepStatus _status;
        public StepStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value, nameof(Status));
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value, nameof(Progress));
        }

        public List<string> DegreeList { get; private set; }

        public void SetStatus(int progress, int previousProgress = 0)
        {
            bool isLessThanProgress = progress < Progress;
            bool isLessThanProgressButMoreThanPrevious = previousProgress is 0 ? 
                true : (isLessThanProgress && progress >= previousProgress);

            bool isLessThanProgressAndLessThanPrevious = isLessThanProgress && progress < previousProgress;
            bool progressPassed = progress >= Progress;

            if(progressPassed)
                Status = StepStatus.Completed;
            else if (isLessThanProgressAndLessThanPrevious)
                Status = StepStatus.NotStarted;
            else if (isLessThanProgressButMoreThanPrevious)
                Status = StepStatus.InProgress;

            SetDegreesCollection();
        }

        private void SetDegreesCollection()
        {
            DegreeList = new List<string>();

            if (Progress is 150)
                AddDegrees(new string[]
                {
                    "Bachelor of Arts (BA)",
                    "Bachelor of Education (Teaching) (BEd(Tchg))",
                    "Bachelor of Education (Teaching English to Speakers of Other Languages) (BEd (TESOL))",
                    "Bachelor of Fine Arts (BF)",
                    "Bachelor of Music (BMus)",
                    "Bachelor of Social Work (BSW)",
                    "Bachelor of Sport, Health and Physical Education (BSportHPE)"
                });

            else if (Progress is 165)
                AddDegrees(new string[] { "Bachelor of Science (BSc) – excluding Biomedical Science and Food Science and Nutrition" });

            else if (Progress is 180)
                AddDegrees(new string[] 
                {
                    "Bachelor of Commerce (BCom)",
                    "Bachelor of Design (BDes)",
                    "Bachelor of Property (BProp)",
                    "Bachelor of Urban Planning (Honours) (BUrbPlan(Hons))"
                });

            else if (Progress is 200)
                AddDegrees(new string[] { "Bachelor of Science (BSc) – Food Science and Nutrition" });

            else if (Progress is 210)
                AddDegrees(new string[]
                {
                    "Bachelor of Global Studies (BGlobalSt)",
                    "Bachelor of Global Studies conjoints",
                    "Bachelor of Arts conjoints",
                    "Bachelor of Commerce conjoints",
                    "Bachelor of Design conjoints",
                    "Bachelor of Fine Arts conjoints",
                    "Bachelor of Music conjoints",
                    "Bachelor of Property conjoints",
                    "Bachelor of Science conjoints"
                });

            else if (Progress is 230)
                AddDegrees(new string[]
                {
                    "Bachelor of Architectural Studies (BAS)",
                    "Bachelor of Nursing (BNurs)",
                    "Bachelor of Nursing conjoints"
                });

            else if (Progress is 250)
                AddDegrees(new string[]
                {
                    "Bachelor of Health Sciences (BHSc)",
                    "Bachelor of Health Sciences conjoints"
                });

            else if (Progress is 260)
                AddDegrees(new string[]
                {
                    "Bachelor of Advanced Science (Honours) (BAdvSci(Hons))",
                    "Bachelor of Engineering (Honours) (BE(Hons))"
                });

            else if (Progress is 275)
                AddDegrees(new string[]
                {
                    "Bachelor of Advanced Science (Honours) conjoints",
                    "Bachelor of Engineering (Honours) conjoints"
                });

            else if (Progress is 280)
                AddDegrees(new string[] { "Bachelor of Science (BSc) – Biomedical Science" });
        }

        private void AddDegrees(string[] degrees)
        {
            foreach (string degree in degrees)
                DegreeList.Add(degree);
        }
    }
}
