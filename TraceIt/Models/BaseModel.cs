using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TraceIt.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetProperty<T, T2>(ref T property, T value, T2 publicProperty)
        {
            property = value;
            OnPropertyChanged(nameof(publicProperty));
        }
    }
}
