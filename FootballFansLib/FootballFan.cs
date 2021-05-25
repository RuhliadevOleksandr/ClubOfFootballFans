using System;

namespace FootballFansLib
{
    public class FootballFan : Person, IRootable
    {
        private string _favouritePlayer;
        private string _favouriteTeam;
        public FootballFan(string surname) : base(surname) {}
        public string FavouritePlayer
        {
            get
            {
                return _favouritePlayer;
            }
            set
            {
                bool isCorrectSurname = true;
                if (value == "" || value == null)
                {
                    _favouritePlayer = null;
                    throw new NullReferenceException("\nYou haven't got a favourite player!");
                }
                else
                {
                    for (int i = 0; i < value.Length; i++)
                        if (!Char.IsLetter(value[i]))
                            isCorrectSurname = false;
                }
                if (isCorrectSurname)
                    _favouritePlayer = value;
                else
                    throw new ArgumentException("\nSurname of player must have only letters!");
            }
        }
        public string FavouriteTeam
        {
            get
            {
                return _favouriteTeam;
            }
            set
            {
                bool isCorrectSurname = true;
                if (value == "" || value == null)
                {
                    _favouriteTeam = null;
                    throw new NullReferenceException("\nYou haven't got a favourite team!");
                }
                else
                {
                    for (int i = 0; i < value.Length; i++)
                        if (!Char.IsLetter(value[i]))
                            isCorrectSurname = false;
                }
                if (isCorrectSurname)
                    _favouriteTeam = value;
                else
                    throw new ArgumentException("\nName of team must have only letters!");

            }
        }
    }
}