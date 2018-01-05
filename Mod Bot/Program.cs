﻿using System;
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
using Mod_Bot.Commands;
using Mod_Bot.Services;

namespace Mod_Bot
{
    class Program
    {
        private ModBotClient _client;
        public static IServiceProvider Services;

        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {          

//            var client = new ModBotClient();
//            await client.Initialize();

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
                .AddSingleton<ICommandService, ModBodCommandService>()
                .AddSingleton<IModCommand, ChatDutyCommand>()
                .BuildServiceProvider();
        }
    }
}
