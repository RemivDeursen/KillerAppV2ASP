using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace KillerAppV2ASP.Models.DBO
{
    public class RPGSQLContext : IRPGContext
    {
        private static SqlConnection conString = new SqlConnection(
            "Server=mssql.fhict.local;Database=dbi348991;User Id = dbi348991; Password=banaan;");

        public DataTable GetAttributes(string name)
        {
            throw new NotImplementedException();
        }

        public Character GetCharacterByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Character> GetCharactersFromUser(int userId)
        {
            List<Character> returnlist = new List<Character>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            DataTable dt = new DataTable();
            cmd.CommandText =
                "SELECT ch.CharacterID, ch.weaponid, ch.armorid, ch.Name, at.Health, at.Mana FROM Character ch join Class cl on ch.ClassID = cl.ClassID join BaseAttributes ba on ba.ClassID = cl.ClassID join Attributes at on at.AttributeID = ba.AttributeID join User_Characters uc on uc.characterID = ch.CharacterID where uc.UserID = " + userId;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnlist.Add(new Character(Convert.ToInt32(reader["CharacterID"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["Health"]), Convert.ToInt32(reader["Mana"]), Convert.ToInt32(reader["weaponid"]), Convert.ToInt32(reader["armorid"])));
            }
            conString.Close();
            return returnlist;
        }

        public List<Item> GetInventory(int id)
        {
            string query = "select itm.itemid, itm.name, itm.durability, cat.name from Character ch join Inventory inv on inv.CharacterID = ch.CharacterID join Item itm on itm.ItemID = inv.ItemID join Category cat on cat.CategoryID = itm.ItemID where ch.CharacterID = " + id;
            List<Item> items = new List<Item>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;
            
            conString.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Item(Convert.ToInt32(reader["itemid"]), Convert.ToString(reader["name"]), Convert.ToInt32(reader["durability"]), Convert.ToString(reader["name"])));
            }
            conString.Close();
            return items;
        }

        public Item GetItemById(int id)
        {
            string query = "select itm.itemid, itm.name, itm.durability, cat.Name as catname from item itm join Category cat on cat.CategoryID = itm.CategoryID where itm.ItemID = @id";
            Item itm = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                itm = new Item(Convert.ToInt32(reader["itemid"]), Convert.ToString(reader["name"]), Convert.ToInt32(reader["durability"]), Convert.ToString(reader["catname"]));
            }
            conString.Close();
            return itm;
        }

        public void ExecuteDamageToItem(int ItemID)
        {
            SqlCommand cmd = new SqlCommand("DamageDurability", conString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ItemID", ItemID));
            conString.Open();
            cmd.ExecuteNonQuery();
            conString.Close();
        }

        public DataTable GetItems()
        {
            throw new NotImplementedException();
        }

        public List<StoryItems> GetStory()
        {
            List<StoryItems> returnlist = new List<StoryItems>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            DataTable dt = new DataTable();
            cmd.CommandText =
                "Select storytext, storybutton1, storybutton2 from story";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnlist.Add(new StoryItems(Convert.ToString(reader["storytext"]), Convert.ToString(reader["storybutton1"]), Convert.ToString(reader["storybutton2"])));
            }
            conString.Close();
            return returnlist;
        }

        public bool TryLogin(string username, string password)
        {
            bool succes = false;
            using (SqlConnection conn = new SqlConnection("Server=mssql.fhict.local;Database=dbi348991;User Id = dbi348991; Password=banaan;"))
            {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from Users where UserName = @uname and Password = @upw", conn);

                    cmd.Parameters.AddWithValue("@uname", username);
                    cmd.Parameters.AddWithValue("@upw", password);

                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        succes = true;
                    }
                    else
                    {
                        succes = false;
                }
            }

            return succes;
        }

        public int GetUserId(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection("Server=mssql.fhict.local;Database=dbi348991;User Id = dbi348991; Password=banaan;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select userid from Users where UserName = @uname and Password = @upw", conn);

                cmd.Parameters.AddWithValue("@uname", username);
                cmd.Parameters.AddWithValue("@upw", password);

                
                int userid = (int)cmd.ExecuteScalar();
                return userid;
            }
            
        }

        public void AddUserToDB(string naam, string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into [dbo].[Users] (UserName, Password)\r\nvalues (\'" + naam +"\', \'" + password + "\')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            cmd.ExecuteReader();
            conString.Close();
        }

        public Character GetById(int id)
        {
            Character character = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "SELECT ch.CharacterID, ch.Name, at.Health, at.Mana, ch.weaponid, ch.armorid FROM Character ch  join Class cl on ch.ClassID = cl.ClassID join BaseAttributes ba on ba.ClassID = cl.ClassID join Attributes at on at.AttributeID = ba.AttributeID join User_Characters uc on uc.characterID = ch.CharacterID where ch.CharacterID = " + id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                character = new Character(Convert.ToInt32(reader["CharacterID"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["Health"]), Convert.ToInt32(reader["Mana"]), Convert.ToInt32(reader["weaponid"]), Convert.ToInt32(reader["armorid"]));
            }
            conString.Close();
            return character;

        }
        public int GetProgressById(int id)
        {
            int prog = 0;
            Character character = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "SELECT select progress from character where CharacterID = @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prog = Convert.ToInt32(reader.Read());
            }
            conString.Close();
            return prog;
        }

        public void SetProgressById(int id)
        {

        }

        public void ExecuteRepairDurability(int ItemID)
        {
            SqlCommand cmd = new SqlCommand("RepairDurability", conString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ItemID", ItemID));
            conString.Open();
            cmd.ExecuteNonQuery();
            conString.Close();
        }

        public void AddCharacter(int userid, int classid, int raceid, string name)
        {
            SqlCommand cmd = new SqlCommand("CreateCharacter", conString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserID", userid));
            cmd.Parameters.Add(new SqlParameter("@ClassID", classid));
            cmd.Parameters.Add(new SqlParameter("@RaceID", raceid));
            cmd.Parameters.Add(new SqlParameter("@Name", name));
            conString.Open();
                cmd.ExecuteNonQuery();
                conString.Close();
        }
    }
}