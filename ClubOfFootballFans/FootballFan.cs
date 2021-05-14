using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballFan : Person, IRootable
    {
        public FootballPlayer FavouritePlayer { set; get; }
        public FootballTeam FavouriteTeam { set; get; }
        public FootballFan(string surname)
        {
            if (surname != null)
            {
                _surname = surname;
            }
        }
        void IRootable.Root()
        {
            Console.WriteLine("Huray!");
        }
    }
}