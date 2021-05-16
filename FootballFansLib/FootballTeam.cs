using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballTeam : IRootable
    {
        public int NumberOfAwards { internal set; get; }
        public string FavouritePlayer { get; set; }
        public string FavouriteTeam { get; set; }

        private Group team;
        public FootballTeam(FootballPlayer[] footballPlayers, string nameFootballTeam)
        {
            try
            {
                team = new Group(footballPlayers, nameFootballTeam);
                NumberOfAwards = 0;
            }
            catch (NullReferenceException message)
            {
                throw new NullReferenceException(message.ToString());
            }
        }
        public string[] GetSurnamesOfTeam()
        {
            return team.GetSurnamesOfMember();
        }
        public void AddPlayer(FootballPlayer footballPlayer)
        {
            try
            {
                team.AddMember(footballPlayer);
            }
            catch (NullReferenceException exception)
            {
                throw new NullReferenceException(exception.Message);
            }
        }
        public void RemovePlayer(FootballPlayer footballPlayer)
        {
            try
            {
                team.RemoveMember(footballPlayer);
            }
            catch (NullReferenceException exception)
            {
                throw new NullReferenceException(exception.Message);
            }
        }
        public string GetNameOfTeam()
        {
            return team.NameOfGroup;
        }
        public int GetNumberOfMembers()
        {
            return team.NumberOfMembers;
        }
    }
}