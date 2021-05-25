using System;

namespace FootballFansLib
{
    public class FootballPlayer : Person, IRootable
    {
        public string FavouritePlayer { get; set; }
        public string FavouriteTeam { get; set; }
        public FootballPlayer(string surname) : base(surname) { }
    }
}