using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Diagnostics;
using System.Linq;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.SystemFunctions;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;
using NUnit.Framework;
using User = Microsoft.Azure.Documents.User;
using NUnit.Framework;

namespace MobilePhoneCardiography.Services.DataStore
{
    public class CosmosDBService
    {
            // Valg at iDatabase
 
       
        private static DateTime selectedDate;

        static DocumentClient docClient = null;
        private IUser iUser;
        private static string databaseName = "HeartRecords";
        private static string collectionName;


        // Det er ikke ligegyldigt hvilken database vi skriver til, vi laver dependency injection og vælger
        public CosmosDBService(EnumDatabase databaseChoice, DateTime date )
        {
            selectedDate = date;
            DatabaseChoice(databaseChoice);
        }
            // Forsøger at lave det sådan, at man kan vælge hvilken database man skriver til så vi kun har en enkelt klasse.



        private string DatabaseChoice(EnumDatabase databaseChoice)
        {
            int i = (int)databaseChoice;

            switch (i)
            {
                case 0:

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

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient = null;

                return false;
            }

            return true;
        }

      

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
           
            List<JsonPatientId> todos;
            
            todos = new List<JsonPatientId>();


            if (!await Initialize())
                return todos;

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


        public async static Task<List<IJsonDatabase>> GetCompletedToDoItems()
        {
            var todos = new List<IJsonDatabase>();

            if (!await Initialize())
                return todos;


            return todos;
        }
        
        public async static Task CompleteToDoItem(IJsonDatabase item)
        {

            await UpdateToDoItem(item);
        }
        



        public async static Task InsertToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                item);
        }

      

        public async static Task DeleteToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.id);
            await docClient.DeleteDocumentAsync(docUri);
        }

        public async static Task UpdateToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.id);
            await docClient.ReplaceDocumentAsync(docUri, item);
        }

    }

 
}