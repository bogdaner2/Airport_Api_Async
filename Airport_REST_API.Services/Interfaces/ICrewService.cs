﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.Shared.DTO;

namespace Airport_REST_API.Services.Interfaces
{
    public interface ICrewService : IService<CrewDTO>
    {
        Task<List<Crew>> LoadDataAsync();
    }
}
