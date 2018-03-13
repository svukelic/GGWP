using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGWP.Models.Entities
{
    public class ProfilModel
    {
        public List<TimskiRaspored> timovi;
        public UserModel user;

        public ProfilModel()
        {
            this.timovi = new List<TimskiRaspored>();
            this.user = new UserModel();
        }

        public ProfilModel(int uID)
        {
            this.user = AuthenticationModel.GetKorisnikData(uID);
            this.timovi = TimManager.GetTimskiRaspored(uID);
        }
    }
}