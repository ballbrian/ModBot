using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Mod_Bot.Services
{
    public interface IRoleService
    {
   
        bool AddMod(ModType modType, IGuildUser socketUser);

        bool RemoveMod(ModType modType, IGuildUser socketUser);

        List<IGuildUser> GetMods(ModType modType);

        bool PurgeMods(ModType modType);

    }
}
