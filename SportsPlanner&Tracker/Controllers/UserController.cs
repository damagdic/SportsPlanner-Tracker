using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
                    SelectedNutritionGoal = model.SelectedNutritionGoal,
                    Password = model.Password
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // 📌 Spremi korisnikov ID u sesiju
                HttpContext.Session.SetInt32("UserId", user.Id);
                Console.WriteLine($"User ID {user.Id} saved to session.");

                return RedirectToAction("UserInfo","User");

            }

           

            // If the model state is not valid, re-render the form with validation errors
            return View(model);
        }
        public IActionResult UserInfo()
        {
            // 📌 Dohvati ID korisnika iz sesije
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                Console.WriteLine("Session expired, clearing session and redirecting to Create.");
                HttpContext.Session.Clear(); // Brišemo prethodne podatke
                return RedirectToAction("Create"); // Ako nema korisnika, vrati na formu
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                Console.WriteLine("User not found, clearing session and redirecting to Create.");
                HttpContext.Session.Clear();
                return RedirectToAction("Create"); // Ako korisnik ne postoji, vrati na formu
            }

            // Mapiranje User modela na UserInputVM kako bi se prikazali podaci u View-u
            var model = new UserInputVM
            {
                FullName = user.FullName,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                SelectedSport = user.SelectedSport,
                SelectedTrainingGoal = user.SelectedTrainingGoal,
                SelectedNutritionGoal = user.SelectedNutritionGoal,
                BMI = (decimal?)user.BMI,
                CaloricNeeds = user.CaloricNeeds,
                Password = user.Password
            };
            Console.WriteLine($"User {user.FullName} found and displayed.");

            return View(model);
        }
    }
}
