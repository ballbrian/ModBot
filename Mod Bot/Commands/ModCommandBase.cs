using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Mod_Bot.Services;

namespace Mod_Bot.Commands
{

    //TODO Discord.Net Does Not Find Base Type Commands :( 
    //https://github.com/RogueException/Discord.Net/issues/650

//    public abstract class ModCommandBase : ModuleBase<SocketCommandContext>
//    {
//
//        private readonly IRoleService _roleService;
//
//        protected virtual ModType ModType => ModType.ModDuty;
//
//        protected ModCommandBase(IRoleService roleService)
//        {
//            _roleService = roleService;
//        }
//
//        [Command("add")]
//        public virtual async Task AddMod(SocketGuildUser socketGuildUser)
//        {
//            await _roleService.AddMod(Context, ModType, socketGuildUser);
//        }
//
//        [Command("remove")]
//        public virtual async Task RemoveMod(SocketGuildUser socketGuildUser)
//        {
//            await _roleService.RemoveMod(Context, ModType, socketGuildUser);
//        }
//
//        [Command]
//        public async Task DisplayMods()
//        {
//            await _roleService.DisplayMods(Context, ModType);
//        }
//
//    }
}
