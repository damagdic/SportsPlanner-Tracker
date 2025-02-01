namespace SportsPlanner_Tracker.Services
{
    public class UserService
    {
        public double CalculateBMI(double height, double weight)
        {
            double heightInMeters = height / 100.0;
            return weight / (heightInMeters * heightInMeters);
        }

        public int CalculateCaloricNeeds(double weight, int age)
        {
            return (int)(10 * weight + 6.25 * 170 - 5 * age + 5);
        }
    }
}
