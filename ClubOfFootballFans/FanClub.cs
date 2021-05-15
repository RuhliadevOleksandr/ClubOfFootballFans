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
        }
        public void AddFan(FootballFan footballFan)
        {
            club.AddMember(footballFan);
        }
        public void RemoveFan(FootballFan footballFan)
        {
            club.RemoveMember(footballFan);
        }
    }
}