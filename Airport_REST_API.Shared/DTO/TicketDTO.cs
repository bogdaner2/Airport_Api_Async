using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.Shared.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(1.0, 4000.0, ErrorMessage = "Price must be between $1 and $4000")]
        public double Price { get; set; }

        [Required]
        [MinLength(4,ErrorMessage = "Number must be more than 4 letters")]
        public string Number { get; set; }
    }
}
