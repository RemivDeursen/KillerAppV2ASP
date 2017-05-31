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
            throw new NotImplementedException();
        }

        public bool TryLogin(string username, string password)
        {
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Validate_User"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", Login1.UserName);
                    cmd.Parameters.AddWithValue("@Password", Login1.Password);
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                switch (userId)
                {
                    case -1:
                        Login1.FailureText = "Username and/or password is incorrect.";
                        break;
                    case -2:
                        Login1.FailureText = "Account has not been activated.";
                        break;
                    default:
                        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                        break;
                }
            }
            throw new NotImplementedException();
        }
    }
}