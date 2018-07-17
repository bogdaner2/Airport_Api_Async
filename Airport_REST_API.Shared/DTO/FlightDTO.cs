using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport_REST_API.Shared.DTO
{
    public class FlightDTO
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{3}[0-9]{3}$", ErrorMessage = "AAA000")]
        public string Number { get; set; }

        [Required]
        [MinLength(3)]
        public string PointOfDeparture { get; set; }

        [Required]
        [RegularExpression("^(0[1-9]|1[0-9])-(0[1-9]|[12][0-9]|3[01])-(19|20)[0-9][0-9]$", ErrorMessage = "Format - 01-01-2018")]
        public string DepartureTime { get; set; }

        [MinLength(3)]
        public string Destination { get; set; }

        [Required]
        [RegularExpression("^(0[1-9]|1[0-9])-(0[1-9]|[12][0-9]|3[01])-(19|20)[0-9][0-9]$", ErrorMessage = "Format - 01-01-2018")]
        public string ArrivelTime { get; set; }

        [Required]
        public List<int> TicketsId { get; set; }
    }
}
