using System;
using FootballFansLib;

namespace FootballFans
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu(); 
            FanClub fanClub = null;
            FootballTeam[] teamRegister = CaptainsOfFootballTeams.CreateTeams();
            Season matchRegister = Committee.CreateSeason(in teamRegister);
            Committee.FinishSeason(in matchRegister, in teamRegister);
            bool isWorking = true;
            while (isWorking)
            {
                Console.Write("Choose one point of the list: ");
                bool isParsed = Int32.TryParse(Console.ReadLine(), out int choise);
                if(choise <= 5 && choise >= 0 && isParsed)
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
                            if (fanClub != null)
                            {
                                ShowFanClub(in fanClub);
                            }
                            else
                                Console.WriteLine("\nYou haven't create fan club yet!");
                            ShowMenu();
                            break;
                        case 3:
                            ShowTeamRegister(in teamRegister);
                            ShowMenu();
                            break;
                        case 4:
                            ShowMatchRegister(in matchRegister);
                            ShowMenu();
                            break;
                        case 5:
                            ShowResultOfSeason(in matchRegister, in teamRegister);
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
            Console.WriteLine(" Enter 2 - to show fan club");
            Console.WriteLine(" Enter 3 - to show command register");
            Console.WriteLine(" Enter 4 - to show match register");
            Console.WriteLine(" Enter 5 - to show result of season");
            Console.WriteLine("\n========================================\n");
        }
        static void ShowFanClub(in FanClub fanClub)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine($"Fan club: {fanClub.GetNameOfClub()}");
            if (fanClub.FavouritePlayer != null)
                Console.WriteLine(fanClub.FavouritePlayer);
            if (fanClub.FavouriteTeam != null)
                Console.WriteLine(fanClub.FavouriteTeam);
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
