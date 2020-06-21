using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Models
{
    public class SelectedSubject
    {
        public string Name { get; set; }
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
