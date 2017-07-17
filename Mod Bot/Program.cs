using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Mod_Bot
{
    class Program
    {
        static void Main(string[] args)
        {                       
            var bot = new ModBot();          
            bot.RegisterCommandsAndConnectBot();
        }
    }
}
