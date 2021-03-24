using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DTOs;
using Xamarin.Forms;

namespace MobilePhoneCardiography.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class MeasurementDetailViewModel : BaseViewModel
    {
        private int itemId;
        private Stream heartSound;
        private DateTime _startTime;
        private long _amountOfSamples;
        private int _probabilityPercentage;
        private string _patientID;
        private string _healthProffesionalID;
        private PlacementOfDeviceEnum _placementOfDevice;

        public int Id { get; set; }

        public Stream HeartSound
        {
            get => heartSound;
            set => SetProperty(ref heartSound, value);
        }
        public DateTime StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        public long AmountOfSamples
        {
            get => _amountOfSamples;
            set => SetProperty(ref _amountOfSamples, value);
        }

        public int ProbabilityPercentage
        {
            get => _probabilityPercentage;
            set => SetProperty(ref _probabilityPercentage, value);
        }

        public PlacementOfDeviceEnum PlacementOfDevice
        {
            get => _placementOfDevice;
            set => SetProperty(ref _placementOfDevice, value);
        }

        public string PatientID
        {
            get => _patientID;
            set => SetProperty(ref _patientID, value);
        }

        public string HealthProffesionalID
        {
            get => _healthProffesionalID;
            set => SetProperty(ref _healthProffesionalID, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStoreUserMeasurement.GetItemAsync(itemId);
                Id = item.Id;
                StartTime = item.StartTime;
                HeartSound = item.HeartSound; //TODO TILFØJET HEART SOUND
                //SoundSamples = item.SoundSamples;
                //TODO TROR SOUND SAMPLES ER UDGÅET
                //AmountOfSamples = item.AmountOfSamples;
                //TODO TROR AMOUNT OF SAMPLES ER UDGÅET
                ProbabilityPercentage = item.ProbabilityProcent;
                PatientID = item.PatientID;
                HealthProffesionalID = item.HealthProfID;
                PlacementOfDevice = item.PlacementEnum;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
