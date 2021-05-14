using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballPlayer : Person, IRootable
    {
        public int NumberOfAwards { private set; get; }
        private int _experience;
        public FootballPlayer(string surname)
        {
            if (surname != null)
            {
                _surname = surname;
                _experience = 0;
            }
        }
        public FootballPlayer(string surname, int experience)
        {
            if (surname != null)
            {
                _surname = surname;
                if (experience >= 0)
                    _experience = experience;
            }
        }
        void IRootable.Root()
        {
            Console.WriteLine("Hooray!");
        }
    }

}