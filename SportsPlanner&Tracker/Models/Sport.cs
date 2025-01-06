namespace SportsPlanner_Tracker.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; } // Npr. Trčanje, Plivanje, Biciklizam
        public string Description { get; set; } // Opis sporta

        // Relations
        public ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
