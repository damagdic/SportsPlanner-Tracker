using System.ComponentModel.DataAnnotations;

namespace SportsPlanner_Tracker.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
