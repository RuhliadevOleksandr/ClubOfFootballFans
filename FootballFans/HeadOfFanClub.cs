using System;
using FootballFansLib;

namespace FootballFans
{
    internal abstract class HeadOfFanClub
    {
        internal static FanClub[] CreateFanClubs()
        {
            FanClub[] clubs = new FanClub[3];

            FootballFan[] club = new FootballFan[3];
            club[0] = new FootballFan("Patterson");
            club[1] = new FootballFan("Cortez");
            club[2] = new FootballFan("Glass");
            clubs[0] = new FanClub(club, "Barcelona fans");
            clubs[0].FavouriteTeam = "Barcelona";
            clubs[0].FavouritePlayer = "Messi";

            FootballFan[] club2 = new FootballFan[3];
            club2[0] = new FootballFan("Holland");
            club2[1] = new FootballFan("Oconnell");
            club2[2] = new FootballFan("Mclean");
            clubs[1] = new FanClub(club2, "Chelsea fans");
            clubs[1].FavouriteTeam = "Chelsea";
            clubs[1].FavouritePlayer = "Verner";

            FootballFan[] club3 = new FootballFan[3];
            club3[0] = new FootballFan("Page");
            club3[1] = new FootballFan("Henderson");
            club3[2] = new FootballFan("Dalton");
            clubs[2] = new FanClub(club3, "Liverpool fans");
            clubs[2].FavouriteTeam = "Liverpool";
            clubs[2].FavouritePlayer = "Milner";

            return clubs;
        }
        private static FanClub CreateFanClub()
        {
            Console.Write("\nEnter the name of fan club: ");
            string nameOfFanClub = Console.ReadLine();
            bool isParsed = false;
            FanClub club = null;
            while (!isParsed)
            {
                Console.Write("\nEnter the number of fan club members: ");
                isParsed = Int32.TryParse(Console.ReadLine(), out int numberOfMembers);
                if (isParsed && numberOfMembers > 1)
                {
                    Console.Write("\nEnter the favourite player: ");
                    string favouritePlayer = Console.ReadLine();
                    Console.Write("\nEnter the favourite team: ");
                    string favouriteTeam = Console.ReadLine();
                    FootballFan[] footballFans = new FootballFan[numberOfMembers];
                    for (int i = 0; i < numberOfMembers; i++)
                    {
                        Console.Write($"\nEnter the surname of {i + 1} fan: ");
                        string surname = Console.ReadLine();
                        footballFans[i] = new FootballFan(surname);
                    }
                    club = new FanClub(footballFans, nameOfFanClub);
                    club.FavouritePlayer = favouritePlayer;
                    club.FavouriteTeam = favouriteTeam;
                }
                else
                    Console.WriteLine("\nThe number of fan club members must be integer and more than one!");
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
                CheckMemberInOther(ref fanClubs, footballFan);
                fanClubs[numberOfFanClubs - 1].AddFan(footballFan);
                Console.WriteLine($"\nYou have join the {fanClubs[numberOfFanClubs - 1].GetNameOfClub()}");
            }
            else 
            {
                fanClubs = new FanClub[1];
                fanClubs[0] = CreateFanClub();
                CheckMemberInOther(ref fanClubs, footballFan);
                fanClubs[0].AddFan(footballFan); 
                Console.WriteLine($"\nYou have join the {fanClubs[0].GetNameOfClub()}");
            }
        }
        private static void CheckMemberInOther(ref FanClub[] fanClubs, FootballFan footballFan)
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
                CheckMemberInOther(ref fanClubs, footballFan);
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