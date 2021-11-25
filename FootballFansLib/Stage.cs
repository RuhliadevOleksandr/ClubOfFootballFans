using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class Stage
    {
        public int NumberOfMatches { get { return stage.Count; } }
        public Match this[int index] 
        { 
            get 
            { 
                if(index >= 0 && index < NumberOfMatches)
                    return stage[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of matches!");
            } 
        }
        private List<Match> stage;
        public Stage(List<Match> matches)
        {
            if (matches != null)
                stage = new List<Match>(matches);
            else
                throw new NullReferenceException("\nStage was not created! You can't create a season from nothing!");
        }
    }
}