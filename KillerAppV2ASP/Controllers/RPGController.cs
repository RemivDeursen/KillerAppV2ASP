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
        private UserLoginViewModel loginview = new UserLoginViewModel();
        private CharacterViewModel charViewModel;
        private EventsViewModel eventsViewModel = new EventsViewModel();
        RPGSQLContext rpgct = new RPGSQLContext();
        
        // GET: Character
        public ActionResult Character(UserLoginViewModel loginView)
        {

            RPGRepository rpgrepo = new RPGRepository(rpgct);

            charViewModel = new CharacterViewModel
            {
                Characters = rpgrepo.GetCharactersFromUser(loginView.UserID)
            };
            eventsViewModel.UserLoginViewModel = loginView;
            eventsViewModel.CharacterViewModel = charViewModel;

            return View(charViewModel);
        }

        public ActionResult RPGgame()
        {

            return View(eventsViewModel);
        }
        
        public ActionResult Play(int id)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgct);

            Character character = rpgrepo.GetById(id);

            if (character == null)
                throw new Exception("id does not exist.");

            Player player = new Player(character.CharacterID, character.Name, character.HP, character.Mana);

            eventsViewModel._EventSystem = new EventSystem(player);
            eventsViewModel.SelectedCharacter = character;

            return View(eventsViewModel);
        }
    }
}