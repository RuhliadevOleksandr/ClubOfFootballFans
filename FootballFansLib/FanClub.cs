using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FanClub : IRootable
    {
        private Group club;
        private string _favouritePlayer;
        private string _favouriteTeam;
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
                    throw new Exception("\nYou haven't got a favourite player!");
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
                    throw new Exception("\nYou haven't got a favourite team!");
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
        public FanClub(FootballFan[] footballFans, string nameFanClub)
        {
            try
            {
                club = new Group(footballFans, nameFanClub);
            }
            catch (NullReferenceException exception)
            {
                throw new NullReferenceException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
        public string[] GetSurnamesOfClub()
        {
            return club.GetSurnamesOfMember();
        }
        public void AddFan(FootballFan footballFan)
        {
            try
            {
                club.AddMember(footballFan);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public void RemoveFan(FootballFan footballFan)
        {
            try
            {
                club.RemoveMember(footballFan);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public string GetNameOfClub()
        {
            return club.NameOfGroup;
        }
        public int GetNumberOfMembers()
        {
            return club.NumberOfMembers;
        }
    }
}