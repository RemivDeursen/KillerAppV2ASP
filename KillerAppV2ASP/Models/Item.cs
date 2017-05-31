using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class Item : IDestructable
    {
        private string name;
        private int durability;
        private string category;

        public void DamageItem(int damage)
        {
            throw new NotImplementedException();
        }

        public void RepairItem()
        {
            throw new NotImplementedException();
        }
    }
}