using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class Season
    {
        public string NameOfSeason { private set; get; }
        public int NumberOfStages { get { return season.Count; } }
        public Stage this[int index]
        {
            get
            {
                if (index >= 0 && index < NumberOfStages)
                    return season[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of stages!");
            }
        }
        private List<Stage> season;
        public Season(List<Stage> stages, string nameOfSeason)
        {
            if (stages != null && !string.IsNullOrEmpty(nameOfSeason))
            {
                season = new List<Stage>(stages);
                NameOfSeason = nameOfSeason;
            }
            else
                throw new NullReferenceException("\nSeason was not created! You can't create a season from nothing! And name of season must have letters!");
        }
        public void AddStage(Stage stage) 
        {
            if (stage != null && stage.NumberOfMatches > 0)
                season.Add(stage);
            else
                throw new ArgumentNullException("\nStage was not added! Stage must have matches!");
        }
    }
}
