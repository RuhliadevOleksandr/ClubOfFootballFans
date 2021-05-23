using FootballFansLib;

namespace FootballFans
{

    internal static class Committee
    {
        internal static Season CreateSeason(in FootballTeam[] commands)
        {
            Match[] matches = new Match[3];
            matches[0] = CreateMatch(Match.Types.ChampionsLeague, in commands);
            matches[1] = CreateMatch(Match.Types.EuropaLeague, in commands);
            matches[2] = CreateMatch(Match.Types.EURO, in commands);
            Season season = new Season(matches);
            return season;
        }
        internal static Match CreateMatch(Match.Types type, in FootballTeam[] commands)
        {
            Match match = null;
            switch (type)
            {
                case Match.Types.ChampionsLeague:
                    match = new Match(commands[0], commands[1], "29.05.2021");
                    break;
                case Match.Types.EuropaLeague:
                    match = new Match(commands[1], commands[2], "26.05.2021");
                    break;
                case Match.Types.EURO:
                    match = new Match(commands[2], commands[0], "11.05.2021");
                    break;
            }
            return match;
        }
        internal static bool FinishSeason(in Season matchRegister, in FootballTeam[] teamRegister)
        {
            Match.Result[] result = new Match.Result[3];
            result[0] = Match.Result.Win;
            result[1] = Match.Result.Win;
            result[2] = Match.Result.Draw;
            matchRegister.AddResultOfMatch(teamRegister, result);
            return true;
        }
    }
}
