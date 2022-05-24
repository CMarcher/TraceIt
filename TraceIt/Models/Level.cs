using System;
using System.Collections.Generic;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    public class Level : BaseModel
    {
        public enum Levels
        {
            One = 1, 
            Two,
            Three
        }

        public Levels SelectedLevel { get; set; }
        public int AchievedCredits { get; set; }
        public int RequiredCredits { get; private set; } = 80;

        public Level(Levels level)
        {
            SelectedLevel = level;
        }

        public void SetAchievedCredits(IEnumerable<Standard> standards)
        {
            AchievedCredits = 0;
            var levelIndex = (int)SelectedLevel;

            AchievedCredits += standards.CountCredits(x => (int)x.Level >= levelIndex && (int)x.FinalGrade >= 2);
        }

    }
}
