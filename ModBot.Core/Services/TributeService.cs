﻿using System;
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

        public bool SetTributes(string tribute1, string tribute2)
        {
            if(_tributes.Count == 0)
            {
                _tributes.Add(tribute1);
                _tributes.Add(tribute2);
                RedemptionRound = false;
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
            return RedemptionRound = true;
        }

        public bool RedemptionRound { get; private set; }
    }
}
