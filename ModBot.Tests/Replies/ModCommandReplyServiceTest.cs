using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Core.Services;
using Mod_Bot.Services;
using Moq;

namespace ModBot.Tests.Replies
{
    [TestClass]
    public class ModCommandReplyServiceTest
    {

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        public void AddModResponse_AddModToDuty_ReturnSuccessMessage(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            user.Setup(x => x.Username).Returns("Test1");

            var roleService = new Mock<IRoleService>();
            roleService.Setup(x => x.AddMod(modType, user.Object)).Returns(true);

            var expected = $"Added {user.Object.Username}";

            var modCommandReplyService = new ModCommandReplyService(roleService.Object);

            //Test
            var actual = modCommandReplyService.AddModResponse(modType, user.Object);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        public void AddModResponse_AddModToDuty_ReturnErrorMessage(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            user.Setup(x => x.Username).Returns("Test1");

            var roleService = new Mock<IRoleService>();
            roleService.Setup(x => x.AddMod(modType, user.Object)).Returns(false);

            var expected = $"Something went wrong - {user.Object.Username} Not Added";

            var modCommandReplyService = new ModCommandReplyService(roleService.Object);

            //Test
            var actual = modCommandReplyService.AddModResponse(modType, user.Object);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        public void RemoveModResponse_RemoveModToDuty_ReturnErrorMessage(ModType modType)
        {
            var user = new Mock<IGuildUser>();
            user.Setup(x => x.Username).Returns("Test1");

            var roleService = new Mock<IRoleService>();
            roleService.Setup(x => x.RemoveMod(modType, user.Object)).Returns(false);

            var expected = $"Something went wrong - {user.Object.Username} was not removed";

            var modCommandReplyService = new ModCommandReplyService(roleService.Object);

            //Test
            var actual = modCommandReplyService.RemoveModResponse(modType, user.Object);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [DataRow(ModType.ChatDuty)]
        [DataRow(ModType.Tributes)]
        [DataRow(ModType.Counters)]
        public void DisplayModResponse_DisplayModsForDuty_ReturnModsForTypeMessage(ModType modType)
        {
            var user1 = new Mock<IGuildUser>();
            user1.Setup(x => x.Username).Returns("Test1");

            var user2 = new Mock<IGuildUser>();
            user2.Setup(x => x.Username).Returns("Test2");

            var mods = new List<IGuildUser>() {user1.Object, user2.Object};

            var roleService = new Mock<IRoleService>();
            roleService.Setup(x => x.GetMods(modType)).Returns(mods);

            var modTypeString = "";
            switch (modType)
            {
                    case ModType.ChatDuty:
                        modTypeString = "Chat Duty";
                        break;
                    case ModType.Tributes:
                        modTypeString = "Tributes Duty";
                        break;
                case ModType.Counters:
                    modTypeString = "Counters Duty";
                    break;
            }

            var expected = $"{modTypeString}:\n{string.Join("\n", mods.Select(x => x.Username))}\r\n";

            var modCommandReplyService = new ModCommandReplyService(roleService.Object);

            //Test
            var actual = modCommandReplyService.DisplayModsResponse(modType);

            Assert.AreEqual(expected, actual);

        }

    }
}
