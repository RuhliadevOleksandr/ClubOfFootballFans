﻿using System;
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
            List<Season> matchRegister = Committee.CreateSeason(in teamRegister);
            int numberOfStages = Committee.GetNumberOfStages(in teamRegister);
            int currentStage = 1;

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
                            if (matchRegister != null)
                                ShowMatches(in matchRegister, currentStage);
                            else
                                Console.WriteLine("\nStage matches isn't formed!");
                            break;
                        case 6:
                            Console.WriteLine("\nYou have choosed the 6 point");
                            if (Convert.ToBoolean(currentStage))
                                ShowResultOfMatches(GetParticipants(in matchRegister, currentStage, teamRegister));
                            else
                                Console.WriteLine("\nNo stage finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                        case 7:
                            Console.WriteLine("\nYou have choosed the 7 point");
                            try 
	                        {
                                List<FootballTeam> participants = GetParticipants(in matchRegister, currentStage, teamRegister);
                                Committee.QualifyCommands(ref participants);
                                matchRegister.Add(Committee.CreateStage(in participants));
                                currentStage++;
                                Season stage = matchRegister[currentStage - 1];
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
                                ShowMatches(in matchRegister, matchRegister.Count + 1);
                            else
                                Console.WriteLine("\nSeason isn't finished yet! Please choose: \"to show matches of next stage\"");
                            break;
                        case 9:
                            Console.WriteLine("\nYou have choosed the 9 point");
                            if (currentStage == numberOfStages)
                                ShowResultOfMatches(in teamRegister);
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
        static List<FootballTeam> GetParticipants(in List<Season> matchRegister, int currentStage, in List<FootballTeam> teamRegister)
        {
            List<FootballTeam> commands = new List<FootballTeam>();
            Season season = matchRegister[currentStage - 1];
            for (int i = 0; i < season.NumberOfMatches; i++)
			{
                Match match = season[i];
                commands.Add(GetFootballTeam(teamRegister, match.MembersOfTheMatch[0]));
                commands.Add(GetFootballTeam(teamRegister, match.MembersOfTheMatch[1]));
            }
            return commands;
        }
        static FootballTeam GetFootballTeam(in List<FootballTeam> commands, string nameOfTeam)
        {
            foreach (FootballTeam team in commands)
	        {
                if(team.GetNameOfTeam() == nameOfTeam)
                    return team;
	        }
            return null;
        }
        static void ShowMatches(in List<Season> matchRegister, int currentStage)
        {
            Console.WriteLine("\n========================================\n");
            if(currentStage < matchRegister.Count + 1)
            {
                Console.WriteLine($"Register of matches of {currentStage} stage:");
                ShowStage(matchRegister[currentStage - 1]);
            }
            else
            {
                int i = 0;
                foreach(Season stage in matchRegister)
                {
                    i++;
                    Console.WriteLine($"\nRegister of matches of {i} stage:");
                    ShowStage(stage);
                }
            }
            Console.WriteLine("\n========================================\n");
        }
        static void ShowStage(in Season season)
        {
            int j = 0;
            for (int i = 0; i < season.NumberOfMatches; i++)
			{
                j++;
                Match match = season[i];
                Console.Write($"\nMembers of the {j} match: {match.MembersOfTheMatch[0]}");
                Console.WriteLine($" vs {match.MembersOfTheMatch[1]}");
                Console.WriteLine($"Date of the {j} match: {match.DateOfTheMatch}");
            }
        }
        static void ShowResultOfMatches(in List<FootballTeam> commands)
        {
            Console.WriteLine("\n========================================\n");
            Console.WriteLine("Result of matches(number of awards):");
            commands.Sort((team, team2) => team2.NumberOfAwards.CompareTo(team.NumberOfAwards));
            foreach (FootballTeam command in commands)
	        {
                Console.Write($"\n{command.GetNameOfTeam()}: ");
                Console.WriteLine(command.NumberOfAwards);
        	}
            Console.WriteLine("\n========================================\n");
        }
    }
}