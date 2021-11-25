using System;
using FootballFansLib;
using System.Collections.Generic;

namespace FootballFans
{
    internal static class Committee
    {
        internal static Season CreateSeason(in List<FootballTeam> commands, Match.Types type)
        {
            List<Stage> matchRegister = new List<Stage>();
            Stage stage = CreateStage(in commands, type);
            matchRegister.Add(stage);
            FinishStage(in stage, in commands);
            return new Season(matchRegister, type.ToString() + " 2022");
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
        internal static void QualifyCommands(ref List<FootballTeam> commands, in Stage seasons)
        {
            if (commands.Count > 2)
            {
                int numberOfCommandsToRemove = commands.Count / 2;
                int[] indexToRemove = new int[numberOfCommandsToRemove];
                List<int> numberOfAwards = NumberOfAwards(in commands, in seasons);
                for (int i = 0; i < numberOfCommandsToRemove; i++)
                {
                    if (numberOfAwards[2 * i] > numberOfAwards[2 * i + 1])
                        indexToRemove[i] = 2 * i + 1;
                    else
                        indexToRemove[i] = 2 * i;
                }
                for (int i = numberOfCommandsToRemove - 1; i >= 0; i--)
                    commands.RemoveAt(indexToRemove[i]);
            }
            else
            {
                throw new ArgumentException("\nNumber of commands must be more than 1!");
            }
        }
        internal static Stage CreateStage(in List<FootballTeam> commands, Match.Types type)
        {
            List<FootballTeam> copyCommands = new List<FootballTeam>(commands);
            List<DateTime> dateTimes = RandomDateList(commands.Count / 2);
            List<Match> matches = new List<Match>();
            for (int i = 0; i < commands.Count / 2; i++)
                matches.Add(CreateMatch(ref copyCommands, dateTimes[i], type));
            return new Stage(matches);
        }
        internal static void FinishStage(in Stage matches, in List<FootballTeam> commands)
        {
            List<int> scores = RandomList(commands.Count, 0, 5);
            for (int i = 0; i < commands.Count / 2; i++)
                matches[i].ResultOfTheMatch = (scores[2 * i], scores[2 * i + 1]);
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
        private static List<int> NumberOfAwards(in List<FootballTeam> commands, in Stage stage)
        {
            List<int> numberOfAwards = new List<int>();
            for (int i = 0; i < commands.Count; i++)
                numberOfAwards.Add(0);
            for (int i = 0; i < stage.NumberOfMatches; i++)
            {
                (int firstScore, int secondScore) = stage[i].ResultOfTheMatch;
                (FootballTeam firstTeam, FootballTeam secondTeam) = stage[i].MembersOfTheMatch;
                if (firstScore > secondScore)
                    numberOfAwards[commands.IndexOf(firstTeam)] += 2;
                else
                {
                    if (firstScore < secondScore)
                        numberOfAwards[commands.IndexOf(secondTeam)] += 2;
                    else
                    {
                        numberOfAwards[commands.IndexOf(firstTeam)] += 1;
                        numberOfAwards[commands.IndexOf(secondTeam)] += 1;
                    }
                }
            }
            return numberOfAwards;
        }
    }
}