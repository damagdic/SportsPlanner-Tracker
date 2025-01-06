namespace SportsPlanner_Tracker.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; } // Npr. Doručak, Ručak, Večera
        public int Calories { get; set; } // Broj kalorija u obroku
        public string Description { get; set; } // Opis obroka

        // Relations
        public int NutritionPlanId { get; set; }
        public NutritionPlan NutritionPlan { get; set; }
    }
}
