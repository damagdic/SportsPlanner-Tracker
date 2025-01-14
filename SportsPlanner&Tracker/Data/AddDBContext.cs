using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using SportsPlanner_Tracker.Models;

namespace SportsPlanner_Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<NutritionPlan> NutritionPlans { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<TrainingGoal> TrainingGoals { get; set; }
        public DbSet<NutritionGoal> NutritionGoals { get; set; }
    }
}
