using System;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    internal static class HeadOfFanClub
    {
        private static FanClub CreateFanClub()
        {
            bool isParsed = false;
            FanClub club = null;
            while (!isParsed)
            {
                Console.Write("\nEnter the number of fan club members: ");
                isParsed = Int32.TryParse(Console.ReadLine(), out int numberOfMembers);
                if (isParsed && numberOfMembers > 1)
                {
                    try
                    {
                        List<FootballFan> footballFans = new List<FootballFan>();
                        for (int i = 0; i < numberOfMembers; i++)
                        {
                            Console.Write($"\nEnter the surname of {i + 1} fan: ");
                            string surname = Console.ReadLine();
                            footballFans.Add(new FootballFan(surname));
                        }
                        Console.Write("\nEnter the name of fan club: ");
                        club = new FanClub(footballFans.ToArray(), Console.ReadLine());
                        Console.Write("\nEnter the favourite player: ");
                        club.FavouritePlayer = Console.ReadLine();
                        Console.Write("\nEnter the favourite team: ");
                        club.FavouriteTeam = Console.ReadLine();
                    }
                    catch (SystemException exception)
                    {
                        Console.WriteLine(exception.Message); ;
                        isParsed = false;
                    }
                    catch(Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        try
                        {
                            Console.Write("\nEnter the favourite team: ");
                            club.FavouriteTeam = Console.ReadLine();
                        }
                        catch (SystemException exception2)
                        {
                            Console.WriteLine(exception2.Message); ;
                            isParsed = false;
                        }
                        catch (Exception exception2)
                        {
                            Console.WriteLine(exception2.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nThe number of fan club members must be integer and more than one!");
                    isParsed = false;
                }
            }
            return club;
        }
        internal static void AddFanClub(ref List<FanClub> fanClubs, FootballFan footballFan)
        {
            if (fanClubs != null)
            {
                fanClubs.Add(CreateFanClub()); 
                CheckIfMemberInOther(ref fanClubs, footballFan);
                fanClubs[fanClubs.Count - 1].AddFan(footballFan);
                Console.WriteLine($"\nYou have join the {fanClubs[fanClubs.Count - 1].GetNameOfClub()}");
            }
            else 
            {
                fanClubs = new List<FanClub>();
                fanClubs.Add(CreateFanClub());
                CheckIfMemberInOther(ref fanClubs, footballFan);
                fanClubs[0].AddFan(footballFan); 
                Console.WriteLine($"\nYou have join the {fanClubs[0].GetNameOfClub()}");
            }
        }
        private static void CheckIfMemberInOther(ref List<FanClub> fanClubs, FootballFan footballFan)
        {
            foreach (FanClub fanClub in fanClubs)
	        {
                int numberOfMembers = fanClub.GetNumberOfMembers();
                for (int i = 0; i < numberOfMembers; i++)
                {
                    if (fanClub[i].Surname == footballFan.Surname)
                    {
                        fanClub.RemoveFan(footballFan);
                        Console.WriteLine($"\nYou have left the {fanClub.GetNameOfClub()}");
                    }
                }
	        }
        }
        internal static void JoinAFanClub(ref List<FanClub> fanClubs, FootballFan footballFan)
        {
            if (fanClubs != null)
            {
                bool isChoose = false;
                CheckIfMemberInOther(ref fanClubs, footballFan);
                while (!isChoose)
                {
                    Console.Write("\nEnter name one of the club you wish to join: ");
                    string nameOfTheClub = Console.ReadLine();
                    foreach (FanClub fanClub in fanClubs)
	                {
                        if (fanClub.GetNameOfClub() == nameOfTheClub)
                        {
                            fanClub.AddFan(footballFan);
                            Console.WriteLine($"\nYou have join the {nameOfTheClub}");
                            isChoose = true;
                        }
	                }
                    if (!isChoose)
                        Console.WriteLine("\nThere is no such name in the list!");
                }
            }
            else
                Console.WriteLine("\nNo fan club created yet!");
        }
    }
}