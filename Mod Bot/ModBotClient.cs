using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Mod_Bot.Services;

namespace Mod_Bot
{
    public class ModBotClient : DiscordSocketClient, IClient
    {
        private readonly ICommandService _commandService;

        public ModBotClient(ICommandService commandService)
        {
            _commandService = commandService;

            Log += LogHandler;
            MessageReceived += HandleCommandAsync;

        }

        public async Task Initialize()
        {
            var token = ConfigurationManager.AppSettings.Get("discordToken");

            await LoginAsync(TokenType.Bot, token);
            await StartAsync();
        }

        private Task LogHandler(LogMessage msg)
        {
            return Task.Run(() =>
            {
                Console.WriteLine(msg.ToString());
            });
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(CurrentUser, ref argPos))) return;
            
            var context = new SocketCommandContext(this, message);

            var result = await _commandService.ExecuteAsync(context, argPos);
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

    }
}
