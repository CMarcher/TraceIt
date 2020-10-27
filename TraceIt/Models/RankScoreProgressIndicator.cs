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

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value, nameof(Title));
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value, nameof(Progress));
        }

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
        }
    }
}
