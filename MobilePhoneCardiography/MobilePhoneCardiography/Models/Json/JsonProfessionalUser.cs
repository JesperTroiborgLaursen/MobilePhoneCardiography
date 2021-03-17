using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
    public class JsonProfessionalUser : IJsonProffessoinalUser, INotifyPropertyChanging 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string _healthProfID { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set;}
        public string _userPW { get; set;}

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
      
    }
}