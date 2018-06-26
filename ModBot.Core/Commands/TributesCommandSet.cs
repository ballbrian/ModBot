using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ModBot.Core.Services;
using Mod_Bot.Commands;

namespace ModBot.Core.Commands
{
    [Group("tribute")]
    public class TributesCommandSet : ModuleBase<SocketCommandContext>
    {
        private readonly ITributeCommandReplyService _tributeCommandReplyService;

        public TributesCommandSet(ITributeCommandReplyService tributeCommandReplyService)
        {
            _tributeCommandReplyService = tributeCommandReplyService;
        }

        [Command]
        public async Task HandleSetTributesCommand([Remainder] string tributes)
        {
            await ReplyAsync(_tributeCommandReplyService.SetTributesResponse(tributes));
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

    }
}
