using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.ViewModels
{
    public class UserInputVM
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public double Height { get; set; } // in cm
        public double Weight { get; set; } // in kg

        public decimal? BMI { get; set; } // percentage

        public int? CaloricNeeds { get; set; } // in number

        // Dropdown selections
        public string SelectedSport { get; set; }
        public string SelectedTrainingGoal { get; set; }
        public string SelectedNutritionGoal { get; set; }

        // Dropdown options
        public List<string> Sports { get; set; } = new List<string>
        {
            "Football", "Tennis", "Volleyball", "Basketball", "Handball", "Fitness"
        };

        public List<string> TrainingGoals { get; set; } = new List<string>
        {
            "Physical", "Technical", "Tactical"
        };

        public List<string> NutritionGoals { get; set; } = new List<string>
        {
            "Weight Loss", "Gaining Muscle Mass", "Maintaining Weight"
        };

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int CaloricAverage { get; set; }

        public List<string> WeeklyTrainingPlan { get; set; } = new List<string>();
    }
}

