using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.WebSocket;
using Mod_Bot.Services;

namespace ModBot.Core.Services
{
    public class ModCommandReplyService : ICommandReplyService
    {
        private readonly IRoleService _roleService;

        public ModCommandReplyService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public string AddModResponse(ModType modType, IGuildUser socketUser)
        {
            return _roleService.AddMod(modType, socketUser) ? $"Added {socketUser.Username}" : $"Something went wrong - {socketUser.Username} Not Added";
        }

        public string RemoveModResponse(ModType modType, IGuildUser socketUser)
        {
            return _roleService.RemoveMod(modType, socketUser) ? $"Removed {socketUser.Username}" : $"Something went wrong - {socketUser.Username} was not removed";
        }

        public string DisplayModsResponse(ModType modType)
        {
            var modDisplay = new StringBuilder();

            try
            {
                if (modType == ModType.ChatDuty || modType == ModType.ModDuty)
                {
                    var chatMods = _roleService.GetMods(ModType.ChatDuty);
                    if (chatMods.Count > 0)
                    {
                        modDisplay.AppendLine($"Chat Duty:\n{string.Join("\n", chatMods.Select(x => x.Username))}");
                    }
                }

                if (modType == ModType.Tributes || modType == ModType.ModDuty)
                {
                    var tributeMods = _roleService.GetMods(ModType.Tributes);
                    if (tributeMods.Count > 0)
                    {
                        modDisplay.AppendLine(
                            $"Tributes Duty:\n{string.Join("\n", tributeMods.Select(x => x.Username))}");
                    }
                }

                if (modType == ModType.Counters || modType == ModType.ModDuty)
                {
                    var countersDuty = _roleService.GetMods(ModType.Counters);
                    if (countersDuty.Count > 0)
                    {
                        modDisplay.AppendLine(
                            $"Counters Duty:\n{string.Join("\n", countersDuty.Select(x => x.Username))}");
                    }
                }

                return modDisplay.Length > 0 ? modDisplay.ToString() : $"No Mods have been added for this.";
            }
            catch (Exception ex)
            {
                return $"Something went wrong - Can't display Mods";
            }
        }

        public string PurgeModsResponse(ModType modType)
        {
            return _roleService.PurgeMods(modType) ? "The Purge is Complete <:ksalute:311537363429883904>" : $"Something went wrong - Purging is not legal";
        }
    }
}
