using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using GGWP.Models.db;

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
        public TimModel tim1 { get; set; }
        public TimModel tim2 { get; set; }
        public string datum { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Raspored(string id, string igra, TimModel tim1, TimModel tim2, string datum)
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
        public TimModel tim { get; set; }
        public List<RasporedModel> rasporedi;

        public TimskiRaspored(TimModel t)
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

    public class TimModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public List<Clan> clanovi;
        public string naziv { get; set; }
        public string igra { get; set; }
        public string open { get; set; }
        public string opis { get; set; }
        public string vlasnik { get; set; }

        public TimModel() { }

        public TimModel(string naziv, string igra, string open, string opis)
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

        public static List<TimModel> GetTimovi(bool onlyOpen)
        {
            List<TimModel> lista = new List<TimModel>();

            using (var db = new ggwpDBEntities())
            {
                //if (onlyOpen) lista = db.Tim.Where(x => x.otvoren == 1).ToList();
            }

            return lista;
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