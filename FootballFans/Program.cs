using System;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    class Program
    {
        static void Main(string[] args)
        {
            FootballFan user = AddUser();
            List<FanClub> fanClubs = DataFromFile.CreateFanClubs("FanClubs.txt");
            List<FootballTeam> teamRegister = DataFromFile.CreateTeams("FootballTeams.txt");
            List<FootballTeam> stageParticipants = new List<FootballTeam>(teamRegister);
            int stage = Committee.GetNumberOfStages(in stageParticipants);
            List<Season> matchRegister = new List<Season>();
            Season stageMatches = Committee.CreateStage(in stageParticipants);
            matchRegister.Add(stageMatches);
            int currentStage = Committee.FinishStage(in stageMatches, in stageParticipants);

            bool isWorking = true;
            while (isWorking)
            {
                ShowMenu();
                Console.Write("Choose one point of the list: ");
                bool isParsed = Int32.TryParse(Console.ReadLine(), out int choise);
                if (choise <= 9 && choise >= 0 && isParsed)
                {
                    switch (choise)
                    {
                        case 0:
                            isWorking = false;
                            break;
                        case 1:
                            Console.WriteLine("\nYou have choosed the 1 point");
                            HeadOfFanClub.AddFanClub(ref fanClubs, user);
                            break;
                        case 2:
                            Console.WriteLine("\nYou have choosed the 2 point");
                            ShowFanClubs(fanClubs);
                            HeadOfFanClub.JoinAFanClub(ref fanClubs, user);
                            break;
                        case 3:
                            Console.WriteLine("\nYou have choosed the 3 point");
                            if (fanClubs != null)
                                ShowFanClubs(in fanClubs);
                            else
                                Console.WriteLine("\nNo fan club created yet!");
                            break;
                        case 4:
                            Console.WriteLine("\nYou have choosed the 4 point");
                            if (teamRegister != null)
                                ShowTeamRegister(in teamRegister);
                            else
                                Console.WriteLine("\nTeams register isn't formed!");
                            break;
                        case 5:
                            Console.WriteLine("\nYou have choosed the 5 point");
                            if (stageMatches != null)
                                ShowMatches(in stageMatches);
                            else
                                Console.WriteLine("\nStage matches isn't formed!");
                            break;
                        case 6:
                            Console.WriteLine("\nYou have choosed the 6 point");
                            if (Convert.ToBoolean(currentStage))
                                ShowResultOfMatches(in stageParticipants);
                            else
                                Console.WriteLine("\nNo stage finished yet!");
                            break;
                        case 7:
                            Console.WriteLine("\nYou have choosed the 7 point");
                            if (currentStage != stage)
                            {
                                Committee.QualifyCommands(ref stageParticipants, stageParticipants.Count / 2);
                                stageMatches = Committee.CreateStage(in stageParticipants);
                                matchRegister.Add(stageMatches);
                                currentStage += Committee.FinishStage(in stageMatches, in stageParticipants);
                                ShowMatches(in stageMatches);
                            }
                            else
                                Console.WriteLine("\nThat was the last stage!");
                            break;
                        case 8:
                            Console.WriteLine("\nYou have choosed the 8 point");
                            if (currentStage == stage)
                                foreach (Season matches in matchRegister)
			                    {
                                    ShowMatches(in matches);
			                    }
                            else
                                Console.WriteLine("\nSeason isn't finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                        case 9:
                            Console.WriteLine("\nYou have choosed the 9 point");
                            if (currentStage == stage)
                            {
                                teamRegister.Sort((team, team2) => team2.NumberOfAwards.CompareTo(team.NumberOfAwards));
                                ShowResultOfMatches(in teamRegister);
                            }
                            else
                                Console.WriteLine("\nSeason isn't finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                    }
                }
                else
                    Console.WriteLine("\nThere isn't such point! Please try again\n");
            }
        }
        static FootballFan AddUser()
        {
            FootballFan user = null;
            bool isCorrectSurname = false;
            while (!isCorrectSurname)
            {
                try
                {
                    Console.Write("\nEnter surname: ");
                    string userName = Console.ReadLine();
                    user = new FootballFan(userName);
                    Console.WriteLine($"\nWelcome, {userName}");
                    isCorrectSurname = true;
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            return user;
        }
        static void ShowMenu()
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine(" Enter 0 - to stop the program");
            Console.WriteLine(" Enter 1 - to create fan club");
            Console.WriteLine(" Enter 2 - to join fan club");
            Console.WriteLine(" Enter 3 - to show fan clubs");
            Console.WriteLine(" Enter 4 - to show team register");
            Console.WriteLine(" Enter 5 - to show matches of current stage");
            Console.WriteLine(" Enter 6 - to show result of current stage");
            Console.WriteLine(" Enter 7 - to show matches of next stage");
            Console.WriteLine(" Enter 8 - to show matches of season");
            Console.WriteLine(" Enter 9 - to show result of season");
            Console.WriteLine("\n========================================\n");
        }
        static void ShowFanClubs(in List<FanClub> fanClubs)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Fan clubs:");
            foreach (var fanClub in fanClubs)
	        {
                Console.WriteLine($"\nFan club: {fanClub.GetNameOfClub()}");
                if (fanClub.FavouritePlayer != null)
                {
                    Console.Write("Favourite player: ");
                    Console.WriteLine(fanClub.FavouritePlayer);
                }
                if (fanClub.FavouriteTeam != null)
                {
                    Console.Write("Favourite team: ");
                    Console.WriteLine(fanClub.FavouriteTeam);
                }
                int numberOfMembers = fanClub.GetNumberOfMembers();
                string[] surnames = fanClub.GetSurnamesOfClub();
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Member {j + 1} : {surnames[j]}");
                }    
            }
            Console.WriteLine("\n========================================\n");

        }
        static void ShowTeamRegister(in List<FootballTeam> commands)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Register of teams:");
            foreach (FootballTeam command in commands)
	        {
                Console.WriteLine($"\n{command.GetNameOfTeam()}:");
                int numberOfMembers = command.GetNumberOfMembers();
                string[] surnames = command.GetSurnamesOfTeam();
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Surname: {surnames[j]}");
                }
        	}
            Console.WriteLine("\n========================================\n");
        }
        static void ShowMatches(in Season matchRegister)
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
        static void ShowResultOfMatches(in List<FootballTeam> commands)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Result of matches(number of awards):");
            foreach (FootballTeam command in commands)
	        {
                Console.Write($"\n{command.GetNameOfTeam()}: ");
                Console.WriteLine(command.NumberOfAwards);
        	}
            Console.WriteLine("\n========================================\n");
        }
    }
}