using FootballFansLib;

namespace FootballFans
{
    internal static class CaptainsOfFootballTeams
    {
        internal static FootballTeam[] CreateTeams()
        {
            FootballTeam[] teams = new FootballTeam[3];

            FootballPlayer[] team = new FootballPlayer[3];
            team[0] = new FootballPlayer("Griezmann", 10);
            team[1] = new FootballPlayer("Braithwaite", 12);
            team[2] = new FootballPlayer("Messi", 19);
            teams[0] = new FootballTeam(team, "Barcelona");

            FootballPlayer[] team2 = new FootballPlayer[3];
            team2[0] = new FootballPlayer("Pedro");
            team2[1] = new FootballPlayer("Verner", 5);
            team2[2] = new FootballPlayer("Mount", 17);
            teams[1] = new FootballTeam(team2, "Chelsea");

            FootballPlayer[] team3 = new FootballPlayer[3];
            team3[0] = new FootballPlayer("Gerrard", 7);
            team3[1] = new FootballPlayer("Henderson");
            team3[2] = new FootballPlayer("Milner", 13);
            teams[2] = new FootballTeam(team3, "Liverpool");

            return teams;
        }
    }
}
