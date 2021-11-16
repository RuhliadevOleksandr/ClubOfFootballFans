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
        public void AddResultOfMatch(Match.Result[] result)
        {
            for (int i = 0; i < NumberOfMatches; i++)
            {
                season[i].EndOfTheMatch(result[i]);
            }
        }
    }
}