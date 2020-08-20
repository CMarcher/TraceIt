using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace TraceIt.Models
{
    [Table("AssessmentStandards")]
    public class AssessmentStandard
    {
        [PrimaryKey, NotNull, Unique]
        public int ID { get; set; }

        [NotNull]
        public string Subfield { get; set; }

        [NotNull]
        public string Domain { get; set; }

        [NotNull, Unique]
        public int Code { get; set; }

        [NotNull]
        public string Standard_Type { get; set; }

        [NotNull]
        public string Assessment_Type { get; set; }

        public string Subject { get; set; }

        public string Subject_Reference { get; set; }

        [NotNull]
        public string Title { get; set; }

        [NotNull]
        public int Level { get; set; }

        [NotNull]
        public int Credits { get; set; }

        [NotNull]
        public string Grading_Scheme { get; set; }

        [NotNull]
        public string Status { get; set; }

        public string Expiry_Date { get; set; }

        [NotNull]
        public string Publication_Date { get; set; }

        [NotNull]
        public int CurrentVersion { get; set; }

        public string Hyperlink { get; set; }

        [NotNull]
        public bool Selected { get; set; } = false;






    }
}
