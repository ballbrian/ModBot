using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mod_Bot.Services;
using Moq;

namespace ModBot.Tests.Services
{
    [TestClass]
    public class RoleServiceTests
    {
        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        [DataRow(ModType.PRDuty)]
        [DataRow(ModType.Bets)]
        public void AddMod_AddUserWithSpecificModType_ReturnTrue(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            var test = new RoleService();

            var currentCount = test.GetMods(modType).Count;

            //Test
            var actual = test.AddMod(modType, user.Object);

            var expectedCount = currentCount + 1;

            var testList = test.GetMods(modType);

            Assert.IsTrue(actual);
            Assert.AreEqual(expectedCount, testList.Count);
            Assert.IsTrue(testList.Contains(user.Object));

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        [DataRow(ModType.PRDuty)]
        [DataRow(ModType.Bets)]
        public void RemoveMod_AddUserWithSpecificModType_ReturnTrue(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            var test = new RoleService();           

            test.AddMod(modType, user.Object);

            var currentCount = test.GetMods(modType).Count;

            //Test
            var actual = test.RemoveMod(modType, user.Object);

            var expectedCount = currentCount - 1;
            var testList = test.GetMods(modType);

            Assert.IsTrue(actual);
            Assert.AreEqual(expectedCount, testList.Count);
            Assert.IsFalse(testList.Contains(user.Object));

        }

        [TestMethod]
        public void RemoveMod_RemoveModWhileOtherMod_ReturnTrue()
        {
            var user1 = new Mock<IGuildUser>();
            var user1ModType = ModType.ChatDuty;
            var user2 = new Mock<IGuildUser>();
            var user2ModType = ModType.Counters;
            var test = new RoleService();

            test.AddMod(user1ModType, user1.Object);
            test.AddMod(user2ModType, user2.Object);

            //Test
            var actual = test.RemoveMod(user1ModType, user1.Object);
            
            var chatDutyMods = test.GetMods(user1ModType);
            var counterMods = test.GetMods(user2ModType);

            Assert.IsTrue(actual);
            Assert.AreEqual(0, chatDutyMods.Count);
            Assert.IsFalse(chatDutyMods.Contains(user1.Object));
            Assert.IsTrue(counterMods.Contains(user2.Object));

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        [DataRow(ModType.PRDuty)]
        [DataRow(ModType.Bets)]
        public void DisplayMods_DisplayModsForSpecificRoleWithAtLeastOneMod_ReturnListOfOneModForModRole(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            var test = new RoleService();

            test.AddMod(modType, user.Object);

            //Test
            var testList = test.GetMods(modType);

            Assert.IsTrue(testList.Count == 1);
            Assert.IsInstanceOfType(testList, typeof(List<IGuildUser>));

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        [DataRow(ModType.PRDuty)]
        [DataRow(ModType.Bets)]
        public void DisplayMods_DisplayModsForSpecificRoleWithTwoMods_ReturnListOfTwoModsForModRole(ModType modType)
        {
            var user1 = new Mock<IGuildUser>();
            var user2 = new Mock<IGuildUser>();
            var test = new RoleService();

            test.AddMod(modType, user1.Object);
            test.AddMod(modType, user2.Object);

            //Test
            var testList = test.GetMods(modType);

            Assert.IsTrue(testList.Count == 2);
            Assert.IsInstanceOfType(testList, typeof(List<IGuildUser>));

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        [DataRow(ModType.PRDuty)]
        [DataRow(ModType.Bets)]
        public void PurgeMods_PurgeModsForSpecificRoleWithOneMods_ReturnTrue(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            var test = new RoleService();

            test.AddMod(modType, user.Object);            

            //Test
            var actual = test.PurgeMods(modType);

            var testList = test.GetMods(modType);

            Assert.IsTrue(actual);
            Assert.IsTrue(testList.Count == 0);
            Assert.IsFalse(testList.Contains(user.Object));

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        [DataRow(ModType.PRDuty)]
        [DataRow(ModType.Bets)]
        public void PurgeMods_PurgeModsForSpecificRoleWithTwoMods_ReturnTrue(ModType modType)
        {
            var user1 = new Mock<IGuildUser>();
            var user2 = new Mock<IGuildUser>();
            var test = new RoleService();

            test.AddMod(modType, user1.Object);
            test.AddMod(modType, user2.Object);

            //Test
            var actual = test.PurgeMods(modType);

            var testList = test.GetMods(modType);

            Assert.IsTrue(actual);
            Assert.IsTrue(testList.Count == 0);
            Assert.IsFalse(testList.Contains(user1.Object));
            Assert.IsFalse(testList.Contains(user2.Object));

        }

        [TestMethod]
        [DataRow(ModType.ModDuty)]
        public void RemoveMod_RemoveUser1FromAllRoles_ReturnTrue(ModType modType)
        {
            var user1 = new Mock<IGuildUser>();
            var user2 = new Mock<IGuildUser>();
            var test = new RoleService();

            test.AddMod(ModType.ChatDuty, user1.Object);
            test.AddMod(ModType.Counters, user1.Object);
            test.AddMod(ModType.Tributes, user1.Object);

            test.AddMod(ModType.ChatDuty, user2.Object);
            test.AddMod(ModType.Counters, user2.Object);
            test.AddMod(ModType.Tributes, user2.Object);

            //Test
            var actual = test.RemoveMod(modType, user1.Object);

            var testList = test.GetMods(modType);

            Assert.IsTrue(actual);
            Assert.IsTrue(testList.Count == 3);
            Assert.IsFalse(testList.Contains(user1.Object));
            Assert.IsTrue(testList.Contains(user2.Object));

        }

    }
}
