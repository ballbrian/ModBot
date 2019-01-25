using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Mod_Bot.Services
{
    public class RoleService : IRoleService
    {

        private string ADMIN_ROLE = "Admin";
        private string STAFF_ROLE = "Staff";
        private string MODERATORS_ROLE = "Moderators";

        private List<IGuildUser> _counterMods;
        private List<IGuildUser> _tributeMods;
        private List<IGuildUser> _chatdutyMods;
        private List<IGuildUser> _prMods;
        private List<IGuildUser> _betsMods;

        public RoleService()
        {
            _counterMods = new List<IGuildUser>();
            _tributeMods = new List<IGuildUser>();
            _chatdutyMods = new List<IGuildUser>();
            _prMods = new List<IGuildUser>();
            _betsMods = new List<IGuildUser>();
        }

        public bool AddMod(ModType modType, IGuildUser socketUser)
        {
            try
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
                    case ModType.Bets:
                        if (!_betsMods.Contains(user))
                        {
                            _betsMods.Add(user);
                        }
                        break;
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveMod(ModType modType, IGuildUser socketUser)
        {             
            switch (modType)
            {
                case ModType.Counters:
                    return _counterMods.Remove(socketUser);
                case ModType.Tributes:
                    return _tributeMods.Remove(socketUser);
                case ModType.ChatDuty:
                    return _chatdutyMods.Remove(socketUser);
                case ModType.PRDuty:
                    return _prMods.Remove(socketUser);
                case ModType.Bets:
                    return _betsMods.Remove(socketUser);
                case ModType.ModDuty:
                    _counterMods.Remove(socketUser);
                    _tributeMods.Remove(socketUser);
                    _chatdutyMods.Remove(socketUser);
                    _prMods.Remove(socketUser);
                    _betsMods.Remove(socketUser);
                    return true;
            }
            return false;

        }

        public List<IGuildUser> GetMods(ModType modType)
        {
            var list = new List<IGuildUser>();

            switch (modType)
            {
                case ModType.Counters:
                    if (_counterMods.Count > 0)
                    {
                        list.AddRange(_counterMods);
                    }
                    break;
                case ModType.Tributes:
                    if (_tributeMods.Count > 0)
                    {
                        list.AddRange(_tributeMods);
                    }
                    break;
                case ModType.ChatDuty:
                    if (_chatdutyMods.Count > 0)
                    {
                        list.AddRange(_chatdutyMods);
                    }
                    break;
                case ModType.PRDuty:
                    if (_prMods.Count > 0)
                    {
                        list.AddRange(_prMods);
                    }
                    break;
                case ModType.Bets:
                    if (_betsMods.Count > 0)
                    {
                        list.AddRange(_betsMods);
                    }
                    break;
                case ModType.ModDuty:
                    if (_counterMods.Count > 0)
                    {
                        list.AddRange(_counterMods);
                    }
                    if (_tributeMods.Count > 0)
                    {
                        list.AddRange(_tributeMods);
                    }
                    if (_chatdutyMods.Count > 0)
                    {
                        list.AddRange(_chatdutyMods);
                    }
                    if (_prMods.Count > 0)
                    {
                        list.AddRange(_prMods);
                    }
                    if (_betsMods.Count > 0)
                    {
                        list.AddRange(_betsMods);
                    }
                    break;
            }

            return list;

        }

        public bool PurgeMods(ModType modType)
        {
            try
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
                    case ModType.Bets:
                        _betsMods.Clear();
                        break;
                    case ModType.ModDuty:
                        _counterMods.Clear();
                        _tributeMods.Clear();
                        _chatdutyMods.Clear();
                        _prMods.Clear();
                        _betsMods.Clear();
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
