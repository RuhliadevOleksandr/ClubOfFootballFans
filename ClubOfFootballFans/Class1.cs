using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballFansLib
{
    public abstract class Person
    {
        protected string _surname;
    }

    public class FootballPlayer : Person, IRootable
    {
        public int NumberOfAwards { private set; get; }
        private int _experience;
        public FootballPlayer(string surname)
        {
            if (surname != null)
            {
                _surname = surname;
                _experience = 0;
            }
        }
        public FootballPlayer(string surname, int experience)
        {
            if(surname != null)
            {
                _surname = surname;
                if (experience >= 0)
                    _experience = experience;
            }
        }
        void IRootable.Root()
        {
            Console.WriteLine("Hooray!");
        }
    }

    public class FootballFan : Person, IRootable
    {
        public FootballPlayer FavouritePlayer { set; get; }
        public FootballTeam FavouriteTeam { set; get; }
        public FootballFan(string surname)
        {
            if (surname != null)
            {
                _surname = surname;
            }
        }
        void IRootable.Root()
        {
            Console.WriteLine("Huray!");
        }
    }

    public interface IRootable
    {
        void Root();
    }

    public class Group
    {
        private int _numberOfMembers;
        private string _nameOfGroup;
        Person[] group;
        public Group(Person[] people, string nameOfGroup)
        {
            if (people != null)
            {
                _numberOfMembers = people.Length;
                for (int i = 0; i < _numberOfMembers; i++)
                {
                    group[i] = people[i];
                }
                if (nameOfGroup != null)
                    _nameOfGroup = nameOfGroup;
                else
                    _nameOfGroup = "NoName";
            }
        }
    }
    public class FootballTeam
    {
        public int NumberOfAwards { private set; get; }
        Group team;
        public FootballTeam(FootballPlayer[] footballPlayers, string nameFootballTeam)
        {
            team = new Group(footballPlayers, nameFootballTeam);
        }
    }
    public class FanClub
    {
        private string _meetingPlace;
        Group club;
        public FanClub(FootballFan[] footballFans, string nameFanClub)
        {
            club = new Group(footballFans, nameFanClub);
        }
    }

    public class Match
    {
        public enum types { ChampionsLeague, EuropaLeague, EURO }
        public string DateOfTheMatch { private set; get; }
        public enum result { Win, Draw, Lose }
    }

    public class Season
    {
        private int _numberOfMatches;
        Match[] season;
        public Season(Match[] matches)
        {
            if (matches != null)
            {
                _numberOfMatches = matches.Length;
                for (int i = 0; i < _numberOfMatches; i++)
                {
                    season[i] = matches[i];
                }
            }
        }

    }
}
