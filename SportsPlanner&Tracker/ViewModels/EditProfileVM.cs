using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.ViewModels
{
    public class EditProfileVM
    {
        public string FullName { get; set; }

        [Required]
        [Range(10, 100)]
        public int Age { get; set; }

        [Required]
        [Range(100, 250)]
        public double Height { get; set; }

        [Required]
        [Range(30, 200)]
        public double Weight { get; set; }

        [Required]
        public string SelectedSport { get; set; }

        [Required]
        public string SelectedTrainingGoal { get; set; }

        [Required]
        public string SelectedNutritionGoal { get; set; }

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
            "WeightLoss", "GainingMuscleMass", "Maintaining Weight"
        };
    }
}
