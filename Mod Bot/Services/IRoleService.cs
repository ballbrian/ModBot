using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Mod_Bot.Services
{
    public interface IRoleService
    {

        Task AddMod(ICommandContext context, ModType modType, SocketGuildUser socketUser);

        Task RemoveMod(ICommandContext context, ModType modType, SocketGuildUser socketUser);

        Task DisplayMods(ICommandContext context, ModType modType);

        Task PurgeMods(ICommandContext context, ModType modType);

    }
}
