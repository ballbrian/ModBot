using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Mod_Bot.Services;

namespace Mod_Bot.Commands
{
    [Group("counters")]
    public class CountersDutyCommand : ModuleBase<SocketCommandContext>, IModCommand
    {
        private readonly IRoleService _roleService;

        protected ModType ModType => ModType.Counters;

        public CountersDutyCommand(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Command("add")]
        public async Task AddMod(SocketGuildUser socketGuildUser)
        {
            await _roleService.AddMod(Context, ModType, socketGuildUser);
        }

        [Command("remove")]
        public async Task RemoveMod(SocketGuildUser socketGuildUser)
        {
            await _roleService.RemoveMod(Context, ModType, socketGuildUser);
        }

        [Command]
        public async Task DisplayMods()
        {
            await _roleService.DisplayMods(Context, ModType);
        }

        [Command("purge")]
        public async Task PurgeMods()
        {
            await _roleService.PurgeMods(Context, ModType);
        }
    }
}
