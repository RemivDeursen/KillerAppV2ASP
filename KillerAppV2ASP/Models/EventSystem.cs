using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class EventSystem
    {
        public Player player { get; private set; }
        public Character character { get; set; }
        public List<StoryItems> ScenarioList = new List<StoryItems>();
        public int playerProgression { get; set; }
        public int UserId { get; set; }


        public EventSystem()
        {
        }
    }
}