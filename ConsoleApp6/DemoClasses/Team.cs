using System.Collections.Generic;

namespace ConsoleApp6.DemoClasses
{
    public class Team
    {
        public string Name { get; set; } // name of the team
        public long Captain { get; set; } // steamid of the captain
        public int RoundWins { get; set; } // total rounds won by the team
        public int Kills { get; set; } // total kills 
        public int Assists { get; set; } // total assists
        public int Deaths { get; set; } // total deaths
        public double HltvRating { get; set; } // average hltv between players
        public List<long> SteamIDs { get; set; } // list of steamids of every player on the team
    }
}