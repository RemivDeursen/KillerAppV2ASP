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
        Character GetCharacterByName(string name);
        DataTable GetAttributes(string name);
        List<string> GetStory();
        DataTable GetItems();
        List<Item> GetInventory(string name);
        List<Character> GetCharactersFromUser(int userId);

    }
}
