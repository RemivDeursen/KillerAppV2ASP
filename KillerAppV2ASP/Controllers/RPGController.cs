using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerAppV2ASP.Models;
using KillerAppV2ASP.Models.DBO;
using KillerAppV2ASP.ViewModels;

namespace KillerAppV2ASP.Controllers
{
    public class RPGController : Controller
    {
        // GET: Character
        public ActionResult Character()
        {
            RPGSQLContext rpgct= new RPGSQLContext();
            RPGRepository rpgrepo = new RPGRepository(rpgct);
            
            var characters = new List<Character>();
            characters = rpgrepo.GetCharactersFromUser(1);

            var viewModel = new CharacterViewModel()
            {
                Characters = characters
            };
            return View(viewModel);
        }
    }
}