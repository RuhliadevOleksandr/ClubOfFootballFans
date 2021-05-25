using System;
using System.IO;
using FootballFansLib;

namespace FootballFans
{
    internal static class CaptainsOfFootballTeams
    {
        internal static FootballTeam[] CreateTeams(string textName)
        {
            int stringIndex = 0;
            string[] text = File.ReadAllLines(textName);

            int numberOfFootballTeams = Int32.Parse(Program.CutText(text, ref stringIndex));
            FootballTeam[] teams = new FootballTeam[numberOfFootballTeams];
            for (int i = 0; i < numberOfFootballTeams; i++)
            {
                stringIndex++;
                if (stringIndex < text.Length && !String.IsNullOrEmpty(text[stringIndex]))
                {
                    string nameOfTeam = Program.CutText(text, ref stringIndex);
                    int numberOfMembers = Int32.Parse(Program.CutText(text, ref stringIndex));
                    FootballPlayer[] group = new FootballPlayer[numberOfMembers];
                    for (int k = 0; k < numberOfMembers; k++)
                        group[k] = new FootballPlayer(Program.CutText(text, ref stringIndex));
                    teams[i] = new FootballTeam(group, nameOfTeam);
                }
                else
                    stringIndex++;
            }
            return teams;
        }
    }
}
