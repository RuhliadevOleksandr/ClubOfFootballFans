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
            ShowMenu(); 
            FanClub fanClub = null;
            Season matchRegister = null;
            FootballTeam[] teamRegister = null;
            bool isWorking = true;
            while (isWorking)
            {
                Console.Write("Choose one point of the list: ");
                bool isParsed = Int32.TryParse(Console.ReadLine(), out int choise);
                if(choise <= 7 && choise >= 0 && isParsed)
                {
                    switch (choise)
                    {
                        case 0:
                            isWorking = false;
                            break;
                        case 1:
                            fanClub = HeadOfFanClub.CreateFanClub();
                            ShowMenu();
                            break;
                        case 2:
                            teamRegister = CaptainsOfFootballTeams.CreateTeams();
                            Console.WriteLine("\nYou formed football teams!");
                            ShowMenu();
                            break;
                        case 3:
                            if (teamRegister != null)
                            {
                                matchRegister = Committee.CreateSeason(in teamRegister);
                            }
                            else
                                Console.WriteLine("\nYou haven't already form football teams!");
                            ShowMenu();
                            break;
                        case 4:
                            if (fanClub != null)
                            {
                                ShowFanClub(in fanClub);
                            }
                            else
                                Console.WriteLine("\nYou haven't already create fan club!");
                            ShowMenu();
                            break;
                        case 5:
                            if(teamRegister != null)
                            {
                                ShowTeamRegister(in teamRegister);
                            }
                            else
                                Console.WriteLine("\nYou haven't already form football teams!");
                            ShowMenu();
                            break;
                        case 6:
                            if (matchRegister != null)
                            {
                                ShowMatchRegister(in matchRegister);
                            }
                            else
                                Console.WriteLine("\nYou haven't already start the season!");
                            ShowMenu();
                            break;
                        case 7:
                            if(teamRegister != null)
                            {
                                if (matchRegister != null)
                                {
                                    ShowResultOfSeason(in matchRegister, in teamRegister);
                                }
                                else
                                    Console.WriteLine("\nYou haven't already start the season!");
                            }
                            else
                                Console.WriteLine("\nYou haven't already form football teams!");
                            ShowMenu();
                            break;
                    }
                }
                else
                    Console.WriteLine("\nThere isn't such point! Please try again\n");
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine(" Enter 0 - to stop the program");
            Console.WriteLine(" Enter 1 - to create fan club");
            Console.WriteLine(" Enter 2 - to form football teams");
            Console.WriteLine(" Enter 3 - to start the season");
            Console.WriteLine(" Enter 4 - to show fan club");
            Console.WriteLine(" Enter 5 - to show command register");
            Console.WriteLine(" Enter 6 - to show match register");
            Console.WriteLine(" Enter 7 - to show result of season");
            Console.WriteLine("\n========================================\n");
        }
        static void ShowFanClub(in FanClub fanClub)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine($"Fan club: {fanClub.GetNameOfClub()}");
            int numberOfMembers = fanClub.GetNumberOfMembers();
            string[] surnames = fanClub.GetSurnamesOfClub();
            for (int j = 0; j < numberOfMembers; j++)
            {
                Console.WriteLine($"Surname: {surnames[j]}");
            }
            Console.WriteLine("\n========================================\n");
        }
        static void ShowTeamRegister(in FootballTeam[] teamRegister)
        {
            int numberOfTeams = teamRegister.Length;
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Register of teams:");
            for (int i = 0; i < numberOfTeams; i++)
            {
                Console.WriteLine($"\n{teamRegister[i].GetNameOfTeam()}:");
                int numberOfMembers = teamRegister[i].GetNumberOfMembers();
                string[] surnames = teamRegister[i].GetSurnamesOfTeam();
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Surname: {surnames[j]}");
                }
            }
            Console.WriteLine("\n========================================\n");
        }
        static void ShowMatchRegister(in Season matchRegister)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Register of matches:");
            int numberOfMatches = matchRegister.NumberOfMatches;
            string[][] membersOfTheMatches = matchRegister.MembersOfTheMatches();
            string[] datesOfTheMatches = matchRegister.GetDatesOfTheMatches();
            for (int j = 0; j < numberOfMatches; j++)
            {
                Console.Write($"\nMembers Of the {j + 1} match: {membersOfTheMatches[0][j]}");
                Console.WriteLine($" vs {membersOfTheMatches[1][j]}");
                Console.WriteLine($"Date Of the {j + 1} match: {datesOfTheMatches[j]}");
            }
            Console.WriteLine("\n========================================\n");
        }
        static void ShowResultOfSeason(in Season matchRegister, in FootballTeam[] teamRegister)
        {
            Committee.FinishSeason(in matchRegister, in teamRegister);
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Result of matches(number of awards):");
            int numberOfTeams = teamRegister.Length;
            for (int j = 0; j < numberOfTeams; j++)
            {
                Console.Write($"\n{teamRegister[j].GetNameOfTeam()} :");
                Console.WriteLine(teamRegister[0].NumberOfAwards);
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
            bool isParsed = false;
            FanClub club = null;
            while (!isParsed)
            {
                Console.Write("Enter the number of fan club members: ");
                isParsed = Int32.TryParse(Console.ReadLine(), out int numberOfMembers);
                if (isParsed && numberOfMembers > 1)
                {
                    FootballFan[] footballFans = new FootballFan[numberOfMembers];
                    for (int i = 0; i < numberOfMembers; i++)
                    {
                        Console.Write($"Enter the surname of {i + 1} fan: ");
                        string surname = Console.ReadLine();
                        //try
                        //{
                        //    footballFans[i] = new FootballFan(surname);
                        //}
                        //catch (Exception exception)
                        //{
                        //    Console.WriteLine($"{exception.Message}");
                        //}
                        footballFans[i] = new FootballFan(surname);
                    }
                    club = new FanClub(footballFans, nameOfFanClub);
                }
                else
                    Console.WriteLine("\nThe number of fan club members must be more than one!");
            }
            return club;
        }
    }
    public abstract class CaptainsOfFootballTeams
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
        public static void FinishSeason(in Season matchRegister, in FootballTeam[] teamRegister)
        {
            Match.Result[] result = new Match.Result[3];
            result[0] = Match.Result.Win;
            result[1] = Match.Result.Lose;
            result[2] = Match.Result.Draw;
            matchRegister.AddResultOfMatch(teamRegister, result);
        }
    }
}
