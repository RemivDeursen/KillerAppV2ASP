using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using KillerAppV2ASP.Models;
using KillerAppV2ASP.Models.DBO;
using KillerAppV2ASP.ViewModels;

namespace KillerAppV2ASP.Controllers
{
    public class AccountController : Controller
    {
        static List<User> users = new List<User>();
        RPGSQLContext rpgsql = new RPGSQLContext();
        static UserLoginViewModel userview = new UserLoginViewModel()
        {
            Loggedin = false,
            LoginMessage = "Log in aub."
        };
        // GET: Account
        public ActionResult Index()
        {
            return View(userview);
        }

        public ActionResult Register()
        {
            return View("Register", userview);
        }
        
        public ActionResult Login()
        {
            //database login aanroepen hier?
            return View(userview);
        }

        public ActionResult RedirectToRpg()
        {
            return RedirectToAction("Character", "RPG", userview);
        }

        [HttpPost]
        public ActionResult Login(string naam, string pass)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgsql);
            try
            {
                if (rpgrepo.TryLogin(naam, pass))
                {
                    userview.LoginMessage = "U bent ingelogd.";
                    userview.UserID = rpgrepo.GetUserId(naam, pass);
                    userview.Name = naam;
                    userview.Password = pass;
                    userview.Loggedin = true;
                }
                else
                {
                    userview.LoginMessage = "Verkeerde gebruikersnaam of wachtwoord. Probeer het opnieuw.";
                }
            }
            catch (Exception e)
            {
                userview.LoginMessage = "Connection to database failed. Please connect to VPN.";
            }

            return View(userview);
        }


        [HttpPost]
        public ActionResult Register(string naam, string pass)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgsql);
            rpgrepo.AddUserToDB(naam, pass);

            return View(userview);
        }
    }
}