using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    [Table("Subjects")]
    public class Subject : BaseModel, ISubjectItem
    {
        [PrimaryKey]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Subfield { get; set; }

        private bool _custom;
        [NotNull]
        public bool Custom
        {
            get => _custom;
            set => SetProperty(ref _custom, value, nameof(Custom));
        }

        public Subject() { }

        public async Task PushChangesAsync()
            => await App.DataService.UpdateOrInsertSubjectAsync(this);

        public async Task Delete()
        {
            if (Custom is false)
                throw new Exception("Can't delete base subject.");
            else
                await App.DataService.DeleteSubjectAsync(this);
        }
    }
}
