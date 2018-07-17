using System;
using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.DataAccess.Models
{
    public class Aircraft
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AircraftType Type { get; set; }

        public DateTime ReleseDate { get; set; }
        public TimeSpan Lifetime { get; set; }
    }
}
