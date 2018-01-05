using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_Bot.Commands
{
    public interface IModCommand
    {

        Task AddMod(SocketGuildUser socketGuildUser);

        Task RemoveMod(SocketGuildUser socketGuildUser);

        Task DisplayMods();

        Task PurgeMods();

    }
}
