using System;

namespace FootballFansLib
{
    public class FootballPlayer : Person, IRootable
    {
        public string FavouritePlayer { get; set; }
        public string FavouriteTeam { get; set; }
        private int _experience;
        public FootballPlayer(string surname) : base(surname)
        {
            _experience = 0;
        }
        public FootballPlayer(string surname, int experience) :base(surname)
        {
            if (experience > 0)
                _experience = experience;
            else
                throw new ArgumentException("Experience of football player must be a positive number!");
        }
    }

}