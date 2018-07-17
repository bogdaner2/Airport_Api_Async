using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport_REST_API.Shared.DTO
{
    public class PilotDTO
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }

        [Required]
        [Range(1,50)]
        public int Experience { get; set; }
    }
}
