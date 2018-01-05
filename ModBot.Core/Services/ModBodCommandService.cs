using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Mod_Bot.Services
{
    public class ModBodCommandService : CommandService, ICommandService
    {
        public async Task RegisterModulesAsync()
        {
            await AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task<IResult> ExecuteAsync(ICommandContext context, int argPos)
        {
            return await ExecuteAsync(context, argPos, Program.Services);
        }
    }
}
