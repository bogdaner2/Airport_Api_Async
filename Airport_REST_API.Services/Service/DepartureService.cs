using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using AutoMapper;

namespace Airport_REST_API.Services.Service
{
    public class DepartureService : IDepartureService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public DepartureService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeparturesDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<DeparturesDTO>>(await db.Departures.GetAllAsync());
        }

        public async Task<DeparturesDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<DeparturesDTO>(await db.Departures.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false; 
            await db.Departures.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }

        public async Task<bool> CreateObjectAsync(DeparturesDTO obj)
        {
            var crew = await db.Crews.GetAsync(obj.CrewId.Value);
            var aircraft = await db.Aircrafts.GetAsync(obj.AircraftId.Value);
            if (crew == null || aircraft == null)
                return false; 
            var departure = _mapper.Map<Departures>(obj);
            departure.Aircraft = aircraft;
            departure.Crew = crew;
            await db.Departures.CreateAsync(departure);
            try
            {
                await db.SaveAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateObjectAsync(int id, DeparturesDTO obj)
        {
            var crew = await db.Crews.GetAsync(obj.CrewId.Value);
            var aircraft = await db.Aircrafts.GetAsync(obj.AircraftId.Value);
            if (crew == null || aircraft == null)
                return false; 
            var departure = _mapper.Map<Departures>(obj);
            departure.Aircraft = aircraft;
            departure.Crew = crew;
            var result = db.Departures.Update(id, departure);
            await db.SaveAsync();
            return result;
        }
    }
}
