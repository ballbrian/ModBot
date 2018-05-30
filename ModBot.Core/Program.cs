using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using ModBot.Core.Services;
using Mod_Bot.Commands;
using Mod_Bot.Services;

namespace Mod_Bot
{
    class Program
    {
        public static IServiceProvider Services;

        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {          

            //Register Dependecies
            ConfigureServiceProvider();

            await Services.GetService<IClient>().Initialize();
            await Services.GetService<ICommandService>().RegisterModulesAsync();
//
            await Task.Delay(-1);

        }

        private void ConfigureServiceProvider()
        {
            Services = new ServiceCollection()
                .AddSingleton<IClient, ModBotClient>()
                .AddSingleton<IRoleService, RoleService>()
                .AddSingleton<ITributeService, TributeService>()
                .AddSingleton<ICommandService, ModBodCommandService>()
                .AddSingleton<ICommandReplyService, ModCommandReplyService>()
                .AddSingleton<ITributeCommandReplyService, TributeCommandReplyService>()
                .AddSingleton<IRedemptionCommandReplyService, RedemptionCommandReplyService>()
                .BuildServiceProvider();
        }
    }
}
