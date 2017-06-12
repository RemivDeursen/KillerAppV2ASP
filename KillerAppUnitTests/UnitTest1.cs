using System;
using System.Collections.Generic;
using KillerAppV2ASP.Controllers;
using KillerAppV2ASP.Models;
using KillerAppV2ASP.Models.DBO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerAppUnitTests
{
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// To test a new account add another number after the testlogin/testpassword
        /// </summary>
        [TestMethod]
        public void Account_Add()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);
            string testlogin = "Testlogin4";
            string testPassword = "Testpassword4";
            //Add user with a test login/password
            repo.AddUserToDB(testlogin, testPassword);

            //Create a user with the created account
            User loggedUser = new User();
            loggedUser.Username = testlogin;
            loggedUser.Password = testPassword;

            //Test correct password entry with the newly created account
            Assert.AreEqual(true, repo.TryLogin(loggedUser.Username, loggedUser.Password));

            //Test incorrect password entry
            Assert.AreEqual(false, repo.TryLogin(loggedUser.Username, "Randompass"));
        }

        [TestMethod]
        public void UserLoginTest()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);

            User loggedUser = new User();
            loggedUser.Username = "Bob";
            loggedUser.Password = "Pass";
            //Test correct password entry
            Assert.AreEqual(true, repo.TryLogin(loggedUser.Username, loggedUser.Password));
            //Test incorrect password entry
            Assert.AreEqual(false, repo.TryLogin(loggedUser.Username, "Randompass"));
        }

        [TestMethod]
        public void DatabaseDataTest()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);
            Character character = repo.GetById(1);

            //Character info test
            Assert.AreEqual("Boro", character.Name);
            Assert.AreEqual(9, character.Wepid);
            Assert.AreEqual(12, character.Armorid);
            Assert.AreEqual(250, character.HP);
            Assert.AreEqual(25, character.Mana);
        }

        /// <summary>
        /// If you get errors, change the testname variable to a new name.
        /// </summary>
        [TestMethod]
        public void Add_Character()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);
            string testname = "TestUserCharacter9";
            //Add a test character to userid 2, with class and race id 2 and the name TestUserCharacter
            repo.AddCharacter(2, 2, 2, testname);

            //Get the character from the database
            Character character = null;
            List<Character> characters = repo.GetCharactersFromUser(2);
            foreach (Character chars in characters)
            {
                if (chars.Name == testname)
                {
                     character = chars;
                }
            }

            //Character info test
            Assert.AreEqual(testname, character.Name);
        }

        [TestMethod]
        public void Select_Character()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);
            User user = new User();
            user.Username = "Bob";
            user.Password = "Pass";

            //Get the userid from the logged user
            int userid = repo.GetUserId(user.Username, user.Password);

            //Get the character from the user
            List<Character> characters = repo.GetCharactersFromUser(userid);

            //Send selected character to login view
            RPGController rpgController = new RPGController();
            rpgController.Play(characters[1].CharacterID);
            
        }

    }
}
