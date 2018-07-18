using System;
using System.Collections.Generic;

namespace Airport_REST_API.Shared.DTO
{
    public class LoadCrewDTO
    {
        public string id { get; set; }
        public List<PilotDTO> pilot { get; set; }
        public List<StewardessDTO> stewardess { get; set; }
    }
}
