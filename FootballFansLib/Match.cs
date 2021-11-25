using System;

namespace FootballFansLib
{
    public class Match
    {
        public enum Types { ChampionsLeague, EuropaLeague, EURO }
        public Types TypeOfMatch { private set; get; }
        public DateTime DateOfTheMatch { private set; get; }
        public (FootballTeam, FootballTeam) MembersOfTheMatch { private set; get; }
        public (int, int) ResultOfTheMatch { set; get; }
        public enum Result { Win, Draw, Lose }
        public Match(FootballTeam team, FootballTeam team2, DateTime dateOfTheMatch, Types type)
        {
            if (dateOfTheMatch != null)
            {
                MembersOfTheMatch = (team, team2);
                DateOfTheMatch = dateOfTheMatch;
                TypeOfMatch = type;
            }
            else
                throw new NullReferenceException("\nMatch need to have date!");
        }
    }
}