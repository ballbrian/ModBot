using System;
using System.Collections.Generic;
using System.Text;
using Mod_Bot.Services;

namespace ModBot.Core.Services
{
    public class RedemptionCommandReplyService : IRedemptionCommandReplyService
    {
        private ITributeService _tributeService;

        public RedemptionCommandReplyService(ITributeService tributeService)
        {
            _tributeService = tributeService;
        }

        public string GrantRedemptionRoundResponse()
        {
            return _tributeService.GrantRedemption() 
                ? "Redemption Round Granted" 
                : "Cannot Grant Redemption Round if Kabby's Kommandment has been granted";
        }

        public string GrantKabbysKommandmentResponse()
        {
            return _tributeService.GrantKabbyKommandment() 
                ? "Kabby's Kommandment Granted Redemption" 
                : "Cannot Grant Kabby's Kommandment Round if Redemption has been granted";
        }

    }

    public interface IRedemptionCommandReplyService
    {
        string GrantRedemptionRoundResponse();

        string GrantKabbysKommandmentResponse();
    }
}
