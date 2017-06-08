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

        public Character GetById(int id)
        {
            return context.GetById(id);
        }

        public List<Item> GetInventory(string name)
        {
            return context.GetInventory(name);
        }

        public DataTable GetItems()
        {
            return context.GetItems();
        }

        public List<StoryItems> GetStory()
        {
            return context.GetStory();
        }

        public bool TryLogin(string username, string password)
        {
            return context.TryLogin(username, password);
        }

        public void AddUserToDB(string name, string password)
        {
            context.AddUserToDB(name, password);
        }

        public List<Character> GetCharactersFromUser(int userID)
        {
            return context.GetCharactersFromUser(userID);
        }

        public int GetUserId(string username, string password)
        {
            return context.GetUserId(username, password);
        }

        public int GetProgressById(int id)
        {
            return context.GetProgressById(id);
        }

        public void SetProgressById(int id)
        {
            context.SetProgressById(id);
        }
    }
}