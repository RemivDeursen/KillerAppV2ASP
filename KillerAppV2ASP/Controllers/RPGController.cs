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
        private UserLoginViewModel _loginview = new UserLoginViewModel();
        private CharacterViewModel _charViewModel;
        private EventsViewModel eventsViewModel = new EventsViewModel();
        private GloballyAccessibleClass GaClass;
        RPGSQLContext rpgct = new RPGSQLContext();
        
        // GET: Character
        public ActionResult Character(UserLoginViewModel loginView)
        {

            RPGRepository rpgrepo = new RPGRepository(rpgct);

            _charViewModel = new CharacterViewModel
            {
                Characters = rpgrepo.GetCharactersFromUser(loginView.UserID)
            };
            _loginview = loginView;
            return View(_charViewModel);
        }
        
        
        public ActionResult Play(int id)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgct);

            Character character = rpgrepo.GetById(id);

            if (character == null)
                throw new Exception("id does not exist.");

            Player player = new Player(character.CharacterID, character.Name, character.HP, character.Mana);

            eventsViewModel.EventsSystem = new EventSystem();
            eventsViewModel.EventsSystem.ScenarioList = rpgrepo.GetStory();
            eventsViewModel.EventsSystem.playerProgression = 1;
            eventsViewModel.EventsSystem.character = player;
            Session["CharId"] = player.CharacterID;
            Session["playerProgression"] = 1;
            Session["Button2Name"] = "Look through the window";
            Session["Button1Name"] = "Rush outside";

            GloballyAccessibleClass.Instance.EventsSystem = new EventSystem();
            return View(eventsViewModel);
        }
        
        public ActionResult RedirectToCommand(string command)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgct);
            int prog = Convert.ToInt32(Session["playerProgression"]);
            if (command == "Action1")
            {
                if (prog == 1)
                {
                    prog = 2;
                }
                else
                {
                    Session["Button1Name"] = "Next";
                    Session["Button2Name"] = "Next";
                }
            }
            else if (command == "Action2")
            {
                if (prog == 1)
                {
                    prog = 4;
                }
                else
                {
                    Session["Button1Name"] = "Next";
                    Session["Button2Name"] = "Next";
                }
            }

            if (prog == 6)
            {
                Session["Button1Name"] = "Attack";
                Session["Button2Name"] = "Skill";
                Session["Button3Name"] = "Skill";
            }

            if (prog == 9)
            {
                Session["Button1Name"] = "Eat Apple";
                Session["Button2Name"] = "Drop Apple";
            }
            Session["playerProgression"] = prog;

            return View("Play", GetEventsViewModel());
        }

        public void changeProgression()
        {
            
        }

        public EventsViewModel GetEventsViewModel()
        {
            RPGSQLContext rpgsqlContext = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(rpgsqlContext);
            Character cha = repo.GetById(Convert.ToInt32(Session["CharId"]));
            eventsViewModel.EventsSystem = new EventSystem();
            eventsViewModel.EventsSystem.character = cha;
            eventsViewModel.EventsSystem.ScenarioList = repo.GetStory();
            eventsViewModel.EventsSystem.playerProgression = Convert.ToInt32(Session["playerProgression"]);
            string phrase = Convert.ToString(Session["ActionsToSkip"]);
            string[] words = phrase.Split(',');
            foreach (string var in words)
            {
                //eventsViewModel.EventsSystem.ScenarioList.RemoveAt(Convert.ToInt32(var) - 1);
            }
            return eventsViewModel;
        }
    }
}