using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using MobilePhoneCardiography.Models.Json;

namespace DataAccessLayer
{
    public interface ICosmosDBService
    {
        
        public Task<List<JsonProfessionalUser>> GetLogin(IUser iUser);
        public Task<List<JsonPatientId>> GetSSN(IPatient iPatient);
        public Task InsertToDoItem(Object item);
        public  Task DeleteToDoItem(Object item);
        public  Task UpdateToDoItem(Object item);
    }
}