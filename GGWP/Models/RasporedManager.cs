using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GGWP.Models.Entities;
using Microsoft.Azure.Documents.Client;

namespace GGWP.Models
{
    public class RasporedManager
    {
        private const string EndpointUrl = "https://ggwp.documents.azure.com:443/";
        private const string PrimaryKey = "CtyBDkeOifv0T7f7cHiZqKZt1AKNJFjIA9sBWpSzSHWEnsGcEzpE0zM8R95siiN7ItF5Oyvvx8BypRiMluiTNw==";
        private DocumentClient client;

        public async Task<List<RasporedModel>> ExecuteReadRasporedAll(object obj)
        {

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            IQueryable<RasporedModel> Query;
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            if (obj != null && !obj.ToString().Equals("++null++"))
            {
                Query = this.client.CreateDocumentQuery<RasporedModel>(
                        UriFactory.CreateDocumentCollectionUri("ggwpDB", "RasporedCollection"), queryOptions)
                    .Where(x => x.tim1.Equals(obj.ToString()) || x.tim2.Equals(obj.ToString()));
            }
            else
            {
                Query = this.client.CreateDocumentQuery<RasporedModel>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "RasporedCollection"), queryOptions);
            }

            List<RasporedModel> returnData = new List<RasporedModel>();

            if (Query.Count() < 1)
            {
                return null;
            }

            foreach (RasporedModel item in Query)
            {
                returnData.Add(item);
            }

            return returnData;
        }

        /*public async Task<List<Tim>> ExecuteReadTimAll(object obj)
        {
            string username = obj.ToString();
            List<Tim> returnData = new List<Tim>();

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Tim> Query;

            Query = this.client.CreateDocumentQuery<Tim>(
                UriFactory.CreateDocumentCollectionUri("ggwpDB", "TimCollection"), queryOptions);

            /*if (!username.Equals(""))
            {
                Query = this.client.CreateDocumentQuery<Tim>(
                        UriFactory.CreateDocumentCollectionUri("ggwpDB", "TimCollection"), queryOptions)
                    .Where(x => x.clanovi.Any(y => y.username.Equals(username)));
            }
            else
            {
                Query = this.client.CreateDocumentQuery<Tim>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "TimCollection"), queryOptions);
            }

            if (Query.Count() < 1)
            {
                return null;
            }

            foreach (Tim item in Query)
            {
                if (!username.Equals("++null++"))
                {
                    if (item.clanovi.Any(x => x.username.Equals(username))) returnData.Add(item);
                }
                else returnData.Add(item);
            }

            return returnData;
        }*/

        /*private async Task<RasporedModel> CreateRaspored(object obj)
        {
            RasporedModel rm = (RasporedModel) obj;
            Guid newId = Guid.NewGuid();
            rm.id = newId.ToString();

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<RasporedModel> Query = this.client.CreateDocumentQuery<RasporedModel>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "RasporedCollection"), queryOptions)
                .Where(f => f.tim1.Equals(rm.tim1) && f.tim2.Equals(rm.tim2));

            if (Query.Count() > 0)
            {
                return null;
            }
            else
            {
                this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("ggwpDB", "TimCollection"), rm);
                return rm;
            }
        }*/

        /*public async Task<Tim> CreateTim(object obj)
        {
            Tim noviTim = (Tim) obj;
            Guid newId = Guid.NewGuid();
            noviTim.id = newId.ToString();

            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<Tim> Query = this.client.CreateDocumentQuery<Tim>(
                    UriFactory.CreateDocumentCollectionUri("ggwpDB", "TimCollection"), queryOptions)
                .Where(f => f.naziv.Equals(noviTim.naziv));

            if (Query.Count() > 0)
            {
                return null;
            }
            else
            {
                this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("ggwpDB", "TimCollection"), noviTim);
                return noviTim;
            }
        }*/
    }
}