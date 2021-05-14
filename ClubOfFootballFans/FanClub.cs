using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FanClub
    {
        private string _meetingPlace;
        Group club;
        public FanClub(FootballFan[] footballFans, string nameFanClub)
        {
            club = new Group(footballFans, nameFanClub);
        }
    }
}