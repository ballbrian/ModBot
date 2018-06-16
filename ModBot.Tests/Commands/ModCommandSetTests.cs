using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Core.Commands;
using ModBot.Core.Services;
using Mod_Bot.Commands;
using Moq;

namespace ModBot.Tests.Commands
{
    [TestClass]
    public class ModCommandSetTests
    {

        [TestMethod]
        public void Constructor_CreateInstance_ModTypeIsChatDuty()
        {
            var modType = ModType.ChatDuty;

            var commandReplyService = new Mock<ICommandReplyService>();

            var test = new ChatDutyCommandSet(commandReplyService.Object);            

            Assert.AreEqual(modType, test.ModType);            
        }

        [TestMethod]
        public void Constructor_CreateInstance_ModTypeIsTributesDuty()
        {
            var modType = ModType.Tributes;

            var commandReplyService = new Mock<ICommandReplyService>();

            var test = new TributeDutyCommandSet(commandReplyService.Object);

            Assert.AreEqual(modType, test.ModType);
        }

        [TestMethod]
        public void Constructor_CreateInstance_ModTypeIsCountersDuty()
        {
            var modType = ModType.Counters;

            var commandReplyService = new Mock<ICommandReplyService>();

            var test = new CountersDutyCommandSet(commandReplyService.Object);

            Assert.AreEqual(modType, test.ModType);
        }

        [TestMethod]
        public void Constructor_CreateInstance_ModTypeIsBetsDuty()
        {
            var modType = ModType.Bets;

            var commandReplyService = new Mock<ICommandReplyService>();

            var test = new BetsDutyCommandSet(commandReplyService.Object);

            Assert.AreEqual(modType, test.ModType);
        }

    }
}
