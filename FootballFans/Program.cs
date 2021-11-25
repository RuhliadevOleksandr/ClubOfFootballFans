using System;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isGetData = DataFromDB.GetData(out List<FanClub> fanClubs, out List<FootballTeam> teamRegister, out List<Season> seasons);
            FootballFan user = AddUser();
            Match.Types type = Match.Types.ChampionsLeague;
            Season matchRegister = Committee.CreateSeason(in teamRegister, type);
            int numberOfStages = Committee.GetNumberOfStages(in teamRegister);
            int currentStage = 1;

            bool isWorking = true;
            while (isWorking)
            {
                ShowMenu();
                Console.Write("Choose one point of the list: ");
                bool isParsed = Int32.TryParse(Console.ReadLine(), out int choise);
                if (choise <= 11 && choise >= 0 && isParsed)
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
                            if (isGetData)
                                ShowFanClubs(in fanClubs);
                            else
                                Console.WriteLine("\nNo fan club created yet!");
                            break;
                        case 4:
                            Console.WriteLine("\nYou have choosed the 4 point");
                            if (isGetData)
                                ShowTeamRegister(in teamRegister);
                            else
                                Console.WriteLine("\nTeams register isn't formed!");
                            break;
                        case 5:
                            Console.WriteLine("\nYou have choosed the 5 point");
                            if (matchRegister != null)
                                ShowMatches(in matchRegister, currentStage);
                            else
                                Console.WriteLine("\nStage matches isn't formed!");
                            break;
                        case 6:
                            Console.WriteLine("\nYou have choosed the 6 point");
                            if (Convert.ToBoolean(currentStage))
                                ShowResultOfMatches(in matchRegister, currentStage);
                            else
                                Console.WriteLine("\nNo stage finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                        case 7:
                            Console.WriteLine("\nYou have choosed the 7 point");
                            try 
	                        {
                                List<FootballTeam> participants = GetParticipants(in matchRegister, currentStage);
                                Stage currentMatches = matchRegister[currentStage - 1];
                                Committee.QualifyCommands(ref participants, in currentMatches);
                                matchRegister.AddStage(Committee.CreateStage(in participants, type));
                                currentStage++;
                                Stage stage = matchRegister[currentStage - 1];
                                Committee.FinishStage(in stage, in participants);
                                ShowMatches(in matchRegister, currentStage);
                            }
	                        catch (ArgumentException)
	                        {
                                Console.WriteLine("\nThat was the last stage!");
	                        }
                            break;
                        case 8:
                            Console.WriteLine("\nYou have choosed the 8 point");
                            if (currentStage == numberOfStages) 
                                ShowMatches(in matchRegister, matchRegister.NumberOfStages + 1);
                            else
                                Console.WriteLine("\nSeason isn't finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                        case 9:
                            Console.WriteLine("\nYou have choosed the 9 point");
                            if (currentStage == numberOfStages)
                                ShowResultOfMatches(in matchRegister, currentStage + 1);
                            else
                                Console.WriteLine("\nSeason isn't finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                        case 10:
                            Console.WriteLine("\nYou have choosed the 10 point");
                            if (isGetData)
                            {
                                foreach (Season season in seasons)
                                {
                                    ShowMatches(in season, season.NumberOfStages + 1);
                                }
                            }
                            else
                                Console.WriteLine("\nRecent seasons aresn't formed!");
                            break;
                        case 11:
                            Console.WriteLine("\nYou have choosed the 11 point");
                            if (isGetData)
                            {
                                foreach (Season season in seasons)
                                {
                                    ShowResultOfMatches(in season, season.NumberOfStages + 1);
                                }
                            }
                            else
                                Console.WriteLine("\nRecent results aren't formed!");
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
            Console.WriteLine(" Enter 10 - to show recent matches of seasons");
            Console.WriteLine(" Enter 11 - to show recent result of seasons");
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
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Member {j + 1} : {fanClub[j].Surname}");
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
                for (int j = 0; j < numberOfMembers; j++)
                {
                    Console.WriteLine($"Surname: {command[j].Surname}");
                }
        	}
            Console.WriteLine("\n========================================\n");
        }
        static List<FootballTeam> GetParticipants(in Season matchRegister, int currentStage)
        {
            List<FootballTeam> commands = new List<FootballTeam>();
            Stage stage = matchRegister[currentStage - 1];
            for (int i = 0; i < stage.NumberOfMatches; i++)
			{
                (FootballTeam firstTeam, FootballTeam secondTeam) = stage[i].MembersOfTheMatch;
                commands.Add(firstTeam);
                commands.Add(secondTeam);
            }
            return commands;
        }
        static void ShowMatches(in Season matchRegister, int currentStage)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine($"Register of {matchRegister.NameOfSeason}!");
            if(currentStage < matchRegister.NumberOfStages + 1)
            {
                Console.WriteLine($"\nRegister of matches of {currentStage} stage:");
                ShowStage(matchRegister[currentStage - 1]);
            }
            else
            {
                for (int i = 0; i < matchRegister.NumberOfStages; i++)
                {
                    Console.WriteLine($"\nRegister of matches of {i + 1} stage:");
                    ShowStage(matchRegister[i]);
                }
            }
            Console.WriteLine("\n========================================\n");
        }
        static void ShowStage(in Stage stage, bool showResult = false)
        {
            for (int i = 0; i < stage.NumberOfMatches; i++)
            {
                (FootballTeam firstTeam, FootballTeam secondTeam) = stage[i].MembersOfTheMatch;
                Console.Write($"\nMembers of the {i + 1} match: {firstTeam.GetNameOfTeam()}");
                Console.WriteLine($" vs {secondTeam.GetNameOfTeam()}");
                Console.WriteLine($"Date of the {i + 1} match: {stage[i].DateOfTheMatch}");
                if (showResult)
                {
                    (int firstScore, int secondScore) = stage[i].ResultOfTheMatch;
                    Console.WriteLine($"Result of the {i + 1} match: ({firstScore} : {secondScore})");
                }
            }
        }
        static void ShowResultOfMatches(in Season matchRegister, int currentStage)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine($"Result of {matchRegister.NameOfSeason}:");
            if (currentStage < matchRegister.NumberOfStages + 1)
            {
                Console.WriteLine($"\nResult of matches of {currentStage} stage:");
                ShowStage(matchRegister[currentStage - 1], true);
            }
            else
            {
                for (int i = 0; i < matchRegister.NumberOfStages; i++)
                {
                    Console.WriteLine($"\nResult of matches of {i + 1} stage:");
                    ShowStage(matchRegister[i], true);
                }
            }
            Console.WriteLine("\n========================================\n");
        }
    }
}