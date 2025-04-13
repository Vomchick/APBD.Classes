using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using Veterinary.Models.Enums;

namespace Veterinary.Contracts.Requests.Animal
{
    public class CreateAnimalRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        //[JsonConverter(typeof(StringEnumConverter))]
        public AnimalCategory Category { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        [StringLength(30)]
        public required string FurColor { get; set; }
    }
}
