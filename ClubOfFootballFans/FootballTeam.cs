using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class FootballTeam : IRootable
    {
        public int NumberOfAwards { internal set; get; }
        private Group team;
        public FootballTeam(FootballPlayer[] footballPlayers, string nameFootballTeam)
        {
            team = new Group(footballPlayers, nameFootballTeam);
            NumberOfAwards = 0;
            //try
            //{
            //    team = new Group(footballPlayers, nameFootballTeam);
            //    NumberOfAwards = 0;
            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
        }
        public string[] GetSurnamesOfTeam()
        {
            return team.GetSurnamesOfMember();
        }
        public void AddPlayer(FootballPlayer footballPlayer)
        {
            team.AddMember(footballPlayer);
            //try
            //{
            //    team.AddMember(footballPlayer);
            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
        }
        public void RemovePlayer(FootballPlayer footballPlayer)
        {
            team.RemoveMember(footballPlayer);
            //try
            //{
            //    team.RemoveMember(footballPlayer);
            //}
            //catch (Exception message)
            //{
            //    throw new Exception(message.ToString());
            //}
        }
        public string GetNameOfTeam()
        {
            return team.NameOfGroup;
        }
        public int GetNumberOfMembers()
        {
            return team.NumberOfMembers;
        }
        public void Root()
        {
            Console.WriteLine($"Hooray! {team.NameOfGroup}!");
        }
    }
}