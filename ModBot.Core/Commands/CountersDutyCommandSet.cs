using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.Core.Services;
using Mod_Bot.Services;

namespace Mod_Bot.Commands
{
    [Group("counters")]
    public class CountersDutyCommandSet : ModuleBase<SocketCommandContext>, IModCommandSet
    {
        private readonly ICommandReplyService _commandReplyService;

        public ModType ModType => ModType.Counters;

        public CountersDutyCommandSet(ICommandReplyService commandReplyService)
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
