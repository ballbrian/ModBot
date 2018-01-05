using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.Core.Services;
using Mod_Bot.Services;

namespace Mod_Bot.Commands
{
    [Group("Modduty")]
    public class ModDutyCommand : ModuleBase<SocketCommandContext>   
    {
        private readonly ICommandReplyService _commandReplyService;

        protected ModType ModType => ModType.ModDuty;

        public ModDutyCommand(ICommandReplyService commandReplyService)
        {
            _commandReplyService = commandReplyService;
        }

        [Command]
        public async Task GetMods()
        {
            await ReplyAsync(_commandReplyService.DisplayModsResponse(ModType));
        }

        [Command("purge")]
        public async Task PurgeMods()
        {
            await ReplyAsync(_commandReplyService.PurgeModsResponse(ModType));
        }
    }
}
