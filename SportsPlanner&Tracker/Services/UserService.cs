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

        //Nova metoda: prilagođava kalorijske potrebe na osnovu trening cilja
        public int AdjustCaloriesBasedOnTrainingGoal(int caloricNeeds, string selectedTrainingGoal, string selectedNutritionGoal)
        {
            int adjustment = caloricNeeds; // Počinje s osnovnim kalorijama

            // 📌 PRVO primjenjujemo NutritionGoal (deficit/suficit)
            if (selectedNutritionGoal == "WeightLoss")
            {
                adjustment -= 500; // Deficit za mršavljenje
            }
            else if (selectedNutritionGoal == "GainingMuscleMass")
            {
                adjustment += 500; // Suficit za dobivanje mišićne mase
            }

            // 📌 ONDA primjenjujemo TrainingGoal (dodatni kalorijski utjecaj)
            switch (selectedTrainingGoal)
            {
                case "Physical":
                    adjustment += 200;
                    break;
                case "Technical":
                    adjustment += 100;
                    break;
                case "Tactical":
                    adjustment -= 100;
                    break;
            }

            Console.WriteLine($"[DEBUG] Adjusted Calories After Nutrition Goal: {adjustment}");

            return adjustment; // 📌 Vraćamo ispravno izračunate kalorijske potrebe
        }
        public List<string> GenerateWeeklyTrainingPlan(string sport, string selectedTrainingGoal, string selectedNutritionGoal)
        {
            var schedule = new List<string>();

            if (sport == "Fitness")
            {
                // Fitness korisnici imaju push/pull/legs raspored
                schedule.Add("Monday: Push Workout");
                schedule.Add("Tuesday: Pull Workout");
                schedule.Add("Wednesday: Rest");
                schedule.Add("Thursday: Legs Workout");
                schedule.Add("Friday: Full Body Workout");
                schedule.Add("Saturday: Active Recovery (Stretching, Yoga)");
                schedule.Add("Sunday: Rest");
            }
            else
            {
                // Sport-specific trening + teretana
                schedule.Add("Monday: Sport-Specific Training");
                schedule.Add("Tuesday: Gym Workout");
                schedule.Add("Wednesday: Sport-Specific Training");
                schedule.Add("Thursday: Gym Workout");
                schedule.Add("Friday: Sport-Specific Training");
                schedule.Add("Saturday: Match Preparation");
                schedule.Add("Sunday: Rest Day");

                if (selectedTrainingGoal == "Technical")
                {
                    schedule[1] = "Tuesday: Technical Drills";
                    schedule[3] = "Thursday: Technical Drills";
                }
                else if (selectedTrainingGoal == "Tactical")
                {
                    schedule[1] = "Tuesday: Tactical Analysis";
                    schedule[3] = "Thursday: Tactical Game Situations";
                }
                else if (selectedTrainingGoal == "Physical")
                {
                    schedule[1] = "Tuesday: Strength Training";
                    schedule[3] = "Thursday: Endurance Training";
                }
            }

            return schedule;
        }

    }
}
