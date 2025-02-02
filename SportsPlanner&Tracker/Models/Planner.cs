using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.Models
{
    public class Planner
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // Relacija s korisnikom

        [Required]
        public DateTime Date { get; set; } // Datum plana

        public string TrainingType { get; set; } // Vrsta treninga (npr. "Gym", "Cardio", "Sport-specific")
        public string MealPlan { get; set; } // Plan prehrane (npr. "High protein, low carb")

        public string Notes { get; set; } 
    }
}
