using System.ComponentModel;

namespace Veterinary.Models.Enums
{
    public enum AnimalCategory
    {
        [Description("Mammal")]
        Mammal,

        [Description("Bird")]
        Bird,

        [Description("Reptile")]
        Reptile,

        [Description("Amphibian")]
        Amphibian,

        [Description("Fish")]
        Fish,

        [Description("Insect")]
        Insect
    }
}
