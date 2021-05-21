using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using MobilePhoneCardiography.Models.Json;

namespace DataAccessLayer.Services
{
    public class MockCosmosUser : ICosmosDBService
    {
        readonly List<User> users;
  
        public MockCosmosUser()
        {
            users = new List<User>()
            {
                //User test test with Sha256 only
                new User { Id = Guid.NewGuid().ToString(), Username = "test", Password="9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                new User { Id = Guid.NewGuid().ToString(), Username = "Second item", Password="This is an item description." },
                new User { Id = Guid.NewGuid().ToString(), Username = "Third item", Password="This is an item description." },
                new User { Id = Guid.NewGuid().ToString(), Username = "Fourth item", Password="This is an item description." },
                new User { Id = Guid.NewGuid().ToString(), Username = "Fifth item", Password="This is an item description." },
                new User { Id = Guid.NewGuid().ToString(), Username = "Sixth item", Password="This is an item description." }
            };
        }

        public async Task<List<JsonProfessionalUser>> GetLogin(IUser iUser)
        {
            var todos = new List<JsonProfessionalUser>();

            foreach (var user in users)
            {
                if (iUser.Username == user.Username && iUser.Password == user.Password)
                {
                    todos.Add(new JsonProfessionalUser {HealthProfID = user.Username, UserPW = user.Password});
                    return todos;
                }
            }
            return todos;
        }







        public Task<List<JsonPatientId>> GetSSN(IPatient iPatient)
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