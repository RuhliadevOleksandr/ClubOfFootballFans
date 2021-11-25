using System;
using FootballFansLib;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;

namespace FootballFans
{
    internal static class DataFromDB
    {
        public static bool GetData(out List<FanClub> fanClubs, out List<FootballTeam> footballTeams, out List<Season> seasons)
        {
            seasons = null;
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
                seasons = GetSeasons(command, connection, footballTeams);
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
                    for (int i = 0; i < fanClubs.Count; i++)
                    {
                        if (fanClubs[i].GetNameOfClub() == currentNameOfFanClub)
                            indexOfFanClub = i;
                    }
                    if (indexOfFanClub == -1)
                    {
                        List<FootballFan> fans = new List<FootballFan> { fan };
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
        private static List<Season> GetSeasons(DbCommand command, DbConnection connection, in List<FootballTeam> matchRegister)
        {
            List<Season> seasons = new List<Season>();
            command.Connection = connection;
            command.CommandText = "USE FootballFans; SELECT	FootballMatch.MatchID, NameOfFootballTeam AS 'NameOfTeam', FootballMatch.DateOfTheMatch, FootballMatch.ResultOfTheMatch, FootballMatch.MatchType, Stage.StageID, Season.SeasonName, Season.SeasonID ";
            command.CommandText += "FROM (FootballMatch INNER JOIN MatchesAndTeams ON FootballMatch.MatchID = MatchesAndTeams.MatchID) INNER JOIN FootballTeam ON MatchesAndTeams.FootballTeamID = FootballTeam.FootballTeamID ";
            command.CommandText += "INNER JOIN Stage ON FootballMatch.StageID = Stage.StageID INNER JOIN Season ON Stage.SeasonID = Season.SeasonID;";
            using (DbDataReader dbDataReader = command.ExecuteReader())
            {
                Stage stage;
                int lastStage = 1;
                int seasonID = 0;
                int indexOfSeason = 0;
                List<int> matchIDs = new List<int>();
                List<FootballTeam>[] footballTeams = new List<FootballTeam>[2];
                footballTeams[0] = new List<FootballTeam>();
                footballTeams[1] = new List<FootballTeam>();
                List<Match> matches = new List<Match>();
                while (dbDataReader.Read())
                {
                    if (Convert.ToInt32(dbDataReader["StageID"]) != lastStage)
                    {
                        lastStage++;
                        stage = new Stage(matches);
                        matches.Clear();
                        if (seasonID != indexOfSeason)
                        {
                            indexOfSeason++;
                            List<Stage> stages = new List<Stage>();
                            stages.Add(stage);
                            Season season = new Season(stages, dbDataReader["SeasonName"].ToString());
                            seasons.Add(season);
                        }
                        else
                        {
                            seasons[indexOfSeason - 1].AddStage(stage);
                        }
                    }
                    int matchID = Convert.ToInt32(dbDataReader["MatchID"]);
                    int indexOfMatches = matchIDs.IndexOf(matchID);
                    if (indexOfMatches == -1)
                    {
                        matchIDs.Add(matchID);
                        footballTeams[0].Add(FindTeam(matchRegister, dbDataReader["NameOfTeam"].ToString()));
                    }
                    else
                    {
                        footballTeams[1].Add(FindTeam(matchRegister, dbDataReader["NameOfTeam"].ToString()));
                        DateTime dateOfMatch = DateTime.ParseExact(dbDataReader["DateOfTheMatch"].ToString(), "M/d/yyyy HH:mm:ss tt", null);
                        Enum.TryParse(dbDataReader["MatchType"].ToString(), out Match.Types type);
                        Match match = new Match(footballTeams[0][indexOfMatches], footballTeams[1][indexOfMatches], dateOfMatch, type);
                        match.ResultOfTheMatch = IntoCortege(dbDataReader["ResultOfTheMatch"].ToString());
                        matches.Add(match);
                        seasonID = Convert.ToInt32(dbDataReader["SeasonID"]);
                    }
                }
                stage = new Stage(matches);
                seasons[indexOfSeason - 1].AddStage(stage);
            }
            return seasons;
        }
        private static FootballTeam FindTeam(in List<FootballTeam> teams, string nameOfTeam)
        {
            foreach (FootballTeam team in teams)
            {
                if (team.GetNameOfTeam() == nameOfTeam)
                    return team;
            }
            return null;
        }
        private static (int, int) IntoCortege(string text)
        {
            int firstScore = (text[text.IndexOf("(") + 1]) - 48;
            int secondScore = (text[text.IndexOf(")") - 1]) - 48;
            return (firstScore, secondScore);
        }
    }
}
