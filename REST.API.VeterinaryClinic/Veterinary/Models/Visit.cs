using Veterinary.Model;

namespace Veterinary.Models
{
    public class Visit
    {
        public int Id { get; set; }

        public DateTime DateOfVisit { get; set; }

        public required string Description { get; set; }

        public double Price { get; set; }

        public int AnimalId { get; set; }
    }
}
