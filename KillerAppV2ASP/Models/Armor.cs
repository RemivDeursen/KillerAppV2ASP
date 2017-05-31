using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Armor : Item
    {
        private int damageReduction;

        public Armor(int damageReduction)
        {
            this.damageReduction = damageReduction;
        }
    }
}