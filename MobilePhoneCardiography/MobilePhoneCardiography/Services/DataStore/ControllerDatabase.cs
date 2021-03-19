﻿using System;
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
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> FindPatient til databasen virker
            
            foreach (var VARIABLE in todos)
            {
                if (todos != null && VARIABLE.UserPW == user.Password && VARIABLE.HealthProfID == user.Username) return true;
                
            }
<<<<<<< HEAD

            return false;
=======

            if (todos != null&&todos._userPW == user.Password &&todos._firstName == user.Username) return true;
            else return false;
>>>>>>> Implementering af Get SSN
=======

            return false;
>>>>>>> FindPatient til databasen virker

        }

        public async Task<bool> ValidatePatient(IPatient patient)
        {
            var todos = await cosmosDbService.GetSSN(patient);
<<<<<<< HEAD
<<<<<<< HEAD
            foreach (var VARIABLE in todos)
            {
                if (VARIABLE != null&&VARIABLE.PatientId == patient.SocSec) return true;

            }
            return false;
=======

            if (todos != null&&todos.PatientId == patient.SocSec) return true;
            else return false;

>>>>>>> Implementering af Get SSN
=======
            foreach (var VARIABLE in todos)
            {
                if (VARIABLE != null&&VARIABLE.PatientId == patient.SocSec) return true;

            }
            return false;
>>>>>>> FindPatient til databasen virker
        }

        
    }
}