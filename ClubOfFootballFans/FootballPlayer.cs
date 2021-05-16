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
            //try
            //{
            //  _experience = 0;
            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
            _experience = 0;
        }
        public FootballPlayer(string surname, int experience) :base(surname)
        {
            //try
            //{
            //  if (experience >= 0)
            //      _experience = experience;
            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
            if (experience >= 0)
                _experience = experience;
            else
                throw new Exception("");
        }
        void IRootable.Root()
        {
            Console.WriteLine($"\nHooray! {Surname}!");
        }
    }

}