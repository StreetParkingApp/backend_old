using System.ComponentModel.DataAnnotations;

namespace StreetParking.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "should provide a name")]
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
