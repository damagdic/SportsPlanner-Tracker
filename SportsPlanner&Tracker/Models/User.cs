using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }

        public string SelectedSport { get; set; }           // Odabrani sport kao string
        public string SelectedTrainingGoal { get; set; }    // Odabrani cilj treninga kao string
        public string SelectedNutritionGoal { get; set; }   // Odabrani cilj prehrane kao string

        public ICollection<TrainingPlan> TrainingPlans { get; set; }
        public ICollection<NutritionPlan> NutritionPlans { get; set; }
        public int CaloricNeeds { get; internal set; }
        public double BMI { get; internal set; }
        
        [Required]
        public string Password { get; set; }
    }
}
