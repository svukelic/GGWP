using GGWP.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGWP.Models.Entities
{
    public class ProfilModel
    {
        public List<TimskiRaspored> timRasporedi;
        public UserModel user;
        //public List<Tim> timovi;

        public ProfilModel()
        {
            this.timRasporedi = new List<TimskiRaspored>();
            this.user = new UserModel();
            //this.timovi = new List<Tim>();
        }

        public ProfilModel(int uID)
        {
            this.user = AuthenticationModel.GetKorisnikData(uID);
            this.timRasporedi = TimManager.GetTimskiRaspored(uID);
            //this.timovi = TimManager.GetKorisnikoviTimovi(uID);
        }
    }
}