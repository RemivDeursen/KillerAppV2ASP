using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerAppV2ASP.Models.DBO;

namespace KillerAppV2ASP.Models
{
    public class Item : IDestructable
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Durability { get; set; }
        public string Category { get; set; }

        public Item(int id, string name, int dura, string category)
        {
            ItemId = id;
            Name = name;
            Durability = dura;
            Category = category;
        }
        public void DamageItem(int damage)
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);

            repo.ExecuteDamageToItem(ItemId);
        }

        public void RepairItem()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);

            repo.ExecuteRepairDurability(ItemId);
        }
    }
}