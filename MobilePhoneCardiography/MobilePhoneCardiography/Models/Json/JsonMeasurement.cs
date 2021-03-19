using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
    public class JsonMeasurement : IJsonDatabase, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //TODO add a measurement interface
        string _measurementId;

        [JsonProperty("measurementId")]
        public string MeasurementId
        {
            get => _measurementId;
            set
            {
                if (_measurementId == value)
                    return;

                _measurementId = value;

                HandlePropertyChanged();
            }
        }

        string _soundFile;

        [JsonProperty("sound file")]
        public string SoundFile
        {
            get => _soundFile;
            set
            {
                if (_soundFile == value)
                    return;

                _soundFile = value;

                HandlePropertyChanged();
            }
        }

        DateTime _startTime;

        [JsonProperty("start time")]
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime == value)
                    return;

                _startTime = value;

                HandlePropertyChanged();
            }
        }

        int _amountOfSoundSamples;

        [JsonProperty("amount of sound samples")]
        public int AmountOfSoundSamples
        {
            get => _amountOfSoundSamples;
            set
            {
                if (_amountOfSoundSamples == value)
                    return;

                _amountOfSoundSamples = value;

                HandlePropertyChanged();
            }
        }

        int _probabilityPercentage;

        [JsonProperty("probability percentage")]
        public int ProbabilityPercentage
        {
            get => _probabilityPercentage;
            set
            {
                if (_probabilityPercentage == value)
                    return;

                _probabilityPercentage = value;

                HandlePropertyChanged();
            }
        }

        int _patientId;

        [JsonProperty("patient ID")]
        public int PatientId
        {
            get => _patientId;
            set
            {
                if (_patientId == value)
                    return;

                _patientId = value;

                HandlePropertyChanged();
            }
        }

        int _healthProfId;

        [JsonProperty("healthproffesional ID")]
        public int HealthProfId
        {
            get => _healthProfId;
            set
            {
                if (_healthProfId == value)
                    return;

                _healthProfId = value;

                HandlePropertyChanged();
            }
        }

        int _placementOfDevice;

        [JsonProperty("placement of device")]
        public int PlacementOfDevice
        {
            get => _placementOfDevice;
            set
            {
                if (_placementOfDevice == value)
                    return;

                _placementOfDevice = value;

                HandlePropertyChanged();
            }
        }



        void HandlePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, eventArgs);
        }

        public string id { get; set; }
        public string PatientID { get; set; }
        public DateTime date { get; set; }
        public event PropertyChangingEventHandler PropertyChanging;
    }
}