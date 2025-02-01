﻿namespace SportsPlanner_Tracker.ViewModels
{
    public class ResultVM
    {
        // info about user
        public string FullName { get; set; }
        public int Age { get; set; }
        public double Height { get; set; } // in cm
        public double Weight { get; set; } // in kg

        // BMI and caloric needs
        public double BMI { get; set; }
        public int CaloricRequirement { get; set; }

        // chooses of user
        public string SelectedSport { get; set; }
        public string SelectedTrainingGoal { get; set; }
        public string SelectedNutritionGoal { get; set; }

        // training plan
        public List<TrainingSessionVM> TrainingSchedule { get; set; } = new List<TrainingSessionVM>();
    }

    public class TrainingSessionVM
    {
        public string Day { get; set; }
        public string TrainingType { get; set; }
    }
}
