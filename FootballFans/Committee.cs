using System;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    internal static class Committee
    {
        internal static Season CreateStage(in List<FootballTeam> commands)
        {
            List<FootballTeam> copyCommands = new List<FootballTeam>(commands);
            List<DateTime> dateTimes = RandomDateList(commands.Count / 2);
            Match[] stage = new Match[commands.Count / 2];
            for (int i = 0; i < commands.Count / 2; i++)
			{
                stage[i] = CreateMatch(ref copyCommands, dateTimes[i]);
			}
            Season season = new Season(stage);
            return season;
        }
        private static Match CreateMatch(ref List<FootballTeam> copyCommands, DateTime dateTime)
        {
            FootballTeam firstTeam = copyCommands[new Random().Next(copyCommands.Count)];
            copyCommands.Remove(firstTeam);
            FootballTeam secondTeam = copyCommands[new Random().Next(copyCommands.Count)];
            copyCommands.Remove(secondTeam);
            Match match = new Match(firstTeam, secondTeam, dateTime.ToString());
            return match;
        }
        private static List<DateTime> RandomDateList(int length)
        {
            List<int> day = RandomList(length, 1, 29);
            List<int> month = RandomList(length, 1, 13);
            List<DateTime> dateTimes = new List<DateTime>();
            for (int i = 0; i < length; i++)
			{
                dateTimes.Add(new DateTime(2022, month[i], day[i]));
			}
            return dateTimes;
        }
        private static List<int> RandomList(int length, int minValue, int maxValue)
        {
            Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < length; i++)
			{
                list.Add(random.Next(minValue, maxValue));
			}
            return list;
        }
        private static Match.Result FinishMatch(int firstTeamScore, int secondTeamScore)
        {
            if(firstTeamScore > secondTeamScore)
                return Match.Result.Win;
            else
            {
                if (firstTeamScore < secondTeamScore)
                    return Match.Result.Lose;
                else
                    return Match.Result.Draw;
            }
        }
        internal static int FinishStage(in Season matches, in List<FootballTeam> commands)
        {
            Match.Result[] result = new Match.Result[commands.Count / 2];
            List<int> scores = RandomList(commands.Count, 0, 5);
            for (int i = 0; i < result.Length; i++)
			{
                result[i] = FinishMatch(scores[2 * i], scores[2 * i + 1]);
			}
            matches.AddResultOfMatch(commands.ToArray(), result);
            return 1;
        }
        internal static void QualifyCommands(ref List<FootballTeam> commands, int numberOfCommands)
        {
            for (int i = 0; i < numberOfCommands; i++)
		    {
                FootballTeam teamToRemove = commands[0];
                foreach (FootballTeam team in commands)
	            {
                    if(team.NumberOfAwards < teamToRemove.NumberOfAwards)
                    {
                        teamToRemove = team;
                    }
        	    }
                commands.Remove(teamToRemove);
            }
        }
        internal static int GetNumberOfStages(in List<FootballTeam> stageParticipants)
        {
            int stage = 1;
            while (Math.Pow(2, stage) != stageParticipants.Count)
			{
                stage++;
			}
            return stage;
        }
    }
}
