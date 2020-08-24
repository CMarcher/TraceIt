using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TraceIt.Models
{
    [Table("Subjects")]
    public class Subject : BaseModel, ISubjectItem 
    {
        private int _id;
        [PrimaryKey]
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _name;
        [NotNull]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [NotNull]
        public string Subfield { get; set; }

        private bool _selected;
        [NotNull]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        private bool _custom;
        [NotNull]
        public bool Custom
        {
            get { return _custom; }
            set
            {
                _custom = value;
                OnPropertyChanged(nameof(_custom));
            }
        }

        public Subject() { }
    }
}
