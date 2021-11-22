using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class Match
    {
        public enum Types { ChampionsLeague, EuropaLeague, EURO }
        public Types TypeOfMatch { private set; get; }
        public DateTime DateOfTheMatch { private set; get; }
        public List<FootballTeam> MembersOfTheMatch { private set; get; }
        public enum Result { Win, Draw, Lose }
        public Match(FootballTeam team, FootballTeam team2, DateTime dateOfTheMatch, Types type)
        {
            if (dateOfTheMatch != null)
            {
                MembersOfTheMatch = new List<FootballTeam>();
                MembersOfTheMatch.Add(team);
                MembersOfTheMatch.Add(team2);
                DateOfTheMatch = dateOfTheMatch;
                TypeOfMatch = type;
            }
            else
                throw new NullReferenceException("\nMatch need to have date!");
        }
        public void EndOfTheMatch(Result result)
        {
            switch (result)
            {
                case Result.Win:
                    MembersOfTheMatch[0].NumberOfAwards += 2;
                    break;
                case Result.Draw:
                    MembersOfTheMatch[0].NumberOfAwards += 1;
                    MembersOfTheMatch[1].NumberOfAwards += 1;
                    break;
                case Result.Lose:
                    MembersOfTheMatch[1].NumberOfAwards += 2;
                    break;
            }
        }
    }
}