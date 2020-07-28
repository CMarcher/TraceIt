using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Models
{
    public class UserDetails
    {
        public string Name { get; set; }
        enum Level
        {
            LevelOne = 1,
            LevelTwo,
            LevelThree
        }
        public int SelectedLevel { get; set; }
    }
}
