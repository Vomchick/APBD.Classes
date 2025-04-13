using Veterinary.Models;

namespace Veterinary.Data
{
    public class VisitsRepository
    {
        public static List<Visit> GetTestVisits()
        {
            var random = new Random();
            var visits = new List<Visit>();
            var descriptions = new[]
            {
                "General check-up",
                "Vaccination",
                "Dental cleaning",
                "Minor surgery",
                "Injury treatment",
                "Parasite control",
                "Follow-up appointment",
                "Nutritional consultation",
                "Behavioral assessment",
                "Routine examination"
            };

            for (int i = 1; i <= 30; i++)
            {
                var animalId = random.Next(1, 21);
                var description = descriptions[random.Next(descriptions.Length)];
                var date = DateTime.Today.AddDays(-random.Next(0, 365));
                var price = Math.Round(random.NextDouble() * 200 + 20, 2);

                visits.Add(new Visit
                {
                    Id = i,
                    AnimalId = animalId,
                    Description = description,
                    DateOfVisit = date,
                    Price = price
                });
            }

            return visits;
        }
    }
}
