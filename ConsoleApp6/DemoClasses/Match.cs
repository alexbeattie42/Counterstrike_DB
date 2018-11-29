using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrikeDB.DemoClasses
{
    class Match
    {
        public bool Valid { get; set; } // wether or not this match is counted in global statistics
                                        // only one match is currently invalid
        public int Version { get; set; } // can be ignored, added for completness 
        public string UID { get; set; } // match unique identifier (pug_{mapname}_{year}-{month}-{day}_{hour} OR {year}-{month}-{day}_{hour}_{mapname}_pug)

        public int Hour { get; set; } // hour of match start
        public int Day { get; set; } // day of match start
        public int Month { get; set; } // month of match start
        public int Year { get; set; } // year of match start
        public double UnixTime { get; set; } // unixtime of match start

        public string Map { get; set; } // map name

        public int Rounds { get; set; } // total rounds played this match
        public Team TeamA { get; set; } // info on teamA
        public Team TeamB { get; set; } // info on teamB
        public List<long> SteamIDs { get; set; } // list of all steamids in the game, disconnected or not
        public long Captain1 { get; set; } // steamid of captain 1
        public long Captain2 { get; set; } // steamid of captain 2
        public bool HasPicks { get; set; } // wether we have a list of captain's player picks for this match
        public List<Pick> Picks { get; set; } // player pick info
        public List<Award> Awards { get; set; } // custom awards info
        public List<RoundWinInfo> RoundWinReasons { get; set; } // round end resons and winning team for each round
        public Dictionary<long, int> DisconnectedClients { get; set; } // clients that left before the match was over
        public Dictionary<long, int> RingerClients { get; set; } // clients that stood in for someone who disconnected
        public Dictionary<long, Player> Players { get; set; } // list of all player data in this match
    }
}
