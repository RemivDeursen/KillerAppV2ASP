using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Enemy : Character
    {
        public Enemy(int CharacterID, string name, int HP, int Mana, int wepid, int armorid) : base (CharacterID, name, HP, Mana, wepid, armorid)
        {
            
        }
    }
}