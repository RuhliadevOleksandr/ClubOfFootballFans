using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Season
    {
        private int _numberOfMatches;
        Match[] season;
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
        }
    }
}