using System;
using System.Collections.Generic;
using Airport_REST_API.DataAccess.Models;


namespace Airport_REST_API.Shared.DTO
{
    public class LoadCrewDTO
    {
        public int id { get; set; }
        public List<Pilot> pilot { get; set; }
        public List<Stewardess> stewardess { get; set; }
    }
}
