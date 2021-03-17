using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Azure.Documents.Linq;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Models.Json;
using User = Microsoft.Azure.Documents.User;


namespace MobilePhoneCardiography.Services.DataStore
{
    public class CosmosDBService
    {
        
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

        // <GetToDoItems>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        /// private IJsonDatabase iDatabase;

        #region GetFromDatabase

        public async Task<IJsonProffessoinalUser> GetLogin(IUser iUser)
        {

            // Dette er hvad vi søger efter
            this.iUser = iUser;

            // Dette 
            var todos = new JsonProfessionalUser();

            if (!await Initialize())
                return todos;

            var todoQuery = docClient.CreateDocumentQuery<IJsonProffessoinalUser>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo._healthProfID == iUser.Username).Where(todo => todo._userPW == iUser.Password)
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<IJsonProffessoinalUser>();
            }

            return todos;
        }

        private IPatient iPatient;
        public async Task<IJsonPatient> GetSSN(IPatient iPatient)
        {
            this.iPatient = iPatient;

            var todos = new JsonPatientId();

            if (!await Initialize())
                return todos;

            var todoQuery = docClient.CreateDocumentQuery<IJsonPatient>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(todo => todo.PatientId == iPatient.SocSec)
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<IJsonPatient>();
            }

            return todos;
        }


        #endregion

        // </GetToDoItems>

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

        #region InsertToDatabase

        public async static Task InsertToDoItem(IJsonDatabase item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                item);
        }

        #endregion
        // <InsertToDoItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>


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
