using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using GGWP.Models;
using GGWP.Models.db;
using GGWP.Models.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.SystemFunctions;

namespace GGWP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Raspored()
        {
            List<RasporedModel> model = new List<RasporedModel>();

            QueryManager queryManager = new QueryManager();
            ResultModel result = queryManager.InitiateQuery("ReadRasporedAll", "++null++");

            if (result.data != null)
            {
                model = (List<RasporedModel>)result.obj;
            }

            return View(model);
        }

        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(LoginModel model)
        {
            ResultModel result = AuthenticationModel.LoginUser(model);

            if (result.success)
            {
                Korisnik user = (Korisnik) result.obj;
                this.Session["UserId"] = user.id;
                this.Session["Username"] = user.username;
                ViewBag.Message = "Dobrodošli, " + user.username;

                return View("Index");
            }
            else
            {
                ViewBag.Message = "Neuspješna prijava";
                return View("Prijava");
            }
        }

        public ActionResult Odjava()
        {
            this.Session.Remove("UserId");
            this.Session.Remove("Username");
            ViewBag.Message = "Uspješna odjava";

            return View("Index");
        }

        public ActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracija(UserModel model)
        {
            List<string> messages = new List<string>();
            bool ok = true;

            if (!model.password.Equals(model.rpass))
            {
                messages.Add("Lozinka i ponovljena lozinka nisu iste!");
                ok = false;
            }

            if (!(new EmailAddressAttribute().IsValid(model.email)))
            {
                messages.Add("E-mail nije pravilnog formata!");
                ok = false;
            }

            if (ok)
            {
                ResultModel result = AuthenticationModel.CreateUser(model);

                if (result.success)
                {
                    messages.Add("Registracija uspješna!");
                    ViewBag.messages = messages;
                    return View("Prijava");
                }
                else
                {
                    messages.Add("Greška prilikom registracije!");
                    ViewBag.messages = messages;
                    return View("Registracija");
                }
            }
            else
            {
                ViewBag.messages = messages;
                return View("Registracija");
            }
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult OglasnaPloca()
        {
            List<Tim> model = new List<Tim>();

            using (var db = new ggwpDBEntities())
            {
                model = db.Tim.Where(x => x.otvoren == 1).ToList();
            }

            /*QueryManager queryManager = new QueryManager();
            ResultModel result = queryManager.InitiateQuery("ReadTimAll", "");

            if (result.data != null)
            {
                model = (List<Tim>) result.obj;
            }*/

            return View(model);
        }

        public ActionResult Igre()
        {
            return View();
        }

        public ActionResult KreirajTim()
        {
            if (this.Session["UserId"] != null && this.Session["Username"] != null)
            {
                return View();
            }
            else return View("Prijava");
        }

        [HttpPost]
        public ActionResult KreirajTim(Tim tim, string openCB)
        {
            return View();
            /*if (this.Session["UserId"] != null && this.Session["Username"] != null)
            {
                List<string> messages = new List<string>();

                tim.vlasnik = this.Session["Username"].ToString();

                if (openCB == null) tim.open = "0";
                else tim.open = "1";

                tim.DodajClana(this.Session["Username"].ToString(), "Vlasnik tima");

                QueryManager queryManager = new QueryManager();
                ResultModel result = queryManager.InitiateQuery("CreateTim", tim);

                if (result.data == null)
                {
                    messages.Add("Greška prilikom kreiranja tima!");
                    ViewBag.messages = messages;
                    return View("KreirajTim");
                }
                else
                {
                    messages.Add("Kreiranje tima uspješno!");
                    ViewBag.messages = messages;
                    return Redirect("Profil");
                }
            }
            else return View("Prijava");*/
        }

        public ActionResult Kontakt()
        {
            return View();
        }

        public ActionResult Profil()
        {
            if (this.Session["UserId"] != null && this.Session["Username"] != null)
            {
                ProfilModel model = new ProfilModel();
                model.GetData(this.Session["Username"].ToString());

                return View(model);
            }
            else return View("Prijava");
        }

        public ActionResult Poruke()
        {
            if (this.Session["UserId"] != null && this.Session["Username"] != null)
            {
                return View();
            }
            else return View("Prijava");
        }


    }
}