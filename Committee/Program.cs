using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballFansLib;

namespace FootballFans
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayMenu();
            FootballTeam[] commandRegister = CaptainOfFootballTeam.CreateTeams();
            bool isWorking = true;
            while (isWorking)
            {
                Console.Write("Choose one point of the list: ");
                Int32.TryParse(Console.ReadLine(), out int choise);
                switch (choise)
                {
                    case 0:
                        isWorking = false;
                        break;
                    case 1:
                        Season matchRegister = Committee.CreateSeason(in commandRegister);
                        break;
                    case 2:
                        FanClub fanClub = HeadOfFanClub.CreateFanClub();
                        break;
                    case 3:
                        ShowCommandRegister(in commandRegister);
                        break;
                }
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine(" Enter 0 - to stop the program");
            Console.WriteLine(" Enter 1 - to start the season");
            Console.WriteLine(" Enter 2 - to create fan club");
            Console.WriteLine(" Enter 3 - to show command register");
            Console.WriteLine("\n========================================\n");
        }
        static void ShowCommandRegister(in FootballTeam[] commandRegister)
        {
            int numberOfTeams = commandRegister.Length;
            Console.WriteLine("\n========================================\n");
            for (int i = 0; i < numberOfTeams; i++)
            {
                Console.WriteLine($"\n{commandRegister[i].GetNameOfTeam()}:");
                int numberOfMembers = commandRegister[i].GetNumberOfMembers();
                string[] surnames = commandRegister[i].GetSurnamesOfTeam();
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Surname: {surnames[j]}");
                }
            }
            Console.WriteLine("\n========================================\n");
        }
    }
    public abstract class HeadOfFanClub
    {
        public static FanClub CreateFanClub()
        {
            Console.Write("\nEnter the name of fan club: ");
            string nameOfFanClub = Console.ReadLine();
            Console.Write("Enter the number of fan club members: ");
            Int32.TryParse(Console.ReadLine(), out int numberOfMembers);
            FootballFan[] footballFans = new FootballFan[numberOfMembers];
            for (int i = 0; i < numberOfMembers; i++)
            {
                Console.Write($"Enter the surname of {i + 1} fan: ");
                string surname = Console.ReadLine();
                footballFans[0] = new FootballFan(surname);
            }
            FanClub club = new FanClub(footballFans, nameOfFanClub);
            return club;
        }
    }
    public abstract class CaptainOfFootballTeam
    {
        public static FootballTeam[] CreateTeams()
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
    public abstract class Committee
    {
        public static Season CreateSeason(in FootballTeam[] commands)
        {
            Match[] matches = new Match[3];
            matches[0] = CreateMatch(Match.Types.ChampionsLeague, in commands);
            matches[1] = CreateMatch(Match.Types.EuropaLeague, in commands);
            matches[2] = CreateMatch(Match.Types.EURO, in commands);
            Season season = new Season(matches);
            return season;
        }
        public static Match CreateMatch(Match.Types type, in FootballTeam[] commands)
        {
            Match match = null;
            switch (type)
            {
                case Match.Types.ChampionsLeague:
                    match = new Match(commands[0], commands[1], "29.05.2021");
                    break;
                case Match.Types.EuropaLeague:
                    match = new Match(commands[1], commands[2], "26.05.2021");
                    break;
                case Match.Types.EURO:
                    match = new Match(commands[2], commands[0], "11.05.2021");
                    break;
            }
            return match;
        }
    }
}
