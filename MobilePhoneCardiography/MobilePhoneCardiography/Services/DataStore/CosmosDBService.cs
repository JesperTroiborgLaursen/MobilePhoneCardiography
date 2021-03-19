using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Diagnostics;
<<<<<<< HEAD
<<<<<<< HEAD
using System.Linq;
using Microsoft.Azure.Cosmos.Linq;
<<<<<<< HEAD
=======
using System.Linq;
>>>>>>> Ændret i Services. CosmosDBService
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.SystemFunctions;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;
using NUnit.Framework;
=======
=======
>>>>>>> FindPatient til databasen virker
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.SystemFunctions;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> CosmosDB branch added
=======
using User = Microsoft.Azure.Documents.User;
>>>>>>> iUser
=======

>>>>>>> Downloaded NuggetPackages efter der opstod fejl
=======
using NUnit.Framework;
>>>>>>> FindPatient til databasen virker


namespace MobilePhoneCardiography.Services.DataStore
{
    public class CosmosDBService
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        
<<<<<<< HEAD
        private static DateTime selectedDate;

        // Det er ikke ligegyldigt hvilken database vi skriver til, vi laver dependency injection og vælger
        public CosmosDBService(EnumDatabase databaseChoice, DateTime date )
        {
            selectedDate = date;
            DatabaseChoice(databaseChoice);
        }
            // Forsøger at lave det sådan, at man kan vælge hvilken database man skriver til så vi kun har en enkelt klasse.


        static DocumentClient docClient = null;
        private IUser iUser;
        private static string databaseName = "HeartRecords";
        private static string collectionName;

        // Valg at iDatabase
        private string DatabaseChoice(EnumDatabase databaseChoice)
        {
            int i = (int)databaseChoice;
=======
=======
        private IJsonDatabase iDatabase = new JsonMeasurement();
=======
        private JsonProfessionalUser iDatabase = new JsonMeasurement();
>>>>>>> iUser
=======
        
>>>>>>> Implementering af Get SSN
        private static DateTime selectedDate;

>>>>>>> Ændret i Services. CosmosDBService
        // Det er ikke ligegyldigt hvilken database vi skriver til, vi laver dependency injection og vælger
        public CosmosDBService(EnumDatabase databaseChoice, DateTime date )
        {
            selectedDate = date;
            DatabaseChoice(databaseChoice);
        }
            // Forsøger at lave det sådan, at man kan vælge hvilken database man skriver til så vi kun har en enkelt klasse.


        static DocumentClient docClient = null;
        private IUser iUser;
        private static string databaseName = "HeartRecords";
        private static string collectionName;

        // Valg at iDatabase
<<<<<<< HEAD
        //private IJsonDatabase DatabaseChoice(EnumDatabase databaseChoice)
        //{
            

<<<<<<< HEAD
            int i = (int) databaseChoice;
>>>>>>> CosmosDB branch added
=======
        private string DatabaseChoice(EnumDatabase databaseChoice)
        {
            int i = (int)databaseChoice;
>>>>>>> Implementering af Get SSN

            switch (i)
            {
                case 0:
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> Implementering af Get SSN
                    {
                        return collectionName = "Patient";
                    }
                case 1:
                    {
                        return collectionName = "ProfessionalUser";
                    }
                case 2:
                    {
                        return collectionName = "Measurement"; 
                    }
                default:
                    {
                        return null;
                    }
            }
        }
<<<<<<< HEAD
<<<<<<< HEAD
        

=======
                {
                    databaseName = "Patient";
                    return iDatabase = new JsonPatientId();
                }
                case 1:
                {
                        databaseName = "ProfessionalUser";
                        return iDatabase = new JsonProfessionalUser();
                }
                case 2:
                {
                        databaseName = "Measurement";
                        return iDatabase = new JsonMeasurement();
                }
                default:
                {
                    return null;
                }
            }
        }
=======
        //    int i = (int) databaseChoice;

        //    switch (i)
        //    {
        //        case 0:
        //        {
        //            databaseName = "Patient";
        //            return iDatabase = new JsonPatientId();
        //        }
        //        case 1:
        //        {
        //                databaseName = "ProfessionalUser";
        //                return iDatabase = new JsonProfessionalUser();
        //        }
        //        case 2:
        //        {
        //                databaseName = "Measurement";
        //                return iDatabase = new JsonMeasurement();
        //        }
        //        default:
        //        {
        //            return null;
        //        }
        //    }
        //}
>>>>>>> iUser


       
>>>>>>> CosmosDB branch added
=======



>>>>>>> Implementering af Get SSN
=======
        

>>>>>>> Downloaded NuggetPackages efter der opstod fejl
        static async Task<bool> Initialize()
        {
            if (docClient != null)
                return true;

            try
            {
                docClient = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal

                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseName),
                    new DocumentCollection { Id = collectionName },
                    new RequestOptions { OfferThroughput = 400 }
                );
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> CosmosDB branch added
=======
>>>>>>> Implementering af Get SSN
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient = null;

                return false;
            }

            return true;
        }

        // <GetToDoItems>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        /// private IJsonDatabase iDatabase;

<<<<<<< HEAD
<<<<<<< HEAD
        #region GetFromDatabase

        public async Task<List<JsonProfessionalUser>> GetLogin(IUser iUser)
        {
            // Dette er hvad vi søger efter
            this.iUser = iUser;

            // Dette
            var todos = new List<JsonProfessionalUser>();

           
            if (!await Initialize())
                return todos;

            var todoQuery = docClient.CreateDocumentQuery<JsonProfessionalUser>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.HealthProfID == iUser.Username).Where(todo => todo.UserPW == iUser.Password)
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<JsonProfessionalUser>();
                todos.AddRange(queryResults);
            }

            return todos;
        }

        private IPatient iPatient;
        public async Task<List<JsonPatientId>> GetSSN(IPatient iPatient)
        {
            this.iPatient = iPatient;
            List<JsonPatientId> todos;
            
            todos = new List<JsonPatientId>();
=======
=======
        #region GetFromDatabase

        public async Task<List<JsonProfessionalUser>> GetLogin(IUser iUser)
        {
            // Dette er hvad vi søger efter
            this.iUser = iUser;
>>>>>>> iUser

            // Dette
            var todos = new List<JsonProfessionalUser>();

           
            if (!await Initialize())
                return todos;

            var todoQuery = docClient.CreateDocumentQuery<JsonProfessionalUser>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.HealthProfID == iUser.Username).Where(todo => todo.UserPW == iUser.Password)
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<JsonProfessionalUser>();
                todos.AddRange(queryResults);
            }

            return todos;
        }

        private IPatient iPatient;
        public async Task<List<JsonPatientId>> GetSSN(IPatient iPatient)
        {
<<<<<<< HEAD
            var todos = new List<IJsonDatabase>();
>>>>>>> CosmosDB branch added
=======
            this.iPatient = iPatient;
<<<<<<< HEAD

            var todos = new JsonPatientId();
>>>>>>> Implementering af Get SSN
=======
            List<JsonPatientId> todos;
            
            todos = new List<JsonPatientId>();
>>>>>>> FindPatient til databasen virker

            if (!await Initialize())
                return todos;

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            var todoQuery = docClient.CreateDocumentQuery<JsonPatientId>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.PatientId == iPatient.SocSec)
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<JsonPatientId>();
                todos.AddRange(queryResults);
            }

            return todos;
        }

        #endregion
        // </GetToDoItems>

=======
            /*
             //This method was used to put the items to completed in the app.
                var todoQuery = docClient.CreateDocumentQuery<ToDoItem>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.ProbabilityPercentage == false)
=======
            var todoQuery = docClient.CreateDocumentQuery<IJsonDatabase>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
<<<<<<< HEAD
                .Where(todo => todo.PatientID == "1234").Where(todo => todo.date ==selectedDate)
>>>>>>> Ændret i Services. CosmosDBService
=======
                .Where(todo => todo.PatientID == "1234").Where(todo => todo.date == selectedDate)
>>>>>>> iUser
=======
            var todoQuery = docClient.CreateDocumentQuery<IJsonPatient>(
=======
            var todoQuery = docClient.CreateDocumentQuery<JsonPatientId>(
>>>>>>> FindPatient til databasen virker
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.PatientId == iPatient.SocSec)
>>>>>>> Implementering af Get SSN
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<JsonPatientId>();
                todos.AddRange(queryResults);
            }

            return todos;
        }

        #endregion
        // </GetToDoItems>
<<<<<<< HEAD
<<<<<<< HEAD


>>>>>>> CosmosDB branch added
=======
        
>>>>>>> Ændret i Services. CosmosDBService
=======

>>>>>>> iUser
        // <GetCompletedToDoItems>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task<List<IJsonDatabase>> GetCompletedToDoItems()
        {
            var todos = new List<IJsonDatabase>();

            if (!await Initialize())
                return todos;

            /*
             //For the completed method
             
            var completedToDoQuery = docClient.CreateDocumentQuery<ToDoItem>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.ProbabilityPercentage == true)
                .AsDocumentQuery();

            while (completedToDoQuery.HasMoreResults)
            {
                var queryResults = await completedToDoQuery.ExecuteNextAsync<ToDoItem>();

                todos.AddRange(queryResults);
            }
            */

            return todos;
        }
        // </GetCompletedToDoItems>


        // <CompleteToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task CompleteToDoItem(IJsonDatabase item)
        {
            //item.ProbabilityPercentage = true;

            await UpdateToDoItem(item);
        }
        // </CompleteToDoItem>

<<<<<<< HEAD
<<<<<<< HEAD
        #region InsertToDatabase

=======

        // <InsertToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
>>>>>>> CosmosDB branch added
=======
        #region InsertToDatabase

>>>>>>> iUser
        public async static Task InsertToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                item);
        }
<<<<<<< HEAD
<<<<<<< HEAD

        #endregion
        // <InsertToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>


=======
>>>>>>> CosmosDB branch added
=======

<<<<<<< HEAD
>>>>>>> Ændret i Services. CosmosDBService
=======
        #endregion
        // <InsertToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>


>>>>>>> iUser
        // </InsertToDoItem>  

        // <DeleteToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.id);
            await docClient.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.id);
            await docClient.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  
    }

 
}
