using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOs;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;

namespace MobilePhoneCardiography.Services.DataStore
{
    public class ControllerDatabase : IControllerDatabase
    {
        private ICosmosDBService cosmosDbService;
        public ControllerDatabase(ICosmosDBService cosmosDb)
        {
            this.cosmosDbService = cosmosDb;
        }

        public async Task<bool> ValidateLogin(IUser user)
        {
            var todos = await cosmosDbService.GetLogin(user);


            //TODO Denne linje er under test
            if (todos != null && todos[0].UserPW == user.Password && todos[0].HealthProfID == user.Username) return true;

            //TODO Denne linje er væk fra test
            //foreach (var VARIABLE in todos)
            //{
            //    if (todos != null && VARIABLE.UserPW == user.Password && VARIABLE.HealthProfID == user.Username) return true;
                
            //}

            return false;
        }

        public async Task<bool> ValidatePatient(IPatient patient)
        {
            var todos = await cosmosDbService.GetSSN(patient);
            if (todos != null && todos[0].PatientId == patient.SocSec)
            {
                patient.FirstName = todos[0].FirstName;
                patient.LastName = todos[0].LastName;
                return true;
            }

            //foreach (var VARIABLE in todos)
            //{
            //    if (VARIABLE != null && VARIABLE.PatientId == patient.SocSec)
            //    {
            //        patient.FirstName = VARIABLE.FirstName;
            //        patient.LastName = VARIABLE.LastName;
            //        return true;
            //    }
                  

            //}
            return false;
        }

        public async Task<List<DateTime>> GetPatientLibrary(IMeasurement measurenent) //Er det ikke kun socSec vi skal have fat i?
        {
            var todos = new List<DateTime>();

            todos = await cosmosDbService.GetPatientDateTimes(measurenent);
            
            return todos;
        }

    }
}