namespace FootballFansLib
{
    public class FootballFan : Person, IRootable
    {
        public FootballFan(string surname) : base(surname) {}
        public string FavouritePlayer { get; set; }
        public string FavouriteTeam { get; set; }
    }
}