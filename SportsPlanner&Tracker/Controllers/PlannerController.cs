using Microsoft.AspNetCore.Mvc;
using SportsPlanner_Tracker.Data;
using SportsPlanner_Tracker.Models;
using SportsPlanner_Tracker.ViewModels;
using System.Linq;

namespace SportsPlanner_Tracker.Controllers
{
    public class PlannerController : Controller
    {
        private readonly AppDbContext _context;

        public PlannerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Dohvati sve planove korisnika
            var plans = _context.Planners
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Date)
                .ToList();

            // Mapiraj modele `Planner` u `PlannerEntryVM`
            var viewModel = plans.Select(p => new PlannerEntryVM
            {
                Id = p.Id,
                Date = p.Date,
                TrainingType = p.TrainingType,
                MealPlan = p.MealPlan,
                Notes = p.Notes
            }).ToList();

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult GetPlanForDate(DateTime date)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var existingPlan = _context.Planners
                .FirstOrDefault(p => p.UserId == userId && p.Date.Date == date.Date);

            if (existingPlan != null)
            {
                return RedirectToAction("Edit", new { id = existingPlan.Id });
            }

            return RedirectToAction("Create", new { date = date.ToString("yyyy-MM-dd") });
        }

        [HttpGet]
        public IActionResult Create(string date)
        {
            var model = new PlannerVM
            {
                Date = DateTime.Parse(date)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PlannerVM model)
        {
            if (ModelState.IsValid)
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "User");
                }

                var plan = new Planner
                {
                    UserId = userId.Value,
                    Date = model.Date,
                    TrainingType = model.TrainingType,
                    MealPlan = model.MealPlan,
                    Notes = model.Notes
                };

                _context.Planners.Add(plan);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var plan = _context.Planners.FirstOrDefault(p => p.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            var model = new PlannerVM
            {
                Id = plan.Id,
                Date = plan.Date,
                TrainingType = plan.TrainingType,
                MealPlan = plan.MealPlan,
                Notes = plan.Notes
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PlannerVM model)
        {
            if (ModelState.IsValid)
            {
                var plan = _context.Planners.FirstOrDefault(p => p.Id == model.Id);
                if (plan == null)
                {
                    return NotFound();
                }

                plan.TrainingType = model.TrainingType;
                plan.MealPlan = model.MealPlan;
                plan.Notes = model.Notes;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
