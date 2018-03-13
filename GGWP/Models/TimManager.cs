using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GGWP.Models.db;
using GGWP.Models.Entities;

namespace GGWP.Models
{
    public static class TimManager
    {
        public static List<Tim> GetTimovi(bool onlyOpen)
        {
            List<Tim> lista = new List<Tim>();

            using (var db = new ggwpDBEntities())
            {
                if (onlyOpen) lista = db.Tim.Where(x => x.otvoren == 1).ToList();
                else lista = db.Tim.Where(x => x.otvoren == 0).ToList();
            }

            return lista;
        }

        public static List<Tim> GetKorisnikoviTimovi(int uID)
        {
            List<Tim> lista = new List<Tim>();

            using (var db = new ggwpDBEntities())
            {
                List<KorisnikTim> ktlist = db.KorisnikTim.Where(x => x.korisnik_id == uID).ToList();
                
                foreach(KorisnikTim kt in ktlist)
                {
                    lista.Add(db.Tim.Find(kt.tim_id));
                }
            }

            return lista;
        }

        public static Tim GetTim(int tID)
        {
            Tim t = null;

            using (var db = new ggwpDBEntities())
            {
                t = db.Tim.Find(tID);
            }

            return t;
        }

        public static List<Clan> GetTimClanovi(int tID)
        {
            List<Clan> result = new List<Clan>();

            using (var db = new ggwpDBEntities())
            {
                List<KorisnikTim> ktlist = db.KorisnikTim.Where(x => x.tim_id == tID && x.tip == 1).ToList();

                foreach (KorisnikTim kt in ktlist)
                {
                    Korisnik k = db.Korisnik.Find(kt.korisnik_id);

                    if (k != null)
                    {
                        Clan c = new Clan(k.username, ""); //dodati opis
                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public static List<TimskiRaspored> GetTimskiRaspored(int uID)
        {
            List<TimskiRaspored> rasporedi = new List<TimskiRaspored>();

            using (var db = new ggwpDBEntities())
            {
                List<Tim> lista = GetKorisnikoviTimovi(uID);

                foreach (Tim t in lista)
                {
                    rasporedi.Add(new TimskiRaspored(t));
                }
            }

            return rasporedi;
        }

        public static List<RasporedModel> GetRaspored(int tID)
        {
            List<RasporedModel> rasporedi = new List<RasporedModel>();

            using (var db = new ggwpDBEntities())
            {
                List<Raspored> lista = new List<Raspored>();

                if(tID == -1) lista = db.Raspored.ToList();
                else lista = db.Raspored.Where(x => x.tim1_id == tID || x.tim2_id == tID).ToList();

                foreach (Raspored r in lista)
                {
                    rasporedi.Add(new RasporedModel(r));
                }
            }

            return rasporedi;
        }

        public static Igra GetIgra(int igraID)
        {
            Igra i = null;

            using (var db = new ggwpDBEntities())
            {
                i = db.Igra.Find(igraID);
            }

            return i;
        }
    }
}