using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballPlayer : Person, IRootable
    {
        private int _experience;
        public FootballPlayer(string surname) : base(surname)
        {
           _experience = 0;
        }
        public FootballPlayer(string surname, int experience) :base(surname)
        {
            if (experience >= 0)
                    _experience = experience;
        }
        void IRootable.Root()
        {
            Console.WriteLine($"\nHooray! {Surname}!");
        }
    }

}