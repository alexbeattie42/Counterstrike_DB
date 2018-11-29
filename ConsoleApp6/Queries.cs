using CounterStrikeDB.DemoClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrikeDB
{
    class Queries
    {
        private const long NO_MATCH = 0;
        public enum StatType {Match_Stat = 1,Player_Stat, Global_Stat,Match_Award }
        public Dictionary<string, string> playerAvatars;
        private DBConnection dbCon = DBConnection.Instance();
        public Dictionary<string, string> PlayerAvatars { get => playerAvatars; set => playerAvatars = value; }
        private int btoi(bool value)
        {
            return (value == true) ? 1 : 0;
        }
        private string ExecuteQuery(string queryStr)
        {
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(queryStr, dbCon.Connection);
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string result = reader.GetString(0);
                        //Console.WriteLine(result);
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Execution SQL Statment:" + e);
                }
                finally
                {
                    dbCon.Close();
                }

            }
            return null;
        }
        public void AddStatTypes()
        {
            foreach(StatType statType in Enum.GetValues(typeof(StatType)))
            {
                string query = String.Format("INSERT INTO `Stat_Types` VALUES (\"{0}\",\"{1}\")",statType.ToString("D"), statType);
                ExecuteQuery(query);
            }
           

        }

        public void AddPlayerWeapons(Player player, long matchId, StatType  statType)

        {
            if (player.WeaponStats != null)
            {
                foreach (KeyValuePair<string, WeaponStat> weaponStat in player.WeaponStats)
                {
                    long weaponID = AddWeapon(weaponStat.Key);
                    AddWeaponStats(player.SteamID, weaponStat.Value, weaponID, matchId, statType);
                }

            }
        }
        //public void AddAllWeapons(Dictionary<string, WeaponStat> weaponStats)
        //{
        //    if (weaponStats != null)
        //    {
        //        foreach (KeyValuePair<string, WeaponStat> weaponStat in weaponStats)
        //        {
        //            AddWeapon(weaponStat.Key);
        //        }

        //    }
           
        //}
        public void AddWeaponStats(long playerId,WeaponStat weapon, long weaponId, long matchId, StatType statType)
        {
            long statID = AddStats(playerId, matchId, "Weapon_Stat", 0,statType );
            string query = String.Format("INSERT INTO `Weapon_Stats`  VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\")", weaponId, statID, weapon.Shots, weapon.Kills, weapon.Deaths, weapon.Hits, weapon.Damage);
            ExecuteQuery(query);
        }
        public long AddWeapon(string weaponName) 
        {
            string wKey = getWeaponIdStr(weaponName);
            if (wKey == null) 
            {
                string query = String.Format("INSERT INTO `WEAPON` (Name) VALUES (\"{0}\")", weaponName);
                ExecuteQuery(query);
                return long.Parse(getLastPk());
            }
            else return long.Parse(wKey);
        }
        public string getWeaponIdStr(string weaponName)
        {

            string checkWeapon = String.Format("select Weapon_ID from Weapon WHERE Name = \"{0}\"", weaponName);
            string wResult = ExecuteQuery(checkWeapon);
            return wResult;
        }
        public long getWeaponId(string weaponName)
        {
            return long.Parse(getWeaponIdStr(weaponName));
        }
        public void AddGlobalStats(GlobalStats globalStats)
        {
            AddStats(globalStats.Player.SteamID, NO_MATCH, "Matches", globalStats.Matches, StatType.Global_Stat);
            AddStats(globalStats.Player.SteamID, NO_MATCH, "Players", globalStats.Players, StatType.Global_Stat);
            AddStats(globalStats.Player.SteamID, NO_MATCH, "Rounds", globalStats.Rounds, StatType.Global_Stat);
            AddAllPlayerStats(globalStats.Player, NO_MATCH, StatType.Global_Stat);
        }
        public void AddAllPlayerStats(Player player, long matchID, StatType statType)
        {
            AddPlayerWeapons(player, matchID, statType);
            Console.WriteLine("Adding Player Stats");
            AddStats(player.SteamID, matchID, "Kills", player.Kills, statType);
            AddStats(player.SteamID, matchID, "Deaths", player.Deaths, statType);
            AddStats(player.SteamID, matchID, "Assists", player.Assists, statType);
            AddStats(player.SteamID, matchID, "TeamKills", player.TeamKills, statType);
            AddStats(player.SteamID, matchID, "TradeKills", player.TradeKills, statType);
            AddStats(player.SteamID, matchID, "TimesTraded", player.TimesTraded, statType);
            AddStats(player.SteamID, matchID, "FirstKills", player.FirstKills, statType);
            AddStats(player.SteamID, matchID, "FirstDeaths", player.FirstDeaths, statType);
            AddStats(player.SteamID, matchID, "Headshots", player.Headshots, statType);
            AddStats(player.SteamID, matchID, "Plants", player.Plants, statType);
            AddStats(player.SteamID, matchID, "Defuses", player.Defuses, statType);
            AddStats(player.SteamID, matchID, "HealthDamage", player.HealthDamage, statType);
            AddStats(player.SteamID, matchID, "ArmorDamage", player.ArmorDamage, statType);
            AddStats(player.SteamID, matchID, "Rounds", player.Rounds, statType);
            AddStats(player.SteamID, matchID, "RoundWins", player.RoundWins, statType);
            AddStats(player.SteamID, matchID, "KASTRounds", player.KASTRounds, statType);
            AddStats(player.SteamID, matchID, "MVPRounds", player.MVPRounds, statType);
            AddStats(player.SteamID, matchID, "HltvRating", (decimal)player.HltvRating, statType);
            AddStats(player.SteamID, matchID, "Rings", player.Rings, statType);
            AddStats(player.SteamID, matchID, "Disconnects", player.Disconnects, statType);
        }
        public long AddStats(long playerID,long matchID, String statName, decimal value, StatType statType )
        {
            string query;
            if (matchID != NO_MATCH)
            {
                query = String.Format("INSERT INTO `Statistics` (Match_ID,User_ID,Title,Value, Type) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")", matchID, playerID, statName, value, statType.ToString("D"));
            }
            else
            {
                query = String.Format("INSERT INTO `Statistics` (User_ID,Title,Value, Type) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\")", playerID, statName, value, statType.ToString("D"));
            }
            ExecuteQuery(query);
            return long.Parse(getLastPk());
        }
        public void AddMatch(Match match)
        {
            string query = String.Format("INSERT INTO `Match` VALUES (\"{0}\",\"{1}\",{2},\"{3}\",\"{4}\",{5})",match.UID, match.UnixTime,btoi( match.Valid),match.Map,match.Rounds,btoi(match.HasPicks));
            Console.WriteLine(query);
            ExecuteQuery(query);
            long teamAID = AddTeam(match.TeamA);
            long teamBID = AddTeam(match.TeamB);

            AddTeamsToMatch(match.UID,teamAID,teamBID);
            if (match.Players != null)
            {
                foreach (KeyValuePair<long, Player> player in match.Players)
                {
                    long.TryParse(match.UID, out long matchId);
                    AddAllPlayerStats(player.Value, matchId, StatType.Match_Stat);
                }

            }

           

        }
        public void AddAllMatches(List<Match> matches)
        {
            matches.ForEach(match => AddMatch(match));
        }
        public void AddPlayerToTeam(long teamID, long playerID)
        {
            Player player = new Player();
            player.SteamID = playerID;
            player.UID = playerID.ToString();
            AddPlayer(player);
            string query = String.Format("INSERT INTO Belongs_To (User_ID,Team_ID) VALUES (\"{0}\",\"{1}\")", playerID,teamID);
            Console.WriteLine(query);
            ExecuteQuery(query);
        }
        public void AddAllPlayersToTeam(long teamID, List<long> playerIDs )
        {
            playerIDs.ForEach(player => AddPlayerToTeam(teamID, player));
        }
        public long AddTeam(Team team)
        {
            string query;
            //query = String.Format("INSERT INTO (Name,RoundsWin,Kills,Assists,Deaths,HltvRating)  TEAM VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\")",
            //    team.Name, team.RoundWins,team.Kills,team.Assists,team.Deaths,team.HltvRating);
            query = String.Format("INSERT INTO TEAM (Name,Captain) VALUES (\"{0}\",\"{1}\")", team.Name,team.Captain);
            Console.WriteLine(query);
            ExecuteQuery(query);
            string pk = getLastPk();
            long teamID = long.Parse(pk);
            AddAllPlayersToTeam(teamID,team.SteamIDs);
            return teamID;

        }
     
        public void AddAllTeams(List<Team> teams)
        {
            teams.ForEach(team => AddTeam(team));
        }
        public void AddTeamsToMatch(string matchID,long teamAID,long teamBID)
        {
            string query = String.Format("INSERT INTO Teams_In_Match VALUES (\"{0}\",\"{1}\",\"{2}\")",matchID, teamAID, teamBID);
            Console.WriteLine(query);
            ExecuteQuery(query);
        }
        public void AddPlayer(Player player)
        {
            string checkPlayer = String.Format("select User_ID from PLAYER WHERE User_ID = \"{0}\"",player.SteamID);
            string cPResult = ExecuteQuery(checkPlayer);
            //Console.WriteLine(cPResult);
            if (cPResult == null)
            {
                string query;
                if (playerAvatars.TryGetValue(player.UID, out string avatar))
                {
                    query = String.Format("INSERT INTO PLAYER VALUES (\"{0}\",\"{1}\",\"{2}\")", player.SteamID, player.Name, avatar);
                }
                else
                {
                    query = String.Format("INSERT INTO PLAYER (User_ID, NAME) VALUES (\"{0}\",\"{1}\")", player.SteamID, player.Name);
                }

                Console.WriteLine(query);
                ExecuteQuery(query);
                AddAllPlayerStats(player, NO_MATCH, StatType.Player_Stat);

            }
            
        }
        public void AddAllPlayers(List<Player> playerList)
        {
            playerList.ForEach(player => AddPlayer(player));
        }

        public string getLastPk()
        {
            string getpk = "select last_insert_id()";
            return ExecuteQuery(getpk);
        }
        public void TestConnection()
        {
            string query = "SHOW TABLES FROM " + dbCon.DatabaseName;
            ExecuteQuery(query);
        }


    }
}
