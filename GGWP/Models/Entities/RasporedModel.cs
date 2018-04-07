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
        public int id { get; set; }
        public string igra { get; set; }
        public TimModel tim1 { get; set; }
        public TimModel tim2 { get; set; }
        public DateTime datum { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public RasporedModel() { }

        public RasporedModel(Raspored raspored)
        {
            this.id = raspored.id;

            this.tim1 = new TimModel(TimManager.GetTim(raspored.tim1_id));
            this.tim2 = new TimModel(TimManager.GetTim(raspored.tim2_id));

            this.igra = this.tim1.igra;

            this.datum = raspored.datum;
        }
    }

    public class TimskiRaspored
    {
        public Tim tim { get; set; }
        public List<RasporedModel> rasporedi;
        public List<Clan> clanovi;

        public TimskiRaspored(int tID)
        {
            this.tim = TimManager.GetTim(tID);
            this.rasporedi = TimManager.GetRaspored(tID);
            this.clanovi = TimManager.GetTimClanovi(tID);
        }

        public TimskiRaspored(Tim t)
        {
            this.tim = t;
            this.rasporedi = TimManager.GetRaspored(t.id);
        }
    }

    public class TimModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public List<Clan> clanovi;
        public string naziv { get; set; }
        public string igra { get; set; }
        public int open { get; set; }
        public string opis { get; set; }
        public string vlasnik { get; set; }

        public TimModel() { }

        public TimModel(string naziv, string igra, int open, string opis)
        {
            this.naziv = naziv;
            this.igra = igra;
            this.open = open;
            this.opis = opis;

            this.clanovi = new List<Clan>();
        }

        public TimModel(Tim t)
        {
            this.naziv = t.naziv;
            this.igra = TimManager.GetIgra(t.igra_id).naziv;
            this.open = t.otvoren;
            this.opis = t.opis;
            this.vlasnik = ""; //placeholder

            this.clanovi = TimManager.GetTimClanovi(t.id);
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