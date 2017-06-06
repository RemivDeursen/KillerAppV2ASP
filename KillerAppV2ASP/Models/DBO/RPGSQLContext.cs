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
                "SELECT ch.CharacterID, ch.Name, at.Health, at.Mana \r\nFROM Character ch\r\njoin Class cl on ch.ClassID = cl.ClassID\r\njoin BaseAttributes ba on ba.ClassID = cl.ClassID\r\njoin Attributes at on at.AttributeID = ba.AttributeID\r\njoin User_Characters uc on uc.characterID = ch.CharacterID\r\nwhere uc.UserID = " +userId;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnlist.Add(new Character(Convert.ToInt32(reader["CharacterID"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["Health"]), Convert.ToInt32(reader["Mana"])));
            }
            conString.Close();
            return returnlist;
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
            List<string> returnlist = new List<string>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            DataTable dt = new DataTable();
            cmd.CommandText =
                "Select storytext from story";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnlist.Add(Convert.ToString(reader["storytext"]));
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
                "SELECT ch.CharacterID, ch.Name, at.Health, at.Mana\r\nFROM Character ch \r\njoin Class cl on ch.ClassID = cl.ClassID\r\njoin BaseAttributes ba on ba.ClassID = cl.ClassID\r\njoin Attributes at on at.AttributeID = ba.AttributeID\r\njoin User_Characters uc on uc.characterID = ch.CharacterID\r\nwhere ch.CharacterID = " + id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conString;

            conString.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                character = new Character(Convert.ToInt32(reader["CharacterID"]), Convert.ToString(reader["Name"]),
                    Convert.ToInt32(reader["Health"]), Convert.ToInt32(reader["Mana"]));
            }
            conString.Close();
            return character;

        }
    }
}