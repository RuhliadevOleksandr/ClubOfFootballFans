using System;

namespace FootballFansLib
{
    public class FootballFan : Person, IRootable
    {
        private string _favouritePlayer;
        private string _favouriteTeam;
        public string FavouritePlayer
        {
            get { return _favouritePlayer; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _favouritePlayer = null;
                else
                {  
                    if (isCorrectName(value))
                        _favouritePlayer = value;
                    else
                        throw new ArgumentException("\nSurname of player must have only letters!");
                }
            }
        }
        public string FavouriteTeam
        {
            get { return _favouriteTeam; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _favouriteTeam = null;
                else
                {
                    if (isCorrectName(value))
                        _favouriteTeam = value;
                    else
                        throw new ArgumentException("\nName of team must have only letters!");
                }
            }
        }
        private bool isCorrectName(string name)
        {
            for (int i = 0; i < name.Length; i++)
                        if (!Char.IsLetter(name[i]))
                            return false;
            return true;
        }
        public FootballFan(string surname) : base(surname) {}
    }
}