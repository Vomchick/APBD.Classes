using System.ComponentModel.DataAnnotations;

namespace Veterinary.Contracts.Requests.Visit
{
    public class CreateVisitRequest
    {
        [Required]
        public DateTime DateOfVisit { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }
    }
}
