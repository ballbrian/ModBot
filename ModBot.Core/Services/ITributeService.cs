using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Core.Services
{
    public interface ITributeService
    {

        bool SetTributes(params string[] tributes);

        bool PurgeTributes();

        List<string> GetTributes();

        bool GrantRedemption();

        bool GrantKabbyKommandment();

        bool RedemptionRound { get; }

        bool KabbyKommandment { get; }

    }
}
