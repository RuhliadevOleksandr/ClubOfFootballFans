using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballTeam
    {
        public int NumberOfAwards { internal set; get; }

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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public void RemovePlayer(FootballPlayer footballPlayer)
        {
            try
            {
                team.RemoveMember(footballPlayer);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
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