using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GGWP.Models.Entities
{
    public class RasporedModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string igra { get; set; }
        public string tim1 { get; set; }
        public string tim2 { get; set; }
        public string datum { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public RasporedModel(string id, string igra, string tim1, string tim2, string datum)
        {
            this.id = id;
            this.igra = igra;
            this.tim1 = tim1;
            this.tim2 = tim2;
            this.datum = datum;
        }
    }

    public class Raspored
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string igra { get; set; }
        public Tim tim1 { get; set; }
        public Tim tim2 { get; set; }
        public string datum { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Raspored(string id, string igra, Tim tim1, Tim tim2, string datum)
        {
            this.id = id;
            this.igra = igra;
            this.tim1 = tim1;
            this.tim2 = tim2;
            this.datum = datum;
        }
    }

    public class TimskiRaspored
    {
        public Tim tim { get; set; }
        public List<RasporedModel> rasporedi;

        public TimskiRaspored(Tim t)
        {
            this.tim = t;
            this.rasporedi = new List<RasporedModel>();

            QueryManager queryManager = new QueryManager();

            ResultModel result = queryManager.InitiateQuery("ReadRasporedAll", t);
            if (result.data != null)
            {
                this.rasporedi = (List<RasporedModel>)result.obj;
            }
        }
    }

    public class Tim
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public List<Clan> clanovi;
        public string naziv { get; set; }
        public string igra { get; set; }
        public string open { get; set; }
        public string opis { get; set; }
        public string vlasnik { get; set; }

        public Tim() { }

        public Tim(string naziv, string igra, string open, string opis)
        {
            this.naziv = naziv;
            this.igra = igra;
            this.open = open;
            this.opis = opis;

            this.clanovi = new List<Clan>();
        }

        public void DodajClana(string username, string uloga)
        {
            Clan novi = new Clan(username, uloga);
            this.clanovi.Add(novi);
        }
    }

    public class Clan
    {
        public string username { get; set; }
        public string uloga { get; set; }

        public Clan(string username, string uloga)
        {
            this.username = username;
            this.uloga = uloga;
        }
    }


}