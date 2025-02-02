using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.Models
{
    public class UserProgress
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double BMI { get; set; }

        [Required]
        public int CaloricIntake { get; set; }
    }
}
