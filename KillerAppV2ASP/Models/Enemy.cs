using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Enemy : Character
    {
        public Enemy(string name, int HP, int Mana) : base (name, HP, Mana)
        {
            
        }
    }
}