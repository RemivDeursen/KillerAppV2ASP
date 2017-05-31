using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerAppV2ASP.Models;

namespace KillerAppV2ASP.Controllers
{
    public class HomeController : Controller
    {
        private EventSystem Events;
        public ActionResult Index()
        {
            Events = new EventSystem(new Player("Bob", 50, 50));
            return View("Index", Events);
        }
    }
}