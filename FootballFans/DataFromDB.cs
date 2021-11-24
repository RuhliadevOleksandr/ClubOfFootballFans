using System;
using FootballFansLib;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;

namespace FootballFans
{
	internal static class DataFromDB
	{
		public static bool GetData(out List<FanClub> fanClubs, out List<FootballTeam> footballTeams)
		{
            fanClubs = null;
            footballTeams = null;
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    throw new NullReferenceException("\nCan't connect to database!");
                }
                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    throw new NullReferenceException("\nCan't create command!");
                }
                fanClubs = GetFanClubs(command, connection);
                footballTeams = GetFootballTeams(command, connection);
                return true;
            }
        }
        private static List<FanClub> GetFanClubs(DbCommand command, DbConnection connection)
        {
            List<FanClub> fanClubs = new List<FanClub>();
            command.Connection = connection;
            command.CommandText = "USE FootballFans; SELECT FootballFan.Surname AS 'FanSurname', ";
            command.CommandText += "FanClub.FanClubName, FootballPlayer.Surname AS 'PlayerSurname', FootballTeam.NameOfFootballTeam FROM((FootballFan INNER JOIN FanClub ON FootballFan.FanClubID = FanClub.FanClubID) ";
            command.CommandText += "INNER JOIN FootballPlayer ON FootballFan.FavouritePlayer = FootballPlayer.FootballPlayerID) INNER JOIN FootballTeam ON FootballPlayer.FootballTeamID = FootballTeam.FootballTeamID; ";
            using (DbDataReader dbDataReader = command.ExecuteReader())
            {
                while (dbDataReader.Read())
                {
                    FootballFan fan = new FootballFan(dbDataReader["FanSurname"].ToString());
                    string currentNameOfFanClub = dbDataReader["FanClubName"].ToString();
                    fan.FavouritePlayer = dbDataReader["PlayerSurname"]?.ToString() ?? null;
                    fan.FavouriteTeam = dbDataReader["NameOfFootballTeam"]?.ToString() ?? null;
                    int indexOfFanClub = -1;
                    for(int i = 0; i < fanClubs.Count; i++)
                    {
                        if(fanClubs[i].GetNameOfClub() == currentNameOfFanClub)
                            indexOfFanClub = i;
                    }
                    if (indexOfFanClub == -1)
                    {
                        List <FootballFan> fans = new List<FootballFan>{ fan };
                        fanClubs.Add(new FanClub(fans, currentNameOfFanClub));
                        for (int i = 0; i < fanClubs.Count; i++)
                        {
                            if (fanClubs[i].GetNameOfClub() == currentNameOfFanClub)
                                indexOfFanClub = i;
                        }
                        fanClubs[indexOfFanClub].FavouritePlayer = fan.FavouritePlayer;
                        fanClubs[indexOfFanClub].FavouriteTeam = fan.FavouriteTeam;
                    }
                    else
                    {
                        fanClubs[indexOfFanClub].AddFan(fan);
                        fanClubs[indexOfFanClub].FavouritePlayer = fan.FavouritePlayer;
                        fanClubs[indexOfFanClub].FavouriteTeam = fan.FavouriteTeam;
                    }
                }
            }
            return fanClubs;
        }
        private static List<FootballTeam> GetFootballTeams(DbCommand command, DbConnection connection)
        {
            List<FootballTeam> footballTeams = new List<FootballTeam>();
            command.Connection = connection;
            command.CommandText = "USE FootballFans; SELECT FootballPlayer.Surname AS 'PlayerSurname', FootballTeam.NameOfFootballTeam AS 'NameOfTeam' ";
            command.CommandText += "FROM FootballPlayer INNER JOIN FootballTeam ON FootballPlayer.FootballTeamID = FootballTeam.FootballTeamID;";
            using (DbDataReader dbDataReader = command.ExecuteReader())
            {
                while (dbDataReader.Read())
                {
                    FootballPlayer player = new FootballPlayer(dbDataReader["PlayerSurname"].ToString());
                    string currentNameOfTeam = dbDataReader["NameOfTeam"].ToString();
                    int indexOfTeam = -1;
                    for (int i = 0; i < footballTeams.Count; i++)
                    {
                        if (footballTeams[i].GetNameOfTeam() == currentNameOfTeam)
                            indexOfTeam = i;
                    }
                    if (indexOfTeam == -1)
                    {
                        List<FootballPlayer> players = new List<FootballPlayer> { player };
                        footballTeams.Add(new FootballTeam(players, currentNameOfTeam));
                    }
                    else
                        footballTeams[indexOfTeam].AddPlayer(player);
                }
            }
            return footballTeams;
        }
    }
}
