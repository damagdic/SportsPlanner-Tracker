using Microsoft.AspNetCore.Mvc;
using SportsPlanner_Tracker.Models;
using SportsPlanner_Tracker.ViewModels;

namespace SportsPlanner_Tracker.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserInputVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(UserInputVM model)
        {
            if (ModelState.IsValid)
            {
                // Save the user data and selected goals to the database
                var user = new User
                {
                    FullName = model.FullName,
                    Age = model.Age,
                    Height = model.Height,
                    Weight = model.Weight,
                    Sport = model.SelectedSport,
                    TrainingGoals = model.SelectedTrainingGoal,
                    NutritionGoals = model.SelectedNutritionGoal
                };

                // Assuming _context is the AppDbContext instance
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            // If the model state is not valid, re-render the form with validation errors
            return View(model);
        }
    }
}
