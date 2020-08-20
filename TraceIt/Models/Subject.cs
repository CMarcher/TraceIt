using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TraceIt.Models
{
    [Table("Subjects")]
    public class Subject : ISubjectItem
    {
        [PrimaryKey]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Subfield { get; set; }

        [NotNull]
        public bool Selected { get; set; } = false;

        [NotNull]
        public bool Custom { get; set; } = false;

        public Subject() { }
    }
}
