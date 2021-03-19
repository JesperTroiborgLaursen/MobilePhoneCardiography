using System;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOs;
using MobilePhoneCardiography.Models;

namespace MobilePhoneCardiography.Services.DataStore
{
    public class ControllerDatabase
    {
        private CosmosDBService cosmosDbService;
        public ControllerDatabase(CosmosDBService cosmosDb)
        {
            this.cosmosDbService = cosmosDb;
        }

        public async Task<bool> ValidateLogin(IUser user)
        {
            var todos = await cosmosDbService.GetLogin(user);

            foreach (var VARIABLE in todos)
            {
                if (todos != null && VARIABLE.UserPW == user.Password && VARIABLE.HealthProfID == user.Username) return true;
                
            }

            return false;
        }

        public async Task<bool> ValidatePatient(IPatient patient)
        {
            var todos = await cosmosDbService.GetSSN(patient);

            foreach (var VARIABLE in todos)
            {
                if (VARIABLE != null&&VARIABLE.PatientId == patient.SocSec) return true;

            }
            return false;
        }

    }
}