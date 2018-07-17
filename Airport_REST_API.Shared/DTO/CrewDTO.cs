using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.Shared.DTO
{
    public class CrewDTO
    {
        public int Id { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int? PilotId { get; set; }

        [Required]
        public List<int> StewardessesId { get; set; }
    }
}
