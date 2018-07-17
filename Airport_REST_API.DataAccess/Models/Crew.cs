using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.DataAccess.Models
{
    public class Crew
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Pilot Pilot { get; set; }

        [Required]
        public List<Stewardess> Stewardesses { get; set; }
    }
}
