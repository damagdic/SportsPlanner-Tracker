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

        public DbSet<Planner> Planners { get; set; }

        public DbSet<UserProgress> UserProgress { get; set; }
    }
}
