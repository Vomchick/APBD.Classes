using Veterinary.Models.Enums;

namespace Veterinary.Model
{
    public class Animal
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public AnimalCategory Category { get; set; }

        public double Weight { get; set; }

        public required string FurColor { get; set; }
    }
}
