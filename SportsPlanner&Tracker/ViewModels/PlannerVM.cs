using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.ViewModels
{
    public class PlannerVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Training type is required.")]
        public string TrainingType { get; set; }

        [Required(ErrorMessage = "Meal plan is required.")]
        public string MealPlan { get; set; }

        [Required(ErrorMessage = "Please enter notes.")]
        public string Notes { get; set; }
    }
}

