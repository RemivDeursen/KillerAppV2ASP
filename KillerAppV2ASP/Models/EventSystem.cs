using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class EventSystem
    {
        public Player player { get; private set; }
        public List<string> ScenarioList = new List<string>();
        

        public EventSystem(Player player)
        {
            this.player = player;
        }

        public List<string> GetScenarioFromDB()
        {

            return null;
        }

        public void ExecutePlayerCommand(string command)
        {
            
        }

        public void LoadCharacter(string username, string pass)
        {
            
        }

        public void CreateNewCharacter(string name, string classname, string racename)
        {
            
        }
    }
}