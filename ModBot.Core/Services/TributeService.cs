using System;
using System.Collections.Generic;
using System.Text;
using Mod_Bot.Services;

namespace ModBot.Core.Services
{
    public class TributeService : ITributeService
    {
        private List<string> _tributes;

        private readonly IRoleService _roleService;

        public TributeService(IRoleService roleService)
        {
            _roleService = roleService;      
            
            _tributes = new List<string>();
        }

        public bool SetTributes(params string[] tributes)
        {
            if(_tributes.Count == 0)
            {
                foreach (var tribute in tributes)
                {
                    _tributes.Add(tribute);
                }                
                RedemptionRound = KabbyKommandment = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PurgeTributes()
        {
            try
            {
                _tributes.Clear();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> GetTributes()
        {
            return _tributes;
        }

        public bool GrantRedemption()
        {
            if (KabbyKommandment)
            {
                return false;
            }
            return RedemptionRound = true;
        }

        public bool GrantKabbyKommandment()
        {
            if (RedemptionRound)
            {
                return false;
            }
            return KabbyKommandment = true;
        }

        public bool RedemptionRound { get; private set; }

        public bool KabbyKommandment { get; private set; }
    }
}
