using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.API.Client;
using Discord.API.Client.Rest;
using Discord.Commands;
using User = Discord.User;

namespace Mod_Bot
{
    public class ModBot
    {
        private string token = ConfigurationManager.AppSettings.Get("discordToken");

        private string ADMIN_ROLE = "Admin";
        private string STAFF_ROLE = "Staff";
        private string MODERATORS_ROLE = "Moderators";


        private List<User> _counterMods;
        private List<User> _tributeMods;
        private List<User> _chatdutyMods;

        public ModBot()
        {
            _counterMods = new List<User>();
            _tributeMods = new List<User>();
            _chatdutyMods = new List<User>();
        }

        public void RegisterCommandsAndConnectBot()
        {
            var client = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            //!counters !tributes !chatduty
            var commands = client.GetService<CommandService>();

            commands.CreateCommand("counters add").Parameter("mod", ParameterType.Optional)
                .Do((e) =>
                {
                    AddMod(e, ModType.Counters);
                });

            commands.CreateCommand("tributes add").Parameter("mod", ParameterType.Optional)
                .Do((e) =>
                {
                    AddMod(e, ModType.Tributes);
                });

            commands.CreateCommand("chatduty add").Parameter("mod", ParameterType.Optional)
                .Do((e) =>
                {
                    AddMod(e, ModType.ChatDuty);
                });

            commands.CreateCommand("counters remove").Parameter("mod", ParameterType.Optional)
                .Do((e) =>
                {
                    RemoveMod(e, ModType.Counters);
                });

            commands.CreateCommand("tributes remove").Parameter("mod", ParameterType.Optional)
                .Do((e) =>
                {
                    RemoveMod(e, ModType.Tributes);
                });

            commands.CreateCommand("chatduty remove").Parameter("mod", ParameterType.Optional)
                .Do((e) =>
                {
                    RemoveMod(e, ModType.ChatDuty);
                });

            commands.CreateCommand("counters purge")
                .Do((e) =>
                {
                    PurgeMods(e, ModType.Counters);
                });

            commands.CreateCommand("tributes purge")
                .Do((e) =>
                {
                    PurgeMods(e, ModType.Tributes);
                });

            commands.CreateCommand("chatduty purge")
                .Do((e) =>
                {
                    PurgeMods(e, ModType.ChatDuty);
                });

            commands.CreateCommand("modduty purge")
                .Do((e) =>
                {
                    PurgeMods(e, ModType.ModDuty);
                });

            commands.CreateCommand("counters")
                .Do((e) =>
                {
                    DisplayMods(e, ModType.Counters);
                });

            commands.CreateCommand("tributes")
                .Do((e) =>
                {
                    DisplayMods(e, ModType.Tributes);
                });

            commands.CreateCommand("chatduty")
                .Do((e) =>
                {
                    DisplayMods(e, ModType.ChatDuty);
                });
            commands.CreateCommand("modduty")
                .Do((e) =>
                {
                    DisplayMods(e, ModType.ModDuty);
                });

            client.ExecuteAndWait(async () =>
            {
                await client.Connect(token, TokenType.Bot);
            });

        }

        private async void DisplayMods(CommandEventArgs e, ModType modtype)
        {
            var list = new List<string>();

            switch (modtype)
            {
                case ModType.Counters:
                    if (_counterMods.Count > 0)
                    {
                        list.Add("Counters:");
                        list.AddRange(_counterMods.Select(x => x.Name));
                    }
                    break;
                case ModType.Tributes:
                    if (_tributeMods.Count > 0)
                    {
                        list.Add("Tributes:");
                        list.AddRange(_tributeMods.Select(x => x.Name));
                    }
                    break;
                case ModType.ChatDuty:
                    if (_chatdutyMods.Count > 0)
                    {
                        list.Add("Chat Duty:");
                        list.AddRange(_chatdutyMods.Select(x => x.Name));
                    }
                    break;
                case ModType.ModDuty:
                    if (_counterMods.Count > 0)
                    {
                        list.Add("Counters:");
                        list.AddRange(_counterMods.Select(x => x.Name));
                        list.Add("\n");
                    }
                    if (_tributeMods.Count > 0)
                    {
                        list.Add("Tributes:");
                        list.AddRange(_tributeMods.Select(x => x.Name));
                        list.Add("\n");
                    }
                    if (_chatdutyMods.Count > 0)
                    {
                        list.Add("Chat Duty:");
                        list.AddRange(_chatdutyMods.Select(x => x.Name));
                        list.Add("\n");
                    }
                    break;
            }

            if (list.Count > 0)
            {
                await e.Channel.SendMessage($"{string.Join("\n", list)}");
            }
            else
            {
                await e.Channel.SendMessage($"No Mods have been added for this.");
            }
        }

        private async void AddMod(CommandEventArgs e, ModType modtype)
        {
            //            var user = e.User;

            var roles = e.Channel.Server.Roles;
            var allowedRoles = roles.Where(x => x.Name == ADMIN_ROLE || x.Name == STAFF_ROLE || x.Name == MODERATORS_ROLE);
            var isRole = false;

            foreach (var allowedRole in allowedRoles)
            {
                isRole = e.User.HasRole(allowedRole);
                if (isRole) break;
            }

            if (isRole)
            {

                User user = null;

                if (e.Args.Length > 0)
                {
                    user = e.Server.Users.FirstOrDefault(x => x.Name == e.Args[0] || x.Nickname == e.Args[0]);                    

                    if (user == null)
                    {                       
                        try
                        {
                            var userString = e.Args[0];
                            ulong userId = Convert.ToUInt64(userString.Trim('<', '>', '@'));

                            user = e.Server.Users.FirstOrDefault(x => x.Id == userId);

                            if (user == null)
                            {
                                await e.Channel.SendMessage($"User Does Not Exist");
                            }
                        }
                        catch (Exception ex)
                        {
                            await e.Channel.SendMessage($"User Does Not Exist");
                        }
                    }
                }
                else
                {
                    user = e.User;
                }                

                if (user != null)
                    {

                        foreach (var allowedRole in allowedRoles)
                        {
                            isRole = user.HasRole(allowedRole);
                            if (isRole) break;
                        }

                        if (isRole)
                        {
                            switch (modtype)
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
                            }

                            await e.Channel.SendMessage($"Added {user.Name}");
                        }
                        else
                        {
                            await e.Channel.SendMessage($"{user.Name} is not a Mod");
                        }

                    }
            }
            else
            {
                await e.Channel.SendMessage($"You are not Authorized to use this Command - {e.User.Name}");
            }

        }

        private async void RemoveMod(CommandEventArgs e, ModType modtype)
        {
            var roles = e.Channel.Server.Roles;
            var allowedRoles = roles.Where(x => x.Name == ADMIN_ROLE || x.Name == STAFF_ROLE || x.Name == MODERATORS_ROLE);
            var isRole = false;

            foreach (var allowedRole in allowedRoles)
            {
                isRole = e.User.HasRole(allowedRole);
                if (isRole) break;
            }

            if (isRole)
            {

                User user = null;

                if (e.Args.Length > 0)
                {
                    user = e.Server.Users.FirstOrDefault(x => x.Name == e.Args[0] || x.Nickname == e.Args[0]);

                    if (user == null)
                    {                        
                        try
                        {
                            var userString = e.Args[0];
                            ulong userId = Convert.ToUInt64(userString.Trim('<', '>', '@'));

                            user = e.Server.Users.FirstOrDefault(x => x.Id == userId);

                            if (user == null)
                            {
                                await e.Channel.SendMessage($"User Does Not Exist");
                            }
                        }
                        catch (Exception ex)
                        {
                            await e.Channel.SendMessage($"User Does Not Exist");
                        }
                    }
                }
                else
                {
                    user = e.User;
                }

                if (user != null)
                {

                    switch (modtype)
                    {
                        case ModType.Counters:
                            if (_counterMods.Contains(user))
                            {
                                _counterMods.Remove(user);
                            }
                            break;
                        case ModType.Tributes:
                            if (_tributeMods.Contains(user))
                            {
                                _tributeMods.Remove(user);
                            }
                            break;
                        case ModType.ChatDuty:
                            if (_chatdutyMods.Contains(user))
                            {
                                _chatdutyMods.Remove(user);
                            }
                            break;
                    }
                    await e.Channel.SendMessage($"Removed {user.Name}");
                }
                else
                {
                    await e.Channel.SendMessage($"User Does Not Exist");
                }                
            }
            else
            {
                await e.Channel.SendMessage($"You are not Authorized to use this Command - {e.User.Name}");
            }
        }

        private async void PurgeMods(CommandEventArgs e, ModType modtype)
        {
            var roles = e.Channel.Server.Roles;
            var allowedRoles = roles.Where(x => x.Name == ADMIN_ROLE || x.Name == STAFF_ROLE || x.Name == MODERATORS_ROLE);
            var isRole = false;

            foreach (var allowedRole in allowedRoles)
            {
                isRole = e.User.HasRole(allowedRole);
                if (isRole) break;
            }

            if (isRole)
            {
                switch (modtype)
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
                    case ModType.ModDuty:
                        _counterMods.Clear();
                        _tributeMods.Clear();
                        _chatdutyMods.Clear();
                        break;
                }
                await e.Channel.SendMessage($"https://www.youtube.com/watch?v=welqyAKtmeE \n ");
                await Task.Delay(2000);
                await e.Channel.SendMessage("The Purge is Complete :)");
            }
            else
            {
                await e.Channel.SendMessage($"You are not Authorized to use this Command - {e.User.Name}");
            }
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        enum ModType
        {
            Counters,
            Tributes,
            ChatDuty,
            ModDuty
        }

    }
}
