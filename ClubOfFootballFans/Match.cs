using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Match
    {
        public enum types { ChampionsLeague, EuropaLeague, EURO }
        public string DateOfTheMatch { private set; get; }
        public enum result { Win, Draw, Lose }
    }
}