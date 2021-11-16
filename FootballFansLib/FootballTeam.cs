using System;

namespace FootballFansLib
{
    public class FootballTeam
    {
        public int NumberOfAwards { internal set; get; }
        public Person this[int index]
        {
            get
            {
                if(index >= 0 && index < team.NumberOfMembers)
                    return team[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of members!");
            }
        }
        private Group team;
        public FootballTeam(FootballPlayer[] footballPlayers, string nameFootballTeam)
        {
            try
            {
                team = new Group(footballPlayers, nameFootballTeam);
                NumberOfAwards = 0;
            }
            catch (NullReferenceException exception)
            {
                throw new NullReferenceException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
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