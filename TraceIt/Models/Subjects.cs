using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TraceIt.Models
{
    public class Subjects
    {
        [PrimaryKey]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Subfield { get; set; }

        [NotNull]
        public string Selected { get; set; } = "false";

        [NotNull]
        public string Custom { get; set; } = "false";

        public Subjects() { }
    }
}
