namespace SportsPlanner_Tracker.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "Weight Loss", "Muscle Gain", etc.
        public string Description { get; set; } // Optional description of the goal
        public GoalType Type { get; set; } // Enum for distinguishing between Training or Nutrition goal
    }

    public enum GoalType
    {
        Training,  // Training related goals (Physical, Technical, etc.)
        Nutrition  // Nutrition related goals (Weight Loss, Gaining Mass, etc.)
    }
}
