﻿using System;
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
        //Initialize viemodels
        private UserLoginViewModel _loginview = new UserLoginViewModel();
        private CharacterViewModel _charViewModel;
        private EventsViewModel eventsViewModel = new EventsViewModel();
        //Initialize the database context
        private RPGSQLContext rpgct = new RPGSQLContext();
        private EnemyViewModel _enemyViewModel = new EnemyViewModel();
        
        /// <summary>
        /// Load the page where the user can see all of the character linked to his userid.
        /// </summary>
        /// <param name="loginView"></param>
        /// <returns></returns>
        public ActionResult Character(UserLoginViewModel loginView)
        {
            //Add characters from the logged in users to a view
            if (loginView != null)
            {
                Session["UserID"] = loginView.UserID;
            }
            RPGRepository rpgrepo = new RPGRepository(rpgct);
            _charViewModel = new CharacterViewModel
            {
                Characters = rpgrepo.GetCharactersFromUser(loginView.UserID)
            };
            _loginview = loginView;
            return View(_charViewModel);
        }
        
        /// <summary>
        /// Start the game with the given character id. The story and the events system also gets loaded here.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Play(int id)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgct);

            Character character = rpgrepo.GetById(id);
            character.InventoryList = rpgrepo.GetInventory(id);
            character.initWeapons();

            if (character == null)
                throw new Exception("id does not exist.");
            

            eventsViewModel.EventsSystem = new EventSystem();
            eventsViewModel.EventsSystem.ScenarioList = rpgrepo.GetStory();
            eventsViewModel.EventsSystem.playerProgression = 1;
            eventsViewModel.EventsSystem.character = character;

            if (Session["itemdrop"] != null)
            {
                int itmid = (int)Session["itemdrop"];
                Item item = rpgrepo.GetItemById(itmid);
                Session["itemdrop"] = item;
                character.InventoryList.Add(item);
                eventsViewModel.droppedItem = item;
            }
            Session["CharId"] = character.CharacterID;
            if (Session["playerProgression"] == null)
            {
                Session["playerProgression"] = 1;
            }
            Session["Button1Name"] = eventsViewModel.EventsSystem.ScenarioList[(int)Session["playerProgression"] -1].Button1Text;
            Session["Button2Name"] = eventsViewModel.EventsSystem.ScenarioList[(int)Session["playerProgression"] -1].Button2Text;

            GloballyAccessibleClass.Instance.EventsSystem = new EventSystem();
            return View(eventsViewModel);
        }

        /// <summary>
        /// The player comes out of the combat screen and gets an item drop. His weapon also takes durability damage.
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnFromCombat()
        {
            RPGRepository rpgrepo = new RPGRepository(rpgct);

            int ids = (int) Session["CharId"];
            Session["evm"] = null;
            int itemid = rpgrepo.GetItemDrop((int) Session["CharId"]);
            Character character = rpgrepo.GetById((int) Session["CharId"]);
            int itemidweapon = character.Wepid;
            rpgrepo.ExecuteDamageToItem(itemidweapon);
            Session["itemdrop"] = itemid;

            return RedirectToAction("Play", new {id = ids});
        }

        /// <summary>
        /// This method uses the characters progressionid to show events linked to that id. It also uses this to redirect the user to combat if nessesary. 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ActionResult RedirectToCommand(string command)
        {
            RPGRepository rpgrepo = new RPGRepository(rpgct);
            int prog = Convert.ToInt32(Session["playerProgression"]);
            if (command == "Action1")
            {
                if (prog == 1)
                {
                    prog = 3;
                }
                else if (prog == 2)
                {
                    //add combat redirect
                    prog = 5;
                    Session["playerProgression"] = prog;
                    return RedirectToAction("Combat");
                }
                else if (prog == 3)
                {
                    prog = 4;
                }
                else if (prog == 4)
                {
                    prog = 5;
                }
                else if (prog == 5)
                {
                    //Add DBO querry voor weapon repair
                    prog = 7;

                    RPGSQLContext rpgsqlContext = new RPGSQLContext();
                    RPGRepository repo = new RPGRepository(rpgsqlContext);
                    Character cha = repo.GetById(Convert.ToInt32(Session["CharId"]));
                    cha.InventoryList = repo.GetInventory(Convert.ToInt32(Session["CharId"]));
                    cha.initWeapons();

                    cha.Weapon.RepairItem();
                }
                else if (prog == 6)
                {
                    prog = 7;
                }
                else if (prog == 7)
                {
                    prog = 10;
                    Session["playerProgression"] = prog;
                    return RedirectToAction("Combat");
                }
                else if (prog == 9)
                {
                    prog = 11;
                }
                else if (prog == 10)
                {
                    prog = 11;
                }
                else if (prog == 11)
                {
                    Session["playerProgression"] = 1;
                    return RedirectToAction("Logout", "Account");
                }
            }
            else if (command == "Action2")
            {
                if (prog == 1)
                {
                    prog = 2;
                }
                else if (prog == 4)
                {
                    prog = 6;
                }
                else if (prog == 7)
                {
                    prog = 9;
                }
                else if (prog == 11)
                {
                    Session["playerProgression"] = 1;
                    return RedirectToAction("Logout", "Account");
                }
            }
            Session["playerProgression"] = prog;

            return View("Play", GetEventsViewModel());
        }

        /// <summary>
        /// This method returns a created eventsviewmodel which loads its content from a database with the characterid stored in the session.
        /// </summary>
        /// <returns></returns>
        public EventsViewModel GetEventsViewModel()
        {
            RPGSQLContext rpgsqlContext = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(rpgsqlContext);
            Character cha = repo.GetById(Convert.ToInt32(Session["CharId"]));
            cha.InventoryList = repo.GetInventory(Convert.ToInt32(Session["CharId"]));
            cha.initWeapons();
            eventsViewModel.EventsSystem = new EventSystem();
            eventsViewModel.EventsSystem.character = cha;
            eventsViewModel.EventsSystem.ScenarioList = repo.GetStory();
            eventsViewModel.EventsSystem.playerProgression = Convert.ToInt32(Session["playerProgression"]);
            Session["Button1Name"] = eventsViewModel.EventsSystem.ScenarioList[(int)Session["playerProgression"] -1].Button1Text;
            Session["Button2Name"] = eventsViewModel.EventsSystem.ScenarioList[(int)Session["playerProgression"] -1].Button2Text;

            return eventsViewModel;
        }

        /// <summary>
        /// This method show the user the character creation page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {

            return View();
        }

        /// <summary>
        /// This method add a character to the database with the given parameters. it then redirect the user back to the character select screen.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="classid"></param>
        /// <param name="raceid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCharacter(string name, int classid, int raceid)
        {
            RPGSQLContext rpgsqlContext = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(rpgsqlContext);
            repo.AddCharacter((int)Session["UserID"], classid, raceid, name);
            _loginview.UserID = (int) Session["UserID"];
            return RedirectToAction("Character", _loginview);
        }

        /// <summary>
        /// This method loads the combat page and spawns 3 enemy's.
        /// </summary>
        /// <returns></returns>
        public ActionResult Combat()
        {
            if ((EnemyViewModel)Session["evm"] == null)
            {
                _enemyViewModel.EnemyList = new List<Enemy>();
                for (int i = 0; i < 3; i++)
                {
                    _enemyViewModel.EnemyList.Add(new Enemy(10, "Goblin", 200, 50, 9, 10));
                }
                Session["evm"] = _enemyViewModel;
            }
            _enemyViewModel = (EnemyViewModel)Session["evm"];
            return View("Combat", _enemyViewModel);
        }

        /// <summary>
        /// This method does damage to the enemys spawned in the Combat method.
        /// </summary>
        /// <param name="dmg"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Damageunit(int dmg, int id)
        {
            _enemyViewModel = (EnemyViewModel)Session["evm"];
            if (id == 1)
            {
                _enemyViewModel.EnemyList[0].Health = _enemyViewModel.EnemyList[0].Health - 50;
            }
            else if (id == 2)
            {
                _enemyViewModel.EnemyList[1].Health = _enemyViewModel.EnemyList[1].Health - 50;
            }
            else if (id == 3)
            {
                _enemyViewModel.EnemyList[2].Health = _enemyViewModel.EnemyList[2].Health - 50;
            }

            return View("Combat", _enemyViewModel);
        }
        

    }
}