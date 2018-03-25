using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ModBot.Core.Services;
using Mod_Bot.Commands;

namespace ModBot.Core.Commands
{
    [Group("prduty")]
    public class PRDutyCommandSet : ModuleBase<SocketCommandContext>, IModCommandSet
    {

        private readonly ICommandReplyService _commandReplyService;

        public ModType ModType => ModType.PRDuty;

        public PRDutyCommandSet(ICommandReplyService commandReplyService)
        {
            _commandReplyService = commandReplyService;
        }

        [Command("add")]
        public async Task HandleAddCommand(IGuildUser socketGuildUser)
        {
            await ReplyAsync(_commandReplyService.AddModResponse(ModType, socketGuildUser));
        }

        [Command("remove")]
        public async Task HandleRemoveCommand(IGuildUser socketGuildUser)
        {
            await ReplyAsync(_commandReplyService.RemoveModResponse(ModType, socketGuildUser));
        }

        [Command]
        public async Task HandleDisplayCommand()
        {
            await ReplyAsync(_commandReplyService.DisplayModsResponse(ModType));
        }

        [Command("purge")]
        public async Task HandlePurgeCommand()
        {
            await ReplyAsync(_commandReplyService.PurgeModsResponse(ModType));
        }

    }
}
