using System;
using System.Threading.Tasks;
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

            if (todos != null&&todos._userPW == user.Password &&todos._firstName == user.Username) return true;
            else return false;

        }

        public async Task<bool> ValidatePatient(IPatient patient)
        {
            var todos = await cosmosDbService.GetSSN(patient);

            if (todos != null&&todos.PatientId == patient.SocSec) return true;
            else return false;

        }

        
    }
}