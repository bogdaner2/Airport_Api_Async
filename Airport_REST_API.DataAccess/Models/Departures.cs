using System;
using System.ComponentModel.DataAnnotations;


namespace Airport_REST_API.DataAccess.Models
{
    public class Departures
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50,MinimumLength = 6)]
        public string Number { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public Crew Crew { get; set; }

        [Required]
        public Aircraft Aircraft { get; set; }
    }
}
