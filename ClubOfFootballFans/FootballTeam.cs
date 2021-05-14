using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballTeam
    {
        public int NumberOfAwards { private set; get; }
        Group team;
        public FootballTeam(FootballPlayer[] footballPlayers, string nameFootballTeam)
        {
            team = new Group(footballPlayers, nameFootballTeam);
        }
    }
}