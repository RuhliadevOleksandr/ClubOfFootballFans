using System;

namespace FootballFansLib
{
    public class FootballPlayer : Person, IRootable
    {
        public int Experience { get; private set; }
        public string FavouritePlayer { get; set; }
        public string FavouriteTeam { get; set; }
        public FootballPlayer(string surname) : base(surname)
        {
            Experience = 0;
        }
        public FootballPlayer(string surname, int experience) :base(surname)
        {
            if (experience > 0)
                Experience = experience;
            else
                throw new ArgumentException("Experience of football player must be a positive number!");
        }
    }

}