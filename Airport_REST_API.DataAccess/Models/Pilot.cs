using System;
using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.DataAccess.Models
{
    public class Pilot
    {   
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Experience { get; set; }
    }
}
