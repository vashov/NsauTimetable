using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NsauTimetable.Client.ViewModels.BaseViewModels
{
    public class BaseNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T prop, T newValue, 
            [CallerMemberName] string propertyName ="",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, newValue))
            {
                return false;
            }

            prop = newValue;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
