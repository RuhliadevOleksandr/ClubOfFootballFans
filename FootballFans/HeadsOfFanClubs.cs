using System;
using FootballFansLib;

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
                        FootballFan[] footballFans = new FootballFan[numberOfMembers];
                        for (int i = 0; i < numberOfMembers; i++)
                        {
                            Console.Write($"\nEnter the surname of {i + 1} fan: ");
                            string surname = Console.ReadLine();
                            footballFans[i] = new FootballFan(surname);
                        }
                        Console.Write("\nEnter the name of fan club: ");
                        club = new FanClub(footballFans, Console.ReadLine());
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
        internal static void AddFanClub(ref FanClub[] fanClubs, FootballFan footballFan)
        {
            if (fanClubs != null)
            {
                int numberOfFanClubs = fanClubs.Length;
                FanClub[] oldFanclubs = new FanClub[numberOfFanClubs];
                for (int i = 0; i < numberOfFanClubs; i++)
                {
                    oldFanclubs[i] = fanClubs[i];
                }
                numberOfFanClubs++;
                fanClubs = new FanClub[numberOfFanClubs];
                for (int i = 0; i < numberOfFanClubs - 1; i++)
                {
                    fanClubs[i] = oldFanclubs[i];
                }
                fanClubs[numberOfFanClubs - 1] = CreateFanClub(); 
                CheckIfMemberInOther(ref fanClubs, footballFan);
                fanClubs[numberOfFanClubs - 1].AddFan(footballFan);
                Console.WriteLine($"\nYou have join the {fanClubs[numberOfFanClubs - 1].GetNameOfClub()}");
            }
            else 
            {
                fanClubs = new FanClub[1];
                fanClubs[0] = CreateFanClub();
                CheckIfMemberInOther(ref fanClubs, footballFan);
                fanClubs[0].AddFan(footballFan); 
                Console.WriteLine($"\nYou have join the {fanClubs[0].GetNameOfClub()}");
            }
        }
        private static void CheckIfMemberInOther(ref FanClub[] fanClubs, FootballFan footballFan)
        {
            for (int i = 0; i < fanClubs.Length; i++)
            {
                string[] surnames = fanClubs[i].GetSurnamesOfClub();
                int numberOfMembers = fanClubs[i].GetNumberOfMembers();
                for (int j = 0; j < numberOfMembers; j++)
                {
                    if (surnames[j] == footballFan.Surname)
                    {
                        fanClubs[i].RemoveFan(footballFan);
                        Console.WriteLine($"\nYou have left the {fanClubs[i].GetNameOfClub()}");
                    }
                }
            }
        }
        internal static void JoinAFanClub(ref FanClub[] fanClubs, FootballFan footballFan)
        {
            if (fanClubs != null)
            {
                bool isChoose = false;
                CheckIfMemberInOther(ref fanClubs, footballFan);
                while (!isChoose)
                {
                    Console.Write("\nEnter name one of the club you wish to join: ");
                    string nameOfTheClub = Console.ReadLine();
                    for (int i = 0; i < fanClubs.Length; i++)
                    {
                        if (fanClubs[i].GetNameOfClub() == nameOfTheClub)
                        {
                            fanClubs[i].AddFan(footballFan);
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