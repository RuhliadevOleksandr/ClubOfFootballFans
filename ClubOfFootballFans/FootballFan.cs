using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballFan : Person, IRootable
    {
        public string FavouritePlayer { set; get; }
        public string FavouriteTeam { set; get; }
        public FootballFan(string surname) : base(surname)
        {
            //try
            //{

            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
        }
        void IRootable.Root()
        {
            if (FavouritePlayer != null)
                Console.WriteLine($"\nHuray! {FavouritePlayer}!");
            if (FavouriteTeam != null)
                Console.WriteLine($"\nHuray! {FavouriteTeam}!");
        }
    }
}