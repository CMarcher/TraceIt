using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace TraceIt.Models
{
    public class AssessmentStandardRepository
    {
        SQLiteConnection database;
        public AssessmentStandardRepository()
        {
            string filename = "";
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
            database = new SQLiteConnection(filepath);
        }
    }
}
