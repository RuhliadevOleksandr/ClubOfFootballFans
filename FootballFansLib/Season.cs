using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class Season
    {
        public int NumberOfMatches { get { return season.Count; } }
        public Match this[int index] 
        { 
            get 
            { 
                if(index >= 0 && index < NumberOfMatches)
                    return season[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of matches!");
            } 
        }
        private List<Match> season;
        public Season(List<Match> matches)
        {
            if (matches != null)
                season = new List<Match>(matches);
            else
                throw new NullReferenceException("\nSeason was not created! You can't create a season from nothing!");
        }
        public void AddResultOfMatch(List<Match.Result> results)
        {
            if(results != null)
                for (int i = 0; i < NumberOfMatches; i++)
                    season[i].EndOfTheMatch(results[i]);
            else
                throw new NullReferenceException("\nResuts of matches were not added!");
        }
    }
}