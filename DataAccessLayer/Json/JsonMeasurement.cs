using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
    public class JsonMeasurement : INotifyPropertyChanging
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
        public string HeartSound
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

        string _patientId;

        [JsonProperty("patient ID")]
        public string PatientId
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

        string _healthProfId;

        [JsonProperty("healthproffesional ID")]
        public string HealthProfId
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

        public event PropertyChangingEventHandler PropertyChanging;
    }
}