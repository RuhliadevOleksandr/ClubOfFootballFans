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
        }
        public string[] GetSurnamesOfTeam()
        {
            return team.GetSurnamesOfMember();
        }
        public void AddPlayer(FootballPlayer footballPlayer)
        {
            team.AddMember(footballPlayer);
        }
        public void RemovePlayer(FootballPlayer footballPlayer)
        {
            team.RemoveMember(footballPlayer);
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