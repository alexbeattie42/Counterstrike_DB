using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp6.DemoClasses;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalStats globalStats = Util.LoadJSON<GlobalStats>("global.json"); // some global stats
            List<Match> matches = Util.LoadJSON<List<Match>>("matches.json"); // list of all matches
            Dictionary<string, string> playerAvatars = Util.LoadJSON<Dictionary<string, string>>("playerAvatars.json"); // list of player avatar images (steam urls)
            List<Player> players = Util.LoadJSON<List<Player>>("players.json"); // list of all players
            Dictionary<long, string> uidTable = Util.LoadJSON<Dictionary<long, string>>("uidTable.json"); // list of custom UIDs and associated steamids

            // you can do what you need to here
        }
    }
}
