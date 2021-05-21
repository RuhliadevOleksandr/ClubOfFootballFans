using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FanClub : IRootable
    {
        private Group club;

        public string FavouritePlayer { get; set; }
        public string FavouriteTeam { get; set; }

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