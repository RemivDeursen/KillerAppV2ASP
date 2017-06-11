using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerAppV2ASP.Models.DBO
{
    public interface IRPGContext
    {
        bool TryLogin(string username, string password);
        void AddUserToDB(string name, string password);
        Character GetCharacterByName(string name);
        void AddCharacter(int userid, int classid, int raceid, string name);
        Character GetById(int id);
        DataTable GetAttributes(string name);
        List<StoryItems> GetStory();
        DataTable GetItems();
        List<Item> GetInventoryById(int id);
        List<Character> GetCharactersFromUser(int userId);
        int GetUserId(string username, string password);
        int GetProgressById(int id);
        void SetProgressById(int id);
        Item GetItemById(int id);
        void ExecuteDamageToItem(int ItemID);
        void ExecuteRepairDurability(int ItemID);
        int GetItemDrop(int CharId);

    }
}
