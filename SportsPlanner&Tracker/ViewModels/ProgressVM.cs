using SportsPlanner_Tracker.Models;

namespace SportsPlanner_Tracker.ViewModels
{
    public class ProgressVM
    {
        public List<UserProgress> UserProgress { get; set; }
        public byte[] ChartData { get; set; }
    }
}
