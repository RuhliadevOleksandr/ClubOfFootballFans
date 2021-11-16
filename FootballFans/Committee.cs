using System;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    internal static class Committee
    {
        internal static List<Season> CreateSeason(in List<FootballTeam> commands, Match.Types type)
        {
            List<Season> matchRegister = new List<Season>();
            Season stage = CreateStage(in commands, type);
            matchRegister.Add(stage);
            FinishStage(in stage, in commands);
            return matchRegister;
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
        internal static void QualifyCommands(ref List<FootballTeam> commands)
        {
            if(commands.Count > 2)
            {
                int numberOfCommandsToRemove = commands.Count / 2;
                for (int i = 0; i < numberOfCommandsToRemove; i++)
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
            else
            {
                throw new ArgumentException("\nNumber of commands must be more than 1!");
            }
        }
        internal static Season CreateStage(in List<FootballTeam> commands, Match.Types type)
        {
            List<FootballTeam> copyCommands = new List<FootballTeam>(commands);
            List<DateTime> dateTimes = RandomDateList(commands.Count / 2);
            Match[] stage = new Match[commands.Count / 2];
            for (int i = 0; i < commands.Count / 2; i++)
			{
                stage[i] = CreateMatch(ref copyCommands, dateTimes[i], type);
			}
            Season season = new Season(stage);
            return season;
        }
        internal static void FinishStage(in Season matches, in List<FootballTeam> commands)
        {
            Match.Result[] result = new Match.Result[commands.Count / 2];
            List<int> scores = RandomList(commands.Count, 0, 5);
            for (int i = 0; i < result.Length; i++)
			{
                result[i] = FinishMatch(scores[2 * i], scores[2 * i + 1]);
			}
            matches.AddResultOfMatch(result);
        }
        private static Match CreateMatch(ref List<FootballTeam> copyCommands, DateTime dateTime, Match.Types type)
        {
            FootballTeam firstTeam = copyCommands[new Random().Next(copyCommands.Count)];
            copyCommands.Remove(firstTeam);
            FootballTeam secondTeam = copyCommands[new Random().Next(copyCommands.Count)];
            copyCommands.Remove(secondTeam);
            Match match = new Match(firstTeam, secondTeam, dateTime, type);
            return match;
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
    }
}