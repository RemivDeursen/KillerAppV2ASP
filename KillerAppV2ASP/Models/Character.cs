using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Character
    {
        public int CharacterID { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }

        public Character(int CharacterID, string name, int HP, int Mana)
        {
            this.CharacterID = CharacterID;
            this.Name = name;
            this.HP = HP;
            this.Mana = Mana;
        }
    }
}