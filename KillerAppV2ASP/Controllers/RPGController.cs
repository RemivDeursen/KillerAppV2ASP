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
        private UserLoginViewModel _loginview = new UserLoginViewModel();
        private CharacterViewModel _charViewModel;
        private EventsViewModel eventsViewModel = new EventsViewModel();
        private GloballyAccessibleClass GaClass;
        RPGSQLContext rpgct = new RPGSQLContext();
        
        // GET: Character
        public ActionResult Character(UserLoginViewModel loginView)
        {
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
            Session["CharId"] = character.CharacterID;
            Session["playerProgression"] = 1;
            Session["Button1Name"] = eventsViewModel.EventsSystem.ScenarioList[(int)Session["playerProgression"] -1].Button1Text;
            Session["Button2Name"] = eventsViewModel.EventsSystem.ScenarioList[(int)Session["playerProgression"] -1].Button2Text;

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
                    prog = 3;
                }
                else if (prog == 2)
                {
                    //add combat redirect
                    prog = 4;
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
                    prog = 8;
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

        public ActionResult Create()
        {
            RPGSQLContext rpgsqlContext = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(rpgsqlContext);

            return View();
        }

        [HttpPost]
        public ActionResult AddCharacter(string name, int classid, int raceid)
        {
            RPGSQLContext rpgsqlContext = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(rpgsqlContext);
            repo.AddCharacter((int)Session["UserID"], classid, raceid, name);
            _loginview.UserID = (int) Session["UserID"];
            return RedirectToAction("Character", _loginview);
        }
    }
}