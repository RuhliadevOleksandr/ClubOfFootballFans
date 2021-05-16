using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FanClub
    {
        private Group club;
        public FanClub(FootballFan[] footballFans, string nameFanClub)
        {
            club = new Group(footballFans, nameFanClub);
            //try
            //{
            //    club = new Group(footballFans, nameFanClub);
            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
        }
        public string[] GetSurnamesOfClub()
        {
            return club.GetSurnamesOfMember();
        }
        public void AddFan(FootballFan footballFan)
        {
            club.AddMember(footballFan);
        }
        public void RemoveFan(FootballFan footballFan)
        {
            club.RemoveMember(footballFan);
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