using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
//using CommandEventArgs = Discord.Commands.CommandEventArgs;
//using ParameterType = Discord.Commands.ParameterType;
//using User = Discord.User;

namespace Mod_Bot
{
    public class ModBot
    {
        private string token = ConfigurationManager.AppSettings.Get("discordToken");

        private string ADMIN_ROLE = "Admin";
        private string STAFF_ROLE = "Staff";
        private string MODERATORS_ROLE = "Moderators";


//        private List<User> _counterMods;
//        private List<User> _tributeMods;
//        private List<User> _chatdutyMods;
//        private List<User> _prMods;
        private List<string> _tributes;
        private bool _redemptionRound;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public ModBot()
        {
//            _counterMods = new List<User>();
//            _tributeMods = new List<User>();
//            _chatdutyMods = new List<User>();
//            _tributes = new List<string>();
//            _prMods = new List<User>();
        }

        public async Task RegisterCommandsAndConnectBot()
        {
            _client = new DiscordSocketClient();



            _commands = new CommandService();

//            client.UsingCommands(x =>
//            {
//                x.PrefixChar = '!';
//                x.AllowMentionPrefix = true;
//            });
//
//            //!counters !tributes !chatduty
//            var commands = client.GetService<CommandService>();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();


            _client.MessageReceived += HandleCommandAsync;

//            await _commands.AddModulesAsync(Assemby.GetEntryAssembly());


            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await Task.Delay(-1);


            //            commands.CreateCommand("tribute").Parameter("tribute1").Parameter("tribute2")
            //                .Do(async e =>
            //                {
            //                    if (_tributes.Count == 2)
            //                    {
            //                        if (_tributeMods.Count > 0)
            //                        {                            
            //                            await e.Channel.SendMessage($"{string.Join("", _tributeMods.Select(u => u.Mention))} Please Remove Tributes!");
            //                        }
            //                        else
            //                        {
            //                            await e.Channel.SendMessage($"Please Remove Tributes!");
            //                        }                        
            //                    }
            //                    else
            //                    {
            //                        foreach (var tribute in e.Args)
            //                        {
            //                            _tributes.Add(tribute);
            //                        }
            //                        _redemptionRound = false;
            //                        var rr = _redemptionRound ? "Redemption" : "1st Round";
            //                        var message = $"Tributes ({rr}):\n {string.Join(" / ", _tributes)}";
            //                        await e.Channel.SendMessage(message);
            //                    }                    
            //                });
            //            commands.CreateCommand("tribute")
            //                .Do(async e =>
            //                {
            //                    var rr = _redemptionRound ? "Redemption" : "1st Round";
            //                    var message = $"Tributes ({rr}):\n {string.Join(" / ", _tributes)}";
            //                    await e.Channel.SendMessage(message);
            //                });
            //            commands.CreateCommand("tribute rr")
            //                .Do(async e =>
            //                {
            //                    _redemptionRound = true;
            //                    await e.Channel.SendMessage($"Redemption Round Granted!");
            //                });
            //            commands.CreateCommand("tribute purge")
            //                .Do(async e =>
            //                {
            //                    try
            //                    {
            //                        _tributes.Clear();
            //                        await e.Channel.SendMessage($"Tributes Cleared");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        //TODO 
            //                    }
            //                });
            //            commands.CreateCommand("counters add").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    AddMod(e, ModType.Counters);
            //                });
            //
            //            commands.CreateCommand("tributes add").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    AddMod(e, ModType.Tributes);
            //                });
            //
            //            commands.CreateCommand("chatduty add").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    AddMod(e, ModType.ChatDuty);
            //                });
            //
            //            commands.CreateCommand("pr add").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    AddMod(e, ModType.PRDuty);
            //                });
            //
            //            commands.CreateCommand("counters remove").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    RemoveMod(e, ModType.Counters);
            //                });
            //
            //            commands.CreateCommand("tributes remove").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    RemoveMod(e, ModType.Tributes);
            //                });
            //
            //            commands.CreateCommand("chatduty remove").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    RemoveMod(e, ModType.ChatDuty);
            //                });
            //
            //            commands.CreateCommand("pr remove").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    RemoveMod(e, ModType.PRDuty);
            //                });
            //
            //            commands.CreateCommand("modduty remove").Parameter("mod", ParameterType.Optional)
            //                .Do((e) =>
            //                {
            //                    RemoveMod(e, ModType.ModDuty);
            //                });
            //
            //            commands.CreateCommand("counters purge")
            //                .Do((e) =>
            //                {
            //                    PurgeMods(e, ModType.Counters);
            //                });
            //
            //            commands.CreateCommand("tributes purge")
            //                .Do((e) =>
            //                {
            //                    PurgeMods(e, ModType.Tributes);
            //                });
            //
            //            commands.CreateCommand("chatduty purge")
            //                .Do((e) =>
            //                {
            //                    PurgeMods(e, ModType.ChatDuty);
            //                });
            //
            //            commands.CreateCommand("pr purge")
            //                .Do((e) =>
            //                {
            //                    PurgeMods(e, ModType.PRDuty);
            //                });
            //
            //            commands.CreateCommand("modduty purge")
            //                .Do((e) =>
            //                {
            //                    PurgeMods(e, ModType.ModDuty);
            //                });
            //
            //            commands.CreateCommand("counters")
            //                .Do((e) =>
            //                {
            //                    DisplayMods(e, ModType.Counters);
            //                });
            //
            //            commands.CreateCommand("tributes")
            //                .Do((e) =>
            //                {
            //                    DisplayMods(e, ModType.Tributes);
            //                });
            //
            //            commands.CreateCommand("chatduty")
            //                .Do((e) =>
            //                {
            //                    DisplayMods(e, ModType.ChatDuty);
            //                });
            //
            //            commands.CreateCommand("pr")
            //                .Do((e) =>
            //                {
            //                    DisplayMods(e, ModType.PRDuty);
            //                });
            //
            //            commands.CreateCommand("modduty")
            //                .Do((e) =>
            //                {
            //                    DisplayMods(e, ModType.ModDuty);
            //                });
            //
            //            client.ExecuteAndWait(async () =>
            //            {
            //                await client.Connect(token, TokenType.Bot);
            //            });

        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new SocketCommandContext(_client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
            {
                  await context.Channel.SendMessageAsync(result.ErrorReason);
//                await context.Channel.SendMessageAsync("Crickey, we have a dino here... This here is called the Ninja Wolf");
            }                
        }

        //        private async void DisplayMods(CommandEventArgs e, ModType modtype)
        //        {
        //            var list = new List<string>();
        //
        //            switch (modtype)
        //            {
        //                case ModType.Counters:
        //                    if (_counterMods.Count > 0)
        //                    {
        //                        list.Add("Counters:");
        //                        list.AddRange(_counterMods.Select(x => x.Name));
        //                    }
        //                    break;
        //                case ModType.Tributes:
        //                    if (_tributeMods.Count > 0)
        //                    {
        //                        list.Add("Tributes:");
        //                        list.AddRange(_tributeMods.Select(x => x.Name));
        //                    }
        //                    break;
        //                case ModType.ChatDuty:
        //                    if (_chatdutyMods.Count > 0)
        //                    {
        //                        list.Add("Chat Duty:");
        //                        list.AddRange(_chatdutyMods.Select(x => x.Name));
        //                    }
        //                    break;
        //                case ModType.PRDuty:
        //                    if (_prMods.Count > 0)
        //                    {
        //                        list.Add("PR Duty:");
        //                        list.AddRange(_prMods.Select(x => x.Name));
        //                    }
        //                    break;
        //                case ModType.ModDuty:
        //                    if (_counterMods.Count > 0)
        //                    {
        //                        list.Add("Counters:");
        //                        list.AddRange(_counterMods.Select(x => x.Name));
        //                        list.Add("\n");
        //                    }
        //                    if (_tributeMods.Count > 0)
        //                    {
        //                        list.Add("Tributes:");
        //                        list.AddRange(_tributeMods.Select(x => x.Name));
        //                        list.Add("\n");
        //                    }
        //                    if (_chatdutyMods.Count > 0)
        //                    {
        //                        list.Add("Chat Duty:");
        //                        list.AddRange(_chatdutyMods.Select(x => x.Name));
        //                        list.Add("\n");
        //                    }
        //                    if (_prMods.Count > 0)
        //                    {
        //                        list.Add("PR Duty:");
        //                        list.AddRange(_prMods.Select(x => x.Name));
        //                        list.Add("\n");
        //                    }
        //                    break;
        //            }
        //
        //            if (list.Count > 0)
        //            {
        //                await e.Channel.SendMessage($"{string.Join("\n", list)}");
        //            }
        //            else
        //            {
        //                await e.Channel.SendMessage($"No Mods have been added for this.");
        //            }
        //        }
        //
        //        private async void AddMod(CommandEventArgs e, ModType modtype)
        //        {
        //            //            var user = e.User;
        //
        //            var roles = e.Channel.Server.Roles;
        //            var allowedRoles = roles.Where(x => x.Name == ADMIN_ROLE || x.Name == STAFF_ROLE || x.Name == MODERATORS_ROLE);
        //            var isRole = false;
        //
        //            foreach (var allowedRole in allowedRoles)
        //            {
        //                isRole = e.User.HasRole(allowedRole);
        //                if (isRole) break;
        //            }
        //
        //            if (isRole)
        //            {
        //
        //                User user = null;
        //
        //                if (e.Args.Length > 0)
        //                {
        //                    user = e.Server.Users.FirstOrDefault(x => x.Name == e.Args[0] || x.Nickname == e.Args[0] || x.Mention == e.Args[0] || x.NicknameMention == e.Args[0]);                    
        //
        //                    if (user == null)
        //                    {                       
        //                        try
        //                        {
        //                            var userString = e.Args[0];
        //                            ulong userId = Convert.ToUInt64(userString.Trim('<', '>', '@'));
        //
        //                            user = e.Server.Users.FirstOrDefault(x => x.Id == userId);
        //
        //                            if (user == null)
        //                            {
        //                                await e.Channel.SendMessage($"User Does Not Exist");
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            await e.Channel.SendMessage($"User Does Not Exist");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    user = e.User;
        //                }                
        //
        //                if (user != null)
        //                    {
        //
        //                        foreach (var allowedRole in allowedRoles)
        //                        {
        //                            isRole = user.HasRole(allowedRole);
        //                            if (isRole) break;
        //                        }
        //
        //                        if (isRole)
        //                        {
        //                            switch (modtype)
        //                            {
        //                                case ModType.Counters:
        //                                    if (!_counterMods.Contains(user))
        //                                    {
        //                                        _counterMods.Add(user);
        //                                    }
        //                                    break;
        //                                case ModType.Tributes:
        //                                    if (!_tributeMods.Contains(user))
        //                                    {
        //                                        _tributeMods.Add(user);
        //                                    }
        //                                    break;
        //                                case ModType.ChatDuty:
        //                                    if (!_chatdutyMods.Contains(user))
        //                                    {
        //                                        _chatdutyMods.Add(user);
        //                                    }
        //                                    break;
        //                                case ModType.PRDuty:
        //                                    if (!_prMods.Contains(user))
        //                                    {
        //                                        _prMods.Add(user);
        //                                    }
        //                                    break;
        //                        }
        //
        //                            await e.Channel.SendMessage($"Added {user.Name}");
        //                        }
        //                        else
        //                        {
        //                            await e.Channel.SendMessage($"{user.Name} is not a Mod");
        //                        }
        //
        //                    }
        //            }
        //            else
        //            {
        //                await e.Channel.SendMessage($"You are not Authorized to use this Command - {e.User.Name}");
        //            }
        //
        //        }
        //
        //        private async void RemoveMod(CommandEventArgs e, ModType modtype)
        //        {
        //            var roles = e.Channel.Server.Roles;
        //            var allowedRoles = roles.Where(x => x.Name == ADMIN_ROLE || x.Name == STAFF_ROLE || x.Name == MODERATORS_ROLE);
        //            var isRole = false;
        //
        //            foreach (var allowedRole in allowedRoles)
        //            {
        //                isRole = e.User.HasRole(allowedRole);
        //                if (isRole) break;
        //            }
        //
        //            if (isRole)
        //            {
        //
        //                User user = null;
        //
        //                if (e.Args.Length > 0)
        //                {
        //                    user = e.Server.Users.FirstOrDefault(x => x.Name == e.Args[0] || x.Nickname == e.Args[0] || x.Mention == e.Args[0] || x.NicknameMention == e.Args[0]);
        //
        //                    if (user == null)
        //                    {                        
        //                        try
        //                        {
        //                            var userString = e.Args[0];
        //                            ulong userId = Convert.ToUInt64(userString.Trim('<', '>', '@'));
        //
        //                            user = e.Server.Users.FirstOrDefault(x => x.Id == userId);
        //
        //                            if (user == null)
        //                            {
        //                                await e.Channel.SendMessage($"User Does Not Exist");
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            await e.Channel.SendMessage($"User Does Not Exist");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    user = e.User;
        //                }
        //
        //                if (user != null)
        //                {
        //
        //                    switch (modtype)
        //                    {
        //                        case ModType.Counters:
        //                            if (_counterMods.Contains(user))
        //                            {
        //                                _counterMods.Remove(user);
        //                            }
        //                            break;
        //                        case ModType.Tributes:
        //                            if (_tributeMods.Contains(user))
        //                            {
        //                                _tributeMods.Remove(user);
        //                            }
        //                            break;
        //                        case ModType.ChatDuty:
        //                            if (_chatdutyMods.Contains(user))
        //                            {
        //                                _chatdutyMods.Remove(user);
        //                            }
        //                            break;
        //                        case ModType.PRDuty:
        //                            if (_prMods.Contains(user))
        //                            {
        //                                _prMods.Remove(user);
        //                            }
        //                            break;
        //                        case ModType.ModDuty:
        //                            if (_counterMods.Contains(user))
        //                            {
        //                                _counterMods.Remove(user);
        //                            }
        //                            if (_tributeMods.Contains(user))
        //                            {
        //                                _tributeMods.Remove(user);
        //                            }
        //                            if (_chatdutyMods.Contains(user))
        //                            {
        //                                _chatdutyMods.Remove(user);
        //                            }
        //                            if (_prMods.Contains(user))
        //                            {
        //                                _prMods.Remove(user);
        //                            }
        //                            break;
        //                    }
        //                    await e.Channel.SendMessage($"Removed {user.Name}");
        //                }
        //                else
        //                {
        //                    await e.Channel.SendMessage($"User Does Not Exist");
        //                }                
        //            }
        //            else
        //            {
        //                await e.Channel.SendMessage($"You are not Authorized to use this Command - {e.User.Name}");
        //            }
        //        }
        //
        //        private async void PurgeMods(CommandEventArgs e, ModType modtype)
        //        {
        //            var roles = e.Channel.Server.Roles;
        //            var allowedRoles = roles.Where(x => x.Name == ADMIN_ROLE || x.Name == STAFF_ROLE || x.Name == MODERATORS_ROLE);
        //            var isRole = false;
        //
        //            foreach (var allowedRole in allowedRoles)
        //            {
        //                isRole = e.User.HasRole(allowedRole);
        //                if (isRole) break;
        //            }
        //
        //            if (isRole)
        //            {
        //                switch (modtype)
        //                {
        //                    case ModType.Counters:
        //                        _counterMods.Clear();
        //                        break;
        //                    case ModType.Tributes:
        //                        _tributeMods.Clear();
        //                        break;
        //                    case ModType.ChatDuty:
        //                        _chatdutyMods.Clear();
        //                        break;
        //                    case ModType.PRDuty:
        //                        _prMods.Clear();
        //                        break;
        //                    case ModType.ModDuty:
        //                        _counterMods.Clear();
        //                        _tributeMods.Clear();
        //                        _chatdutyMods.Clear();
        //                        _prMods.Clear();
        //                        break;
        //                }
        //                await e.Channel.SendMessage($"https://www.youtube.com/watch?v=welqyAKtmeE \n ");
        //                await Task.Delay(10000);
        //                await e.Channel.SendMessage("The Purge is Complete <:ksalute:311537363429883904>");
        //            }
        //            else
        //            {
        //                await e.Channel.SendMessage($"You are not Authorized to use this Command - {e.User.Name}");
        //            }
        //        }
        //
        //        private void Log(object sender, LogMessageEventArgs e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //
        //        enum ModType
        //        {
        //            Counters,
        //            Tributes,
        //            ChatDuty,
        //            PRDuty,
        //            ModDuty
        //        }
        //
    }
}
