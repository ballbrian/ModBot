using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Mod_Bot.Services
{
    public interface ICommandService
    {

        Task RegisterModulesAsync();

        Task<IResult> ExecuteAsync(ICommandContext context, int argPos);
    }
}
