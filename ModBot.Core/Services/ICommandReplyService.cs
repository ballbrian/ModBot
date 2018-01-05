using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;

namespace ModBot.Core.Services
{
    public interface ICommandReplyService
    {

        string AddModResponse(ModType modType, IGuildUser socketUser);

        string RemoveModResponse(ModType modType, IGuildUser socketUser);

        string DisplayModsResponse(ModType modType);

        string PurgeModsResponse(ModType modType);

    }
}
