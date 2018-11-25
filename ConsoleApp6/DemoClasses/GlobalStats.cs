using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6.DemoClasses
{
    public class GlobalStats
    {
        public int Matches { get; set; } // total number of matches recorded
        public int Players { get; set; } // total number of players recorded
        public int Rounds { get; set; } // total number of rounds recorded
        public Player Player { get; set; } // totals of all players tracked (uid and steamid are random/not important here)
    }
}
