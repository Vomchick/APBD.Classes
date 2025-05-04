using System.ComponentModel.DataAnnotations;

namespace Veterinary.Contracts.Requests.Animal
{
    public class UpdateAnimalRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Weight { get; set; }
    }
}
