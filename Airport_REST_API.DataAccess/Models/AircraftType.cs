using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.DataAccess.Models
{
    public class AircraftType
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 3)]
        public string Model { get; set; }

        [Required]
        public int CountOfSeats { get; set; }

        [Required]
        public int CarryingCapacity { get; set; }
    }
}
