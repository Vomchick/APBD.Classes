using Veterinary.Model;
using Veterinary.Models.Enums;

namespace Veterinary.Data
{
    public class AnimalsRepository
    {
        public static List<Animal> GetTestAnimals()
        {
            return new List<Animal>
            {
                new Animal { Id = 1, Name = "Lion", Category = AnimalCategory.Mammal, Weight = 190.5, FurColor = "Golden" },
                new Animal { Id = 2, Name = "Tiger", Category = AnimalCategory.Mammal, Weight = 220.3, FurColor = "Orange with black stripes" },
                new Animal { Id = 3, Name = "Elephant", Category = AnimalCategory.Mammal, Weight = 5400.0, FurColor = "Gray" },
                new Animal { Id = 4, Name = "Penguin", Category = AnimalCategory.Bird, Weight = 23.0, FurColor = "Black and white" },
                new Animal { Id = 5, Name = "Eagle", Category = AnimalCategory.Bird, Weight = 6.5, FurColor = "Brown and white" },
                new Animal { Id = 6, Name = "Cobra", Category = AnimalCategory.Reptile, Weight = 5.2, FurColor = "Black" },
                new Animal { Id = 7, Name = "Frog", Category = AnimalCategory.Amphibian, Weight = 0.4, FurColor = "Green" },
                new Animal { Id = 8, Name = "Shark", Category = AnimalCategory.Fish, Weight = 800.0, FurColor = "Gray" },
                new Animal { Id = 9, Name = "Parrot", Category = AnimalCategory.Bird, Weight = 1.2, FurColor = "Colorful" },
                new Animal { Id = 10, Name = "Bear", Category = AnimalCategory.Mammal, Weight = 300.0, FurColor = "Brown" },
                new Animal { Id = 11, Name = "Rabbit", Category = AnimalCategory.Mammal, Weight = 2.5, FurColor = "White" },
                new Animal { Id = 12, Name = "Crocodile", Category = AnimalCategory.Reptile, Weight = 1000.0, FurColor = "Dark green" },
                new Animal { Id = 13, Name = "Salmon", Category = AnimalCategory.Fish, Weight = 4.3, FurColor = "Silver" },
                new Animal { Id = 14, Name = "Butterfly", Category = AnimalCategory.Insect, Weight = 0.02, FurColor = "Colorful" },
                new Animal { Id = 15, Name = "Giraffe", Category = AnimalCategory.Mammal, Weight = 800.0, FurColor = "Spotted" },
                new Animal { Id = 16, Name = "Ant", Category = AnimalCategory.Insect, Weight = 0.001, FurColor = "Black" },
                new Animal { Id = 17, Name = "Owl", Category = AnimalCategory.Bird, Weight = 3.3, FurColor = "Brown" },
                new Animal { Id = 18, Name = "Zebra", Category = AnimalCategory.Mammal, Weight = 350.0, FurColor = "Black and white" },
                new Animal { Id = 19, Name = "Iguana", Category = AnimalCategory.Reptile, Weight = 5.0, FurColor = "Green" },
                new Animal { Id = 20, Name = "Kangaroo", Category = AnimalCategory.Mammal, Weight = 85.0, FurColor = "Light brown" },
            };
        }
    }
}
