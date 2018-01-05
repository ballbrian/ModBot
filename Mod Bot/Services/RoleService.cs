using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Mod_Bot.Services
{
    public class RoleService : IRoleService
    {

        private string ADMIN_ROLE = "Admin";
        private string STAFF_ROLE = "Staff";
        private string MODERATORS_ROLE = "Moderators";

        private List<SocketGuildUser> _counterMods;
        private List<SocketGuildUser> _tributeMods;
        private List<SocketGuildUser> _chatdutyMods;
        private List<SocketGuildUser> _prMods;

        public RoleService()
        {
            _counterMods = new List<SocketGuildUser>();
            _tributeMods = new List<SocketGuildUser>();
            _chatdutyMods = new List<SocketGuildUser>();
            _prMods = new List<SocketGuildUser>();
        }

        public async Task AddMod(ICommandContext context, ModType modType, SocketGuildUser socketUser)
        {

            var user = socketUser;

            switch (modType)
            {
                case ModType.Counters:
                    if (!_counterMods.Contains(user))
                    {
                        _counterMods.Add(user);
                    }
                    break;
                case ModType.Tributes:
                    if (!_tributeMods.Contains(user))
                    {
                        _tributeMods.Add(user);
                    }
                    break;
                case ModType.ChatDuty:
                    if (!_chatdutyMods.Contains(user))
                    {
                        _chatdutyMods.Add(user);
                    }
                    break;
                case ModType.PRDuty:
                    if (!_prMods.Contains(user))
                    {
                        _prMods.Add(user);
                    }
                    break;

            }

            await context.Channel.SendMessageAsync($"Added {user.Username}");
        }

        public async Task RemoveMod(ICommandContext context, ModType modType, SocketGuildUser socketUser)
        {
            switch (modType)
            {
                case ModType.Counters:
                    if (_counterMods.Contains(socketUser))
                    {
                        _counterMods.Remove(socketUser);
                    }
                    break;
                case ModType.Tributes:
                    if (_tributeMods.Contains(socketUser))
                    {
                        _tributeMods.Remove(socketUser);
                    }
                    break;
                case ModType.ChatDuty:
                    if (_chatdutyMods.Contains(socketUser))
                    {
                        _chatdutyMods.Remove(socketUser);
                    }
                    break;
                case ModType.PRDuty:
                    if (_prMods.Contains(socketUser))
                    {
                        _prMods.Remove(socketUser);
                    }
                    break;
                case ModType.ModDuty:
                    if (_counterMods.Contains(socketUser))
                    {
                        _counterMods.Remove(socketUser);
                    }
                    if (_tributeMods.Contains(socketUser))
                    {
                        _tributeMods.Remove(socketUser);
                    }
                    if (_chatdutyMods.Contains(socketUser))
                    {
                        _chatdutyMods.Remove(socketUser);
                    }
                    if (_prMods.Contains(socketUser))
                    {
                        _prMods.Remove(socketUser);
                    }
                    break;
            }
            await context.Channel.SendMessageAsync($"Removed {socketUser.Username}");

        }

        public async Task DisplayMods(ICommandContext context, ModType modType = ModType.ModDuty)
        {
            var list = new List<string>();

            switch (modType)
            {
                case ModType.Counters:
                    if (_counterMods.Count > 0)
                    {
                        list.Add("Counters:");
                        list.AddRange(_counterMods.Select(x => x.Username));
                    }
                    break;
                case ModType.Tributes:
                    if (_tributeMods.Count > 0)
                    {
                        list.Add("Tributes:");
                        list.AddRange(_tributeMods.Select(x => x.Username));
                    }
                    break;
                case ModType.ChatDuty:
                    if (_chatdutyMods.Count > 0)
                    {
                        list.Add("Chat Duty:");
                        list.AddRange(_chatdutyMods.Select(x => x.Username));
                    }
                    break;
                case ModType.PRDuty:
                    if (_prMods.Count > 0)
                    {
                        list.Add("PR Duty:");
                        list.AddRange(_prMods.Select(x => x.Username));
                    }
                    break;
                case ModType.ModDuty:
                    if (_counterMods.Count > 0)
                    {
                        list.Add("Counters:");
                        list.AddRange(_counterMods.Select(x => x.Username));
                        list.Add("\n");
                    }
                    if (_tributeMods.Count > 0)
                    {
                        list.Add("Tributes:");
                        list.AddRange(_tributeMods.Select(x => x.Username));
                        list.Add("\n");
                    }
                    if (_chatdutyMods.Count > 0)
                    {
                        list.Add("Chat Duty:");
                        list.AddRange(_chatdutyMods.Select(x => x.Username));
                        list.Add("\n");
                    }
                    if (_prMods.Count > 0)
                    {
                        list.Add("PR Duty:");
                        list.AddRange(_prMods.Select(x => x.Username));
                        list.Add("\n");
                    }
                    break;
            }

            if (list.Count > 0)
            {
                await context.Channel.SendMessageAsync($"{string.Join("\n", list)}");
            }
            else
            {
                await context.Channel.SendMessageAsync($"No Mods have been added for this.");
            }
        }

        public async Task PurgeMods(ICommandContext context, ModType modType)
        {
            switch (modType)
            {
                case ModType.Counters:
                    _counterMods.Clear();
                    break;
                case ModType.Tributes:
                    _tributeMods.Clear();
                    break;
                case ModType.ChatDuty:
                    _chatdutyMods.Clear();
                    break;
                case ModType.PRDuty:
                    _prMods.Clear();
                    break;
                case ModType.ModDuty:
                    _counterMods.Clear();
                    _tributeMods.Clear();
                    _chatdutyMods.Clear();
                    _prMods.Clear();
                    break;
            }
            await context.Channel.SendMessageAsync($"https://www.youtube.com/watch?v=welqyAKtmeE \n ");
            await Task.Delay(10000);
            await context.Channel.SendMessageAsync("The Purge is Complete <:ksalute:311537363429883904>");
        }
    }
}
