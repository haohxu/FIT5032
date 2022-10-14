using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NiceNeighbourPharmacy.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Nice Neighbor Pharmacy Description";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nice Neighbor Pharmacy Contacts";

            return View();
        }

        public ActionResult LiveChat()
        {
            return View();
        }
    }
}