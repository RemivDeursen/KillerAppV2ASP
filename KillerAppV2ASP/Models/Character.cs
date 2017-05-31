using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Character
    {
        public string name { get; private set; }
        public int HP { get; private set; }
        public int Mana { get; private set; }

        public Character(string name, int HP, int Mana)
        {
            this.name = name;
            this.HP = HP;
            this.Mana = Mana;
        }
    }
}