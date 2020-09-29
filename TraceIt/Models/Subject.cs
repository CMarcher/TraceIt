using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TraceIt.Models
{
    [Table("Subjects")]
    public class Subject : BaseModel, ISubjectItem 
    {
        private int _id;
        [PrimaryKey]
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value, ID);
        }

        private string _name;
        [NotNull]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, Name);
        }

        [NotNull]
        public string Subfield { get; set; }

        private int _credits;
        public int Credits
        {
            get => _credits;
            set => SetProperty(ref _credits, value, Credits);
        }

        private int _meritCredits;
        public int MeritCredits
        {
            get => _meritCredits;
            set => SetProperty(ref _meritCredits, value, MeritCredits);
        }

        private int _excellenceCredits;
        public int ExcellenceCredits
        {
            get => _excellenceCredits;
            set => SetProperty(ref _excellenceCredits, value, ExcellenceCredits);
        }

        private bool _selected;
        [NotNull]
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value, Selected);
        }

        private bool _custom;
        [NotNull]
        public bool Custom
        {
            get => _custom;
            set => SetProperty(ref _custom, value, Custom);
        }

        public Subject() { }

        public async Task PushChanges()
            => await App.DataService.UpdateSubjectAsync(this);
        
        //async Task ResetSubject()
        //{

        //}
    }
}
