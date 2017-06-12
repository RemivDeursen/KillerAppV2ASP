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
            return View(userview);
        }

        public ActionResult RedirectToRpg()
        {
            GloballyAccessibleClass.Instance.testint = 5;
            return RedirectToAction("Character", "RPG", userview);
        }
        /// <summary>
        /// This method runs the username and password through the repositorys login verification query.
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
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
                    Session["UserID"] = userview.UserID;
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

        /// <summary>
        /// This method registers a new user to the database.
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string naam, string pass)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgsql);
            rpgrepo.AddUserToDB(naam, pass);

            return View(userview);
        }
    }
}