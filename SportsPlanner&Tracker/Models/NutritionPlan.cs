namespace SportsPlanner_Tracker.Models
{
        public class NutritionPlan
        {
            public int Id { get; set; }
            public string Name { get; set; } // e.g., "Weight Loss Plan", "Muscle Gain Plan"
            public string Description { get; set; } // Description of the plan
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            // Caloric target for the plan
            public decimal CaloricTarget { get; set; }

            // Relationships
            public int UserId { get; set; }
            public User User { get; set; }

            public int GoalId { get; set; } // Foreign Key for Goal (nutrition-related)
            public Goal Goal { get; set; } // Relationship to Goal
        }
    }

