namespace SportsPlanner_Tracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Hashed password for security
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Goal { get; set; } // Npr. mršavljenje, povećanje mišićne mase
        public int Age { get; set; }

        public string SelectedSport { get; set; }           // Odabrani sport kao string
        public string SelectedTrainingGoal { get; set; }    // Odabrani cilj treninga kao string
        public string SelectedNutritionGoal { get; set; }   // Odabrani cilj prehrane kao string

        public ICollection<TrainingPlan> TrainingPlans { get; set; }
        public ICollection<NutritionPlan> NutritionPlans { get; set; }
    }
}
