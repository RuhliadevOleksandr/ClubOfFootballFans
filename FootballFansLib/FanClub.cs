using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class FanClub : IRootable
    {
        private Group<FootballFan> club;
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
        public FootballFan this[int index]
        {
            get
            {
                if(index >= 0 && index < club.NumberOfMembers)
                    return club[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of members!");
            }
        }
        public FanClub(List<FootballFan> footballFans, string nameFanClub)
        {
            club = new Group<FootballFan>(footballFans, nameFanClub);
        }
        public void AddFan(FootballFan footballFan)
        {
            club.AddMember(footballFan);
        }
        public void RemoveFan(FootballFan footballFan)
        {
            club.RemoveMember(footballFan);
        }
        public string GetNameOfClub() { return club.NameOfGroup; }
        public int GetNumberOfMembers() { return club.NumberOfMembers; }
    }
}