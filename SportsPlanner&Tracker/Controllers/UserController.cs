using Microsoft.AspNetCore.Mvc;
using SportsPlanner_Tracker.Data;  // Ovdje dodaj namespace gdje je AppDbContext
using SportsPlanner_Tracker.Models;
using SportsPlanner_Tracker.ViewModels;

namespace SportsPlanner_Tracker.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        // Konstruktor u kojem injektiraš AppDbContext
        public UserController(AppDbContext context)
        {
            _context = context;
        }

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
                    SelectedSport = model.SelectedSport,
                    SelectedTrainingGoal = model.SelectedTrainingGoal,
                    SelectedNutritionGoal = model.SelectedNutritionGoal
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            // If the model state is not valid, re-render the form with validation errors
            return View(model);
        }
    }
}
