using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Diagnostics;
<<<<<<< HEAD
using System.Linq;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.SystemFunctions;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;
using NUnit.Framework;
=======
using Microsoft.Azure.Documents.Linq;
using MobilePhoneCardiography.Models.Json;
>>>>>>> CosmosDB branch added


namespace MobilePhoneCardiography.Services.DataStore
{
    public class CosmosDBService
    {
        
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
        // Det er ikke ligegyldigt hvilken database vi skriver til, vi laver dependency injection og vælger
        public CosmosDBService(EnumDatabase databaseChoice )
        {
            // Forsøger at lave det sådan, at man kan vælge hvilken database man skriver til så vi kun har en enkelt klasse.
           iDatabase = DatabaseChoice(databaseChoice);
        }

        static DocumentClient docClient = null;

        private static string databaseName;
        static readonly string collectionName = "Items";

        // Valg at iDatabase
        private IJsonDatabase DatabaseChoice(EnumDatabase databaseChoice)
        {
            int i = (int) databaseChoice;
>>>>>>> CosmosDB branch added

            switch (i)
            {
                case 0:
<<<<<<< HEAD
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


       
>>>>>>> CosmosDB branch added
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
=======

>>>>>>> CosmosDB branch added
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

        private IJsonDatabase iDatabase = new JsonPatientId();

        public async static Task<List<IJsonDatabase>> GetToDoItems()
        {
            var todos = new List<IJsonDatabase>();
>>>>>>> CosmosDB branch added

            if (!await Initialize())
                return todos;

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
                .AsDocumentQuery();
            

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<ToDoItem>();

                todos.AddRange(queryResults);
            }
            */
            return todos;

        }

        // </GetToDoItems>


>>>>>>> CosmosDB branch added
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
        #region InsertToDatabase

=======

        // <InsertToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
>>>>>>> CosmosDB branch added
        public async static Task InsertToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                item);
        }
<<<<<<< HEAD

        #endregion
        // <InsertToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>


=======
>>>>>>> CosmosDB branch added
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
