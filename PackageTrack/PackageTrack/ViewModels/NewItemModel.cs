using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using PackageTrack.Models;
using PackageTrack.Services;

namespace PackageTrack.ViewModels
{
    class NewItemModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        string checkInUser = string.Empty;
        public string CheckInUser
        {
            get { return checkInUser; }
            set { SetProperty(ref checkInUser, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        string barCode = string.Empty;
        public string BarCode
        {
            get { return barCode; }
            set { barCode = value; OnPropertyChanged(); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName]string propertyName = "",
            Action onChanged = null)
            {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
            }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
