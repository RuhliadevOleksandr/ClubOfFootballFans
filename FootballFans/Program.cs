using System;
using FootballFansLib;

namespace FootballFans
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nEnter surname: ");
            string userSurname = Console.ReadLine();
            FootballFan user = new FootballFan(userSurname);
            FanClub[] fanClubs = HeadOfFanClub.CreateFanClubs();
            FootballTeam[] teamRegister = CaptainsOfFootballTeams.CreateTeams();
            Season matchRegister = Committee.CreateSeason(in teamRegister);
            bool isSeasonFinished = Committee.FinishSeason(in matchRegister, in teamRegister);
            bool isWorking = true;
            while (isWorking)
            {
                ShowMenu();
                Console.Write("Choose one point of the list: ");
                bool isParsed = Int32.TryParse(Console.ReadLine(), out int choise);
                if (choise <= 6 && choise >= 0 && isParsed)
                {
                    switch (choise)
                    {
                        case 0:
                            isWorking = false;
                            break;
                        case 1:
                            HeadOfFanClub.AddFanClub(ref fanClubs, user);
                            break;
                        case 2:
                            ShowFanClubs(fanClubs);
                            HeadOfFanClub.JoinAFanClub(ref fanClubs, user);
                            break;
                        case 3:
                            if (fanClubs != null)
                                ShowFanClubs(in fanClubs);
                            else
                                Console.WriteLine("\nNo fan club created yet!");
                            break;
                        case 4:
                            if (teamRegister != null)
                                ShowTeamRegister(in teamRegister);
                            else
                                Console.WriteLine("\nTeams register isn't formed!");
                            break;
                        case 5:
                            if (matchRegister != null)
                                ShowMatchRegister(in matchRegister);
                            else
                                Console.WriteLine("\nMatches register isn't formed!");
                            break;
                        case 6:
                            if (isSeasonFinished)
                                ShowResultOfSeason(in matchRegister, in teamRegister);
                            else
                                Console.WriteLine("\nNo season finished yet!");
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
            Console.WriteLine(" Enter 2 - to join fan club");
            Console.WriteLine(" Enter 3 - to show fan clubs");
            Console.WriteLine(" Enter 4 - to show command register");
            Console.WriteLine(" Enter 5 - to show match register");
            Console.WriteLine(" Enter 6 - to show result of season");
            Console.WriteLine("\n========================================\n");
        }
        static void ShowFanClubs(in FanClub[] fanClubs)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Fan clubs:");
            int numberOfFunClubs = fanClubs.Length;
            for (int i = 0; i < numberOfFunClubs; i++)
            {
                Console.WriteLine($"\nFan club: {fanClubs[i].GetNameOfClub()}");
                if (fanClubs[i].FavouritePlayer != null)
                {
                    Console.Write("Favourite player: ");
                    Console.WriteLine(fanClubs[i].FavouritePlayer);
                }
                if (fanClubs[i].FavouriteTeam != null)
                {
                    Console.Write("Favourite team: ");
                    Console.WriteLine(fanClubs[i].FavouriteTeam);
                }
                int numberOfMembers = fanClubs[i].GetNumberOfMembers();
                string[] surnames = fanClubs[i].GetSurnamesOfClub();
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Member {j + 1} : {surnames[j]}");
                }
            }
            Console.WriteLine("\n========================================\n");

        }
        static void ShowTeamRegister(in FootballTeam[] teamRegister)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Register of teams:");
            int numberOfTeams = teamRegister.Length;
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
                Console.Write($"\nMembers of the {j + 1} match: {membersOfTheMatches[0][j]}");
                Console.WriteLine($" vs {membersOfTheMatches[1][j]}");
                Console.WriteLine($"Date of the {j + 1} match: {datesOfTheMatches[j]}");
            }
            Console.WriteLine("\n========================================\n");
        }
        static void ShowResultOfSeason(in Season matchRegister, in FootballTeam[] teamRegister)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Result of matches(number of awards):");
            int numberOfTeams = matchRegister.NumberOfMatches;
            for (int j = 0; j < numberOfTeams; j++)
            {
                Console.Write($"\n{teamRegister[j].GetNameOfTeam()} :");
                Console.WriteLine(teamRegister[j].NumberOfAwards);
            }
            Console.WriteLine("\n========================================\n");
        }
    }
}