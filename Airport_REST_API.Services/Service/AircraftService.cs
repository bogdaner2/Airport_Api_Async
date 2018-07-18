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
    public class AircraftService : IAircraftService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public AircraftService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AircraftDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<AircraftDTO>>(await db.Aircrafts.GetAllAsync());
        }

        public async Task<AircraftDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<AircraftDTO>(await db.Aircrafts.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false;
            await db.Aircrafts.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }

        public async Task<bool> CreateObjectAsync(AircraftDTO obj)
        {
            var type = await db.Types.GetAsync(obj.TypeId);
            if (type == null)
                return false;
            var aircraft = _mapper.Map<Aircraft>(obj);
            aircraft.Type = type;
            await db.Aircrafts.CreateAsync(aircraft);
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

        public async Task<bool> UpdateObjectAsync(int id, AircraftDTO obj)
        {
            var type = await db.Types.GetAsync(obj.TypeId);
            if (type == null)
                return false;
            var aircraft = _mapper.Map<Aircraft>(obj);
            aircraft.Type = type;
            var result = db.Aircrafts.Update(id, aircraft);
            await db.SaveAsync();
            return result;
        }
    }
}
