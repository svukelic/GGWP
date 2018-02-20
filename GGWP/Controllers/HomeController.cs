using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGWP.Models;

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
            return View();
        }

        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(LoginModel model)
        {
            return View("Index");
        }

        public ActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracija(LoginModel model)
        {
            return View("Prijava");
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult OglasnaPloca()
        {
            return View();
        }

        public ActionResult Igre()
        {
            return View();
        }

        public ActionResult Kontakt()
        {
            return View();
        }

        public ActionResult Profil()
        {
            return View();
        }

        public ActionResult Poruke()
        {
            return View();
        }
    }
}