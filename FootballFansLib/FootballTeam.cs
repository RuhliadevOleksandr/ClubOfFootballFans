using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class FootballTeam
    {
        private Group<FootballPlayer> team;
        public FootballPlayer this[int index]
        {
            get
            {
                if(index >= 0 && index < team.NumberOfMembers)
                    return team[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of members!");
            }
        }
        public FootballTeam(List<FootballPlayer> footballPlayers, string nameFootballTeam)
        {
            team = new Group<FootballPlayer>(footballPlayers, nameFootballTeam);
        }
        public void AddPlayer(FootballPlayer footballPlayer)
        {
            team.AddMember(footballPlayer);
        }
        public void RemovePlayer(FootballPlayer footballPlayer)
        {
            team.RemoveMember(footballPlayer);
        }
        public string GetNameOfTeam() { return team.NameOfGroup; }
        public int GetNumberOfMembers() { return team.NumberOfMembers; }
    }
}