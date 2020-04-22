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
            Level_One = 1,
            Level_Two,
            Level_Three
        }
        public int SelectedLevel { get; set; }
    }
}
