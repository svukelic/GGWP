using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using GGWP.Models.Entities;
using GGWP.Security;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace GGWP.Models
{
    public class QueryManager
    {
        private const string EndpointUrl = "https://ggwp.documents.azure.com:443/";
        private const string PrimaryKey = "CtyBDkeOifv0T7f7cHiZqKZt1AKNJFjIA9sBWpSzSHWEnsGcEzpE0zM8R95siiN7ItF5Oyvvx8BypRiMluiTNw==";
        private DocumentClient client;

        public ResultModel InitiateQuery(string queryname, object obj)
        {
            ResultModel result = new ResultModel();
            RasporedManager rm = new RasporedManager();
            try
            {
                switch (queryname)
                {
                    case "Login":
                        var taskLogin = ExecuteReadUser(obj);
                        taskLogin.Wait();
                        if (taskLogin.Result == null)
                        {
                            result.data = null;
                        }
                        else
                        {
                            result.data = JsonConvert.SerializeObject(taskLogin.Result);
                            result.obj = taskLogin.Result;
                        }
                        break;
                    case "Register":
                        var taskRegister = CreateUser(obj);
                        taskRegister.Wait();
                        if (taskRegister.Result == null)
                        {
                            result.data = null;
                        }
                        else
                        {
                            result.data = JsonConvert.SerializeObject(taskRegister.Result);
                            result.obj = taskRegister.Result;
                        }
                        break;
                    case "ReadRasporedAll":
                        var taskRasporedAll = rm.ExecuteReadRasporedAll(obj);
                        taskRasporedAll.Wait();
                        if (taskRasporedAll.Result == null)
                        {
                            result.data = null;
                        }
                        else
                        {
                            result.data = JsonConvert.SerializeObject(taskRasporedAll.Result);
                            result.obj = taskRasporedAll.Result;
                        }
                        break;
                    case "ReadTimAll":
                        var taskTimAll = rm.ExecuteReadTimAll(obj);
                        taskTimAll.Wait();
                        if (taskTimAll.Result == null)
                        {
                            result.data = null;
                        }
                        else
                        {
                            result.data = JsonConvert.SerializeObject(taskTimAll.Result);
                            result.obj = taskTimAll.Result;
                        }
                        break;
                    case "CreateTim":
                        var taskCreateTim = rm.CreateTim(obj);
                        taskCreateTim.Wait();
                        if (taskCreateTim.Result == null)
                        {
                            result.data = null;
                        }
                        else
                        {
                            result.data = JsonConvert.SerializeObject(taskCreateTim.Result);
                            result.obj = taskCreateTim.Result;
                        }
                        break;
                }
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                //Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                throw;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                //Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                throw;
            }
            finally
            {
                //do something
            }

            return result;
        }

        private async Task<UserModel> ExecuteReadUser(object obj)
        {
            LoginModel user = (LoginModel)obj;
            UserModel returnData = new UserModel();

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<UserModel> UserQuery = this.client.CreateDocumentQuery<UserModel>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "UserCollection"), queryOptions)
                .Where(f => f.username.Equals(user.username));

            if (UserQuery.Count() != 1)
            {
                return null;
            }

            foreach (UserModel item in UserQuery)
            {
                returnData = item;
            }

            return returnData;
        }

        private async Task<List<UserModel>> ExecuteLoginQuery(object obj)
        {
            List<UserModel> model = new List<UserModel>();
            LoginModel user = (LoginModel) obj;

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<UserModel> UserQuery = this.client.CreateDocumentQuery<UserModel>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "UserCollection"), queryOptions)
                .Where(f => f.username.Equals(user.username));

            foreach (UserModel User in UserQuery)
            {

                model.Add(User);
            }

            return model;

            /*await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = "ggwpDB" });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("ggwpDB"), new DocumentCollection { Id = "UserCollection" });

            UserModel newUser = new UserModel
            {
                id = "direktor.1",
                username = "direktor",
                password = "test",
                session = "testsession",
                ime = "Sven Vukelic",
                spol = "m",
                dob = "21.04.1993.",
                email = "thedirector444@gmail.com"
            };

            await this.CreateUserDocumentIfNotExists("ggwpDB", "UserCollection", newUser);*/
        }

        private async Task<UserModel> CreateUser(object obj)
        {
            UserModel user = (UserModel)obj;
            Guid newId = Guid.NewGuid();
            user.id = newId.ToString();
            user.session = "creating";

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<UserModel> UserQuery = this.client.CreateDocumentQuery<UserModel>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "UserCollection"), queryOptions)
                .Where(f => f.username.Equals(user.username));

            if (UserQuery.Count() > 0)
            {
                return null;
                //user.session = "fail";
            }
            else
            {
                PWSHasher pwHasher = new PWSHasher();
                byte[] saltBytes = Encoding.ASCII.GetBytes("ggwp");
                HashWithSaltResult hashResult = pwHasher.HashWithGivenSalt(user.password, 64, saltBytes);

                user.password = hashResult.Digest;
                user.rpass = hashResult.Digest;

                this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("ggwpDB", "UserCollection"), user);
                //user.password = "null";
                //user.rpass = "null";
                user.session = "ok";
                return user;
            }
        }
    }
}