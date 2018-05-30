using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ModBot.Core.Services;

namespace ModBot.Core.Commands
{
    public class RedemptionCommandSet : ModuleBase<SocketCommandContext>
    {
        private readonly IRedemptionCommandReplyService _redemptionCommandReplyService;

        public RedemptionCommandSet(IRedemptionCommandReplyService redemptionCommandReplyService)
        {
            _redemptionCommandReplyService = redemptionCommandReplyService;
        }

        [Command("tribrr")]
        public async Task HandleGrantRedemptionCommand()
        {
            await ReplyAsync(_redemptionCommandReplyService.GrantRedemptionRoundResponse());
        }

        [Command("tribkk")]
        public async Task HandleKabbyKommandmentCommand()
        {
            await ReplyAsync(_redemptionCommandReplyService.GrantKabbysKommandmentResponse());
        }

    }
}
