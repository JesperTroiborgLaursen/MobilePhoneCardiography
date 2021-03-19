<<<<<<< HEAD
﻿using System;
using System.ComponentModel;
=======
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
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MobilePhoneCardiography.Models.Json
{
<<<<<<< HEAD
=======
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3
    public class JsonProfessionalUser : IJsonProffessoinalUser, INotifyPropertyChanging 
    {
        public event PropertyChangedEventHandler PropertyChanged; 
        private string _healthProfID;
       private string _firstName;
       private string _lastName;
       private string _userPW;
<<<<<<< HEAD
=======
=======
    public class JsonProfessionalUser : IJsonDatabase, INotifyPropertyChanging 
=======
    public class JsonProfessionalUser : IJsonProffessoinalUser, INotifyPropertyChanging 
>>>>>>> iUser
    {
<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

<<<<<<< HEAD
        string _healthProfID;
>>>>>>> CosmosDB branch added
=======
        public string _healthProfID { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set;}
        public string _userPW { get; set;}
>>>>>>> Implementering af Get SSN
=======
        public event PropertyChangedEventHandler PropertyChanged; 
        private string _healthProfID;
       private string _firstName;
       private string _lastName;
       private string _userPW;
>>>>>>> FindPatient til databasen virker
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3

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
<<<<<<< HEAD
<<<<<<< HEAD
       
=======
        string _firstName;
>>>>>>> CosmosDB branch added
=======
       
>>>>>>> Implementering af Get SSN
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3
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
<<<<<<< HEAD
<<<<<<< HEAD
     
=======
        string _lastName;
>>>>>>> CosmosDB branch added
=======
     
>>>>>>> Implementering af Get SSN
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3
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
<<<<<<< HEAD
<<<<<<< HEAD
       
=======
        string _userPW;
>>>>>>> CosmosDB branch added
=======
       
>>>>>>> Implementering af Get SSN
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3
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
      
=======
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
>>>>>>> fa943cd6a32039f0d20ce94f77c7acec5e102bf3
    }
}