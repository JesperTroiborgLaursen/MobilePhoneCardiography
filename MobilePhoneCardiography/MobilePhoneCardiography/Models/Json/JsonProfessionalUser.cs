using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
    public class JsonProfessionalUser : IJsonDatabase, INotifyPropertyChanging 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _healthProfID;

        [JsonProperty("healthProfID")]
        public string HealthProfID
        {
            get => _healthProfID;
            set
            {
                if (_healthProfID == value)
                    return;

                _healthProfID = value;

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

        string _userPW;
        [JsonProperty("userPW")]
        public string UserPW
        {
            get => _userPW;
            set
            {
                if (_userPW == value)
                    return;

                _userPW = value;

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
        public string PatientID { get; set; }
        public DateTime date { get; set; }
    }
}