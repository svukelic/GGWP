using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGWP.Models;
using GGWP.Models.Api;
using GGWP.Models.db;
using GGWP.Models.Entities;
using Newtonsoft.Json;

namespace GGWP.Controllers
{
    public class MobileController : Controller
    {
        public string GetVijest(int vid)
        {
            ApiResponse response = new ApiResponse();

            using (var db = new ggwpDBEntities())
            {
                Vijesti v = db.Vijesti.Find(vid);

                if(v != null)
                {
                    v.datum = v.datum.Date;
                    response.code = "200";
                    response.payload = v;
                }
                else
                {
                    response.code = "404";
                    response.payload = null;
                }
            }

            return JsonConvert.SerializeObject(response);
        }

        public string GetAllVijesti()
        {
            ApiResponse response = new ApiResponse();

            using (var db = new ggwpDBEntities())
            {
                List<Vijesti> vlist = db.Vijesti.Where(x => x.status != -1).ToList();

                if (vlist.Count > 0)
                {
                    response.code = "200";
                    response.payload = vlist;
                }
                else
                {
                    response.code = "404";
                    response.payload = null;
                }
            }

            return JsonConvert.SerializeObject(response);
        }

        public string GetAllRaspored()
        {
            ApiResponse response = new ApiResponse();

            using (var db = new ggwpDBEntities())
            {
                List<RasporedModel> model = TimManager.GetRaspored(-1);

                if (model.Count > 0)
                {
                    response.code = "200";
                    response.payload = model;
                }
                else
                {
                    response.code = "404";
                    response.payload = null;
                }
            }

            return JsonConvert.SerializeObject(response);
        }

        public string Login(string username, string password)
        {
            LoginModel model = new LoginModel();
            ApiResponse response = new ApiResponse();
            model.username = username;
            model.password = password;
            AuthenticationModel auth = new AuthenticationModel(model);
            bool login = true; //auth.LoginUser();

            if (login)
            {
                response.code = "200";
                response.payload = (UserModel)auth.data;
            }
            else
            {
                response.code = "404";
                response.payload = null;
            }

            return JsonConvert.SerializeObject(response);
        }

        public string Register(string username, string password, string rpass, string ime, string dob, string email)
        {
            UserModel model = new UserModel();
            ApiResponse response = new ApiResponse();

            bool ok = true;

            model.username = username;
            model.password = password;
            model.rpass = rpass;
            model.ime = ime;
            model.dob = dob;
            model.email = email;

            if (!model.password.Equals(model.rpass))
            {
                ok = false;
            }

            if (!(new EmailAddressAttribute().IsValid(model.email)))
            {
                ok = false;
            }

            if (ok)
            {
                /*QueryManager queryManager = new QueryManager();
                ResultModel result = queryManager.InitiateQuery("Register", model);

                if (result.data == null)
                {
                    response.code = "404";
                    response.payload = null;
                }
                else
                {
                    response.code = "200";
                    response.payload = (UserModel)result.obj;
                }*/
            }
            else
            {
                response.code = "404";
                response.payload = null;
            }

            return JsonConvert.SerializeObject(response);
        }
    }
}