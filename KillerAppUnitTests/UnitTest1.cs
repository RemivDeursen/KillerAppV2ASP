using System;
using KillerAppV2ASP.Models;
using KillerAppV2ASP.Models.DBO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerAppUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DatabaseCharacterTest()
        {
            RPGSQLContext context = new RPGSQLContext();
            RPGRepository repo = new RPGRepository(context);
            Character character = repo.GetById(1);
            
            Assert.AreEqual("Boro", character.Name);
            Assert.AreEqual(9, character.Wepid);
            Assert.AreEqual(12, character.Armorid);
            Assert.AreEqual(250, character.HP);
            Assert.AreEqual(25, character.Mana);
        }
    }
}
