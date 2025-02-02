using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SportsPlanner_Tracker.Data;  
using SportsPlanner_Tracker.Models;
using SportsPlanner_Tracker.Services;
using SportsPlanner_Tracker.ViewModels;

namespace SportsPlanner_Tracker.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;

        // Konstruktor u kojem injektiraš AppDbContext
        public UserController(AppDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
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
                // Izračun BMI-a i kalorijskih potreba pomoću UserService
                double bmi = _userService.CalculateBMI(model.Height, model.Weight);
                int caloricAverage = _userService.CalculateCaloricNeeds(model.Weight, model.Age);
                int caloricNeeds;

                caloricNeeds = _userService.AdjustCaloriesBasedOnTrainingGoal(caloricAverage, model.SelectedTrainingGoal, model.SelectedNutritionGoal);

                Console.WriteLine($"[DEBUG] Caloric Average: {caloricAverage}");
                Console.WriteLine($"[DEBUG] Adjusted Caloric Needs: {caloricNeeds}");
                // Spremanje informacija u bazu
                var user = new User
                {
                    FullName = model.FullName,
                    Age = model.Age,
                    Height = model.Height,
                    Weight = model.Weight,
                    SelectedSport = model.SelectedSport,
                    SelectedTrainingGoal = model.SelectedTrainingGoal,
                    SelectedNutritionGoal = model.SelectedNutritionGoal,
                    Password = model.Password,
                    BMI = Math.Round(bmi, 2),
                    CaloricNeeds = caloricNeeds
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Spremi korisnikov ID u sesiju
                HttpContext.Session.SetInt32("UserId", user.Id);
                Console.WriteLine($"User ID {user.Id} saved to session.");

                return RedirectToAction("UserInfo","User");

            }

           
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

            int caloricAverage = _userService.CalculateCaloricNeeds(user.Weight, user.Age);
            var weeklyPlan = _userService.GenerateWeeklyTrainingPlan(user.SelectedSport, user.SelectedTrainingGoal, user.SelectedNutritionGoal);

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
                CaloricAverage = caloricAverage, 
                Password = user.Password,
                WeeklyTrainingPlan = weeklyPlan
            };
            Console.WriteLine($"User {user.FullName} found and displayed.");

            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM()); // Prikazuje login formu
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // Provjera postoji li korisnik s unesenim FullName i Password
                var user = _context.Users.FirstOrDefault(u => u.FullName == model.FullName && u.Password == model.Password);

                if (user != null)
                {
                    // Ako korisnik postoji, spremi njegov ID u sesiju i preusmjeri na UserInfo
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    Console.WriteLine($"User {user.FullName} successfully logged in.");
                    return RedirectToAction("UserInfo", "User");
                }

                // Ako login ne uspije, prikaži poruku o grešci
                ModelState.AddModelError("", "Invalid Full Name or Password.");
            }

            return View(model);
        }

    }
}
