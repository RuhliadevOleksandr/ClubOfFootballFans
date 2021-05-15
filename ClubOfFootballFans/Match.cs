using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Match
    {
        public enum Types { ChampionsLeague, EuropaLeague, EURO }
        public string DateOfTheMatch { private set; get; }
        public string MembersOfTheMatch { private set; get; }
        public enum Result { Win, Draw, Lose }
        public Match(FootballTeam team, FootballTeam team2, string dateOfTheMatch)
        {
            if (dateOfTheMatch != null)
            {
                DateOfTheMatch = dateOfTheMatch;
                MembersOfTheMatch = team.GetNameOfTeam() + " vs ";
                MembersOfTheMatch += team2.GetNameOfTeam();
            }
        }
        public void EndOfTheMatch(FootballTeam team, FootballTeam team2, Match.Result result)
        {
            switch (result)
            {
                case Match.Result.Win:
                    team.NumberOfAwards += 2;
                    break;
                case Match.Result.Draw:
                    team.NumberOfAwards += 1;
                    team2.NumberOfAwards += 1;
                    break;
                case Match.Result.Lose:
                    team2.NumberOfAwards += 2;
                    break;
            }
        }
    }
}