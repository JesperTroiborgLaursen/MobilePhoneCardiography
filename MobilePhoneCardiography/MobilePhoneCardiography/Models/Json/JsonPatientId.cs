using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
    public class JsonPatientId : IJsonDatabase, INotifyPropertyChanging
    {

        public event PropertyChangedEventHandler PropertyChanged;

        string _patientID;
        [JsonProperty("patientID")]
        public string PatientId
        {
            get => _patientID;
            set
            {
                if (_patientID == value)
                    return;

                _patientID = value;

                HandlePropertyChanged();
            }
        }
        string _firstName;
        [JsonProperty("firstName")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value)
                    return;

                _firstName = value;

                HandlePropertyChanged();
            }
        }

        string _lastName;
        [JsonProperty("lastName")]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value)
                    return;

                _lastName = value;

                HandlePropertyChanged();
            }
        }

        void HandlePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, eventArgs);
        }

        // Ved ikke om den her skal være her
        public event PropertyChangingEventHandler PropertyChanging;
        public string id { get; set; }
        public string PatientID, { get; set; }
        public DateTime date { get; set; }
    }
}
