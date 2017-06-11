using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Player : Character
    {
        public Player(int CharacterID, string name, int HP, int Mana, int wepid, int armorid) : base (CharacterID, name, HP, Mana, wepid, armorid)
        {

        }
    }
}