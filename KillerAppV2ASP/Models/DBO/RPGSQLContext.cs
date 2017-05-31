using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models.DBO
{
    public class RPGSQLContext : IRPGContext
    {
        public DataTable GetAttributes(string name)
        {
            throw new NotImplementedException();
        }

        public Character GetCharacterByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetInventory(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetItems()
        {
            throw new NotImplementedException();
        }

        public List<string> GetStory()
        {
            throw new NotImplementedException();
        }

        public bool TryLogin(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}