using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Weapon : Item
    {
        private int damage;


        public Weapon(int damage)
        {
            this.damage = damage;
        }
    }
}