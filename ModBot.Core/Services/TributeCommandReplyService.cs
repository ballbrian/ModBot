using System.Linq;
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

        public string SetTributesResponse(params string[] tributes)
        {
            if (_tributeService.SetTributes(tributes))
            {
                return GetTributesResponse();
            }
            var tributeMods = _roleService.GetMods(ModType.Tributes);
            return tributeMods.Count > 0 ? $"{string.Join("", tributeMods.Select(u => u.Mention))} Please Remove Tributes!" : "Please Remove Tributes!";
        }

        public string SetTributesResponse(string tributes)
        {
            if (string.CompareOrdinal(tributes, "purge") == 0)
            {
                return PurgeTributesResponse();
            }
            var tributesSplit = tributes.Split('/');

            if (_tributeService.SetTributes(tributesSplit))
            {
                return GetTributesResponse();
            }
            var tributeMods = _roleService.GetMods(ModType.Tributes);
            return tributeMods.Count > 0 ? $"{string.Join("", tributeMods.Select(u => u.Mention))} Please Remove Tributes!" : "Please Remove Tributes!";

        }

        public string GetTributesResponse()
        {
            var tributes = _tributeService.GetTributes();

            if (tributes.Count == 0)
            {
                return "Please Add Tributes";
            }

            var tributeStatus = "1st Round";

            if (_tributeService.RedemptionRound)
            {
                tributeStatus = "Redemption";
            }
            else
            {
                if (_tributeService.KabbyKommandment)
                {
                    tributeStatus = "Kabby Kommandment";
                }
            }

            return $"Tributes ({tributeStatus}):\n {string.Join(" / ", tributes)}";
        }

        public string PurgeTributesResponse()
        {
            return _tributeService.PurgeTributes() ? "Tributes Cleared" : "Something went wrong #blameNinjaWolf";
        }

    }

    public interface ITributeCommandReplyService
    {

        string SetTributesResponse(string tributes);

        string GetTributesResponse();

        string PurgeTributesResponse();
 
    }
}
