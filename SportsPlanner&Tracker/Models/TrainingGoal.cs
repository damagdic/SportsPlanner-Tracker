namespace SportsPlanner_Tracker.Models
{
    public class TrainingGoal
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., Focus on Physical Properties, Technical Skills
        public string Description { get; set; } // Description of the goal

        public List<MotorSkill> MotorSkills { get; set; } // If focusing on physical properties, motor skills
    }
}
