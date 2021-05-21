using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using MobilePhoneCardiography.Models.Json;

namespace DataAccessLayer.Services
{
    public class MockCosmosPatient : ICosmosDBService
    {

        readonly List<Patient> patients;

        public MockCosmosPatient()
        {

            patients = new List<Patient>()
            {
                new Patient{ SocSec = "78fbe9c93e7e2079e274922b680975a80901ac2c4273e1274d6d37ad71c0c369", FirstName = "Mr.Robot", LastName= "Anonymous" },
                new Patient{ SocSec = "6500ef5bcd77cf851c726609bf08f60eed665edb8c6a822d099179889fe82bb1", FirstName = "Jim", LastName= "Smith" },
                new Patient{ SocSec = "9e8601ac10c50d92bc1746f5c89c0fccea68c7e32b38af49fae6d148e747ca2f", FirstName = "Anders", LastName= "Salty" }
            };
        }

        public async Task<List<JsonPatientId>> GetSSN(IPatient iPatient)
        {
            var todos = new List<JsonPatientId>();

            foreach (var user in patients)
            {
                if (iPatient.SocSec == user.SocSec )
                {
                    todos.Add(new JsonPatientId
                        {PatientId = user.SocSec, FirstName = user.FirstName, LastName = user.LastName});
                    return todos;
                }
            }
            return todos;

        }




        public Task<List<JsonProfessionalUser>> GetLogin(IUser iUser)
        {
            throw new NotImplementedException();
        }

        public Task StoreHeartSound(JsonMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task DeleteToDoItem(object item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateToDoItem(object item)
        {
            throw new NotImplementedException();
        }

        public string DatabaseChoice(EnumDatabase databaseChoice)
        {
            throw new NotImplementedException();
        }
    }
}