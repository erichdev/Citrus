using Citrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Citrus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Volunteer()
        {
            return View();
        }

        [Route("event/{eventId:int}")]
        public ActionResult Event(int? eventId = 0)
        {
            ItemViewModel<int?> model = new ItemViewModel<int?>();
            model.Item = eventId;
            return View("EventPage", model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }
    }
}