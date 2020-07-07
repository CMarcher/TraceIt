using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TraceIt.Models
{
    public class SelectedSubject : Subject
    {
        public int Credits { get; set; }
        public int Standards { get; set; }

        public SelectedSubject(string name, int credits, int standards)
        {
            Name = name;
            Credits = credits;
            Standards = standards;
        }

        
    }
}
