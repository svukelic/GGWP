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

        public bool GetData(string username)
        {
            LoginModel lm = new LoginModel();
            lm.username = username;
            lm.password = "";

            QueryManager queryManager = new QueryManager();

            ResultModel result = queryManager.InitiateQuery("Login", lm);
            if (result.data != null)
            {
                this.user = (UserModel) result.obj;
            }
            else return false;

            ResultModel result2 = queryManager.InitiateQuery("ReadTimAll", username);
            if (result2.data != null)
            {
                foreach (Tim t in (List<Tim>) result2.obj)
                {
                    TimskiRaspored tr = new TimskiRaspored(t);

                    this.timovi.Add(tr);
                }
            }
            //else return false;

            return true;
        }
    }
}