using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Season
    {
        public int NumberOfMatches { get; private set; }
        public Match this[int index]
        {
            get
            {
                return season[index];
            }
        }
        private Match[] season;
        public Season(Match[] matches)
        {
            if (matches != null)
            {
                NumberOfMatches = matches.Length;
                season = new Match[NumberOfMatches];
                for (int i = 0; i < NumberOfMatches; i++)
                {
                    season[i] = matches[i];
                }
            }
            else
                throw new NullReferenceException("You can't create a season from nothing!");
        }
        public string[][] MembersOfTheMatches() 
        {
            string[][] membersOfTheMatches = new string[2][];
            membersOfTheMatches[0] = new string[NumberOfMatches];
            membersOfTheMatches[1] = new string[NumberOfMatches];
            for (int i = 0; i < NumberOfMatches; i++)
            {
                membersOfTheMatches[0][i] = season[i].MembersOfTheMatch[0];
                membersOfTheMatches[1][i] = season[i].MembersOfTheMatch[1];
            }
            return membersOfTheMatches;
        }
        public void AddResultOfMatch(FootballTeam[] teams, Match.Result[] result)
        {
            for (int i = 0; i < NumberOfMatches; i++)
            {
                season[i].EndOfTheMatch(teams, result[i]);
            }
        }
    }
}