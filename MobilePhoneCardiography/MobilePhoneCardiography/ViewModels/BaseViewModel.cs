﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataAccessLayer.Services;
using DTOs;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Services;
using Xamarin.Forms;

namespace MobilePhoneCardiography.ViewModels
{ 
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<User> DataStoreUser => DependencyService.Get<IDataStore<User>>();
        public IDataStore<Patient> DataStorePatient => DependencyService.Get<IDataStore<Patient>>();
        public IDataStore<Measurement> DataStoreUserMeasurement => DependencyService.Get<IDataStore<Measurement>>();
        public ObservableCollection<Measurement> Measurements { get; }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
