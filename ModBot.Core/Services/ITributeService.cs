using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Core.Services
{
    public interface ITributeService
    {

        bool SetTributes(string tribute1, string tribute2);

        bool PurgeTributes();

        List<string> GetTributes();

        bool GrantRedemption();

        bool GrantKabbyKommandment();

        bool RedemptionRound { get; }

        bool KabbyKommandment { get; }

    }
}
