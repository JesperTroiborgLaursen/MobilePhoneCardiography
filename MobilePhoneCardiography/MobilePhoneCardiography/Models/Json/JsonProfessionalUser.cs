<<<<<<< HEAD
<<<<<<< HEAD
﻿using System;
using System.ComponentModel;
=======
﻿using System.ComponentModel;
>>>>>>> CosmosDB branch added
=======
﻿using System;
using System.ComponentModel;
>>>>>>> Ændret i Services. CosmosDBService
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
<<<<<<< HEAD
<<<<<<< HEAD
    public class JsonProfessionalUser : IJsonProffessoinalUser, INotifyPropertyChanging 
    {
        public event PropertyChangedEventHandler PropertyChanged; 
        private string _healthProfID;
       private string _firstName;
       private string _lastName;
       private string _userPW;
=======
    public class JsonProfessionalUser : IJsonDatabase, INotifyPropertyChanging 
=======
    public class JsonProfessionalUser : IJsonProffessoinalUser, INotifyPropertyChanging 
>>>>>>> iUser
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _healthProfID;
>>>>>>> CosmosDB branch added

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
<<<<<<< HEAD
       
=======
        string _firstName;
>>>>>>> CosmosDB branch added
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

<<<<<<< HEAD
     
=======
        string _lastName;
>>>>>>> CosmosDB branch added
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

<<<<<<< HEAD
       
=======
        string _userPW;
>>>>>>> CosmosDB branch added
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
<<<<<<< HEAD
<<<<<<< HEAD
      
=======
        public string id { get; set; }
<<<<<<< HEAD
>>>>>>> CosmosDB branch added
=======
        public string PatientID { get; set; }
        public DateTime date { get; set; }
>>>>>>> Ændret i Services. CosmosDBService
=======
      
>>>>>>> iUser
    }
}