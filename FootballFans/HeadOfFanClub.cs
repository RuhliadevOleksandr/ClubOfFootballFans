using System;
using FootballFansLib;

namespace FootballFans
{
    internal abstract class HeadOfFanClub
    {
        internal static FanClub CreateFanClub()
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
                    Console.Write("Enter the favourite team: ");
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
                    Console.WriteLine("\nThe number of fan club members must be more than one!");
            }
            return club;
        }
    }
}
