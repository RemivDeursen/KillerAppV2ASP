using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerAppV2ASP.Models.DBO;

namespace KillerAppV2ASP.Models
{
    public class Character
    {
        public int CharacterID { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public Item Weapon { get; set; }
        public Item Armor { get; set; }
        public List<Item> InventoryList { get; set; }
        public int Wepid { get; set; }
        public int Armorid { get; set; }

        public Character(int CharacterID, string name, int hp, int mana, int wepid, int armorid)
        {
            this.CharacterID = CharacterID;
            this.Name = name;
            this.HP = hp;
            this.Mana = mana;
            this.Wepid = wepid;
            this.Armorid = armorid;
        }

        public void initWeapons()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);

            Weapon = repo.GetItemById(Wepid);
            Armor = repo.GetItemById(Armorid);
        }
    }
}