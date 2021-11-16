using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Match
    {
        public enum Types { ChampionsLeague, EuropaLeague, EURO }
        public Types TypeOfMatch { private set; get; }
        public DateTime DateOfTheMatch { private set; get; }
        public string[] MembersOfTheMatch { private set; get; }
        public enum Result { Win, Draw, Lose }
        public Match(FootballTeam team, FootballTeam team2, DateTime dateOfTheMatch, Types type)
        {
            if (dateOfTheMatch != null)
            {
                DateOfTheMatch = dateOfTheMatch;
                MembersOfTheMatch = new string[2];
                MembersOfTheMatch[0] = team.GetNameOfTeam();
                MembersOfTheMatch[1] = team2.GetNameOfTeam();
                TypeOfMatch = type;
            }
            else
                throw new NullReferenceException("Match need to have date!");
        }
        public void EndOfTheMatch(FootballTeam[] teams, Match.Result result)
        {
            FootballTeam firstTeam = null, 
                         secondTeam = null;
            int numberOfTeams = teams.Length;
            for (int i = 0; i < numberOfTeams; i++)
            {
                if (teams[i].GetNameOfTeam() == MembersOfTheMatch[0])
                    firstTeam = teams[i];
                if (teams[i].GetNameOfTeam() == MembersOfTheMatch[1])
                    secondTeam = teams[i];
            }
            switch (result)
            {
                case Match.Result.Win:
                    firstTeam.NumberOfAwards += 2;
                    break;
                case Match.Result.Draw:
                    firstTeam.NumberOfAwards += 1;
                    secondTeam.NumberOfAwards += 1;
                    break;
                case Match.Result.Lose:
                    secondTeam.NumberOfAwards += 2;
                    break;
            }
        }
    }
}