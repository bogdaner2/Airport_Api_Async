using System;
using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.Shared.DTO
{
    public class DeparturesDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 6)]
        public string Number { get; set; }

        [Required]
        [RegularExpression("^(0[1-9]|1[0-9])-(0[1-9]|[12][0-9]|3[01])-(19|20)[0-9][0-9]$", ErrorMessage = "Format - 01-01-2018")]
        public string DepartureTime { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int? CrewId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int? AircraftId { get; set; }
    }
}
