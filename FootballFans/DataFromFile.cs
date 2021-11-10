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
                    clubs.Add(new FanClub(group.ToArray(), nameOfClub)
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
                    teams.Add(new FootballTeam(group.ToArray(), nameOfTeam));
                }
                else
                    stringIndex++;
            }
            return teams;
        }
    }
}
