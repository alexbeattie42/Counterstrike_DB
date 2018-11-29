using System;
using System.Collections.Generic;

namespace CounterStrikeDB.DemoClasses
{
    public class Player
    {
        public string UID { get; set; } // unique identifier, either STEAMID or custom UID (ex: my custom uid is marxy)
        public string Name { get; set; } // display name, either STEAM NAME while in the match, or custom uid (ex: my custom uid is marxy)
        public long SteamID { get; set; } // players steamid

        public int Kills { get; set; } // total kills
        public int Deaths { get; set; } // total deaths
        public int Assists { get; set; } // total assists
        public int TeamKills { get; set; } // total teamkills
        public int TradeKills { get; set; } // total times the player got a trade kill
        public int TimesTraded { get; set; } // total times the player was trade by a teammate
        public int FirstKills { get; set; } // total times the player got the first kill in the round
        public int FirstDeaths { get; set; } // total times the player was the first death in the round
        public int Headshots { get; set; } // total headshots

        public int Plants { get; set; } // total succsesful bomb plants
        public int Defuses { get; set; } // total succsesful bomb defusals
        public int HealthDamage { get; set; } // total damage done to health (NOTE: armor + health = total damage dealt)
        public int ArmorDamage { get; set; } // total damage dont to armor (NOTE: armor + health = total damage dealt)

        public int Rounds { get; set; } // total rounds played, this is only incremented if the player is in the game for the round
        public int RoundWins { get; set; } // total rounds won
        public int KASTRounds { get; set; } // total kast rounds (round where player got a kill, assist, survived, or was traded by a teammate)
        public int MVPRounds { get; set; } // total mvp awards

        public double HltvRating { get; set; } // hltvrating 1.0

        public int Rings { get; set; } // number of times player has stood in for someone who disconnected
        public int Disconnects { get; set; } // number of times player has left the match early

        public List<string> Matches { get; set; } // list of match UIDs the player has participated in
        public Dictionary<int, int> ClutchRoundsEntered { get; set; } // list of clutch rounds entered (ex: clutchroundswon[3] = number of 1v3s player has entered)
        public Dictionary<int, int> ClutchRoundsWon { get; set; } // list of clutch rounds won (ex: clutchroundswon[3] = number of 1v3s player has won)
        public Dictionary<int, int> MultiKillRounds { get; set; } // list of multikill rounds (ex: multikillrounds[5] = number of rounds player has gotten an ace)

        public Dictionary<long, int> KilledPlayers { get; set; } // list of total kills against another steamid (player) 
        public Dictionary<string, WeaponStat> WeaponStats { get; set; } // list of weapon stats

        // These are just calculated from the stats above
        public int TotalClutchesEntered { get => ClutchRoundsEntered[1] + ClutchRoundsEntered[2] + ClutchRoundsEntered[3] + ClutchRoundsEntered[4] + ClutchRoundsEntered[5]; }
        public int TotalClutchesWon { get => ClutchRoundsWon[1] + ClutchRoundsWon[2] + ClutchRoundsWon[3] + ClutchRoundsWon[4] + ClutchRoundsWon[5]; }

        public double HeadshotPercentage { get => Math.Round((Headshots / (double)Kills) * 100, 2); }
        public int KillDeathDifference { get => Kills - Deaths; }
        public int Damage { get => ArmorDamage + HealthDamage; }
        public double KillsPerRound { get => Math.Round(Kills / (double)Rounds, 2); }
        public double AssistsPerRound { get => Math.Round(Assists / (double)Rounds, 2); }
        public double DeathsPerRound { get => Math.Round(Deaths / (double)Rounds, 2); }
        public double DamagePerRound { get => Math.Round(Damage / (double)Rounds, 2); }

        // These are totals of each WeaponStat
        public int Shots
        {
            get
            {
                int count = 0;

                foreach (var stat in WeaponStats.Values)
                    count += stat.Shots;

                return count;
            }
        }

        public int Hits
        {
            get
            {
                int count = 0;

                foreach (var stat in WeaponStats.Values)
                    count += stat.Hits;

                return count;
            }
        }

        public double Accuracy { get => Math.Round((Hits / (double)Shots) * 100, 2); }

        public int FlashAssists { get; set; } // NOTE: never implemented, will always be 0
    }
}