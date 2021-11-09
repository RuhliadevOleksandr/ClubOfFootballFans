using System;
using System.IO;
using FootballFansLib;

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
        internal static FanClub[] CreateFanClubs(string textName)
        {
            int stringIndex = 0;
            string[] text = File.ReadAllLines(textName);

            int numberOfFanClubs = Int32.Parse(CutText(text, ref stringIndex));
            FanClub[] clubs = new FanClub[numberOfFanClubs];
            for (int i = 0; i < numberOfFanClubs; i++)
            {
                stringIndex++;
                if (stringIndex < text.Length && !String.IsNullOrEmpty(text[stringIndex]))
                {
                    string nameOfClub = CutText(text, ref stringIndex);
                    string favouriteTeam = CutText(text, ref stringIndex);
                    string favouritePlayer = CutText(text, ref stringIndex);
                    int numberOfMembers = Int32.Parse(CutText(text, ref stringIndex));
                    FootballFan[] group = new FootballFan[numberOfMembers];
                    for (int k = 0; k < numberOfMembers; k++)
                        group[k] = new FootballFan(CutText(text, ref stringIndex))
                        {
                            FavouritePlayer = favouritePlayer,
                            FavouriteTeam = favouriteTeam
                        };
                    clubs[i] = new FanClub(group, nameOfClub)
                    {
                        FavouritePlayer = favouritePlayer,
                        FavouriteTeam = favouriteTeam
                    };
                }
                else
                    stringIndex++;
            }

            return clubs;
        }
        internal static FootballTeam[] CreateTeams(string textName)
        {
            int stringIndex = 0;
            string[] text = File.ReadAllLines(textName);

            int numberOfFootballTeams = Int32.Parse(CutText(text, ref stringIndex));
            FootballTeam[] teams = new FootballTeam[numberOfFootballTeams];
            for (int i = 0; i < numberOfFootballTeams; i++)
            {
                stringIndex++;
                if (stringIndex < text.Length && !String.IsNullOrEmpty(text[stringIndex]))
                {
                    string nameOfTeam = CutText(text, ref stringIndex);
                    int numberOfMembers = Int32.Parse(CutText(text, ref stringIndex));
                    FootballPlayer[] group = new FootballPlayer[numberOfMembers];
                    for (int k = 0; k < numberOfMembers; k++)
                        group[k] = new FootballPlayer(CutText(text, ref stringIndex));
                    teams[i] = new FootballTeam(group, nameOfTeam);
                }
                else
                    stringIndex++;
            }
            return teams;
        }
    }
}
