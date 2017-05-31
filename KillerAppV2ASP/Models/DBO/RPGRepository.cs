using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models.DBO
{
    public class RPGRepository
    {
        private IRPGContext context;

        public RPGRepository(IRPGContext context)
        {
            this.context = context;
        }
        public DataTable GetAttributes(string name)
        {
            return context.GetAttributes(name);
        }

        public Character GetCharacterByName(string name)
        {
            return context.GetCharacterByName(name);
        }

        public List<Item> GetInventory(string name)
        {
            return context.GetInventory(name);
        }

        public DataTable GetItems()
        {
            return context.GetItems();
        }

        public List<string> GetStory()
        {
            return context.GetStory();
        }

        public bool TryLogin(string username, string password)
        {
            return context.TryLogin(username, password);
        }
    }
}