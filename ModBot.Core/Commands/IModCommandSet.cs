using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Mod_Bot.Commands
{
    public interface IModCommandSet
    {

        Task HandleAddCommand(IGuildUser socketGuildUser);

        Task HandleRemoveCommand(IGuildUser socketGuildUser);

        Task HandleDisplayCommand();

        Task HandlePurgeCommand();

    }
}
