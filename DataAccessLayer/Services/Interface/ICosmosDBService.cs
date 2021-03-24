using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using MobilePhoneCardiography.Models.Json;

namespace DataAccessLayer
{
    public interface ICosmosDBService
    {

        //public string collectionName { get; }

        public Task<List<JsonProfessionalUser>> GetLogin(IUser iUser);
        public Task<List<JsonPatientId>> GetSSN(IPatient iPatient);
        public Task StoreHeartSound(JsonMeasurement measurement);
        public  Task DeleteToDoItem(Object item);
        public  Task UpdateToDoItem(Object item);

        public string DatabaseChoice(EnumDatabase databaseChoice);

    }
}