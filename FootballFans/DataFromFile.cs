using System;
using System.IO;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    internal static class DataFromFile
    {
        private static string CutText(string[] text, ref int stringIndex)
        {
            string result = "";
            for (int i = text[stringIndex].IndexOf(": ") + 2; i < text[stringIndex].Length; i++)
                result += text[stringIndex][i];
            stringIndex++;
            return result;
        }
        private static List<string> CutVS(string text)
        {
            List<string> result = new List<string>();
            string word = "";
            for (int i = 0; i < text.IndexOf(" "); i++)
                word += text[i];
            result.Add(word);
            word = "";
            for (int i = text.IndexOf(" vs ") + 4; i < text.Length; i++)
                word += text[i];
            result.Add(word);
            return result;
        }
        internal static List<FanClub> CreateFanClubs(string textName)
        {
            int stringIndex = 0;
            string[] text = File.ReadAllLines(textName);

            int numberOfFanClubs = Int32.Parse(CutText(text, ref stringIndex));
            List<FanClub> clubs = new List<FanClub>();
            for (int i = 0; i < numberOfFanClubs; i++)
            {
                stringIndex++;
                if (stringIndex < text.Length && !String.IsNullOrEmpty(text[stringIndex]))
                {
                    string nameOfClub = CutText(text, ref stringIndex);
                    string favouriteTeam = CutText(text, ref stringIndex);
                    string favouritePlayer = CutText(text, ref stringIndex);
                    int numberOfMembers = Int32.Parse(CutText(text, ref stringIndex));
                    List<FootballFan> group = new List<FootballFan>();
                    for (int k = 0; k < numberOfMembers; k++)
                        group.Add(new FootballFan(CutText(text, ref stringIndex))
                        {
            FavouritePlayer = favouritePlayer,
            FavouriteTeam = favouriteTeam
                        });
                    clubs.Add(new FanClub(group, nameOfClub)
                    {
                        FavouritePlayer = favouritePlayer,
                        FavouriteTeam = favouriteTeam
                    });
                }
                else
                    stringIndex++;
            }

            return clubs;
        }
        internal static List<FootballTeam> CreateTeams(string textName)
        {
            int stringIndex = 0;
            string[] text = File.ReadAllLines(textName);

            int numberOfFootballTeams = Int32.Parse(CutText(text, ref stringIndex));
            List<FootballTeam> teams = new List<FootballTeam>();
            for (int i = 0; i < numberOfFootballTeams; i++)
            {
                stringIndex++;
                if (stringIndex < text.Length && !String.IsNullOrEmpty(text[stringIndex]))
                {
                    string nameOfTeam = CutText(text, ref stringIndex);
                    int numberOfMembers = Int32.Parse(CutText(text, ref stringIndex));
                    List<FootballPlayer> group = new List<FootballPlayer>();
                    for (int k = 0; k < numberOfMembers; k++)
                        group.Add(new FootballPlayer(CutText(text, ref stringIndex)));
                    teams.Add(new FootballTeam(group, nameOfTeam));
                }
                else
                    stringIndex++;
            }
            return teams;
        }
        internal static (List<List<Season>>, List<List<FootballTeam>>) CreateSeasons(string textNameOfSeasons, string textNameTeams)
        {
            int stringIndex = 0;
            string[] text = File.ReadAllLines(textNameOfSeasons);

            int numberOfSeasons = Int32.Parse(CutText(text, ref stringIndex));
            List<List<Season>> seasons = new List<List<Season>>();
            List<List<FootballTeam>> footballTeams = new List<List<FootballTeam>>();
            for (int i = 0; i < numberOfSeasons; i++)
			{
                stringIndex++;
                if (stringIndex < text.Length && !String.IsNullOrEmpty(text[stringIndex]))
                {
                    Enum.TryParse(CutText(text, ref stringIndex), out Match.Types type);
                    int numberOfStages = Int32.Parse(CutText(text, ref stringIndex));
                    List<Season> stages = new List<Season>();
                    List<FootballTeam> commands = new List<FootballTeam>(CreateTeams(textNameTeams));
                    for (int j = 0; j < numberOfStages; j++)
                    {
                        stringIndex++;
                        int numberOfMatches = Convert.ToInt32(Math.Pow(2, numberOfStages - j) / 2);
                        stages.Add( new Season(FormMatches(numberOfMatches,commands, text,  ref stringIndex, type)));
                    }
                    stringIndex++;
                    seasons.Add(stages);
                    footballTeams.Add(commands);
                    SetResult(footballTeams[i], text,  ref stringIndex);
                }
                else
                    stringIndex++;
			}
            return (seasons, footballTeams);
        }
        private static Match[] FormMatches(int numberOfMatches, in List<FootballTeam> commands, string[] text ,ref int stringIndex, Match.Types type)
        {
            Match[] match = new Match[numberOfMatches];
            for (int i = 0; i < numberOfMatches; i++)
			{
                List<string> membersOfTheMatch = CutVS(CutText(text, ref stringIndex));
                FootballTeam firstTeam = null;
                foreach(FootballTeam team in commands)
                {
                    if (membersOfTheMatch[0] == team.GetNameOfTeam())
	                {
                         firstTeam = team;
	                }
                }
                FootballTeam secondTeam = null;
                foreach(FootballTeam team in commands)
                {
                    if (membersOfTheMatch[0] == team.GetNameOfTeam())
	                {
                        secondTeam = team;
	                }
                }
                string dateOfTheMatch = CutText(text, ref stringIndex);
                DateTime dateTime = DateTime.ParseExact(dateOfTheMatch, "MM/dd/yyyy hh:mm:ss tt", null);
                match[i] = new Match(firstTeam, secondTeam, dateTime, type);
			}
            return match;
        }
        private static void SetResult( in List<FootballTeam> commands, string[] text ,ref int stringIndex)
        {
            stringIndex++;
            foreach (FootballTeam team in commands)
	        {
                string nameOfTeam = "";
                for (int i = 0; i < text[stringIndex].IndexOf(":"); i++)
                    nameOfTeam += text[stringIndex][i];
                int numberOfAwards = Int32.Parse(CutText(text, ref stringIndex));
                foreach (FootballTeam command in commands)
	            {
                    if(command.GetNameOfTeam() == nameOfTeam)
                        command.NumberOfAwards += numberOfAwards;
	            }
	        }
        }
    }
}
