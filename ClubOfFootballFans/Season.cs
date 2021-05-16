using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Season
    {
        private int _numberOfMatches;
        public int NumberOfMatches
        {
            get
            {
                return _numberOfMatches;
            }
        }
        private Match[] season;
        public Season(Match[] matches)
        {
            if (matches != null)
            {
                _numberOfMatches = matches.Length;
                season = new Match[_numberOfMatches];
                for (int i = 0; i < _numberOfMatches; i++)
                {
                    season[i] = matches[i];
                }
            }
            else
                throw new Exception("");
        }
        public string[] GetDatesOfTheMatches() 
        {
            string[] datesOfTheMatches = new string[_numberOfMatches];
            for (int i = 0; i < _numberOfMatches; i++)
            {
                datesOfTheMatches[i] = season[i].DateOfTheMatch;
            }
            return datesOfTheMatches;
        }
        public string[][] MembersOfTheMatches() 
        {
            string[][] membersOfTheMatches = new string[2][];
            membersOfTheMatches[0] = new string[_numberOfMatches];
            membersOfTheMatches[1] = new string[_numberOfMatches];
            for (int i = 0; i < _numberOfMatches; i++)
            {
                membersOfTheMatches[0][i] = season[i].DateOfTheMatch;
                membersOfTheMatches[1][i] = season[i].DateOfTheMatch;
            }
            return membersOfTheMatches;
        }
        public void AddResultOfMatch(FootballTeam[] teams, Match.Result[] result)
        {
            for (int i = 0; i < _numberOfMatches; i++)
            {
                season[i].EndOfTheMatch(teams, result[i]);
            }
        }
    }
}