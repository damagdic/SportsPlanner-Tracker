using Microsoft.AspNetCore.Mvc;
using SportsPlanner_Tracker.Data;
using SportsPlanner_Tracker.Models;
using SportsPlanner_Tracker.Services;
using SportsPlanner_Tracker.ViewModels;

namespace SportsPlanner_Tracker.Controllers
{
    public class TrackerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ProgressChartService _chartService;

        public TrackerController(AppDbContext context, ProgressChartService chartService)
        {
            _context = context;
            _chartService = chartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var progress = _context.UserProgress
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Date)
                .ToList();

            var model = new ProgressVM
            {
                UserProgress = progress,
                ChartData = _chartService.GenerateProgressChart(progress)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProgress()
        {
            return View(new UserProgress());
        }

        [HttpPost]
        public IActionResult AddProgress(UserProgress model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "User");
            }

            // Dohvati korisnika iz baze da koristi ažurirane podatke
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Login", "User");
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($" {error.ErrorMessage}");
                }
                TempData["ErrorMessage"] = "Please fill in all required fields correctly.";
                return View(model);
            }

            // Koristi visinu korisnika iz baze
            model.UserId = userId.Value;
            model.BMI = model.Weight / ((user.Height / 100.0) * (user.Height / 100.0)); // Ažurirana visina iz baze

            _context.UserProgress.Add(model);
            int savedChanges = _context.SaveChanges();

            if (savedChanges > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while saving your progress.";
                return View(model);
            }
        }
    }
}
