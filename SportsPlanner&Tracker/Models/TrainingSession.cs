namespace SportsPlanner_Tracker.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // Datum treninga
        public string Description { get; set; } // Opis vježbi i trajanja
        public TimeSpan Duration { get; set; } // Trajanje treninga

        // Relations
        public int TrainingPlanId { get; set; }
        public TrainingPlan TrainingPlan { get; set; }
    }
}
