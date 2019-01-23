using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Discord;

namespace ModBot.Core.Extensions
{
    public static class IGuildUserExtensions
    {

        public static string UserNickname(this IGuildUser guildUser)
        {
            return string.IsNullOrEmpty(guildUser.Nickname) ? guildUser.Username : guildUser.Nickname;
        }

    }
}
