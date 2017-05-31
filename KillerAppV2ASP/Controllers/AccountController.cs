using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using KillerAppV2ASP.Models;

namespace KillerAppV2ASP.Controllers
{
    public class AccountController : Controller
    {
        static List<User> users = new List<User>();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            var user = new User()
            {
                Password = "pass",
                Username = "user"
            };
            return View(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", user);
            }
            users.Add(user);
            return RedirectToAction("Index");
        }
    }
}