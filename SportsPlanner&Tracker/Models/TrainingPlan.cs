namespace SportsPlanner_Tracker.Models
{
    public class TrainingPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "Strength Training", "Endurance Training"
        public string Description { get; set; } // Plan description
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relationships
        public int UserId { get; set; }
        public User User { get; set; }

        public int GoalId { get; set; } // Foreign Key for Goal (training-related)
        public Goal Goal { get; set; } // Relationship to Goal

        // Training sessions related to this plan
        public ICollection<TrainingSession> TrainingSessions { get; set; }
    }
}
