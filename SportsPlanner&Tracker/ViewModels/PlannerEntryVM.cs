namespace SportsPlanner_Tracker.ViewModels
{
    public class PlannerEntryVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TrainingType { get; set; }
        public string MealPlan { get; set; }
        public string Notes { get; set; }
    }
}
