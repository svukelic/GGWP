using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGWP.Models;
using GGWP.Models.Api;
using GGWP.Models.Entities;
using Newtonsoft.Json;

namespace GGWP.Controllers
{
    public class MobileController : Controller
    {
        public string Login(string username, string password)
        {
            LoginModel model = new LoginModel();
            ApiResponse response = new ApiResponse();
            model.username = username;
            model.password = password;
            AuthenticationModel auth = new AuthenticationModel(model);
            bool login = auth.LoginUser();

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
                QueryManager queryManager = new QueryManager();
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
                }
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