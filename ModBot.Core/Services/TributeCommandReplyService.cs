using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mod_Bot.Services;

namespace ModBot.Core.Services
{
    public class TributeCommandReplyService : ITributeCommandReplyService
    {
        private readonly ITributeService _tributeService;
        private readonly IRoleService _roleService;

        public TributeCommandReplyService(ITributeService tributeService, IRoleService roleService)
        {
            _tributeService = tributeService;
            _roleService = roleService;
        }

        public string SetTributesResponse(string tribute1, string tribute2)
        {
            if (_tributeService.SetTributes(tribute1, tribute2))
            {
                return GetTributesResponse();
            }
            else
            {
                var tributeMods = _roleService.GetMods(ModType.Tributes);
                return tributeMods.Count > 0 ? $"{string.Join("", tributeMods.Select(u => u.Mention))} Please Remove Tributes!" : "Please Remove Tributes!";
            }
        }

        public string SetTributesResponse(string tributes)
        {
            var tributesSplit = tributes.Split('/');
            return tributesSplit.Length == 2 ? SetTributesResponse(tributesSplit[0], tributesSplit[1]) : $"Tributes need to be seperated with a \"/\" example: \"Tribute1/Tribute2\"";
        }

        public string GetTributesResponse()
        {
            var tributes = _tributeService.GetTributes();
            var rr = _tributeService.RedemptionRound ? "Redemption" : "1st Round";
            return $"Tributes ({rr}):\n {string.Join(" / ", tributes)}";
        }

        public string PurgeTributesResponse()
        {
            return _tributeService.PurgeTributes() ? "Tributes Cleared" : "Something went wrong #blameNinjaWolf";
        }

        public string GrantRedemptionRoundResponse()
        {
            _tributeService.GrantRedemption();
            return "Redemption Round Granted";
        }

    }

    public interface ITributeCommandReplyService
    {

        string SetTributesResponse(string tribute1, string tribute2);

        string SetTributesResponse(string tributes);

        string GetTributesResponse();

        string PurgeTributesResponse();

        string GrantRedemptionRoundResponse();        
    }
}
