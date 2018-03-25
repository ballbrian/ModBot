using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ModBot.Core.Services;
using Mod_Bot.Commands;

namespace ModBot.Core.Commands
{
    [Group("tribgrats")]
    public class TributesCommandSet : ModuleBase<SocketCommandContext>
    {
        private readonly ITributeCommandReplyService _tributeCommandReplyService;

        public TributesCommandSet(ITributeCommandReplyService tributeCommandReplyService)
        {
            _tributeCommandReplyService = tributeCommandReplyService;
        }

        [Command]
        public async Task HandleSetTributesCommand(string tributes)
        {
            await ReplyAsync(_tributeCommandReplyService.SetTributesResponse(tributes));
        }

        [Command]
        public async Task HandleSetTributesCommand(string tribute1, string tribute2)
        {
            await ReplyAsync(_tributeCommandReplyService.SetTributesResponse(tribute1, tribute2));
        }

        [Command]
        public async Task HandleGetTributesCommand()
        {
            await ReplyAsync(_tributeCommandReplyService.GetTributesResponse());
        }

        [Command("purge")]
        public async Task HandlePurgeTributesCommand()
        {
            await ReplyAsync(_tributeCommandReplyService.PurgeTributesResponse());
        }

        [Command("rr")]
        public async Task HandleGrantRedemptionCommand()
        {
            await ReplyAsync(_tributeCommandReplyService.GrantRedemptionRoundResponse());
        }

    }
}
